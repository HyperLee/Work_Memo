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
            // 使用 Task 執行多個工作
            Task task1 = Task.Run(() => DoWork(1));
            Task task2 = Task.Run(() => DoWork(2));

            // 等待任務完成
            Task.WaitAll(task1, task2);

            Console.WriteLine("所有任務完成");
            Console.ReadKey();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        static void DoWork(int taskId)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Task {taskId} 執行中 - 第 {i} 次");
                Task.Delay(1000).Wait();  // 模擬工作
            }
        }
    }
}
