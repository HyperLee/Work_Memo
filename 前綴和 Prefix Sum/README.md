# 前綴和 Prefix Sum

示範以前綴累加方式快速取得陣列區間累計值。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

程式先以走訪累加建立前綴結果，再用累加值取得對應的累計結果；請從 `Program.cs` 的 Main 與 helper method 對照每個陣列位置。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 建立前綴累加結果 | O(n) | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\前綴和 Prefix Sum\前綴和 Prefix Sum.csproj"
dotnet build ".\前綴和 Prefix Sum.sln" --configuration Debug --nologo
dotnet run --project ".\前綴和 Prefix Sum\前綴和 Prefix Sum.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
12.75
```

## 專案結構

```text
前綴和 Prefix Sum/
├── 前綴和 Prefix Sum/
│   ├── Program.cs
│   └── 前綴和 Prefix Sum.csproj
├── 前綴和 Prefix Sum.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
