namespace Array_Sort進階用法
{
    internal class Program
    {
        /// <summary>
        /// 2593. Find Score of an Array After Marking All Elements
        /// https://leetcode.com/problems/find-score-of-an-array-after-marking-all-elements/description/?envType=daily-question&envId=2024-12-13
        /// 
        /// 2593. 标记所有元素后数组的分数
        /// https://leetcode.cn/problems/find-score-of-an-array-after-marking-all-elements/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("題目範例", 7, new int[] { 2, 1, 3, 4, 5, 2 });
            passed += RunCase("最小值在左端", 4, new int[] { 1, 2, 3 });
            passed += RunCase("相同最小值依索引處理", 3, new int[] { 2, 2, 1, 2 });

            Console.WriteLine($"Summary: {passed}/3 checks passed.");
            if (passed != 3)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 比較依數值與索引排序後標記元素所得的分數。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期分數。</param>
        /// <param name="input">本案例獨立的輸入陣列。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string name, long expected, int[] input)
        {
            long actual = FindScore([.. input]);
            bool passed = expected == actual;
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/find-score-of-an-array-after-marking-all-elements/solutions/2177231/dai-zhao-xia-biao-qu-pai-xu-pythonjavacg-umuv/
        /// https://leetcode.cn/problems/find-score-of-an-array-after-marking-all-elements/solutions/2790961/2593-biao-ji-suo-you-yuan-su-hou-shu-zu-mqljy/
        /// 
        /// 根據題目描述方式
        /// 模擬其作法
        /// 0. 初始化 Score = 0
        /// 1. 選擇陣列中尚未標記的最小整數。如果有相同數值，則選擇索引最小的那一個。
        /// 2. 將選定整數的值加到 Score 中。
        /// 3. 標記選定的元素及其相鄰的兩個元素（如果存在）。
        /// 4. 重複上述步驟直到陣列中的所有元素都被標記為止。
        /// 
        /// 此份程式碼著重在 Array.Sort
        /// 判斷條件後, 使用 ASC 與 DESC 寫法
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static long FindScore(int[] nums)
        {
            long Score = 0;
            int n = nums.Length;
            // 模擬描述
            int[][] numIndices = new int[n][];
            // 依照順序給予 [element_value][index]
            for (int i = 0; i < n; i++)
            {
                numIndices[i] = new int[2];
                // element_value
                numIndices[i][0] = nums[i];
                // index
                numIndices[i][1] = i;
            }

            // 排序, 排序的方法为首先按照元素值升序排序
            // ，当元素值相等时按照下标(index)升序排序。
            Array.Sort(numIndices, (a, b) =>
            {
                if (a[0] != b[0])
                {
                    // asc 寫法
                    return a[0] - b[0];

                    // desc 寫法
                    //return b[0] - a[0];
                }
                else
                {
                    // asc 寫法
                    return a[1] - b[1];

                    // desc 寫法
                    //return b[1] - a[1];
                }
            });

            // hash 紀錄被標記 element index
            bool[] marked = new bool[n];
            for (int i = 0; i < n; i++)
            {
                int num = numIndices[i][0], index = numIndices[i][1];
                // 選定未標記項目
                if (!marked[index])
                {
                    // 加入 Score
                    Score += num;
                    // 加入後標記選定項目
                    marked[index] = true;

                    // 標定項目的左右兩邊 index 也要一同標記. (index - 1 與 index + 1)
                    if (index > 0)
                    {
                        // 左邊界標記
                        marked[index - 1] = true;
                    }

                    if (index < n - 1)
                    {
                        // 右邊界標記
                        // 注意因為是取 index + 1, 所以條件式是 n - 1.
                        // 不然會超出邊界
                        marked[index + 1] = true;
                    }
                }
            }

            return Score;
        }
    }
}