# 西元年轉民國年

將西元日期字串轉為民國年日期字串，保留原有分隔符與補零處理。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 使用 `2020/1/1` 呼叫 `ADToROC`。方法依 `-`、單引號或 `/` 分隔日期，年份減去 1911，月日只有一位數時在前面補 0；轉換例外會依原程式輸出例外訊息並回傳目前結果。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 日期字串處理 | O(m)，m 為字串長度 | O(m) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\西元年轉民國年\西元年轉民國年.csproj"
dotnet build ".\西元年轉民國年.sln" --configuration Debug --nologo
dotnet run --project ".\西元年轉民國年\西元年轉民國年.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
109-01-01
```

## 專案結構

```text
西元年轉民國年/
├── 西元年轉民國年/
│   ├── Program.cs
│   └── 西元年轉民國年.csproj
├── 西元年轉民國年.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

