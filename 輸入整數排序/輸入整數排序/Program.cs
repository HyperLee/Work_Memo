using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 輸入整數排序
{
    internal class Program
    {
        /// <summary>
        /// 輸入15個整數 排序 輸出
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入15個數字");

            int[] num = new int[15];

            for(int i = 0; i < 15; i++)
            {
                num[i] = int.Parse(Console.ReadLine());
            }

            Array.Sort(num);

            foreach(int i in num) 
            {
                Console.Write(i + ", ");
            }

            Console.ReadKey();
        }
    }
}
