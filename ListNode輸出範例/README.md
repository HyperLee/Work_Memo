# ListNode輸出範例

建立單向鏈結串列，遞迴移除指定值後逐節點輸出。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 建立 `1 → 2 → 6 → 3 → 4 → 5 → 6`。`RemoveElements` 先遞迴處理後續節點，再依目前節點值決定保留自己或接到 `head.next`。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 走訪／遞迴 n 個節點 | O(n) | O(n) 呼叫堆疊 |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\ListNode輸出範例\ListNode輸出範例.csproj"
dotnet build ".\ListNode輸出範例.sln" --configuration Debug --nologo
dotnet run --project ".\ListNode輸出範例\ListNode輸出範例.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

程式使用固定示範資料，執行結果如下：

```text
Ans:1
Ans:2
Ans:3
Ans:4
Ans:5
```

## 專案結構

```text
ListNode輸出範例/
├── ListNode輸出範例/
│   ├── Program.cs
│   └── ListNode輸出範例.csproj
├── ListNode輸出範例.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

