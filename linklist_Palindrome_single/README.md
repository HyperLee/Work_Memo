# linklist_Palindrome_single

以單向鏈結串列的中點、後半段反轉與比較流程判斷回文，並保留原有解說圖片資產。

本專案保留原有鏈結串列／集合演算法、類別、方法與輸出，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

`LinkedList` 先逐步把字元插入串列，`isPalindrome` 使用快慢指標找中點，反轉後半段、比較兩半，再把後半段反轉回去以還原鏈結。主控台會在每次插入後印出串列與判斷結果。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 找中點、反轉與比較 | O(n) | O(1) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\linklist_Palindrome_single\linklist_Palindrome_single.csproj"
dotnet build ".\linklist_Palindrome_single.sln" --configuration Debug --nologo
dotnet run --project ".\linklist_Palindrome_single\linklist_Palindrome_single.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定示範資料。錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

本案固定資料會產生多行輸出，驗證時以實際 stdout 核對。

```text
a->NULL
Is Palindrome

b->a->NULL
Not Palindrome

a->b->a->NULL
Is Palindrome

c->a->b->a->NULL
Not Palindrome

a->c->a->b->a->NULL
Not Palindrome

b->a->c->a->b->a->NULL
Not Palindrome

a->b->a->c->a->b->a->NULL
Is Palindrome
```

## 教學資產

- [FuntionToCheckIfaLinkedListIsPalindrons1.webp](linklist_Palindrome_single/FuntionToCheckIfaLinkedListIsPalindrons1.webp)
- [linklist_single.PNG](linklist_Palindrome_single/linklist_single.PNG)

## 專案結構

```text
linklist_Palindrome_single/
├── linklist_Palindrome_single/
│   ├── Program.cs
│   └── linklist_Palindrome_single.csproj
├── linklist_Palindrome_single.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
