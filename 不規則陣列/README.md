# 不規則陣列

示範 C# jagged array 的宣告、逐列取長度、逐項輸出與總和計算。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

資料型別是 `int[][]`，每一列可以有自己的長度。程式逐列走訪，再逐項累加，因此可以同時示範 jagged array 的索引與資料統計。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 走訪所有元素 | O(N)，N 為各列元素總數 | O(1)（不計輸入陣列） |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\不規則陣列\不規則陣列.csproj"
dotnet build ".\不規則陣列.sln" --configuration Debug --nologo
dotnet run --project ".\不規則陣列\不規則陣列.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
Element(0): 1 2
Element(1): 2 3
Element(2): 3 4
Element(3): 4 5
Element(4): 5 6
Element(5): 6 7
sum: 48
```

## 專案結構

```text
不規則陣列/
├── 不規則陣列/
│   ├── Program.cs
│   └── 不規則陣列.csproj
├── 不規則陣列.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
