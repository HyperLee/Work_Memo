# linklist_Palindrome

以雙向鏈結串列判斷字元序列是否為回文，並保留原有解說圖片資產。

本專案保留原有鏈結串列／集合演算法、類別、方法與輸出，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

每個 `Node` 同時保存 `next` 與 `prev`。`push` 從串列前端插入節點；`isPalindrome` 找到最右節點後，從左右兩端向中間比對。原始固定資料建出的順序是 `4 → 3 → 1`。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 找尾端並左右比對 | O(n) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\linklist_Palindrome\linklist_Palindrome.csproj"
dotnet build ".\linklist_Palindrome.sln" --configuration Debug --nologo
dotnet run --project ".\linklist_Palindrome\linklist_Palindrome.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定示範資料。錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

本案使用程式內固定示範資料，執行結果如下：

```text
Not Palindrome
```

## 教學資產

- [解說.PNG](linklist_Palindrome/解說.PNG)

## 專案結構

```text
linklist_Palindrome/
├── linklist_Palindrome/
│   ├── Program.cs
│   └── linklist_Palindrome.csproj
├── linklist_Palindrome.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

