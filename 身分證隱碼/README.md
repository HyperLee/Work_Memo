# 身分證隱碼

示範固定格式身分證字號的中段隱碼處理，保留前四碼與後三碼。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 使用固定字串 `A123456789`。長度至少 10 碼時，以 `Substring` 組合前四碼、三個星號與後三碼；較短字串則維持原字串。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定長度字串切割 | O(1) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\身分證隱碼\身分證隱碼.csproj"
dotnet build ".\身分證隱碼.sln" --configuration Debug --nologo
dotnet run --project ".\身分證隱碼\身分證隱碼.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
A123***789
```

## 專案結構

```text
身分證隱碼/
├── 身分證隱碼/
│   ├── Program.cs
│   └── 身分證隱碼.csproj
├── 身分證隱碼.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

