# 常見數學除法題目用法

示範逐位取出整數數字，計算能整除原數的位數。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 使用 `1248` 呼叫 `CountDigits`。每輪以 `% 10` 取出目前個位數，再以 `/= 10` 移除個位數，並計算該位數是否能整除原數。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 逐位取出數字 | O(d)，d 為位數 | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\常見數學除法題目用法\常見數學除法題目用法.csproj"
dotnet build ".\常見數學除法題目用法.sln" --configuration Debug --nologo
dotnet run --project ".\常見數學除法題目用法\常見數學除法題目用法.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
4
```

## 專案結構

```text
常見數學除法題目用法/
├── 常見數學除法題目用法/
│   ├── Program.cs
│   └── 常見數學除法題目用法.csproj
├── 常見數學除法題目用法.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

