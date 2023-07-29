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
            // 寫死固定數值 
            int[,] array = new int[3, 5]
            {
                { 1, 2, 3, 4, 5},
                { 6, 7, 8, 9, 10},
                { 11, 12, 13, 14, 15}
            };

            // 亂數產生 3 * 5 array
            int[,] randomarray = GenRandomArray(3, 5);
            printarray(randomarray);
            Console.WriteLine();
            //


            int sum = 0;
            sum = cal(randomarray);

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        /// <summary>
        /// 計算陣列總和
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>

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


        /// <summary>
        /// 亂數產生陣列
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static int[,] GenRandomArray(int row, int column)
        {
            Random random = new Random();
            int[,] array = new int[row, column];

            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    // 亂數範圍大小
                    array[i, j] = random.Next(1, 200);
                }
            }

            return array;
        }

        /// <summary>
        /// 顯示 亂數產生的陣列
        /// </summary>
        /// <param name="array"></param>
        public static void printarray(int[,] array)
        {
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + ", ");
                }
                Console.WriteLine();
            }
        }

    }
}
