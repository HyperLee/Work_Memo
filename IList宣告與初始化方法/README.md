# IList宣告與初始化方法

示範 `IList<IList<string>>` 的宣告、初始化，以及以雜湊集合尋找旅行終點城市。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 建立三段城市路徑，型別是 `IList<IList<string>>`。`DestCity` 先把每段起點放入 `HashSet<string>`，再找出不在起點集合中的終點。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 建立集合並查找終點 | O(n) 平均 | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\IList宣告與初始化方法\IList宣告與初始化方法.csproj"
dotnet build ".\IList宣告與初始化方法.sln" --configuration Debug --nologo
dotnet run --project ".\IList宣告與初始化方法\IList宣告與初始化方法.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

程式使用固定示範資料，執行結果如下：

```text
Sao Paulo
```

## 專案結構

```text
IList宣告與初始化方法/
├── IList宣告與初始化方法/
│   ├── Program.cs
│   └── IList宣告與初始化方法.csproj
├── IList宣告與初始化方法.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

