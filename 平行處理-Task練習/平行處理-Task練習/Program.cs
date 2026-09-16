namespace 平行處理_Task練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，平行處理（Parallel Processing）可以利用 Task 類、
        /// Parallel 類以及 async/await 關鍵字來實現。
        /// 這能夠加速 CPU 密集型的任務執行，因為它可以讓多個任務同時在不同的執行緒上運行。
        /// 
        /// Task 類是異步編程和並行處理的核心之一。使用 Task.Run 可以將工作分配給後台執行緒來執行。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Task<int>[] tasks = { Task.Run(() => DoWork(1)), Task.Run(() => DoWork(2)) };
            Task.WaitAll(tasks);

            RunCase(
                "Task.WaitAll 完成",
                "15,20",
                () => string.Join(",", tasks.Select(task => task.Result)),
                out int passed);

            Console.WriteLine($"Summary: {passed}/1 checks passed.");
            if (passed != 1)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 計算固定五次工作，回傳該 Task 的可驗證結果。
        /// </summary>
        /// <param name="taskId">工作識別碼。</param>
        /// <returns>固定五次計算的總和。</returns>
        private static int DoWork(int taskId)
        {
            int total = 0;
            for (int i = 0; i < 5; i++)
            {
                total += taskId + i;
            }

            return total;
        }

        /// <summary>
        /// 輸出一個 Task 案例的固定驗證結果。
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