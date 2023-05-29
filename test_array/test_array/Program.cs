using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            string[] arr = new string[5];

            string[] arr2 = new string[5];

            for(int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("arr:" + arr[i].ToString());
                Console.WriteLine("arr2: " + arr2[i].ToString());
            }
            */

            /*
            ArrayList al = new ArrayList();
            al.Add("abc");
            al.Add("123");


            foreach (string text in al) //列舉 ArrayList
            {
                Console.WriteLine("arr:" + text);
            }
            */

            /*
            int size = 5;
            string[] arr = new string[size]; // 放db 的 i序號 第幾筆資料
            string[] arr2 = new string[size]; // 放 使用者輸入的重量
            for (int i = 0; i < arr.Length; i++)
            {
                string aa1 = (i + 1).ToString();
                arr.SetValue(aa1, i);
            }

            for (int i = 0; i < arr2.Length; i++)
            {
                string aa1 = (i + 99).ToString();
                arr2.SetValue(aa1, i);
            }


            // 第幾筆資料 是 多少重量
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(arr[i] + " = " + arr2[i]);
            }
            */

            int[] array = new int[] { 1, 2, 3, 4 };

            int length = array.Length;

            if(length % 2 == 0)
            {
                int index = length / 2;
                var result = ((array[index - 1] + array[index]) * 1.0) / 2;
                Console.WriteLine(result );
            }
            else
            {
                int index = (length) / 2;
                int result = (array[index]);
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }
}
