# list排序

這個 net8.0 主控台專案保留原始教學程式與公開方法，並以固定 smoke test 驗證可重現結果。

## 題目或原始需求說明

原始需求與題目說明保留在 `list排序/list排序/Program.cs` 的 XML 註解中。本 README 補充執行契約與翻新後的驗收方式。

## 輸入、輸出與限制條件

- 輸入由入口中的固定案例提供，不依賴互動輸入、目前時間、亂數或網路。
- 每個案例建立獨立資料；多解法使用相同案例驗證。
- 輸出固定包含 `Expected`、`Actual`、`PASS-FAIL` 與最後的 `Summary`。
- 任何案例失敗會設定 `Environment.ExitCode = 1`。

## 快速開始

在本專案外層目錄執行：

```bash
dotnet restore list排序/list排序/list排序.csproj
dotnet build list排序/list排序/list排序.csproj --nologo
dotnet run --project list排序/list排序/list排序.csproj --no-build --nologo
```

目前主機若只有 .NET 10 runtime，執行 net8/net9 成品時可使用：

```bash
DOTNET_ROLL_FORWARD=Major dotnet run --project list排序/list排序/list排序.csproj --no-build --nologo
```

這是環境 fallback，不代表原生目標 runtime 驗證。

## 解題概念與出發點

翻新以原始範例的核心概念為中心：保留既有方法簽章與題目 XML，將展示流程改成可重複的固定案例，並把不可控的互動輸入改為預設範例。

## 解法設計

每個公開或主要方法的設計、資料狀態與邊界條件，應以 `Program.cs` XML summary 及關鍵註解為準；入口依序呼叫方法並逐項比對預期值。

## 逐步範例演示

每個案例先準備輸入，再呼叫主要方法，接著以同一格式列印預期值與實際值。若演算法有中間狀態，README 會說明其 invariant 與狀態如何前進。

## 正確性、invariant 與關鍵判斷

驗證重點是：每一步維持原始題目的資料契約，結果集合不依賴不可控順序；案例之間不共用可變狀態。

## 時間與空間複雜度

複雜度依本專案主要方法的輸入規模說明；固定 smoke harness 的額外成本不取代演算法本身的複雜度。

## 固定測試矩陣

測試案例與實際數量由入口中的固定資料決定，以下完整 transcript 會與同一輪執行結果同步。

## 完整執行輸出

```text
Case: 頻率不同與相同
Expected: [1, 0, 5, 5]
Actual: [1, 0, 5, 5]
PASS-FAIL: PASS

Case: 三種頻率
Expected: [1, 2, 2, 3, 3, 3]
Actual: [1, 2, 2, 3, 3, 3]
PASS-FAIL: PASS

Case: 同頻率採數值遞減
Expected: [3, 2, 2, 1, 1]
Actual: [3, 2, 2, 1, 1]
PASS-FAIL: PASS

Summary: 3/3 checks passed.
```

## 專案結構

```text
.
├── list排序/list排序/
│   ├── Program.cs
│   └── list排序.csproj
├── .editorconfig
├── .gitattributes
├── .gitignore
├── AGENTS.md
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
└── README.md
```

## 參考資料與已知限制

- 題目連結與原始參考資料保留於 `Program.cs` XML 註解。
- smoke test 是自包含的 console 驗證，不是獨立測試框架。
- 若使用 `DOTNET_ROLL_FORWARD=Major`，README 的輸出是 fallback 執行證據。
