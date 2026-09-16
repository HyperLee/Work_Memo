namespace 多工處理_使用_Task_類練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，多工處理（multithreading）是指同時執行多個執行緒（thread），每個執行緒都可以獨立執行不同的工作。
        /// 這種方式讓應用程式能夠更有效地利用系統資源，並在處理大量任務時保持響應性。
        /// C# 提供多種方式來進行多工處理，最常見的包括 Thread 類、ThreadPool、Task、async/await 和 Parallel。
        /// 
        /// Task 是現代 C# 中進行多工處理的推薦方式。它提供了更高層次的 API 並支持異步操作。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Task<int>[] tasks = { Task.Run(() => DoWork(1)), Task.Run(() => DoWork(2)) };
            Task.WaitAll(tasks);

            RunCase(
                "Task.WaitAll 完成",
                "6,12",
                () => string.Join(",", tasks.Select(task => task.Result)),
                out int passed);

            Console.WriteLine($"Summary: {passed}/1 checks passed.");
            if (passed != 1)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 計算固定三次工作，回傳該 Task 的可驗證結果。
        /// </summary>
        /// <param name="taskId">工作識別碼。</param>
        /// <returns>固定三次計算的總和。</returns>
        private static int DoWork(int taskId)
        {
            int total = 0;
            for (int i = 1; i <= 3; i++)
            {
                total += taskId * i;
            }

            return total;
        }

        /// <summary>
        /// 輸出一個 Task 類案例的固定驗證結果。
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