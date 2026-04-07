# 3乘5陣列取總和

> 使用 C# 對一個 3×5 的二維整數陣列計算所有元素的總和。

## 題目說明

給定一個 **3 列 × 5 行** 的二維整數陣列，計算並輸出陣列內所有元素的加總結果。

程式同時提供兩種測試模式：

| 模式 | 說明 |
|------|------|
| 固定測試陣列 | 元素值為 1~15，預期總和為 **120**，用來驗證邏輯正確性 |
| 亂數測試陣列 | 每個元素隨機產生於 1~199 之間，每次執行結果不同 |

## 解題概念與出發點

### 核心問題

二維陣列在 C# 中是以「列優先 (row-major)」方式在記憶體中配置。  
要取得全部元素的總和，最直觀的方式就是**逐一走訪每個位置，累加其值**。

### 解題策略

1. **走訪二維陣列**：使用雙層 `for` 迴圈，外層控制「列 (row)」、內層控制「行 (column)」。
2. **累加**：宣告一個累加變數 `sum`，初始值為 0，每走訪一個元素就加入 `sum`。
3. **回傳結果**：雙層迴圈結束後，`sum` 即為所求的陣列總和。

**時間複雜度**：$O(m \times n)$，其中 $m$ 為列數、$n$ 為行數。  
**空間複雜度**：$O(1)$（僅使用常數個額外變數）。

### 關鍵 API

```csharp
array.GetLength(0)  // 取得第 0 維大小（列數）
array.GetLength(1)  // 取得第 1 維大小（行數）
```

> [!NOTE]
> `GetLength(dimension)` 回傳指定維度的元素數量，一維陣列傳入 `0`，
> 二維陣列的第二個維度傳入 `1`，以此類推。
> 詳見 [Array.GetLength 文件](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.getlength)。

## 詳細解法

### `cal` — 計算陣列總和

```csharp
public int cal(int[,] array)
{
    int sum = 0;

    // 外層迴圈：逐列走訪，共 GetLength(0) 列
    for (int i = 0; i < array.GetLength(0); i++)
    {
        // 內層迴圈：逐行走訪，共 GetLength(1) 行
        for (int j = 0; j < array.GetLength(1); j++)
        {
            sum += array[i, j];   // 累加當前元素
        }
    }

    return sum;
}
```

### `GenRandomArray` — 亂數產生陣列

```csharp
public int[,] GenRandomArray(int row, int column)
{
    Random random = new Random();       // 系統時間為 seed，每次結果不同
    int[,] array = new int[row, column];

    for (int i = 0; i < row; i++)
        for (int j = 0; j < column; j++)
            array[i, j] = random.Next(1, 200);  // 範圍 [1, 199]

    return array;
}
```

### `printarray` — 矩陣格式輸出

```csharp
public void printarray(int[,] array)
{
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
            Console.Write(array[i, j] + " ");

        Console.WriteLine();  // 每列結束換行，呈現矩陣排版
    }
}
```

## 執行演示

### 固定測試陣列（預期輸出）

輸入陣列：

```
 1   2   3   4   5
 6   7   8   9  10
11  12  13  14  15
```

計算過程（依列累加）：

| 列 | 元素 | 小計 |
|----|------|------|
| 第 0 列 | 1 + 2 + 3 + 4 + 5 | 15 |
| 第 1 列 | 6 + 7 + 8 + 9 + 10 | 40 |
| 第 2 列 | 11 + 12 + 13 + 14 + 15 | 65 |
| **合計** | | **120** |

終端機輸出：

```
運算陣列 3 * 5
========================
[固定測試陣列]
1 2 3 4 5
6 7 8 9 10
11 12 13 14 15
fixed array sum: 120
```

### 亂數測試陣列（範例輸出）

```
[亂數測試陣列]
45 132 87 11 193
78 56 104 23 167
9 144 62 188 31
array sum: 1330
```

> [!TIP]
> 每次執行所產生的亂數陣列都不同，但計算邏輯完全相同，可用來驗證各種輸入下的穩定性。

## 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) 或更高版本

## 快速開始

```bash
# 複製專案後進入目錄
cd 3乘5陣列取總和

# 建構並執行
dotnet run --project 3乘5陣列取總和/3乘5陣列取總和.csproj
```

或在 VS Code 中按下 <kbd>F5</kbd> 直接啟動偵錯。
