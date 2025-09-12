# .NET Core（現代）與 WebForms（舊式）在 async/await 使用上的差異與詳細說明

這份說明整理了為什麼在 .NET Core 專案架構中會大量使用 `async/await`，而舊式的 WebForms 專案幾乎不使用的原因，並深入探討其內部機制、優缺點、常見陷阱以及實務上建議的做法。

### 概要

.NET Core（以及 ASP.NET Core）的設計與執行模型偏向高效能、可延展（scalable）的非同步 IO 驅動服務，因此在服務端大量採用 async/await 可以在面對高併發時更有效地利用系統資源（尤其是執行緒與記憶體）。

相反地，傳統的 WebForms 與早期 ASP.NET 平台在設計上較常以同步請求/回應、事件驅動（page lifecycle, postback）為主，生態圈中早期的資料庫驅動或第三方函式庫也多為同步 API，導致專案很少採用非同步程式設計。

### Async/await 基本觀念（簡短）

- `async`/`await` 是編譯層把非同步流程展開成狀態機的語法糖，最常回傳的型別是 `Task`、`Task<T>`、或 `ValueTask<T>`。
- 非同步程式設計最常解決的是 I/O-bound（例如網路、檔案、資料庫）等待時間，而不是 CPU-bound 的工作。

### ASP.NET（Full Framework / WebForms）與 ASP.NET Core 的關鍵差異

- SynchronizationContext：
  - 傳統 ASP.NET（包含 WebForms）使用專屬的 `SynchronizationContext`，它會把後續的執行恢復（resume）嘗試回到原始請求的上下文（例如與 HTTP context/執行緒綁定），因此在程式中如果同步等待 `Task.Result` 或 `Task.Wait()`，很容易造成死結（deadlock）。
  - ASP.NET Core 預設沒有 `SynchronizationContext`（或是更輕量、不會把後續續放到同一個同步上下文），因此 `await` 後的續作通常會在 ThreadPool 上執行，不容易因為同步等待造成死結。

- Host / Threading 模型：
  - ASP.NET Core 對高併發場景做了優化：非同步 I/O 可以讓執行緒被釋放去處理其他請求，提升吞吐量。傳統 WebForms 在面對大量 I/O 等待時，會因為執行緒被同步阻塞而快速耗盡執行緒池。

### 為何 .NET Core 專案大量使用 async/await？

- 可延展性（scalability）：非同步釋放等待的執行緒，能提升每台機器處理更多同時連線的能力。
- 資源利用：I/O-bound 等待期間不佔用執行緒，可減少 context switching 與執行緒管理成本。
- 內建與第三方支援：現代的資料庫驅動、HTTP client、檔案 I/O、雲端 SDK 多提供非同步 API，使得 end-to-end 非同步變成現實。
- 非同步資料流與新 API：`IAsyncEnumerable<T>`、非同步 streaming 等 API 在 .NET Core 生態更完整，支援更高效的資料處理模式。

### 優點（詳細）

1. 更高的吞吐量（throughput）：在大量短暫 I/O 等待的情況下，非同步可以讓相同數量的執行緒服務更多請求。
2. 更好的響應時間（latency）：伺服器在 I/O 等待期間不被阻塞，能更快地接受並處理其他請求。
3. 更低的記憶體與執行緒成本：不用為每個等待的請求佔用一個執行緒。
4. 更現代的 API 與整合：非同步 API（例如 EF Core 的 `ToListAsync`、HttpClient 的 `GetAsync`、Stream 的 `CopyToAsync`）直接使用 async/await 可以避免封裝阻塞。

### 缺點與風險（詳細）

1. 程式複雜度增加：需要理解非同步錯誤處理（例外傳播）、取消（CancellationToken）、以及資料競爭（race condition）。
2. CPU-bound 工作不應濫用 async：把 CPU-heavy 直接包在 `Task.Run` 裡當作「非同步」不是擴充性的解，且會消耗更多執行緒。對 CPU-bound 工作應做真正的平行處理或調整演算法。
3. 死結風險（主要出現在老平台/錯誤使用）：在有 SynchronizationContext（例如 WebForms）的情況下，如果在主執行緒同步等待 `Task.Result` 或 `.Wait()`，就可能發生死結。
4. 呼叫鏈不完整（async all the way）：若某層回傳同步 API 而上層 `async`，會打斷真正的非同步效益（例如 library 使用同步 IO）。
5. 小幅效能開銷：`async/await` 會為狀態機與 Task 產生少許分配，對超高效能、短時間執行的小方法需衡量是否值得（在熱路徑可考慮 ValueTask 等優化）。
6. 診斷與除錯複雜性：stack trace 與同步流程不同，需要使用支援非同步的除錯工具、觀察日誌與分散式追蹤。

### 常見實務陷阱與注意事項

