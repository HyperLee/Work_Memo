# 優化遞迴方式

以爬樓梯問題比較遞迴解與迭代解，說明如何降低重複計算。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 以 n = 3 同時呼叫 `ClimbStairs2` 與 `ClimbStairs3`。前者直接遞迴展開，後者以 `pre`、`next` 狀態迭代，兩者保留相同結果。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| ClimbStairs2 遞迴 / ClimbStairs3 迭代 | O(2^n) / O(n) | O(n) / O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\優化遞迴方式\優化遞迴方式.csproj"
dotnet build ".\優化遞迴方式.sln" --configuration Debug --nologo
dotnet run --project ".\優化遞迴方式\優化遞迴方式.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
total step:3
total step:3
```

## 專案結構

```text
優化遞迴方式/
├── 優化遞迴方式/
│   ├── Program.cs
│   └── 優化遞迴方式.csproj
├── 優化遞迴方式.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

