# C# OOP 多型

示範以基底類別 `Car` 參考衍生類別 `Honda`，讓覆寫後的加速行為在執行期生效。

本專案保留原有類別、方法、提示與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 宣告 `Car c = new Honda()`，透過基底型別操作 Honda 物件。`Honda.Accelerate` 覆寫速度上限為 230；`Car.Turbo` 沒有宣告為 `virtual`，因此此處以 `Car` 型別呼叫時維持原本的空方法，不會套用 `Honda.Turbo` 的加速。原有 `Console.ReadLine` 輸入也予以保留。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定互動流程 | O(1)（不計輸入字串長度） | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\C# OOP 多型\C# OOP 多型.csproj"
dotnet build ".\C# OOP 多型.sln" --configuration Debug --nologo
dotnet run --project ".\C# OOP 多型\C# OOP 多型.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定 stdin smoke test。錯誤輸入仍遵循原本的轉換與例外語意。

## 代表性案例與實際輸出

固定驗證輸入（每行一項）：

```text
20
```

實際輸出如下：

```text
建立一台汽車，廠牌：Honda，顏色：Blue，目前速度：100。

Honda進行加速，輸入要加速的速度？
Honda汽車加速至120

Honda汽車開啟 Turbo，目前速度：120
```

## 專案結構

```text
C# OOP 多型/
├── C# OOP 多型/
│   ├── Program.cs
│   └── C# OOP 多型.csproj
├── C# OOP 多型.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