- 切忌在 ASP.NET / WebAPI 的程式中以同步方式阻塞非同步任務：不要使用 `.Result`、`.Wait()`、`.GetAwaiter().GetResult()` 在請求處理流程上。
- 在 library 層（非 UI/框架層）使用 `ConfigureAwait(false)`，以避免不必要地捕捉 SynchronizationContext（對 ASP.NET Core 雖然影響較小，但對舊版平台非常重要）。
- 對 CPU-bound 工作，不要盲目以 `Task.Run` 包裝在伺服器端以求非同步，這會把工作推到 ThreadPool，反而可能造成執行緒震盪。
- 當使用第三方同步 API（例如某些舊式驅動）時，若要在非同步流程中使用，應尋找對應的非同步驅動或採用專門的封裝與排程策略（例如有限併發的 background queue）。
# .NET Core（現代）與 WebForms（舊式）在 async/await 使用上的差異與詳細說明

這份說明整理了為什麼在 .NET Core 專案架構中會大量使用 `async/await`，而舊式的 WebForms 專案幾乎不使用的原因，並深入探討其內部機制、優缺點、常見陷阱以及實務上建議的做法。

## 概要

.NET Core（以及 ASP.NET Core）的設計與執行模型偏向高效能、可延展（scalable）的非同步 IO 驅動服務，因此在服務端大量採用 async/await 可以在面對高併發時更有效地利用系統資源（尤其是執行緒與記憶體）。

相反地，傳統的 WebForms 與早期 ASP.NET 平台在設計上較常以同步請求/回應、事件驅動（page lifecycle, postback）為主，生態圈中早期的資料庫驅動或第三方函式庫也多為同步 API，導致專案很少採用非同步程式設計。

## Async/await 基本觀念（簡短）

- `async`/`await` 是編譯層把非同步流程展開成狀態機的語法糖，最常回傳的型別是 `Task`、`Task<T>`、或 `ValueTask<T>`。
- 非同步程式設計最常解決的是 I/O-bound（例如網路、檔案、資料庫）等待時間，而不是 CPU-bound 的工作。

## ASP.NET（Full Framework / WebForms）與 ASP.NET Core 的關鍵差異

- SynchronizationContext：
  - 傳統 ASP.NET（包含 WebForms）使用專屬的 `SynchronizationContext`，它會把後續的執行恢復（resume）嘗試回到原始請求的上下文（例如與 HTTP context/執行緒綁定），因此在程式中如果同步等待 `Task.Result` 或 `Task.Wait()`，很容易造成死結（deadlock）。
  - ASP.NET Core 預設沒有 `SynchronizationContext`（或是更輕量、不會把後續續放到同一個同步上下文），因此 `await` 後的續作通常會在 ThreadPool 上執行，不容易因為同步等待造成死結。

- Host / Threading 模型：
  - ASP.NET Core 對高併發場景做了優化：非同步 I/O 可以讓執行緒被釋放去處理其他請求，提升吞吐量。傳統 WebForms 在面對大量 I/O 等待時，會因為執行緒被同步阻塞而快速耗盡執行緒池。

## 為何 .NET Core 專案大量使用 async/await？

- 可延展性（scalability）：非同步釋放等待的執行緒，能提升每台機器處理更多同時連線的能力。
- 資源利用：I/O-bound 等待期間不佔用執行緒，可減少 context switching 與執行緒管理成本。
- 內建與第三方支援：現代的資料庫驅動、HTTP client、檔案 I/O、雲端 SDK 多提供非同步 API，使得 end-to-end 非同步變成現實。
- 非同步資料流與新 API：`IAsyncEnumerable<T>`、非同步 streaming 等 API 在 .NET Core 生態更完整，支援更高效的資料處理模式。

## 優點（詳細）

1. 更高的吞吐量（throughput）：在大量短暫 I/O 等待的情況下，非同步可以讓相同數量的執行緒服務更多請求。
2. 更好的響應時間（latency）：伺服器在 I/O 等待期間不被阻塞，能更快地接受並處理其他請求。
3. 更低的記憶體與執行緒成本：不用為每個等待的請求佔用一個執行緒。
4. 更現代的 API 與整合：非同步 API（例如 EF Core 的 `ToListAsync`、HttpClient 的 `GetAsync`、Stream 的 `CopyToAsync`）直接使用 async/await 可以避免封裝阻塞。

## 缺點與風險（詳細）

1. 程式複雜度增加：需要理解非同步錯誤處理（例外傳播）、取消（CancellationToken）、以及資料競爭（race condition）。
2. CPU-bound 工作不應濫用 async：把 CPU-heavy 直接包在 `Task.Run` 裡當作「非同步」不是擴充性的解，且會消耗更多執行緒。對 CPU-bound 工作應做真正的平行處理或調整演算法。
3. 死結風險（主要出現在老平台/錯誤使用）：在有 SynchronizationContext（例如 WebForms）的情況下，如果在主執行緒同步等待 `Task.Result` 或 `.Wait()`，就可能發生死結。
4. 呼叫鏈不完整（async all the way）：若某層回傳同步 API 而上層 `async`，會打斷真正的非同步效益（例如 library 使用同步 IO）。
5. 小幅效能開銷：`async/await` 會為狀態機與 Task 產生少許分配，對超高效能、短時間執行的小方法需衡量是否值得（在熱路徑可考慮 ValueTask 等優化）。
6. 診斷與除錯複雜性：stack trace 與同步流程不同，需要使用支援非同步的除錯工具、觀察日誌與分散式追蹤。

