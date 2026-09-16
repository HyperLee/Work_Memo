# 使用 async、await 與 Task.WhenAll 的穩定多工範例

這個 .NET 8 主控台專案示範同時啟動多個非同步工作、等待全部完成，再以固定順序彙總結果。預設路徑適合無人值守執行，不依賴鍵盤、長時間延遲或排程交錯順序。

## 題目與需求

在 C# 中，`async` 與 `await` 讓程式在等待非同步操作時不必阻塞目前流程；它們本身不保證建立新執行緒。此範例的要求是：

1. 同時建立一組 `Task<WorkerResult>`。
2. 使用 `Task.WhenAll` 等待所有工作。
3. 工作完成後才輸出依輸入順序排列的個別結果與總和。
4. 不根據哪個 task 先完成來判斷成功。

## 輸入、輸出與限制

- 每個 `WorkerInput` 含工作者編號與一組新的整數集合。
- 非同步工作回傳該集合的 `int` 總和。
- 案例輸出穩定文字，例如 `Task 1=6; Task 2=15; Total=21`。
- 加總使用 `int`，極端輸入受 `Int32` 溢位限制。
- 此範例以 `Task.Yield` 建立非同步讓步點，不模擬實際 I/O，也不宣稱平行 CPU 加速。

## 快速開始

從本 README 所在目錄執行：

```bash
dotnet restore "多工處理-使用 async 和 await練習/多工處理-使用 async 和 await練習.csproj"
dotnet build "多工處理-使用 async 和 await練習/多工處理-使用 async 和 await練習.csproj" --nologo
dotnet run --project "多工處理-使用 async 和 await練習/多工處理-使用 async 和 await練習.csproj" --no-build --nologo
```

若系統沒有 .NET 8 runtime、只有較新 major runtime，可在執行命令前加上 `DOTNET_ROLL_FORWARD=Major`。任何案例失敗時會設定 `Environment.ExitCode = 1`。

## 核心概念

- 呼叫 async 方法會取得 task；先建立所有 task，才能讓工作在等待期間彼此重疊。
- `await Task.WhenAll(tasks)` 直到每一個 task 完成才繼續。
- `WhenAll` 回傳的結果陣列對應原始 task 陣列順序，因此輸出不需要依賴 scheduler 的完成順序。
- worker 不直接寫入主控台，避免輸出因交錯而不穩定。

## 詳細設計

1. `RunCaseAsync` 將每個輸入映射成 `LongRunningTask`，先建立完整 task 陣列。
2. 一次 await `Task.WhenAll`。
3. `LongRunningTask` 在讓步後計算自己的總和，回傳不可變結果。
4. 呼叫端依 task 輸入順序格式化每位工作者結果。
5. 計算 aggregate total，再比較完整文字。
6. 每個案例輸出 `Expected`、`Actual`、`PASS-FAIL`；入口最後輸出 Summary。

## 逐步範例

第一個案例先建立：

- Task 1 處理 `[1, 2, 3]`，結果為 6。
- Task 2 處理 `[4, 5, 6]`，結果為 15。

兩者都完成後才組成 `Task 1=6; Task 2=15; Total=21`。即使 Task 2 實際較早完成，輸出仍按照建立 task 陣列的順序排列。

## 正確性與不變量

每個 worker 僅讀取自己的輸入並回傳自身加總，沒有共享可變狀態。`Task.WhenAll` 完成時，結果陣列中的每一項都已完成且位置對應原始 task；因此逐項結果穩定。aggregate total 是所有 worker total 的和，所以等於所有輸入值的總和。

## 複雜度

令所有工作者的輸入元素總數為 `N`、工作者數為 `W`：

- 總計算量：`O(N)`。
- task 與結果集合：`O(W)`；輸入本身不計入額外空間。
- 實際牆鐘時間取決於 scheduler 與工作內容；本範例不做效能保證。

## 測試矩陣

| 案例 | 工作者結果 | 總和 |
|---|---|---:|
| 兩個工作者彙總 | Task 1=6、Task 2=15 | 21 |
| 空工作負載 | Task 3=0 | 0 |
| 包含負數 | Task 4=4、Task 5=-3 | 1 |

## 完整執行輸出

```text
Case: 兩個工作者彙總
Expected: Task 1=6; Task 2=15; Total=21
Actual: Task 1=6; Task 2=15; Total=21
PASS-FAIL: PASS

Case: 空工作負載
Expected: Task 3=0; Total=0
Actual: Task 3=0; Total=0
PASS-FAIL: PASS

Case: 包含負數
Expected: Task 4=4; Task 5=-3; Total=1
Actual: Task 4=4; Task 5=-3; Total=1
PASS-FAIL: PASS

Summary: 3/3 checks passed.
```

## 專案結構

```text
.
├── 多工處理-使用 async 和 await練習/
│   ├── 多工處理-使用 async 和 await練習.csproj
│   └── Program.cs
├── .vscode/
├── docs/readme-template.md
├── AGENTS.md
└── README.md
```

## 參考資料

- [Microsoft Learn：使用 async 和 await 的非同步程式設計](https://learn.microsoft.com/zh-tw/dotnet/csharp/asynchronous-programming/)
- [Microsoft Learn：Task.WhenAll](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task.whenall)
- [Microsoft Learn：Task.Yield](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task.yield)

## 已知限制

- `Task.Yield` 只用來示範 async 控制流程，不代表真實網路或檔案 I/O。
- 本範例不輸出 worker 進度，也不驗證工作完成先後；這是刻意避免 scheduler-order assertions。
- smoke harness 不是獨立測試框架。
