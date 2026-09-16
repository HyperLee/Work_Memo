namespace 多工處理_使用_Parallel_類練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，多工處理（multithreading）是指同時執行多個執行緒（thread），每個執行緒都可以獨立執行不同的工作。
        /// 這種方式讓應用程式能夠更有效地利用系統資源，並在處理大量任務時保持響應性。
        /// C# 提供多種方式來進行多工處理，最常見的包括 Thread 類、ThreadPool、Task、async/await 和 Parallel。
        /// 
        /// Parallel 類提供了簡單的 API 用於並行處理，例如 Parallel.For 或 Parallel.ForEach，這些方法能有效地利用多個處理器來加速處理。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] doubledValues = new int[5];
            Parallel.ForEach(Enumerable.Range(1, doubledValues.Length), value => doubledValues[value - 1] = value * 2);

            RunCase(
                "Parallel.ForEach 完成",
                "2,4,6,8,10",
                () => string.Join(",", doubledValues),
                out int passed);

            Console.WriteLine($"Summary: {passed}/1 checks passed.");
            if (passed != 1)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 輸出一個 Parallel.ForEach 案例的固定驗證結果。
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