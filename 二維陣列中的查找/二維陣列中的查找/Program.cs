namespace 二維陣列中的查找;

class Program
{
    /// <summary>
    /// LeetCode 240. 搜索二維矩陣 II
    /// 題目描述：
    /// 給定一個 m x n 的整數矩陣 matrix，該矩陣每一行從左到右遞增、每一列從上到下遞增。
    /// 請判斷給定的目標值 target 是否存在於矩陣中。
    /// 
    /// 範例：
    /// matrix = [
    ///   [1, 4, 7, 11, 15],
    ///   [2, 5, 8, 12, 19],
    ///   [3, 6, 9, 16, 22],
    ///   [10, 13, 14, 17, 24],
    ///   [18, 21, 23, 26, 30]
    /// ]
    /// target = 5 回傳 true
    /// target = 20 回傳 false
    /// 
    /// https://leetcode.com/problems/search-a-2d-matrix-ii/description/
    /// https://leetcode.cn/problems/search-a-2d-matrix-ii/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        int[][] matrix =
        {
            new[] { 1, 4, 7, 11, 15 },
            new[] { 2, 5, 8, 12, 19 },
            new[] { 3, 6, 9, 16, 22 },
            new[] { 10, 13, 14, 17, 24 },
            new[] { 18, 21, 23, 26, 30 }
        };

        (string Name, Func<int[][], int, bool> Search, int Target, bool Expected)[] cases =
        {
            ("暴力法-存在", (data, target) => new Program().SearchMatrix(data, target), 5, true),
            ("暴力法-不存在", (data, target) => new Program().SearchMatrix(data, target), 20, false),
            ("二分法-存在", (data, target) => new Program().SearchMatrix_binary(data, target), 5, true),
            ("二分法-不存在", (data, target) => new Program().SearchMatrix_binary(data, target), 20, false),
            ("右上角法-存在", (data, target) => new Program().SearchMatrix_RightTop(data, target), 5, true),
            ("右上角法-不存在", (data, target) => new Program().SearchMatrix_RightTop(data, target), 20, false)
        };

        int total = 0;
        int passed = 0;
        foreach (var testCase in cases)
        {
            RunCase(
                testCase.Name,
                testCase.Expected.ToString(),
                () => testCase.Search(CloneMatrix(matrix), testCase.Target).ToString(),
                ref total,
                ref passed);
        }

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 複製矩陣，讓每個方法都在獨立輸入上執行。
    /// </summary>
    /// <param name="matrix">要複製的矩陣。</param>
    /// <returns>新的鋸齒狀二維陣列。</returns>
    private static int[][] CloneMatrix(int[][] matrix)
    {
        return matrix.Select(row => row.ToArray()).ToArray();
    }

    /// <summary>
    /// 執行一個矩陣搜尋案例並輸出統一的驗證結果。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="expected">預期布林結果。</param>
    /// <param name="actualFactory">產生實際結果的函式。</param>
    /// <param name="total">累積案例數。</param>
    /// <param name="passed">累積通過數。</param>
    private static void RunCase(string name, string expected, Func<string> actualFactory, ref int total, ref int passed)
    {
        total++;
        string actual;
        try
        {
            actual = actualFactory();
        }
        catch (Exception exception)
        {
            actual = $"EXCEPTION: {exception.GetType().Name}: {exception.Message}";
        }

        bool isPassed = actual == expected;
        if (isPassed)
        {
            passed++;
        }

        Console.WriteLine($"[{name}]");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Actual: {actual}");
        Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
    }


    /// <summary>
    /// 方法1: 暴力法
    /// 逐行、逐列檢查二維矩陣中是否存在目標值 target。
    /// 時間複雜度為 O(m*n)，其中 m 為行數，n 為列數。
    /// 若找到目標值則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="matrix">二維整數陣列，每一個 row 代表一行</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳 true，否則回傳 false</returns>
    public bool SearchMatrix(int[][] matrix, int target)
    {
        // 逐行遍歷矩陣
        foreach (int[] row in matrix)
        {
            // 逐列檢查當前行的每個元素
            foreach (int element in row)
            {
                // 若元素等於目標值，立即回傳 true
                if (element == target)
                {
                    return true;
                }
            }
        }
        // 若全部檢查完都沒找到，回傳 false
        return false;
    }

