namespace 計算總和
{
    internal class Program
    {
        /// <summary>
        /// 1 + 2 + 3 + ... + N 計算總和
        /// 
        /// 比較推薦使用公式來實作
        /// 效率比較好
        /// 
        /// for 迴圈只是練習迴圈 好解釋說明而已
        /// N 無限大時候, 效率很差
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 10000;

            Console.WriteLine("方法1: " + CalculateSum(n));
            Console.WriteLine("方法2: " + CalculateSum2(n));
            Console.WriteLine("方法3: " + CalculateSum3(n));

            Console.ReadKey();
        }


        /// <summary>
        /// 最常見的 FOR 迴圈 
        /// 從 1 迴圈到 n，逐一相加。
        /// 但是當 N 無限大時候效率差
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        static int CalculateSum(int N)
        {
            int sum = 0;
            for(int i = 1; i <= N; i++)
            {
                sum += i;
            }

            return sum;
        }


        /// <summary>
        /// 數學公式計算 
        /// 計算效率最高。
        /// n * (n + 1) / 2
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        static int CalculateSum2(int N)
        {
            return N * (N + 1) / 2;
        }


        /// <summary>
        /// LINQ
        /// 
        /// 使用 LINQ 的 Enumerable.Range 生成一個從 1 到 n 的數列，然後用 Sum() 方法求和。
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        static int CalculateSum3(int N)
        {
            int sum = Enumerable.Range(1, N).Sum();

            return sum;
        }
    }
}
