using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _99乘法表
{
    internal class Program
    {
        /// <summary>
        /// 99乘法表
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            for(int i = 1; i<= 9; i++)
            {
                for(int j = 1; j<= 9; j++)
                {
                    Console.WriteLine(i * j);
                }
            }

            Console.ReadKey();
        }
    }
}
