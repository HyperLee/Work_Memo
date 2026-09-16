namespace 二分法查找標準題
{
    internal class Program
    {
        /// <summary>
        /// 704. Binary Search
        /// https://leetcode.com/problems/binary-search/description/
        /// 
        /// 704. 二分查找
        /// https://leetcode.cn/problems/binary-search/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (int[] Numbers, int Target, int Expected)[] cases =
            {
                (new[] { -1, 0, 3, 5, 9, 12 }, 9, 4),
                (new[] { -1, 0, 3, 5, 9, 12 }, 2, -1),
                (new[] { 1, 2, 3, 4 }, 1, 0)
            };

            int total = 0;
            int passed = 0;
            foreach (var testCase in cases)
            {
                RunCase(
                    $"target={testCase.Target}",
                    testCase.Expected.ToString(),
                    () => Search(testCase.Numbers, testCase.Target).ToString(),
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
        /// 執行一個二分搜尋案例並輸出統一的驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期索引。</param>
        /// <param name="actualFactory">產生實際索引的函式。</param>
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
        /// 二分法查找
        /// 
        /// 1. 每次查找時從陣列的中間 element 開始, 如果中間 element 正好是要查找的 element, 則搜尋結束
        /// 2. 如果某一特定 element 大於或者小於中間 element, 則陣列大於或小於中間 element 的那一半中查找,
        /// 而且跟開始一樣從中間 element 開始比較
        /// 3. 如果在某一步驟陣列為空, 則代表找不到
        /// 
        /// 此題目為標準 二分法練習題目
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Search(int[] nums, int target)
        {
            // 左邊界
            int left = 0;
            // 右邊界
            int right = nums.Length - 1;

            while (left <= right)
            {
                // 中間值; 此寫法是避免溢位
                int middle = left + (right - left) / 2;

                if (nums[middle] > target)
                {
                    // 比目標大右邊界左移(縮小)
                    right = middle - 1;
                }
                else if (nums[middle] < target)
                {
                    // 比目標小左邊界右移(放大)
                    left = middle + 1;
                }
                else
                {
                    // 找到目標
                    return middle;
                }
            }

            // 找不到目標
            return -1;
        }
    }
}