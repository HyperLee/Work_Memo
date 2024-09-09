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
            // 總共執行 10 次
            // 由於是平行處理, 所以順序不是固定 0 ~ 9
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"Parallel Task - 第 {i} 次迭代");
                Task.Delay(1000).Wait();
            });

            Console.WriteLine("Parallel For 完成");
            Console.ReadKey();
        }
    }
}