## 常見實務陷阱與注意事項

- 切忌在 ASP.NET / WebAPI 的程式中以同步方式阻塞非同步任務：不要使用 `.Result`、`.Wait()`、`.GetAwaiter().GetResult()` 在請求處理流程上。
- 在 library 層（非 UI/框架層）使用 `ConfigureAwait(false)`，以避免不必要地捕捉 SynchronizationContext（對 ASP.NET Core 雖然影響較小，但對舊版平台非常重要）。
- 對 CPU-bound 工作，不要盲目以 `Task.Run` 包裝在伺服器端以求非同步，這會把工作推到 ThreadPool，反而可能造成執行緒震盪。
- 當使用第三方同步 API（例如某些舊式驅動）時，若要在非同步流程中使用，應尋找對應的非同步驅動或採用專門的封裝與排程策略（例如有限併發的 background queue）。
- 設計時把取消（CancellationToken）納入考量，避免長時間掛起的請求耗盡資源。
- 注意 Task 與物件生命週期（例如不要在可回收的 scope 之外持有 DbContext 的異步操作）。

## 實務建議（Best Practices）

1. async all the way：盡量讓呼叫鏈從控制器/頁面到資料層都支援非同步。
2. 在 library 中使用 `ConfigureAwait(false)`（除非需要特定上下文）以提升相容性與效能。
3. 適當使用 `ValueTask`：只有在極低分配、頻繁呼叫的熱路徑才考慮，並注意其使用限制。
4. 儘量使用提供非同步 API 的資源（例如 EF Core、Dapper 的 async、HttpClient、FileStream 的 async），避免在伺服器端以同步 I/O 阻塞。
5. 避免在 ASP.NET 中用 `Task.Run` 作重負載的 CPU work；若要做背景處理，使用專門的 background worker 或外部工作隊列（例如 Hangfire、Azure Functions、訊息佇列）。
6. 在需要控制同時執行數量時，使用 `SemaphoreSlim` 等原語來避免資源耗盡。
7. 為非同步程式加入良好的日誌與分散式追蹤（CorrelationId、Activity），以便觀察非同步流程中的延遲與例外。

## 範例比較（簡單示意）

- ASP.NET Core（建議的非同步 Controller）

```csharp
// Controller
public async Task<IActionResult> Get()
{
    var data = await _repo.GetItemsAsync(cancellationToken);
    return Ok(data);
}
```

- WebForms（老式，同步處理常見範例）

```csharp
// Page Load
protected void Page_Load(object sender, EventArgs e)
{
    var items = _repo.GetItems(); // 同步 API
    // 綁定到 UI
}
```

如果在 WebForms 中強行把非同步放進事件流程，開發者要非常小心事件生命週期與 SynchronizationContext，否則可能造成不可預期的競爭或 deadlock。

## 典型的 Deadlock 範例（警示）

在有 SynchronizationContext 的環境（例如舊 ASP.NET）中：

```csharp
// 錯誤示例：在 UI/Request 執行緒上同步等待
var result = SomeAsyncMethod().Result; // 可能導致 deadlock
```

正確做法是全程 await：

```csharp
var result = await SomeAsyncMethod();
```

或在 library 層加上 ConfigureAwait(false)，但最好的方式還是避免同步等待。

## 何時不需要使用 async/await？

- 極短暫且 CPU-bound 的工作：async 可能帶來不必要的狀態機/分配成本。
- 第三方庫沒有提供非同步支援，且短時間同步呼叫的成本可接受時（但長期建議尋找或升級到非同步支援的驅動）。

## 小結（結論）

.NET Core 與 ASP.NET Core 推廣非同步的主要動因是延展性與資源效率：在高併發環境下，非同步 I/O 可以讓伺服器利用較少的執行緒處理更多請求，提升吞吐量與效率。WebForms/舊 ASP.NET 在設計、函式庫支援與 SynchronizationContext 的影響下，較少採用非同步程式設計。

採用 async/await 時要注意呼叫鏈完整性（async all the way）、避免同步阻塞、處理取消與例外、並針對熱路徑做必要的效能優化（例如 ValueTask、Pooling）。

---

我已將上述說明新增至本檔案 `Describe.md`，如需我把某段延伸為實際的碼例或針對你的專案做逐檔案檢查與修正（例如把同步 DB 呼叫改為非同步），我可以接續自動化檢查並提出具體修改建議。

## Requirements coverage

- 將詳細說明新增至 `Describe.md`：Done
