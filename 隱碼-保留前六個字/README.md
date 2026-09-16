# 隱碼-保留前六個字

保留地址前六個字，將其餘字元以星號取代。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 以地址字串為例，先計算超過前六個字的數量，建立對應數量的星號，再把前六個字與 mask 串接。長度不超過六個字時不進行隱碼。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 建立 mask 並串接字串 | O(n) | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\隱碼-保留前六個字\隱碼-保留前六個字.csproj"
dotnet build ".\隱碼-保留前六個字.sln" --configuration Debug --nologo
dotnet run --project ".\隱碼-保留前六個字\隱碼-保留前六個字.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
臺北市中正區*****
```

## 專案結構

```text
隱碼-保留前六個字/
├── 隱碼-保留前六個字/
│   ├── Program.cs
│   └── 隱碼-保留前六個字.csproj
├── 隱碼-保留前六個字.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

