namespace 計算N總和
{
    internal class Program
    {
        /// <summary>
        /// 輸入N, N為一正整數. 求出SUM = 1 + 2 + ... + N
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            Console.Write("請輸入一個正整數N1: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int N1))
            {
                Console.WriteLine();
                Console.WriteLine("解法一, 迴圈");
                // 這段程式碼會提示用戶輸入一個正整數N，然後使用一個for循環計算從1到N的總和，最後輸出結果。
                int sum1 = 0;
                for (int i = 1; i <= N1; i++)
                {
                    sum1 += i;
                }

                Console.WriteLine("從1到" + N1 + "的總和是: " + sum1);

                Console.WriteLine();
                Console.WriteLine("解法二, 套數學公式");

                int sum2 = N1 * (N1 + 1) / 2;

                Console.WriteLine("從1到" + N1 + "的總和是: " + sum2);

                Console.WriteLine();
                Console.WriteLine("解法三, 遞迴");

                int sum3 = SumRecursive(N1);

                Console.WriteLine("從1到" + N1 + "的總和是: " + sum3);

                Console.WriteLine();
                Console.WriteLine("解法四, LINQ（Language Integrated Query）");
                // LINQ方法： LINQ（Language Integrated Query）是一種用於查詢數據的語言擴展。

                int sum4 = Enumerable.Range(1, N1).Sum();

                Console.WriteLine("從1到" + N1 + "的總和是: " + sum4);
            }
            else
            {
                Console.WriteLine("輸入的不是一個有效的正整數。");
            }
        }

        /// <summary>
        /// 遞迴
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SumRecursive(int n)
        {
            if (n == 1)
                return 1;
            else
                return n + SumRecursive(n - 1);
        }
    }
}
