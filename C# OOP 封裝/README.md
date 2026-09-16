# C# OOP 封裝

示範以 `Car` 的方法集中處理加速與煞車的速度上下限。

本專案保留原有類別、方法、提示與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 先建立速度 60 的 `Car`，再讀取一次加速值與一次煞車值。`Accelerate` 將速度上限限制在 200，`Brake` 將速度下限限制在 0。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定兩次輸入與方法呼叫 | O(1) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\C# OOP 封裝\C# OOP 封裝.csproj"
dotnet build ".\C# OOP 封裝.sln" --configuration Debug --nologo
dotnet run --project ".\C# OOP 封裝\C# OOP 封裝.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定 stdin smoke test。錯誤輸入仍遵循原本的轉換與例外語意。

## 代表性案例與實際輸出

固定驗證輸入（每行一項）：

```text
50
30
```

實際輸出如下：

```text
建立一台汽車，廠牌：Honda，顏色：Blue，目前速度：60。

進行加速，輸入要加速的速度？
汽車加速至110

進行減速，輸入要減速的速度？
汽車減速至80
```

## 專案結構

```text
C# OOP 封裝/
├── C# OOP 封裝/
│   ├── Program.cs
│   └── C# OOP 封裝.csproj
├── C# OOP 封裝.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

