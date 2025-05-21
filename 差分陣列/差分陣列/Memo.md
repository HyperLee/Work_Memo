# LeetCode 3355: 零陣列轉換 I

本專案解決 LeetCode 3355 零陣列轉換問題。主要使用差分陣列技術實現高效的區間操作計算。

## 📝 問題描述

給定一個整數陣列 `nums`，可以對陣列中的每個元素進行以下操作：

1. 選擇一個索引 `i`，使得 `0 <= i < nums.length`
2. 將 `nums[i]` 的值減少 1

另外，給定一個查詢陣列 `queries`，每個查詢包含兩個整數 `[l, r]`，表示對索引從 l 到 r 的所有元素同時進行上述操作一次。

問題要求判斷是否可以通過執行給定的查詢操作，將 `nums` 中的所有元素都變為 0。

## 🧩 解決方案

專案提供了三種不同的實現方式，都使用差分陣列技術判斷是否可以將陣列轉換為全零陣列：

### 1. IsZeroArray (方法一)

最基本的差分陣列應用，直接計算操作次數與原始值比較。

```csharp
public static bool IsZeroArray(int[] nums, int[][] queries)
```

此方法使用差分陣列技術記錄區間操作，累加得到每個位置的操作次數，再與原始陣列對應位置的值進行比較，判斷是否可以將每個元素變為 0。

### 2. IsZeroArray2 (方法二)

採用差分陣列的逆向思考，先將原始陣列轉換為其差分陣列表示形式。

```csharp
public static bool IsZeroArray2(int[] nums, int[][] queries)
```

此方法首先將原始陣列轉為差分形式，然後對查詢區間進行差分操作標記，通過累加差分陣列判斷結果是否都小於等於 0。

### 3. IsZeroArray3 (方法三)

使用額外的陣列儲存操作次數，邏輯結構更清晰。

```csharp
public bool IsZeroArray3(int[] nums, int[][] queries)
```

此方法使用兩個獨立陣列分別處理差分和操作次數，先計算差分後計算前綴和，提高了程式碼的可讀性。

## 🔍 差分陣列技術詳解

差分陣列是一種高效處理區間更新的資料結構。如果原始陣列為 A，其差分陣列 D 定義為：

- D\[0] = A\[0]
- D\[i] = A\[i] - A\[i-1] (i > 0)

### 差分陣列的優勢

1. **高效的區間操作**：對區間 \[l, r] 中的所有元素加上值 v，只需要：
   - D\[l] += v
   - D\[r+1] -= v
   - 時間複雜度從 O (r-l+1) 優化為 O (1)

2. **批量處理**：多次區間操作可以在差分陣列上疊加，最後一次性還原

3. **空間效率**：只需要與原陣列相同量級的額外空間

### 差分陣列步驟

1. **建立差分陣列**：根據原始陣列或從零開始
2. **記錄區間操作**：在差分陣列的左邊界加上值，右邊界 + 1 處減去相同的值
3. **還原操作結果**：通過前綴和計算還原操作後的陣列

## 📊 複雜度分析

| 方法                 | 時間複雜度    | 空間複雜度 |
| ------------------ | -------- | ----- |
| IsZeroArray (方法一)  | O(n + q) | O(n)  |
| IsZeroArray2 (方法二) | O(n + q) | O(n)  |
| IsZeroArray3 (方法三) | O(n + q) | O(n)  |

其中 n 是陣列長度，q 是查詢操作數量。

## 🧪 使用範例

### 1. 測試案例 1: 可以轉換為全零陣列的情況

```csharp
int[] nums1 = { 2, 1, 3, 2 };
int[][] queries1 = {
    new int[] { 0, 1 },
    new int[] { 1, 3 },
    new int[] { 0, 3 },
    new int[] { 0, 3 }
};

// 方法一測試
bool result1 = IsZeroArray(nums1, queries1);
Console.WriteLine($"方法一結果: {(result1 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

// 方法二測試
bool result2 = IsZeroArray2(nums1, queries1);
Console.WriteLine($"方法二結果: {(result2 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");

// 方法三測試
Program instance = new Program();
bool result3 = instance.IsZeroArray3(nums1, queries1);
Console.WriteLine($"方法三結果: {(result3 ? "可以轉換為全零陣列" : "無法轉換為全零陣列")}");
```

### 2. 測試案例 2: 無法轉換為全零陣列的情況

