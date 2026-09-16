# post-increment

示範後置遞增與前置遞增在運算結果與變數更新時機上的差異。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 先示範 `i++`：該行使用遞增前的值，下一行才看到更新；再示範 `++a`：同一行就使用遞增後的值。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定示範輸出 | O(1) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\post-increment\post-increment.csproj"
dotnet build ".\post-increment.sln" --configuration Debug --nologo
dotnet run --project ".\post-increment\post-increment.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
i:3
i++:3
i:4

a:1.5
++a:2.5
a:2.5
```

## 專案結構

```text
post-increment/
├── post-increment/
│   ├── Program.cs
│   └── post-increment.csproj
├── post-increment.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
