# 亂數產生陣列做加總

建立 5 個隨機整數，逐項輸出後計算陣列總和。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

`GenRandomArray(5)` 產生長度為 5 的陣列，每個元素由 `Random.Next(1, 200)` 產生，因此範圍是 1 至 199。`printarray` 顯示元素，`cal` 逐項累加。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 產生、輸出與加總 n 個元素 | O(n) | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\亂數產生陣列做加總\亂數產生陣列做加總.csproj"
dotnet build ".\亂數產生陣列做加總.sln" --configuration Debug --nologo
dotnet run --project ".\亂數產生陣列做加總\亂數產生陣列做加總.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

不變條件：陣列長度固定為 5；每個數值介於 1（含）到 200（不含）；「亂數總和」等於該次五個元素的加總。

## 代表性案例與實際輸出

```text
95, 27, 99, 180, 19,

亂數總和: 420
```

## 專案結構

```text
亂數產生陣列做加總/
├── 亂數產生陣列做加總/
│   ├── Program.cs
│   └── 亂數產生陣列做加總.csproj
├── 亂數產生陣列做加總.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
