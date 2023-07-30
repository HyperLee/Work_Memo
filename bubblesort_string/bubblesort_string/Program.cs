using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bubblesort_string
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] array = { "A", "C", "B", "Z"};
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
        /// CompareTo (string? strB);
        /// 比較這個執行個體與指定的物件或 String，並傳回一個整數，指出這個執行個體在排
        /// 序次序中，位於指定物件或 String 之前、之後或相同位置。
        /// 
        /// 小於零	這個執行個體位於 strB 之前。 
        /// 零	這個執行個體在排序次序中的位置與 strB 相同。 
        /// 大於零	這個執行個體位於 strB 之後。 
        /// -或-
        /// strB 為 null。
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.string.compareto?view=net-7.0
        /// </summary>
        /// <param name="array"></param>

        public static void bubblesort(string[] array)
        {
            int len = array.Length;

            for (int i = 0; i < len - 1; i++)
            {
                for (int j = 0; j < len - i - 1; j++)
                {
                    // j > j + 1
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        string temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("排序後");
            foreach (var value in array)
            {
                Console.Write(value + ", ");
            }
        }

    }
}
