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
            int n = 10;
            Console.WriteLine(Fibo(n));
            Console.ReadKey();
        }

        public static int Fibo(int n)
        {
            if(n <= 1)
            {
                return n;
            }

            int first = 0;
            int second = 1;
            int result = 0;

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
