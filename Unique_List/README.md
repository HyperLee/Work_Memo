# Unique_List

使用 LINQ `Distinct()` 取得整數清單中的不重複值。

本專案保留原有鏈結串列／集合演算法、類別、方法與輸出，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

`l1` 內含 `1, 1, 3, 9, 2`。`s1` 呼叫 LINQ `Distinct()`，保留第一次出現的順序並輸出不重複結果。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| Distinct 走訪 n 個項目 | O(n) 平均 | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\Unique_List\Unique_List.csproj"
dotnet build ".\Unique_List.sln" --configuration Debug --nologo
dotnet run --project ".\Unique_List\Unique_List.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定示範資料。錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

本案使用程式內固定示範資料，執行結果如下：

```text
Before: 1
Before: 1
Before: 3
Before: 9
Before: 2
Distinct value:
After: 1
After: 3
After: 9
After: 2
```

## 專案結構

```text
Unique_List/
├── Unique_List/
│   ├── Program.cs
│   └── Unique_List.csproj
├── Unique_List.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

