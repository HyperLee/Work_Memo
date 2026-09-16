# List去除重複項目

以兩個字串清單示範保留新增清單中未出現在刪除清單的項目。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

`Ashuzu` 是新增名單，`Bshuzu` 是刪除名單。程式逐項用 `Contains` 比對，只把未出現在刪除名單的值放入 `Cshuzu` 後輸出。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 每項呼叫 List.Contains | O(a × b) | O(a) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\List去除重複項目\List去除重複項目.csproj"
dotnet build ".\List去除重複項目.sln" --configuration Debug --nologo
dotnet run --project ".\List去除重複項目\List去除重複項目.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

程式使用固定示範資料，執行結果如下：

```text
3
5
```

## 專案結構

```text
List去除重複項目/
├── List去除重複項目/
│   ├── Program.cs
│   └── List去除重複項目.csproj
├── List去除重複項目.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

