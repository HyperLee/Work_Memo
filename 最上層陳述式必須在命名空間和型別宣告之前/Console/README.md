# C# 最上層陳述式與 Add Two Numbers

這個 .NET 10 主控台專案保留「最上層陳述式必須在型別宣告之前」的教學目的，並用 LeetCode 2 的反向鏈結串列加法作為可執行範例。

## 題目與需求

C# 最上層陳述式允許省略明確的 `Program` 類別與 `Main` 方法。編譯器會產生隱含入口，但檔案中的最上層陳述式必須出現在任何 namespace 或 type 宣告之前；其他 class 放在下方。

演算法範例接收兩條非空鏈結串列。每個節點保存一位數，位數由低位到高位排列；程式回傳同樣以反向位數表示的總和。

## 輸入、輸出與限制

- smoke harness 以非空 `int[]` 建立每條新鏈結串列。
- 每個節點值應為 `0..9`。
- `addTwoNumbers` 接收兩個非空 `ListNode`，並配置新的答案節點。
- 輸出以 `個位 -> 十位 -> 百位` 顯示。
- 預設入口不讀取鍵盤；任何案例失敗會回傳程序結束碼 1。

## 快速開始

從本 README 所在的 `Console` 目錄執行：

```bash
dotnet restore ConsoleApp1/ConsoleApp1.csproj
dotnet build ConsoleApp1/ConsoleApp1.csproj --nologo
dotnet run --project ConsoleApp1/ConsoleApp1.csproj --no-build --nologo
```

若沒有 .NET 10 runtime 而只有較新 major runtime，可在執行命令前加上 `DOTNET_ROLL_FORWARD=Major`。

## 核心概念

### 最上層陳述式

`Program.cs` 第一個可執行陳述式直接呼叫 `TopLevelEntry.Run()`：

```csharp
Environment.ExitCode = TopLevelEntry.Run();
```

`TopLevelEntry` 與 `ListNode` 都在它之後，因而維持 C# 的排列規則。專案仍有入口，只是 `Main` 由編譯器產生。

### 鏈結串列加法

每輪將左節點、右節點與前一輪進位相加：

- 新節點值為 `sum % 10`。
- 下一輪進位為 `sum / 10`。
- 兩條串列都走完後，若仍有進位就附加新節點。

## 詳細設計

1. 最上層陳述式呼叫具名入口流程並接收結束碼。
2. 每個案例用新的位數陣列建立兩條鏈結串列。
3. `addTwoNumbers` 使用 dummy head 簡化第一個答案節點的建立。
4. 左右指標各自前進；較短串列結束後，另一條仍可繼續。
5. 將答案格式化後與預期文字比較。
6. 最後輸出 `Summary: X/Y checks passed.`。

## 逐步範例

`342 + 465` 的輸入是 `2 -> 4 -> 3` 與 `5 -> 6 -> 4`：

| 位數 | 計算 | 寫入 | 進位 |
|---|---|---:|---:|
| 個位 | 2 + 5 + 0 | 7 | 0 |
| 十位 | 4 + 6 + 0 | 0 | 1 |
| 百位 | 3 + 4 + 1 | 8 | 0 |

結果為 `7 -> 0 -> 8`，代表 807。

## 正確性與不變量

每輪開始時，答案串列已正確保存所有處理過的低位數，`carry` 正好是要加入下一位的進位。該輪使用兩個目前位數與 carry 算出正確個位並更新進位；因此不變量延伸一位。所有節點處理完並補上末端進位後，整條答案串列正確表示兩數之和。

## 複雜度

令兩條串列長度為 `m`、`n`：

- 時間複雜度：`O(max(m, n))`。
- 答案所需空間：`O(max(m, n))`；除輸出外的額外工作空間為 `O(1)`。

## 測試矩陣

| 案例 | 輸入 | 預期反向位數 |
|---|---|---|
| 題目範例 | 342 + 465 | `7 -> 0 -> 8` |
| 兩個零 | 0 + 0 | `0` |
| 連續進位與不同長度 | 9,999,999 + 9,999 | `8 -> 9 -> 9 -> 9 -> 0 -> 0 -> 0 -> 1` |
| 尾端產生進位 | 5 + 5 | `0 -> 1` |

## 完整執行輸出

```text
Case: 題目範例
Expected: 7 -> 0 -> 8
Actual: 7 -> 0 -> 8
PASS-FAIL: PASS

Case: 兩個零
Expected: 0
Actual: 0
PASS-FAIL: PASS

Case: 連續進位與不同長度
Expected: 8 -> 9 -> 9 -> 9 -> 0 -> 0 -> 0 -> 1
Actual: 8 -> 9 -> 9 -> 9 -> 0 -> 0 -> 0 -> 1
PASS-FAIL: PASS

Case: 尾端產生進位
Expected: 0 -> 1
Actual: 0 -> 1
PASS-FAIL: PASS

Summary: 4/4 checks passed.
```

## 專案結構

```text
Console/
├── ConsoleApp1/
│   ├── ConsoleApp1.csproj
│   └── Program.cs
├── .vscode/
├── docs/readme-template.md
├── AGENTS.md
└── README.md
```

## 參考資料

- [Microsoft Learn：最上層陳述式](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/program-structure/top-level-statements)
- [LeetCode 2. Add Two Numbers](https://leetcode.com/problems/add-two-numbers/)

## 已知限制

- `BuildList` 是 smoke harness helper，假設輸入陣列非空且每個元素為十進位位數。
- 本範例著重 top-level statements 與加法流程，未提供可重用的公開鏈結串列 API。
