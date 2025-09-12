# .NET Core（現代）與 WebForms（舊式）在 async/await 使用上的差異與詳細說明

這份說明整理了為什麼在 .NET Core 專案架構中會大量使用 `async/await`，而舊式的 WebForms 專案幾乎不使用的原因，並深入探討其內部機制、優缺點、常見陷阱以及實務上建議的做法。

.NET Core vs Web Forms：async/await 使用差異與建議

本文把重點整理、比對並合併使用者提供的內容，說明為何 ASP.NET Core 廣泛採用 async/await，而傳統 ASP.NET Web Forms 則較少使用，並提供實務上的建議。

1. 為何 ASP.NET Core 大量使用 async/await

- 非同步導向的框架設計：ASP.NET Core 的 HTTP 處理管線（Middleware、Controller、Razor Pages 等）從一開始就支援非同步 API，撰寫 async 方法是自然且常見的做法。Kestrel 伺服器本身也對非同步 I/O 有最佳化，因此在此平台下使用 async/await 幾乎是預設的最佳實務。

- 非同步 I/O 的效益：像是資料庫查詢、第三方 API 呼叫、檔案系統存取等 I/O-bound 工作，使用非同步可以在等待時釋放 ThreadPool 執行緒，避免阻塞，提升單台伺服器可處理的同時連線數（高併發）。

1. 為何 Web Forms 很少用 async/await

- 歷史設計與生命週期：Web Forms 的 Page Life Cycle（例如 Page_Load、PreRender）在早期 (.NET 2.0/3.5/4.0) 以同步為主。雖然 .NET 4.5 引入了 Page.RegisterAsyncTask，但使用體驗與語法不如直接在 Controller 或 Razor Pages 中使用 async/await 直觀，導致採用率低。

- 使用情境不同：許多 Web Forms 應用為內部管理系統或低併發場景，採同步資料存取與 PostBack 模式已足夠，沒有明顯的效能需求去驅動非同步改寫。

- 架構與 UI 耦合：Page Life Cycle 與大量 ViewState、控制項事件的耦合，使得把非同步流程穿透整個生命週期變得困難且易出錯，效能瓶頸常在渲染/UI 處理而非 I/O，async 的收益較小。

1. 使用 async/await 的主要優點（針對 ASP.NET Core）

- 高併發效能：非同步 I/O 能釋放 ThreadPool 執行緒，讓伺服器承載更多同時連線，尤其在等待資料庫或第三方 API 回應時效果明顯。

- 可讀性與維護性：相較於 callback 或舊式 APM（Begin/End），async/await 讓非同步程式的流程像同步程式一樣直覺，降低認知負擔。

- 更佳資源利用：減少 Thread starvation 的風險，降低整體記憶體與執行緒上下文切換成本。

1. 缺點與挑戰（在 .NET Core 或任何採用 async/await 的場景）

- 程式複雜度與傳遞性：非同步必須自上而下傳遞（Controller → Service → Repository），若某層忘記 async/await 或使用 .Result/.Wait()，容易造成死鎖或效能退化。

- 除錯與追蹤複雜度：Call stack 在非同步轉換時會被切斷，診斷較同步程式更困難。需搭配良好的 logging/追蹤工具（如 Application Insights、Serilog 等）與結構化追蹤（correlation id）。

- 不適合處理 CPU-bound 任務：async/await 最適合 I/O-bound 情境；大量 CPU 密集型工作應考慮使用 Task.Run、Parallel 或分散式計算。

1. 實務建議

- 端到端非同步：若專案採用非同步 I/O，盡量在呼叫鏈上保持 async 一致性，避免在同步上下文中呼叫 .Result 或 .Wait()。

- 明確分離 I/O 與 CPU 負載：將高 CPU 的運算封裝在專門的工作 (Task.Run / 背景工作 / 分散式服務) 中，避免在 ASP.NET 請求線程上執行大量計算。

- 加強監控與追蹤：使用 APM 與結構化 logging，因為非同步程式更難直接從 call stack 追蹤錯誤與效能問題。

- 在舊有 Web Forms 專案中的取捨：只有在確有 I/O-bound 高併發需求時，才考慮重構為非同步；否則同步模式在低併發、內網環境仍是合理選擇。

結論

ASP.NET Core：設計與運行環境皆偏向非同步與高併發，async/await 幾乎成為標準實務。ASP.NET Web Forms：因歷史設計、生命週期耦合與使用情境，async/await 的採用成本較高，常見情況下仍以同步為主。

比對與合併：本文從設計哲學、技術細節、優缺點與實務建議四個面向整理，讓讀者快速理解為何兩者在 async/await 採用上的差異，以及在實際專案中應如何取捨。

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
