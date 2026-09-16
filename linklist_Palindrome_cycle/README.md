# linklist_Palindrome_cycle

以 Floyd cycle detection 找到環起點，再判斷帶環鏈結串列是否為回文，並保留原有解說圖片資產。

本專案保留原有鏈結串列／集合演算法、類別、方法與輸出，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

程式先以 Floyd 快慢指標偵測環，再由環中節點計算環長與環起點；`isPalindromeUtil` 以 `Stack<int>` 沿循環範圍收集並比對節點值。原始案例把最後節點連回值為 15 的節點。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 偵測環與堆疊比對 | O(n) | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\linklist_Palindrome_cycle\linklist_Palindrome_cycle.csproj"
dotnet build ".\linklist_Palindrome_cycle.sln" --configuration Debug --nologo
dotnet run --project ".\linklist_Palindrome_cycle\linklist_Palindrome_cycle.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定示範資料。錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

本案固定資料會產生多行輸出，驗證時以實際 stdout 核對。

```text
Not Palindrome
```

## 教學資產

- [cycle.PNG](linklist_Palindrome_cycle/cycle.PNG)
- [cycle2.PNG](linklist_Palindrome_cycle/cycle2.PNG)
- [Screenshot 2022-05-19 at 13-48-09 检查带有循环的链表是否为回文 码农参考.png](linklist_Palindrome_cycle/Screenshot 2022-05-19 at 13-48-09 检查带有循环的链表是否为回文 码农参考.png)

## 專案結構

```text
linklist_Palindrome_cycle/
├── linklist_Palindrome_cycle/
│   ├── Program.cs
│   └── linklist_Palindrome_cycle.csproj
├── linklist_Palindrome_cycle.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```
