using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    internal class Program
    {
        /// <summary>
        /// fibonacci
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 100;
            Console.WriteLine(Fibo(n));
            Console.ReadKey();
        }


        /// <summary>
        /// 迴圈從2開始
        /// result 是取前兩數相加之和
        /// if 那邊就可以得知 n = 0, 1之結果
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Fibo(int n)
        {
            if(n <= 1)
            {
                return n;
            }

            // n = 0, result = 0
            long first = 0;
            // n = 1, result = 1
            long second = 1;
            long result = 0;

            // 要注意這邊是從2開始, 
            for(int i = 2; i <= n; i++)
            {
                result = first + second;
                first = second;
                second = result;
            }

            return result;
        }

    }
}
