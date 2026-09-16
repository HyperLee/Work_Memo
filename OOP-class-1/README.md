# OOP-class-1

以 Cat 與 Duck 類別示範欄位、方法、屬性 setter 與輸入資料的基本 OOP 使用方式。

本專案保留原有類別、方法、提示與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

程式先讀取兩隻貓的姓名與年齡，呼叫 `Meow`、`CaseMice` 示範方法與物件狀態；接著以 `Duck.duckAge` 的 setter 將負年齡限制為 0。移除的只有舊 Visual Studio 暫停鍵讀取，提示與輸入順序不變。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 固定物件互動流程 | O(1)（不計輸入字串長度） | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\OOP-class-1\OOP-class-1.csproj"
dotnet build ".\OOP-class-1.sln" --configuration Debug --nologo
dotnet run --project ".\OOP-class-1\OOP-class-1.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定 stdin smoke test。錯誤輸入仍遵循原本的轉換與例外語意。

## 代表性案例與實際輸出

固定驗證輸入（每行一項）：

```text
Mimi
3
Dodo
5
```

執行時會依輸入姓名產生問候文字；速度與年齡結果可由固定輸入直接推導。

```text
嗨!我是Mimi
喵~~~~~喵~~~
嗨!我是Mimi
我已經抓了 1 隻老鼠
嗨!我是Mimi
我已經抓了 2 隻老鼠
嗨!我是Dodo
喵~~~~~喵~~~
嗨!我是Dodo
我已經抓了 1 隻老鼠
--END1--
duck.duckAge1: 3
duck.duckAge2: 0
--END2--
```

## 專案結構

```text
OOP-class-1/
├── OOP-class-1/
│   ├── Program.cs
│   └── OOP-class-1.csproj
├── OOP-class-1.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
