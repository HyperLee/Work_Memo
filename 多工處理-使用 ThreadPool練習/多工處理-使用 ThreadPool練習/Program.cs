namespace 多工處理_使用_ThreadPool練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，多工處理（multithreading）是指同時執行多個執行緒（thread），每個執行緒都可以獨立執行不同的工作。
        /// 這種方式讓應用程式能夠更有效地利用系統資源，並在處理大量任務時保持響應性。
        /// C# 提供多種方式來進行多工處理，最常見的包括 Thread 類、ThreadPool、Task、async/await 和 Parallel。
        /// 
        /// ThreadPool 管理著一組可重用的執行緒，它會自動管理執行緒的分配和回收，適合需要大量短時間任務的情況。
        /// 詳細說明參考附件 txt 檔案
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] results = new int[2];
            using CountdownEvent completion = new CountdownEvent(results.Length);

            for (int taskId = 1; taskId <= results.Length; taskId++)
            {
                ThreadPool.QueueUserWorkItem(
                    Work,
                    new WorkState(taskId, results, completion));
            }

            completion.Wait();

            RunCase(
                "ThreadPool 等待完成",
                "6,12",
                () => string.Join(",", results),
                out int passed);

            Console.WriteLine($"Summary: {passed}/1 checks passed.");
            if (passed != 1)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 方法1
        /// </summary>
        /// <param name="state">包含工作識別碼、結果陣列與完成訊號的狀態。</param>
        private sealed class WorkState
        {
            public WorkState(int taskId, int[] results, CountdownEvent completion)
            {
                TaskId = taskId;
                Results = results;
                Completion = completion;
            }

            public int TaskId { get; }
            public int[] Results { get; }
            public CountdownEvent Completion { get; }
        }

        /// <summary>
        /// 執行單一 ThreadPool 工作，將計算結果寫入指定位置並通知等待者。
        /// </summary>
        /// <param name="state">包含工作識別碼、結果陣列與完成訊號的狀態。</param>
        static void DoWork(object? state)
        {
            if (state is not WorkState work)
            {
                throw new ArgumentException("ThreadPool state 格式不正確。", nameof(state));
            }

            try
            {
                work.Results[work.TaskId - 1] = work.TaskId * (1 + 2 + 3);
            }
            finally
            {
                work.Completion.Signal();
            }
        }


        /// <summary>
        /// 方法2
        /// </summary>
        /// <param name="state">傳遞給 ThreadPool 工作項目的狀態。</param>
        static void Work(object? state)
        {
            DoWork(state);
        }

        /// <summary>
        /// 輸出一個 ThreadPool 案例的固定驗證結果。
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