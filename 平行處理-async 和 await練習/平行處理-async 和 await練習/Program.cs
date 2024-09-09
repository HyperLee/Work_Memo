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
        /// <returns></returns>
        static async Task Main()
        {
            await Task.WhenAll(LongRunningTask(1), LongRunningTask(2));

            Console.WriteLine("所有異步任務完成");
            Console.ReadKey();
        }


        /// <summary>
        /// 宣告要加上 async
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        static async Task LongRunningTask(int taskId)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Task {taskId} - 第 {i} 次迭代");
                await Task.Delay(1000);  // 模擬異步延遲
            }
        }
    }
}
