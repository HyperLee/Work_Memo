# LeetCode 3355：零陣列轉換 I（差分陣列）

本專案使用 C# / .NET 9 示範如何以差分陣列統計區間查詢的覆蓋次數，並保留三種公開解法互相比對。固定 smoke harness 會執行 5 組案例、共 15 項方法檢查。

- [LeetCode 3355：Zero Array Transformation I](https://leetcode.com/problems/zero-array-transformation-i/)
- [LeetCode 中文題目：零陣列變換 I](https://leetcode.cn/problems/zero-array-transformation-i/)

## 閱讀導覽

1. [題目契約與限制](#1-題目契約與限制)
2. [快速開始](#2-快速開始)
3. [為什麼使用差分陣列](#3-為什麼使用差分陣列)
4. [三種方法](#4-三種方法)
5. [逐步範例](#5-逐步範例)
6. [正確性與不變量](#6-正確性與不變量)
7. [複雜度比較](#7-複雜度比較)
8. [測試矩陣與實際輸出](#8-測試矩陣與實際輸出)
9. [專案結構、限制與參考資料](#9-專案結構限制與參考資料)

## 1. 題目契約與限制

輸入包含：

- `nums[i]`：索引 `i` 還需要被減少多少次才能變成 0。
- `queries[j] = [l, r]`：處理此查詢時，可從閉區間 `[l, r]` 中選擇任意索引子集合，讓被選中的值各減 1。

同一個查詢對同一索引最多提供一次減 1 的機會，但不必把區間內所有值都減 1。因此，若索引 `i` 被 `coverage[i]` 個查詢涵蓋，只要：

```text
coverage[i] >= nums[i]
```

便可以從這些查詢中挑出足夠次數，把 `nums[i]` 降到 0，並跳過多餘操作。

三個既有公開方法簽章保持不變：

```csharp
public static bool IsZeroArray(int[] nums, int[][] queries)
public static bool IsZeroArray2(int[] nums, int[][] queries)
public bool IsZeroArray3(int[] nums, int[][] queries)
```

依官方題目，`nums` 非空，`queries[j]` 是合法的兩元素閉區間，且所有索引都在 `nums` 範圍內。本專案不替不合法輸入拋出自訂例外；固定 harness 額外加入空查詢陣列，驗證「沒有任何可用操作」的自然邊界行為。

## 2. 快速開始

請在本 README 所在的 `差分陣列` 外層目錄執行：

```bash
dotnet restore 差分陣列/差分陣列.csproj
dotnet build 差分陣列/差分陣列.csproj --nologo
DOTNET_ROLL_FORWARD=Major dotnet run --project 差分陣列/差分陣列.csproj --no-build --nologo
```

`DOTNET_ROLL_FORWARD=Major` 只在本機沒有 .NET 9 runtime、但有相容的較新 runtime 時需要。若已安裝 .NET 9 runtime，可直接執行 `dotnet run`。

成功時最後一行是：

```text
Summary: 15/15 checks passed.
```

任一檢查失敗時會在 `PASS-FAIL` 欄位印出 `FAIL`，並將 `Environment.ExitCode` 設為 `1`。本專案沒有獨立測試專案；`Main` 是無互動、固定資料、可重現的 smoke test。

## 3. 為什麼使用差分陣列

若對每個查詢都逐格更新 `[l, r]`，最壞情況可能需要 `O(n × q)` 次操作。差分陣列只標記區間的開始與結束：

```text
對 [l, r] 增加 1 次覆蓋：
diff[l]     += 1
diff[r + 1] -= 1
```

之後由左到右計算前綴和：

```text
coverage[i] = diff[0] + diff[1] + ... + diff[i]
```

如此每個查詢只需兩次端點更新，再以一次線性掃描還原所有位置的覆蓋次數，總時間降為 `O(n + q)`。

使用長度 `n + 1` 的差分陣列，可讓 `r == n - 1` 時的結束標記安全寫入 `diff[n]`，不需要在每次查詢內額外判斷右邊界。

## 4. 三種方法

### 方法一：查詢覆蓋次數差分

`IsZeroArray` 建立全零的 `diff`，對每個查詢套用 `diff[l]++` 與 `diff[r + 1]--`。掃描 `nums` 時同步累加 `operationCount`：

- 若 `nums[i] > operationCount`，索引 `i` 的可用操作不足，立即回傳 `false`。
- 全部位置都足夠時回傳 `true`。

這是最直接的解法：先算每格最多能被操作幾次，再和需求比較。

### 方法二：先差分 nums，再套用查詢

`IsZeroArray2` 先把 `nums` 轉成差分表示：

```text
diff[0] = nums[0]
diff[i] = nums[i] - nums[i - 1]
```

每個查詢代表對 `[l, r]` 提供一次減少機會，因此在差分端點做 `diff[l]--` 與 `diff[r + 1]++`。重新累加後，如果某一位置仍大於 0，代表即使把所有涵蓋它的查詢都用上，需求仍未清零；反之，小於或等於 0 表示操作充足，多餘機會可以不選。

### 方法三：分開保存端點與完整覆蓋次數

`IsZeroArray3` 與方法一採相同數學模型，但把流程拆得更明確：

1. `deltaArray` 保存每個查詢的開始與結束標記。
2. `operationCounts` 保存前綴和還原出的每格覆蓋次數。
3. 最後逐格比較 `operationCounts[i]` 與 `nums[i]`。

這個版本多配置一個陣列，換取更清楚的中間狀態，適合觀察差分值如何轉成實際覆蓋次數。

## 5. 逐步範例

以「剛好覆蓋需求」為例：

```text
nums    = [1, 2, 1]
queries = [[0, 2], [1, 1]]
```

建立長度 4 的 `diff`：

1. 查詢 `[0, 2]`：`diff[0] += 1`、`diff[3] -= 1`，得到 `[1, 0, 0, -1]`。
2. 查詢 `[1, 1]`：`diff[1] += 1`、`diff[2] -= 1`，得到 `[1, 1, -1, -1]`。
3. 對前 3 格做前綴和，得到覆蓋次數 `[1, 2, 1]`。
4. 覆蓋次數逐格等於 `nums` 的需求，因此三個方法都回傳 `true`。

再看操作不足：

```text
nums    = [2, 1]
queries = [[0, 1]]
coverage = [1, 1]
```

索引 0 需要 2 次，但只有 1 個查詢涵蓋，因此無法變成零陣列，回傳 `false`。

## 6. 正確性與不變量

核心不變量是：掃描到索引 `i` 時，差分陣列前綴和恰好等於涵蓋 `i` 的查詢數量。

- `diff[l] += 1` 讓查詢的影響從 `l` 開始。
- `diff[r + 1] -= 1` 讓同一影響在 `r` 後停止。
- 所以前綴和在且只在 `[l, r]` 內包含該查詢的一次貢獻。

對每個索引而言，各查詢是否選擇該索引互不衝突，因為一個查詢可以選擇區間內任意子集合。因此：

- 若任何 `coverage[i] < nums[i]`，即使使用所有涵蓋 `i` 的查詢也不夠，答案必為 `false`。
- 若所有 `coverage[i] >= nums[i]`，可為每個索引挑選恰好 `nums[i]` 個涵蓋它的查詢，其餘機會跳過，答案為 `true`。

這證明三個方法所檢查的條件既是必要條件，也是充分條件。

## 7. 複雜度比較

令 `n = nums.Length`、`q = queries.Length`：

| 方法 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- |
| `IsZeroArray` | `O(n + q)` | `O(n)` | 直接比較覆蓋次數與需求 |
| `IsZeroArray2` | `O(n + q)` | `O(n)` | 對需求本身做差分並套用減量 |
| `IsZeroArray3` | `O(n + q)` | `O(n)` | 額外保存完整覆蓋次數，步驟清楚 |

三個方法都只讀取 `nums` 與 `queries`。harness 仍會為每次方法呼叫建立新的 `nums` 與二維 `queries` 副本，避免未來修改方法時出現跨方法污染。

## 8. 測試矩陣與實際輸出

### 固定測試矩陣

| # | 案例 | `nums` | `queries` | 預期 | 重點 |
| ---: | --- | --- | --- | --- | --- |
| 1 | 已是零陣列 | `[0,0,0]` | `[]` | `true` | 零需求不需要查詢 |
| 2 | 操作次數不足 | `[2,1]` | `[[0,1]]` | `false` | 第一格缺少一次覆蓋 |
| 3 | 剛好覆蓋需求 | `[1,2,1]` | `[[0,2],[1,1]]` | `true` | 覆蓋次數等於需求 |
| 4 | 空查詢但仍需操作 | `[1]` | `[]` | `false` | 沒有查詢可用 |
| 5 | 左右邊界皆被涵蓋 | `[1,1,1,1]` | `[[0,0],[3,3],[1,2]]` | `true` | `l = 0` 與 `r = n - 1` |

每組案例都執行 3 個方法，共 15 項檢查。

### Fresh transcript

以下是執行 `DOTNET_ROLL_FORWARD=Major dotnet run --project 差分陣列/差分陣列.csproj --no-build --nologo` 的完整輸出：

```text
Case: 已是零陣列
Input: nums = [0, 0, 0], queries = []
Check: Method 1
Expected: True
Actual: True
PASS-FAIL: PASS

Check: Method 2
Expected: True
Actual: True
PASS-FAIL: PASS

Check: Method 3
Expected: True
Actual: True
PASS-FAIL: PASS

Case: 操作次數不足
Input: nums = [2, 1], queries = [0, 1]
Check: Method 1
Expected: False
Actual: False
PASS-FAIL: PASS

Check: Method 2
Expected: False
Actual: False
PASS-FAIL: PASS

Check: Method 3
Expected: False
Actual: False
PASS-FAIL: PASS

Case: 剛好覆蓋需求
Input: nums = [1, 2, 1], queries = [0, 2], [1, 1]
Check: Method 1
Expected: True
Actual: True
PASS-FAIL: PASS

Check: Method 2
Expected: True
Actual: True
PASS-FAIL: PASS

Check: Method 3
Expected: True
Actual: True
PASS-FAIL: PASS

Case: 空查詢但仍需操作
Input: nums = [1], queries = []
Check: Method 1
Expected: False
Actual: False
PASS-FAIL: PASS

Check: Method 2
Expected: False
Actual: False
PASS-FAIL: PASS

Check: Method 3
Expected: False
Actual: False
PASS-FAIL: PASS

Case: 左右邊界皆被涵蓋
Input: nums = [1, 1, 1, 1], queries = [0, 0], [3, 3], [1, 2]
Check: Method 1
Expected: True
Actual: True
PASS-FAIL: PASS

Check: Method 2
Expected: True
Actual: True
PASS-FAIL: PASS

Check: Method 3
Expected: True
Actual: True
PASS-FAIL: PASS

Summary: 15/15 checks passed.
```

## 9. 專案結構、限制與參考資料

```text
差分陣列/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
├── 差分陣列.sln
└── 差分陣列/
    ├── Memo.md
    ├── Program.cs
    └── 差分陣列.csproj
```

### 已知限制

- 方法依賴題目保證：`nums` 非空、數值非負、查詢格式與索引合法。
- 不處理 `null`、空 `nums`、反向區間或越界查詢。
- smoke harness 是固定案例的快速驗證，不取代完整單元測試或窮舉測試。
- 專案目標仍為 `net9.0`；較新 runtime 的 roll-forward 只用於本機缺少 .NET 9 runtime 的情況。

### 參考資料

- [LeetCode 3355：Zero Array Transformation I](https://leetcode.com/problems/zero-array-transformation-i/)
- [Microsoft Learn：C# 陣列](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/arrays)
- [Microsoft Learn：集合運算式](https://learn.microsoft.com/dotnet/csharp/language-reference/operators/collection-expressions)
