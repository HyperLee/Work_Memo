namespace _3乘5陣列取總和;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("運算陣列 3 * 5");
        Console.WriteLine("========================");

        // ── 測試資料一：固定已知數值，驗證計算正確性 ──
        // 陣列元素為 1~15，預期總和 = (1+15)*15/2 = 120
        int[,] array = new int[3, 5]
        {
            { 1,  2,  3,  4,  5 },
            { 6,  7,  8,  9, 10 },
            { 11, 12, 13, 14, 15 }
        };

        Console.WriteLine("[固定測試陣列]");
        printarray(array);
        int fixedSum = cal(array);
        // 預期輸出：fixed array sum: 120
        Console.WriteLine("fixed array sum: " + fixedSum);
        Console.WriteLine();

        // ── 測試資料二：亂數產生 3 * 5 陣列 ──
        // 每次執行結果不同，用來驗證不同輸入下計算邏輯的穩定性
        int[,] randomarray = GenRandomArray(3, 5);

        Console.WriteLine("[亂數測試陣列]");
        printarray(randomarray);

        // 計算亂數陣列總和
        int sum = cal(randomarray);
        Console.WriteLine("array sum: " + sum);
    }

    /// <summary>
    /// 計算二維陣列所有元素的總和。
    /// <para>
    /// 【解題說明】<br/>
    /// 二維陣列在記憶體中以「列優先 (row-major)」方式配置，
    /// 最直覺的走訪方式是以雙層 for 迴圈依序掃描每一個元素，
    /// 並累加至同一個變數，最終得到所有元素的總和。
    /// 時間複雜度 O(m×n)，其中 m 為列數、n 為行數。
    /// </para>
    /// <para>
    /// GetLength(0)：取第 0 維（列數，row count）<br/>
    /// GetLength(1)：取第 1 維（行數，column count）<br/>
    /// 參考：https://learn.microsoft.com/zh-tw/dotnet/api/system.array.getlength
    /// </para>
    /// </summary>
    /// <param name="array">要計算總和的二維整數陣列。</param>
    /// <returns>陣列中所有元素相加後的整數總和。</returns>
    public static int cal(int[,] array)
    {
        int sum = 0;

        // 外層迴圈：逐列走訪 (row)，共 GetLength(0) 列
        for (int i = 0; i < array.GetLength(0); i++)
        {
            // 內層迴圈：逐行走訪 (column)，共 GetLength(1) 行
            for (int j = 0; j < array.GetLength(1); j++)
            {
                // 將當前元素累加至總和
                sum += array[i, j];
            }
        }

        return sum;
    }

    /// <summary>
    /// 以亂數填滿並回傳指定大小的二維整數陣列。
    /// <para>
    /// 【解題說明】<br/>
    /// 為了讓每次執行都能測試不同數值，程式在固定陣列之外額外提供
    /// 亂數陣列的生成功能。使用 <see cref="Random.Next(int, int)"/> 產生
    /// 介於 1（含）到 200（不含）的整數，並以雙層 for 迴圈依序填入
    /// 每個陣列位置，確保所有元素都被初始化。
    /// </para>
    /// </summary>
    /// <param name="row">陣列的列數（維度 0 大小）。</param>
    /// <param name="column">陣列的行數（維度 1 大小）。</param>
    /// <returns>以亂數填滿的 <paramref name="row"/> × <paramref name="column"/> 二維整數陣列。</returns>
    public static int[,] GenRandomArray(int row, int column)
    {
        // 建立 Random 實例，預設使用系統時間作為 seed，確保每次結果不同
        Random random = new Random();

        // 配置指定大小的二維陣列
        int[,] array = new int[row, column];

        // 逐一填入亂數值
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                // Random.Next(minValue, maxValue) 回傳 [minValue, maxValue) 的整數
                // 亂數範圍：1 ~ 199（含頭不含尾，即 Next(1, 200)）
                array[i, j] = random.Next(1, 200);
            }
        }

        return array;
    }

    /// <summary>
    /// 將二維整數陣列以矩陣格式輸出至主控台。
    /// <para>
    /// 【解題說明】<br/>
    /// 為了直觀驗算結果，需要將陣列以「列為橫行、行為直欄」的矩陣形式
    /// 顯示在終端機上。做法是：內層迴圈走完同一列的所有元素後，
    /// 呼叫 <see cref="Console.WriteLine()"/> 換行，使每一列獨佔一行輸出，
    /// 形成易讀的矩陣視覺效果。
    /// </para>
    /// </summary>
    /// <param name="array">要顯示的二維整數陣列。</param>
    public static void printarray(int[,] array)
    {
        // 外層迴圈：逐列輸出
        for (int i = 0; i < array.GetLength(0); i++)
        {
            // 內層迴圈：同一列的元素以空格分隔，輸出在同一行
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + " ");
            }

            // 每列結束後換行，保持矩陣排版
            Console.WriteLine();
        }
    }
}
