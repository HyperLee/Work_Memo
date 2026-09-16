# C# OOP 抽象

保留原始 OOP 汽車資料與方法示範；目前 `Car` 是可直接建立的類別，README 明確記錄現況。

本專案保留原有類別、方法、提示與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 建立 `Car`，設定 Brand、Color、Speed 後輸出。雖然專案名稱以「抽象」為教學主題，目前程式並未宣告 `abstract` 類別或方法，因此文件不改寫成不存在的抽象行為。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定欄位設定與輸出 | O(1) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\C# OOP 抽象\C# OOP 抽象.csproj"
dotnet build ".\C# OOP 抽象.sln" --configuration Debug --nologo
dotnet run --project ".\C# OOP 抽象\C# OOP 抽象.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定 stdin smoke test。錯誤輸入仍遵循原本的轉換與例外語意。

## 代表性案例與實際輸出

本案不需要輸入。

實際輸出如下：

```text
建立一台汽車，廠牌：Honda，顏色：Blue，目前速度：60。
```

## 專案結構

```text
C# OOP 抽象/
├── C# OOP 抽象/
│   ├── Program.cs
│   └── C# OOP 抽象.csproj
├── C# OOP 抽象.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

