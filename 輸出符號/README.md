# 輸出符號

示範 C# 字串常值中的雙引號與單引號輸出方式。

本專案保留原有主控台輸出文字，並以 SDK-style .NET 10（`net10.0`）建置。

## 概念說明

C# 字串中的雙引號需要以反斜線跳脫；單引號在一般字串中可直接輸出。程式依序呼叫四次 `Console.WriteLine`，因此輸出順序與原始範例一致。

## 複雜度

| 項目 | 複雜度 |
| --- | --- |
| 時間 | O(1)，固定四行輸出 |
| 額外空間 | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\輸出符號\輸出符號.csproj"
dotnet build ".\輸出符號.sln" --configuration Debug --nologo
dotnet run --project ".\輸出符號\輸出符號.csproj" --configuration Debug --no-build --nologo
```

## 代表性案例與實際輸出

此範例不需要輸入。執行結果為：

```text
""
'
"
''
```

## 專案結構

```text
輸出符號/
├── 輸出符號/
│   ├── Program.cs
│   └── 輸出符號.csproj
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

