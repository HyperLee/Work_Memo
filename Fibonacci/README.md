# Fibonacci

以迴圈計算 Fibonacci 數列，示範前兩項狀態如何逐步往前更新。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 以 n = 100 呼叫 `Fibo`。方法從 Fibonacci 的第 0、1 項開始，以 `first`、`second`、`result` 三個變數逐項更新；結果型別保留原始的 `long`。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 迴圈計算 n 項 | O(n) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\Fibonacci\Fibonacci.csproj"
dotnet build ".\Fibonacci.sln" --configuration Debug --nologo
dotnet run --project ".\Fibonacci\Fibonacci.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
3736710778780434371
```

## 專案結構

```text
Fibonacci/
├── Fibonacci/
│   ├── Program.cs
│   └── Fibonacci.csproj
├── Fibonacci.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
