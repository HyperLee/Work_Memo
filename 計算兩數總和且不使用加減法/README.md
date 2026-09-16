# 計算兩數總和且不使用加減法

這個 net10.0 主控台專案保留原始教學程式與公開方法，並以固定 smoke test 驗證可重現結果。

## 題目或原始需求說明

原始需求與題目說明保留在 `計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/Program.cs` 的 XML 註解中。本 README 補充執行契約與翻新後的驗收方式。

## 輸入、輸出與限制條件

- 輸入由入口中的固定案例提供，不依賴互動輸入、目前時間、亂數或網路。
- 每個案例建立獨立資料；多解法使用相同案例驗證。
- 輸出固定包含 `Expected`、`Actual`、`PASS-FAIL` 與最後的 `Summary`。
- 任何案例失敗會設定 `Environment.ExitCode = 1`。

## 快速開始

在本專案外層目錄執行：

```bash
dotnet restore 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj
dotnet build 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj --nologo
dotnet run --project 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj --no-build --nologo
```

本專案已升級為 net10.0；在已安裝 .NET 10 runtime 的主機上直接執行上列命令即可。

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
Case: 正數加法
Expected: 3
Actual: 3
PASS-FAIL: PASS

Case: 包含負數的加法
Expected: -2
Actual: -2
PASS-FAIL: PASS

Case: 零與整數相加
Expected: 42
Actual: 42
PASS-FAIL: PASS

Case: 正數減法
Expected: 2
Actual: 2
PASS-FAIL: PASS

Case: 負結果減法
Expected: -4
Actual: -4
PASS-FAIL: PASS

Case: 零差值
Expected: 0
Actual: 0
PASS-FAIL: PASS

Summary: 6/6 checks passed.
```

## 專案結構

```text
.
├── 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/
│   ├── Program.cs
│   └── 計算兩數總和且不使用加減法.csproj
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
