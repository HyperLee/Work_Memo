# 動態決定迴圈次數以及要跑資料

保留原始空的主控台入口，作為後續示範動態迴圈資料來源的專案骨架。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

目前 `Main` 保持空白，沒有輸入、輸出或其他副作用；這次翻新只更新專案架構與開發文件，不替原始骨架增添未存在的行為。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 空的 Main | O(1) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\動態決定迴圈次數以及要跑資料\動態決定迴圈次數以及要跑資料.csproj"
dotnet build ".\動態決定迴圈次數以及要跑資料.sln" --configuration Debug --nologo
dotnet run --project ".\動態決定迴圈次數以及要跑資料\動態決定迴圈次數以及要跑資料.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
（無輸出；原始 Main 為空。）
```

## 專案結構

```text
動態決定迴圈次數以及要跑資料/
├── 動態決定迴圈次數以及要跑資料/
│   ├── Program.cs
│   └── 動態決定迴圈次數以及要跑資料.csproj
├── 動態決定迴圈次數以及要跑資料.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

