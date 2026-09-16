# bebblesort

以原地氣泡排序整理固定整數陣列，並在排序前後輸出資料。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 建立固定陣列並先輸出原始資料，再呼叫 `bubblesort`。方法以相鄰元素比較與交換，逐輪把較大值推向右側，最後輸出排序結果。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 氣泡排序 | O(n²) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\bebblesort\bebblesort.csproj"
dotnet build ".\bebblesort.sln" --configuration Debug --nologo
dotnet run --project ".\bebblesort\bebblesort.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

程式使用固定示範資料，執行結果如下：

```text
原始資料
73, 57, 49, 3, 99, 133, 20, 1,
排序後
1, 3, 20, 49, 57, 73, 99, 133,
```

## 專案結構

```text
bebblesort/
├── bebblesort/
│   ├── Program.cs
│   └── bebblesort.csproj
├── bebblesort.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
