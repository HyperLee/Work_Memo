namespace 多工處理_使用_Thread_類練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，多工處理（multithreading）是指同時執行多個執行緒（thread），每個執行緒都可以獨立執行不同的工作。
        /// 這種方式讓應用程式能夠更有效地利用系統資源，並在處理大量任務時保持響應性。
        /// C# 提供多種方式來進行多工處理，最常見的包括 Thread 類、ThreadPool、Task、async/await 和 Parallel。
        /// 
        /// hread 類是 C# 中最基本的多工處理方式，可以手動啟動和管理不同的執行緒。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(DoWork));
            Thread thread2 = new Thread(new ThreadStart(DoWork));

            // 啟動執行緒
            thread1.Start();
            thread2.Start();

            // 等待執行緒完成
            thread1.Join();
            thread2.Join();

            Console.WriteLine("所有執行緒完成");
            Console.ReadKey();
        }


        /// <summary>
        /// 
        /// </summary>
        static void DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"執行緒 {Thread.CurrentThread.ManagedThreadId} 執行中 - 第 {i} 次");
                // 模擬工作
                Thread.Sleep(1000);  
            }
        }
    }
}
