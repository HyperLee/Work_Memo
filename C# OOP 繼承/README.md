# C# OOP 繼承

示範 `Honda`、`Nissan` 繼承 `Car`，並透過互動輸入測試加速與煞車。

本專案保留原有類別、方法、提示與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 建立 Honda（初速 60）與 Nissan（初速 80），依序讀取兩次加速與兩次煞車輸入。兩個子類別沿用 `Car` 的實作，並共同受 0–200 速度邊界保護。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定四次輸入與方法呼叫 | O(1) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\C# OOP 繼承\C# OOP 繼承.csproj"
dotnet build ".\C# OOP 繼承.sln" --configuration Debug --nologo
dotnet run --project ".\C# OOP 繼承\C# OOP 繼承.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定 stdin smoke test。錯誤輸入仍遵循原本的轉換與例外語意。

## 代表性案例與實際輸出

固定驗證輸入（每行一項）：

```text
20
30
10
50
```

實際輸出如下：

```text
建立一台汽車，廠牌：Honda，顏色：Red，目前速度：60。

建立一台汽車，廠牌：Nissan，顏色：Blue，目前速度：80。

Honda進行加速，輸入要加速的速度？
Honda汽車加速至80

Nissan進行加速，輸入要加速的速度？
Nissan汽車加速至110

Honda進行減速，輸入要減速的速度？
Honda汽車減速至70

Nissan進行減速，輸入要減速的速度？
Nissan汽車減速至60
```

## 專案結構

```text
C# OOP 繼承/
├── C# OOP 繼承/
│   ├── Program.cs
│   └── C# OOP 繼承.csproj
├── C# OOP 繼承.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

