using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3乘5陣列取總和
{
    internal class Program
    {
        /// <summary>
        /// 3乘5 array
        /// 取總和
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[,] array = new int[3, 5]
            {
                { 1, 2, 3, 4, 5},
                { 6, 7, 8, 9, 10},
                { 11, 12, 13, 14, 15}
            };

            int sum = 0;
            sum = cal(array);

            Console.WriteLine(sum);
            Console.ReadKey();
        }


        public static int cal(int[,] array)
        {
            int sum = 0;
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    sum += array[i, j];
                }
            }

            return sum;
        }

    }
}
