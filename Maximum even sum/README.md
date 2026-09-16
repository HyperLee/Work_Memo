# Maximum even sum

將陣列分成奇數與偶數後，求長度 K 的最大偶數和子序列。

本專案保留原有鏈結串列／集合演算法、類別、方法與輸出，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

方法先把資料分成 `Even` 與 `Odd` 並排序，再依 K 的奇偶性比較「一個偶數」或「兩個奇數／兩個偶數」的候選和。固定案例是陣列 `4, 2, 6, 7, 8`、K = 3。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 分類與排序 | O(n log n) | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\Maximum even sum\Maximum even sum.csproj"
dotnet build ".\Maximum even sum.sln" --configuration Debug --nologo
dotnet run --project ".\Maximum even sum\Maximum even sum.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定示範資料。錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

本案使用程式內固定示範資料，執行結果如下：

```text
18
```

## 專案結構

```text
Maximum even sum/
├── Maximum even sum/
│   ├── Program.cs
│   └── Maximum even sum.csproj
├── Maximum even sum.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

