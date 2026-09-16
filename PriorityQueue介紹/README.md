# C# `PriorityQueue<TElement, TPriority>` 介紹

本專案使用 C# / .NET 10 示範 `PriorityQueue<TElement, TPriority>` 的預設排序行為：數字優先權值越小，越早被 `Peek` 與 `Dequeue` 取出。固定 smoke harness 同時驗證每一步的 `Count`、`Peek` 與出隊元素。

## 閱讀導覽

1. [行為契約與快速開始](#1-行為契約與快速開始)
2. [PriorityQueue 的兩個型別](#2-priorityqueue-的兩個型別)
3. [建立與操作流程](#3-建立與操作流程)
4. [逐步範例](#4-逐步範例)
5. [正確性與不變量](#5-正確性與不變量)
6. [複雜度](#6-複雜度)
7. [測試矩陣與實際輸出](#7-測試矩陣與實際輸出)
8. [專案結構、限制與參考資料](#8-專案結構限制與參考資料)

## 1. 行為契約與快速開始

本範例使用：

```csharp
PriorityQueue<int, int>
```

並放入三組「元素、優先權」：

| 元素 | 優先權 |
| ---: | ---: |
| `1` | `100` |
| `2` | `10` |
| `3` | `50` |

預設比較器選出最小的優先權值，所以出隊順序必須是 `2 → 3 → 1`。這裡的「優先權較高」是概念上的先處理，不是數值較大；數字 `10` 比 `50`、`100` 更早出隊。

請在本 README 所在的 `PriorityQueue介紹` 外層目錄執行：

```bash
dotnet restore PriorityQueue介紹/PriorityQueue介紹.csproj
dotnet build PriorityQueue介紹/PriorityQueue介紹.csproj --nologo
```

`DOTNET_ROLL_FORWARD=Major` 只在本機沒有 .NET 10 runtime、但有相容的較新 runtime 時需要。成功時最後一行為：

```text
Summary: 9/9 checks passed.
```

任一檢查失敗時會在 `PASS-FAIL` 欄位印出 `FAIL`，並將 `Environment.ExitCode` 設為 `1`。程式不讀取互動輸入，也沒有亂數或等待。

## 2. PriorityQueue 的兩個型別

`PriorityQueue<TElement, TPriority>` 把「保存的資料」與「排序依據」分開：

- `TElement`：真正要排程、取出的資料。本例是 `int` 元素 `1`、`2`、`3`。
- `TPriority`：用來決定先後順序的值。本例同樣是 `int`，分別為 `100`、`10`、`50`。

呼叫：

```csharp
priorityQueue.Enqueue(2, 10);
```

表示把元素 `2` 放入佇列，並以 `10` 作為它的優先權。元素本身不需要和優先權相同，也可以改成例如：

```csharp
PriorityQueue<string, DateTime>
```

用工作名稱當元素、到期時間當優先權。

## 3. 建立與操作流程

### `CreateDemoQueue`

helper 每次建立全新的優先佇列，再依原始示範加入三個元素。固定資料集中在同一處，harness 不會依賴先前執行留下的可變佇列狀態。

```csharp
PriorityQueue<int, int> priorityQueue = new();
priorityQueue.Enqueue(1, 100);
priorityQueue.Enqueue(2, 10);
priorityQueue.Enqueue(3, 50);
```

### `DrainDemoQueue`

只要 `Count > 0`，就依序記錄：

1. 出隊前的 `Count`。
2. `Peek()` 看到、但尚未移除的元素。
3. `Dequeue()` 實際移除並回傳的元素。

每次迴圈完成後，佇列剛好少一個元素；全部完成後，輸入佇列為空。

### `PrintCheck`

每一個整數欄位都獨立輸出 `Expected`、`Actual` 與 `PASS-FAIL`。回傳 `1` 或 `0` 讓 `Main` 累計通過數，最後比較總檢查數並設定失敗 exit code。

## 4. 逐步範例

初始內容可抽象成：

```text
元素：優先權
1：100
2：10   <- 最小優先權值
3：50
```

第一步：

- `Count` 是 `3`。
- `Peek()` 回傳元素 `2`，但不移除它，所以此刻數量仍是 3。
- `Dequeue()` 也回傳元素 `2`，並把它移除。

第二步剩下 `(1,100)` 與 `(3,50)`：

- `Count` 是 `2`。
- 最小優先權值是 `50`，所以 `Peek()` 與 `Dequeue()` 都得到元素 `3`。

第三步只剩 `(1,100)`：

- `Count` 是 `1`。
- `Peek()` 與 `Dequeue()` 都得到元素 `1`。

完整出隊順序因此是：

```text
2 → 3 → 1
```

## 5. 正確性與不變量

每次迴圈開始時維持兩個重要性質：

1. `Count` 等於尚未出隊的元素數量。
2. `Peek()` 回傳目前具有最小優先權值的元素，而且不改變 `Count`。

緊接著的 `Dequeue()` 會移除同一個最小元素，因此下一輪 `Count` 恰好少 1。因為三個優先權值 `10 < 50 < 100` 且互不相同，三輪依序取出元素 `2`、`3`、`1`，之後佇列為空，迴圈終止。

harness 對每輪的 Count、Peek、Dequeue 各做一次獨立比對，因此可分辨下列錯誤：

- 數量沒有隨出隊遞減。
- `Peek` 錯誤地查看非最小優先權元素。
- `Dequeue` 的順序不符合預設比較器。

## 6. 複雜度

令佇列內元素數量為 `n`：

| 操作 | 時間複雜度 | 說明 |
| --- | --- | --- |
| `Enqueue` | `O(log n)` | 維護 heap 順序 |
| `Peek` | `O(1)` | 讀取目前最小元素，不移除 |
| `Dequeue` | `O(log n)` | 移除最小元素並恢復 heap |
| `Count` | `O(1)` | 讀取目前元素數 |
| 完整清空 | `O(n log n)` | 重複 `n` 次 Dequeue |

`PriorityQueue` 的內部儲存需要 `O(n)` 空間；本專案另外以 `QueueStep[]` 保存固定觀察結果，也是 `O(n)`。

## 7. 測試矩陣與實際輸出

### 固定測試矩陣

| 步驟 | 預期 Count | 預期 Peek | 預期 Dequeue | 剩餘優先權 |
| ---: | ---: | ---: | ---: | --- |
| 1 | `3` | `2` | `2` | `50, 100` |
| 2 | `2` | `3` | `3` | `100` |
| 3 | `1` | `1` | `1` | 無 |

每一步有 3 項檢查，共 9 項。

### Fresh transcript

以下是執行 `DOTNET_ROLL_FORWARD=Major dotnet run --project PriorityQueue介紹/PriorityQueue介紹.csproj --no-build --nologo` 的完整輸出：

```text
Case: 預設比較器依數字優先權由小到大出隊
Check: Step 1 Count
Expected: 3
Actual: 3
PASS-FAIL: PASS

Check: Step 1 Peek
Expected: 2
Actual: 2
PASS-FAIL: PASS

Check: Step 1 Dequeue
Expected: 2
Actual: 2
PASS-FAIL: PASS

Check: Step 2 Count
Expected: 2
Actual: 2
PASS-FAIL: PASS

Check: Step 2 Peek
Expected: 3
Actual: 3
PASS-FAIL: PASS

Check: Step 2 Dequeue
Expected: 3
Actual: 3
PASS-FAIL: PASS

Check: Step 3 Count
Expected: 1
Actual: 1
PASS-FAIL: PASS

Check: Step 3 Peek
Expected: 1
Actual: 1
PASS-FAIL: PASS

Check: Step 3 Dequeue
Expected: 1
Actual: 1
PASS-FAIL: PASS

Summary: 9/9 checks passed.
```

## 8. 專案結構、限制與參考資料

```text
PriorityQueue介紹/
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
├── PriorityQueue介紹.sln
└── PriorityQueue介紹/
    ├── Program.cs
    └── PriorityQueue介紹.csproj
```

### 已知限制

- 本例只驗證預設比較器與互不相同的整數優先權，不對相同優先權元素假設 FIFO 順序。
- `Peek()` 與 `Dequeue()` 對空佇列會拋出 `InvalidOperationException`；本例以 `Count > 0` 保護呼叫。
- 反向比較器與負優先權只保留在原始 XML 說明中，本 harness 不驗證「數字越大先出隊」模式。
- smoke harness 是固定案例的快速驗證，不取代完整單元測試。
- 專案目標仍為 `net10.0`；較新 runtime 的 roll-forward 只用於本機缺少 .NET 10 runtime 的情況。

### 參考資料

- [Microsoft Learn：PriorityQueue 類別](https://learn.microsoft.com/dotnet/api/system.collections.generic.priorityqueue-2?view=net-10.0)
- [Microsoft Learn：PriorityQueue.Dequeue](https://learn.microsoft.com/dotnet/api/system.collections.generic.priorityqueue-2.dequeue?view=net-10.0)
- [Microsoft Learn：PriorityQueue.Peek](https://learn.microsoft.com/dotnet/api/system.collections.generic.priorityqueue-2.peek?view=net-10.0)
