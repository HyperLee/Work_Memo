namespace 計數排序
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("題目範例", new int[] { 1, 2, 2, 3, 3, 4, 8 }, new int[] { 4, 2, 2, 8, 3, 3, 1 });
            passed += RunCase("單一值", new int[] { 5, 5, 5 }, new int[] { 5, 5, 5 });
            passed += RunCase("已排序資料", new int[] { 0, 1, 2, 3 }, new int[] { 0, 1, 2, 3 });

            Console.WriteLine($"Summary: {passed}/3 checks passed.");
            if (passed != 3)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行計數排序並比較就地排序後的陣列。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期排序結果。</param>
        /// <param name="input">只含非負整數的本案例輸入。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string name, int[] expected, int[] input)
        {
            int[] actual = [.. input];
            CountingSortAlgorithm(actual);
            bool passed = expected.SequenceEqual(actual);
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: [{string.Join(", ", expected)}]");
            Console.WriteLine($"Actual: [{string.Join(", ", actual)}]");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }


        /// <summary>
        /// 計數排序
        /// </summary>
        /// <param name="arr"></param>
        public static void CountingSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            int[] output = new int[n];

            // Find the maximum element of the array
            int max = arr[0];
            for (int i = 1; i < n; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }

            // Create a count array to store count of individual elements
            int[] count = new int[max + 1];

            // Initialize count array with all zeros
            for (int i = 0; i <= max; ++i)
            {
                count[i] = 0;
            }

            // Store count of each character
            for (int i = 0; i < n; ++i)
            {
                ++count[arr[i]];
            }

            // Change count[i] so that count[i] now contains actual position of this element in output array
            // 修改計數數組
            for (int i = 1; i <= max; ++i)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            // 構建排序後的數組
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[arr[i]] - 1] = arr[i];
                --count[arr[i]];
            }

            // Copy the output array to arr, so that arr now contains sorted characters
            // 複製回原始數組
            for (int i = 0; i < n; ++i)
            {
                arr[i] = output[i];
            }
        }


        /// <summary>
        /// print
        /// </summary>
        /// <param name="arr"></param>
        public static void PrintArray(int[] arr)
        {
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
        }
    }
}