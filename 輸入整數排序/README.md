# 輸入整數排序

讀取 15 個整數，使用 Array.Sort 排序後依序輸出。

本專案保留原有提示與輸入順序，並以 SDK-style .NET 10（`net10.0`）建置。

## 概念說明

程式先建立長度為 15 的整數陣列，再以 `int.Parse(Console.ReadLine())` 依序讀入 15 行資料。輸入完成後交由 .NET 的 `Array.Sort` 排序，最後使用 `foreach` 依序輸出並以 `, ` 分隔。

## 複雜度

令輸入數量為 n（本程式固定 n = 15）：

| 項目 | 複雜度 |
| --- | --- |
| 排序時間 | O(n log n) |
| 陣列額外空間 | O(1)，排序在原陣列上進行 |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\輸入整數排序\輸入整數排序.csproj"
dotnet build ".\輸入整數排序.sln" --configuration Debug --nologo
dotnet run --project ".\輸入整數排序\輸入整數排序.csproj" --configuration Debug --no-build --nologo
```

程式會顯示「請輸入15個數字」，請逐行輸入 15 個可由 `int.Parse` 解析的整數。輸入非整數時，仍保留原程式的例外行為。

## 代表性案例與實際輸出

固定輸入：

```text
15
14
13
12
11
10
9
8
7
6
5
4
3
2
1
```

實際輸出：

```text
請輸入15個數字
1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
```

## 專案結構

```text
輸入整數排序/
├── 輸入整數排序/
│   ├── Program.cs
│   └── 輸入整數排序.csproj
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
