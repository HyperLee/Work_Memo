# 進階排序string_CompareOrdinal

這個 net8.0 主控台專案保留原始演算法與公開方法，並以固定 smoke test 驗證結果。

## 題目或原始需求說明

原始題目、需求與參考連結保留於 進階排序string_CompareOrdinal/進階排序string_CompareOrdinal/Program.cs 的 XML 註解。

## 輸入、輸出與限制條件

- 入口使用固定、可重現的資料，不依賴互動、時間、未固定亂數、網路或檔案狀態。
- 保留原有方法簽章與目標 TFM；題目限制以 XML 註解為準。
- 每個檢查輸出 Expected、Actual、PASS-FAIL，結尾輸出 Summary: X/Y checks passed.。
- 案例失敗時 Environment.ExitCode 為 1。

## 快速開始

dotnet restore 進階排序string_CompareOrdinal/進階排序string_CompareOrdinal/進階排序string_CompareOrdinal.csproj
dotnet build 進階排序string_CompareOrdinal/進階排序string_CompareOrdinal/進階排序string_CompareOrdinal.csproj --nologo
dotnet run --project 進階排序string_CompareOrdinal/進階排序string_CompareOrdinal/進階排序string_CompareOrdinal.csproj --no-build --nologo

本機只有 .NET 10 runtime；net8.0 可執行時使用：
DOTNET_ROLL_FORWARD=Major dotnet run --project 進階排序string_CompareOrdinal/進階排序string_CompareOrdinal/進階排序string_CompareOrdinal.csproj --no-build --nologo

fallback 是環境限制下的執行方式，不等同原生 TFM runtime 驗證。

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
Case: 共同信箱連接帳戶
Expected: John:john00@mail.com,john_newyork@mail.com,johnsmith@mail.com|John:johnnybravo@mail.com|Mary:mary@mail.com
Actual: John:john00@mail.com,john_newyork@mail.com,johnsmith@mail.com|John:johnnybravo@mail.com|Mary:mary@mail.com
PASS-FAIL: PASS

Case: 沒有共同信箱
Expected: Alex:a@mail.com|Bob:b@mail.com
Actual: Alex:a@mail.com|Bob:b@mail.com
PASS-FAIL: PASS

Summary: 2/2 checks passed.
```

## 專案結構

```text
.
├── 進階排序string_CompareOrdinal/進階排序string_CompareOrdinal/
│   ├── Program.cs
│   └── 進階排序string_CompareOrdinal.csproj
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

題目與 API 參考資料保留於 Program.cs XML 註解。smoke test 是自包含 console 驗證，不是獨立測試框架；目前 net8/net9 run 需依主機 runtime 狀態解讀 fallback。
