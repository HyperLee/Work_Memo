namespace 平行處理_Parallel練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，平行處理（Parallel Processing）可以利用 Task 類、
        /// Parallel 類以及 async/await 關鍵字來實現。
        /// 這能夠加速 CPU 密集型的任務執行，因為它可以讓多個任務同時在不同的執行緒上運行。
        /// 
        /// Parallel 類提供了一種簡單的方式來平行執行循環或多個操作。它適合用於需要處理大量簡單任務的場景。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] squares = new int[6];
            Parallel.For(0, squares.Length, i => squares[i] = i * i);

            RunCase(
                "Parallel.For 完成",
                "0,1,4,9,16,25",
                () => string.Join(",", squares),
                out int passed);

            Console.WriteLine($"Summary: {passed}/1 checks passed.");
            if (passed != 1)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 輸出一個 Parallel 案例的固定驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期結果集合。</param>
        /// <param name="actualFactory">產生實際結果的函式。</param>
        /// <param name="passed">輸出通過數量。</param>
        private static void RunCase(string name, string expected, Func<string> actualFactory, out int passed)
        {
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
            passed = isPassed ? 1 : 0;
            Console.WriteLine($"[{name}]");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
        }
    }
}