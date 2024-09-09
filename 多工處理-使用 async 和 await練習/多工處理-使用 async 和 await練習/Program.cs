namespace 多工處理_使用_async_和_await練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，多工處理（multithreading）是指同時執行多個執行緒（thread），每個執行緒都可以獨立執行不同的工作。
        /// 這種方式讓應用程式能夠更有效地利用系統資源，並在處理大量任務時保持響應性。
        /// C# 提供多種方式來進行多工處理，最常見的包括 Thread 類、ThreadPool、Task、async/await 和 Parallel。
        /// 
        /// async 和 await 關鍵字使編寫異步代碼更容易。這些關鍵字不直接處理執行緒，而是使應用程式能夠在等待長時間操作時繼續執行其他工作。
        /// </summary>
        /// <param name="args"></param>
        static async Task Main()
        {
            // 同時啟動兩個異步任務
            await Task.WhenAll(LongRunningTask(1), LongRunningTask(2));

            Console.WriteLine("所有異步任務完成");
            Console.ReadKey();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        static async Task LongRunningTask(int taskId)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Task {taskId} 執行中 - 第 {i} 次");
                await Task.Delay(1000);  // 模擬異步延遲
            }
        }
    }
}
