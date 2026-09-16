# 二分法找mid寫法

比較兩種二分法中點計算式，示範一般寫法與避免 left + right 溢位的寫法。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 固定設定 `left = 0`、`right = 11`，分別呼叫 `Method1` 與 `Method2`；兩式在此案例都得到相同中點，但第二式可避免 `left + right` 的整數加總溢位。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定兩次中點計算 | O(1) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\二分法找mid寫法\二分法找mid寫法.csproj"
dotnet build ".\二分法找mid寫法.sln" --configuration Debug --nologo
dotnet run --project ".\二分法找mid寫法\二分法找mid寫法.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
Method1: 5
Method2: 5
```

## 專案結構

```text
二分法找mid寫法/
├── 二分法找mid寫法/
│   ├── Program.cs
│   └── 二分法找mid寫法.csproj
├── 二分法找mid寫法.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

