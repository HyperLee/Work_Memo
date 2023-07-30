using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bebblesort
{
    internal class Program
    {
        /// <summary>
        /// 排序 練習
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array = { 73, 57, 49, 99, 133, 20, 1 };
            Console.WriteLine("原始資料");
            foreach(var value in array)
            {
                Console.Write(value + ", ");
            }

            Console.WriteLine(" ");
            bubblesort(array);

            Console.ReadKey();
        }

        /// <summary>
        /// 氣泡排序
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static void bubblesort(int[] array)
        {
            int len = array.Length;

            for(int i = 0; i < len - 1; i++)
            {
                for(int j = 0; j < len - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("排序後");
            foreach (int value in array)
            {
                Console.Write(value + ", ");
            }

        }
    }
}
