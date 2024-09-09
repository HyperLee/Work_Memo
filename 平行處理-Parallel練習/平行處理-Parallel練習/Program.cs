namespace 平行處理_Parallel練習
{
    internal class Program
    {
        /// <summary>
        /// 在 C# 中，平行處理（Parallel Processing）可以利用 Task 類、
        /// Parallel 類以及 async/await 關鍵字來實現。
        /// 這能夠加速 CPU 密集型的任務執行，因為它可以讓多個任務同時在不同的執行緒上運行。
        /// 
        /// Parallel 類提供了一種簡單的方式來平行執行循環或多個操作。它適合用於需要處理大量簡單任務的場景。
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
