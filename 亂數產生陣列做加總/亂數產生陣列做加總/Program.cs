using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 亂數產生陣列做加總
{
    internal class Program
    {
        /// <summary>
        /// 一維陣列
        /// 亂數產生陣列數值
        /// 顯示亂數產生之陣列
        /// 最後做加總
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] randomarray = GenRandomArray(5);
            printarray(randomarray);
            Console.WriteLine();

            Console.WriteLine("亂數總和: " + cal(randomarray));

            Console.ReadKey();
        }


        /// <summary>
        /// 亂數產生陣列
        /// 
        /// </summary>
        /// <param name="size">陣列大小</param>
        /// <returns></returns>
        public static int[] GenRandomArray(int size)
        {
            Random r = new Random();
            int[] array = new int[size];

            for(int i = 0; i < array.Length; i++)
            {
                array[i] = r.Next(1, 200);
            }

            return array;
        }


        /// <summary>
        /// 顯示 產生之陣列
        /// </summary>
        /// <param name="array"></param>
        public static void printarray(int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.WriteLine();

        }


        /// <summary>
        /// 計算亂數陣列總和
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int cal(int[] array)
        {
            int sum = 0;
            for(int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }


    }
}