```csharp
int[] nums2 = { 3, 2, 1, 4 };
int[][] queries2 = {
    new int[] { 0, 2 },
    new int[] { 1, 3 }
};

// 測試三種方法
bool test1 = IsZeroArray(nums2, queries2);   // 結果: false
bool test2 = IsZeroArray2(nums2, queries2);  // 結果: false
bool test3 = instance.IsZeroArray3(nums2, queries2);  // 結果: false
```

## 三種方法比較

### 方法一 (IsZeroArray)

```csharp
public static bool IsZeroArray(int[] nums, int[][] queries)
{
    // 獲取陣列長度
    int n = nums.Length;
    // 建立差分陣列，長度為 n+1 以處理邊界情況
    int[] diff = new int[n + 1];

    // 遍歷每個查詢操作
    foreach (int[] q in queries)
    {
        // 對區間 [l,r] 進行操作標記
        diff[q[0]]++;
        diff[q[1] + 1]--;
    }

    // 累加差分陣列得到每個位置的實際操作次數
    int operationCount = 0;
    for (int i = 0; i < n; i++)
    {
        operationCount += diff[i];
        // 檢查操作次數是否足夠將原值變為 0
        if (nums[i] > operationCount)
        {
            return false;
        }
    }
    return true;
}
```

### 方法二 (IsZeroArray2)

```csharp
public static bool IsZeroArray2(int[] nums, int[][] queries)
{
    int n = nums.Length;
    // 建立差分陣列表示形式，將原陣列轉為差分形式
    int[] diff = new int[n + 1];
    diff[0] = nums[0];
    for (int i = 1; i < n; i++)
    {
        diff[i] = nums[i] - nums[i - 1];
    }
    
    // 遍歷每個查詢操作，更新差分陣列
    foreach (int[] q in queries)
    {
        diff[q[0]]--;
        diff[q[1] + 1]++;
    }

    if (diff[0] > 0)
    { 
        return false;
    }
    
    // 檢查每個位置是否可以轉換為 0
    for (int i = 1; i < n; i++)
    {
        diff[i] += diff[i - 1];
        if (diff[i] > 0)
        {
            return false;
        }
    }
    return true;
}
```

### 方法三 (IsZeroArray3)

```csharp
public bool IsZeroArray3(int[] nums, int[][] queries)
{
    // 建立差分陣列，用於記錄區間操作
    int[] deltaArray = new int[nums.Length + 1];
    foreach (int[] query in queries)
    {
        int left = query[0];
        int right = query[1];
        deltaArray[left] += 1;
        deltaArray[right + 1] -= 1;
    }
    
    // 計算每個位置的操作總次數
    int[] operationCounts = new int[deltaArray.Length];
    int currentOperations = 0;
    for (int i = 0; i < deltaArray.Length; i++)
    {
        currentOperations += deltaArray[i];
        operationCounts[i] = currentOperations;
    }
    
    // 檢查每個位置是否可以轉換為0
    for (int i = 0; i < nums.Length; i++)
    {
        if (operationCounts[i] < nums[i])
        {
            return false;
        }
    }
    return true;
}
```

## 🔄 差分陣列工作原理示範

在 LeetCode 3355 問題中，我們使用差分陣列來處理區間操作。以下是一個簡單的實例解釋：

假設有一個陣列 `nums = [2, 1, 3, 2]`，及查詢操作 `queries = [[0, 1], [1, 3]]`：

1. **初始化差分陣列**: `diff = [0, 0, 0, 0, 0]` (長度為 n+1)

2. **處理查詢操作**:
   - 查詢 \[0, 1]: diff \[0]++, diff \[2]-- → diff = \[1, 0, -1, 0, 0]
   - 查詢 \[1, 3]: diff \[1]++, diff \[4]-- → diff = \[1, 1, -1, 0, -1]

3. **計算操作次數**:
   - position 0: diff\[0] = 1
   - position 1: diff\[0] + diff\[1] = 1 + 1 = 2
   - position 2: diff\[0] + diff\[1] + diff\[2] = 1 + 1 + (-1) = 1
   - position 3: diff\[0] + diff\[1] + diff\[2] + diff\[3] = 1 + 1 + (-1) + 0 = 1

4. **比較操作次數與原始值**:
   - nums \[0] = 2 > 操作次數 1: 無法變為 0
   - nums \[1] = 1 < 操作次數 2: 可以變為 0
   - nums \[2] = 3 > 操作次數 1: 無法變為 0
   - nums \[3] = 2 > 操作次數 1: 無法變為 0

因此，通過這些操作，無法將陣列變為全零。
