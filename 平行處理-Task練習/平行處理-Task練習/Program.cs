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
            // 建立並執行一個平行任務
            Task task1 = Task.Run(() => {
                // 模擬長時間執行的任務
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Task 1 - 第 {i} 次迭代");
                    // 模擬延遲
                    Task.Delay(1000).Wait();  
                }
            });

            Task task2 = Task.Run(() => {
                // 另一個平行任務
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Task 2 - 第 {i} 次迭代");
                    // 模擬延遲
                    Task.Delay(1000).Wait();
                }
            });

            // 等待所有任務完成
            Task.WaitAll(task1, task2);

            Console.WriteLine("所有任務完成");

            Console.ReadKey();
        }
    }
}
