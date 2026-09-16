# 陣列中重複的數字

這個 net10.0 主控台專案保留原始演算法與公開方法，並以固定 smoke test 驗證結果。

## 題目或原始需求說明

原始題目、需求與參考連結保留於 陣列中重複的數字/陣列中重複的數字/Program.cs 的 XML 註解。

## 輸入、輸出與限制條件

- 入口使用固定、可重現的資料，不依賴互動、時間、未固定亂數、網路或檔案狀態。
- 保留原有方法簽章與目標 TFM；題目限制以 XML 註解為準。
- 每個檢查輸出 Expected、Actual、PASS-FAIL，結尾輸出 Summary: X/Y checks passed.。
- 案例失敗時 Environment.ExitCode 為 1。

## 快速開始

dotnet restore 陣列中重複的數字/陣列中重複的數字/陣列中重複的數字.csproj
dotnet build 陣列中重複的數字/陣列中重複的數字/陣列中重複的數字.csproj --nologo
dotnet run --project 陣列中重複的數字/陣列中重複的數字/陣列中重複的數字.csproj --no-build --nologo

本機使用 .NET 10 runtime；本專案已設定為 net10.0，可直接執行上列命令。


## 解題概念與出發點

翻新保留原始演算法教學，將入口從一次性展示整理為固定 smoke harness；若有多種解法，會使用等價且獨立的案例驗證。

## 解法設計

主要方法的資料結構、狀態轉移、排序規則與邊界處理仍以 Program.cs 的 XML summary 和關鍵註解為準；入口只負責準備資料、呼叫方法與比對結果。

## 逐步範例演示

每個案例依序建立輸入、執行方法、整理回傳值、列印 Expected/Actual/PASS-FAIL。矩陣、集合、圖或非唯一順序的結果會先正規化再比較。

## 正確性、invariant 與關鍵判斷

每輪迭代維持原始題目的資料契約；harness 不以不可控的列舉或排程順序作為成功條件，並避免跨案例可變狀態污染。

## 時間與空間複雜度

複雜度依主要方法的輸入規模說明；固定 smoke harness 的常數案例數不取代演算法本身的分析。

## 固定測試矩陣

案例涵蓋題目範例、正常路徑、邊界與可接受的空資料/失敗條件；完整數量以 fresh transcript 為準。

## 完整執行輸出

```text
Case: 題目範例 / HashSet
Expected: 2
Actual: 2
PASS-FAIL: PASS

Case: 題目範例 / 原地交換
Expected: 2
Actual: 2
PASS-FAIL: PASS

Case: 沒有重複 / HashSet
Expected: null
Actual: null
PASS-FAIL: PASS

Case: 沒有重複 / 原地交換
Expected: null
Actual: null
PASS-FAIL: PASS

Case: 重複在頭尾 / HashSet
Expected: 1
Actual: 1
PASS-FAIL: PASS

Case: 重複在頭尾 / 原地交換
Expected: 1
Actual: 1
PASS-FAIL: PASS

Case: 重複在中間 / HashSet
Expected: 3
Actual: 3
PASS-FAIL: PASS

Case: 重複在中間 / 原地交換
Expected: 3
Actual: 3
PASS-FAIL: PASS

Case: 空陣列 / HashSet
Expected: null
Actual: null
PASS-FAIL: PASS

Case: 空陣列 / 原地交換
Expected: null
Actual: null
PASS-FAIL: PASS

Case: 含不合法數字 / HashSet
Expected: null
Actual: null
PASS-FAIL: PASS

Case: 含不合法數字 / 原地交換
Expected: null
Actual: null
PASS-FAIL: PASS

Summary: 12/12 checks passed.
```

## 專案結構

```text
.
├── 陣列中重複的數字/陣列中重複的數字/
│   ├── Program.cs
│   └── 陣列中重複的數字.csproj
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

題目與 API 參考資料保留於 Program.cs XML 註解。smoke test 是自包含 console 驗證，不是獨立測試框架；目前已升級為 net10.0；在已安裝 .NET 10 runtime 的主機上直接執行即可。
