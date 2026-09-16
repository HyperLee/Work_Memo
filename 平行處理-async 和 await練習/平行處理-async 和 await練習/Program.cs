namespace 平行處理_async_和_await練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，平行處理（Parallel Processing）可以利用 Task 類、
        /// Parallel 類以及 async/await 關鍵字來實現。
        /// 這能夠加速 CPU 密集型的任務執行，因為它可以讓多個任務同時在不同的執行緒上運行。
        /// 
        /// async 和 await 關鍵字主要用於處理異步操作，例如 I/O 綁定的任務。
        /// 雖然它們不能直接用於 CPU 密集型任務的平行處理，但可以讓應用程式在等待異步任務的同時保持響應。
        /// 
        /// 宣告要加上 async
        /// </summary>
        /// <returns>所有非同步工作完成後的 smoke-test 結果。</returns>
        static async Task Main()
        {
            Task<int>[] tasks = { LongRunningTask(1), LongRunningTask(2) };
            int[] completedIterations = await Task.WhenAll(tasks);

            RunCase(
                "Task.WhenAll 完成",
                "task1=3;task2=3",
                () => $"task1={completedIterations[0]};task2={completedIterations[1]}",
                out int passed);

            Console.WriteLine($"Summary: {passed}/1 checks passed.");
            if (passed != 1)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 宣告要加上 async
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>固定三次迭代完成後的迭代數量。</returns>
        static async Task<int> LongRunningTask(int taskId)
        {
            int iterationCount = 0;
            for (int i = 0; i < 3; i++)
            {
                await Task.Yield();
                iterationCount++;
            }

            return iterationCount;
        }

        /// <summary>
        /// 輸出一個非同步案例的固定驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期結果。</param>
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