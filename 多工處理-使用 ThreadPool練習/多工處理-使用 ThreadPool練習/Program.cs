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
            /* // 方法1
            // 從執行緒池中排隊工作
            ThreadPool.QueueUserWorkItem(DoWork);
            ThreadPool.QueueUserWorkItem(DoWork);

            // 暫停主執行緒，等待執行緒池完成工作
            Thread.Sleep(3000);
            Console.WriteLine("所有工作完成");
            */

            // 方法2
            Console.WriteLine("工作開始");
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Work));
            }
            Console.WriteLine("所有工作完成");


            ///////////////////////////////////////
            Console.ReadKey();
        }


        /// <summary>
        /// 方法1
        /// </summary>
        /// <param name="state"></param>
        static void DoWork(object state)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"執行緒池執行緒 {Thread.CurrentThread.ManagedThreadId} - 第 {i} 次");
                Thread.Sleep(1000);
            }
        }


        /// <summary>
        /// 方法2
        /// </summary>
        /// <param name="state"></param>
        static void Work(object state)
        {
            // 在這裡執行你的任務
            Console.WriteLine("Thread is working: " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
        }
    }
}
