# 資料隱碼中間三位

依保留在左、右兩側的長度，將資料中間部分替換為星號。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 先依輸入長度決定左右保留數，再呼叫 `HideSensitiveInfo`。方法使用 `StringBuilder` 保留 prefix、suffix，並以星號填補中間長度；長度不足時仍保留原本的 fallback 分支。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 建立隱碼字串 | O(n) | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\資料隱碼中間三位\資料隱碼中間三位.csproj"
dotnet build ".\資料隱碼中間三位.sln" --configuration Debug --nologo
dotnet run --project ".\資料隱碼中間三位\資料隱碼中間三位.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

```text
0987***321
```

## 專案結構

```text
資料隱碼中間三位/
├── 資料隱碼中間三位/
│   ├── Program.cs
│   └── 資料隱碼中間三位.csproj
├── 資料隱碼中間三位.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