    /// <summary>
    /// 方法2: 二分法
    /// 對每一行(row)使用二分搜尋法來尋找目標值 target。
    /// 時間複雜度為 O(m*logn)，其中 m 為行數，n 為列數。
    /// 若任一行找到目標值則回傳 true，否則回傳 false。
    /// 
    /// ref:
    /// https://leetcode.cn/problems/search-a-2d-matrix-ii/solutions/1062538/sou-suo-er-wei-ju-zhen-ii-by-leetcode-so-9hcx/
    /// </summary>
    /// <param name="matrix">二維整數陣列，每一個 row 代表一行</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳 true，否則回傳 false</returns>
    public bool SearchMatrix_binary(int[][] matrix, int target)
    {
        // 逐行遍歷矩陣
        foreach (int[] row in matrix)
        {
            // 對當前行使用二分搜尋法
            int index = Search(row, target);
            // 若找到目標值，立即回傳 true
            if (index >= 0)
            {
                return true;
            }
        }
        // 若全部行都沒找到，回傳 false
        return false;
    }

    /// <summary>
    /// 一維陣列的二分搜尋法。
    /// 簡單說就是對每一行做二分法搜尋。
    /// 在已排序的整數陣列 nums 中搜尋目標值 target。
    /// 若找到則回傳目標值的索引，否則回傳 -1。
    /// </summary>
    /// <param name="nums">已排序的一維整數陣列</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳其索引，否則回傳 -1</returns>
    public int Search(int[] nums, int target)
    {
        int low = 0;
        int high = nums.Length - 1;
        // 當 low <= high 時持續搜尋
        while (low <= high)
        {
            // 取中間索引
            int mid = low + (high - low) / 2;
            int num = nums[mid];
            // 若中間值等於目標值，回傳索引
            if (num == target)
            {
                return mid;
            }
            // 若中間值小於目標值，縮小搜尋範圍到右半部
            else if (num < target)
            {
                low = mid + 1;
            }
            // 若中間值大於目標值，縮小搜尋範圍到左半部
            else
            {
                high = mid - 1;
            }
        }
        // 若沒找到，回傳 -1
        return -1;
    }


    /// <summary>
    /// 方法3: 從右上角開始搜尋法
    /// 從矩陣的右上角 (第一行最後一列) 開始，根據當前元素與目標值的比較結果，
    /// 決定往左(減少列)或往下(增加行)移動，直到找到目標值或超出邊界。
    /// 時間複雜度為 O(m+n)，其中 m 為行數，n 為列數。
    /// 若找到目標值則回傳 true，否則回傳 false。
    /// 
    /// ref:
    /// https://leetcode.cn/problems/search-a-2d-matrix-ii/solutions/2783938/tu-jie-pai-chu-fa-yi-tu-miao-dong-python-kytg/
    /// </summary>
    /// <param name="matrix">二維整數陣列，每一個 row 代表一行</param>
    /// <param name="target">要搜尋的目標值</param>
    /// <returns>若找到目標值則回傳 true，否則回傳 false</returns>
    public bool SearchMatrix_RightTop(int[][] matrix, int target)
    {
        // 從右上角 (第0行, 最後一列) 開始
        int i = 0;
        int j = matrix[0].Length - 1;
        // 當 i 未超過行數且 j 未小於0時持續搜尋
        while (i < matrix.Length && j >= 0)
        {
            // 若當前元素等於目標值，回傳 true
            if (matrix[i][j] == target)
            {
                return true;
            }
            // 若當前元素大於目標值，往左移動(減少列)
            else if (matrix[i][j] > target)
            {
                j--;
            }
            // 若當前元素小於目標值，往下移動(增加行)
            else
            {
                i++;
            }
        }
        // 若沒找到，回傳 false
        return false;
    }
}