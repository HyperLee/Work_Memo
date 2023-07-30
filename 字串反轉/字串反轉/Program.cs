using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 字串反轉
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "123ABC";
            
            Console.WriteLine("方法1: " + Reverse(str));
            Console.WriteLine("方法2: " + Reverse2(str));

            Console.ReadKey();

        }


        /// <summary>
        /// 字串反轉
        /// 字符串轉換為字符數組 String.ToCharArray()
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }


        /// <summary>
        /// 不使用內建 Reverse反轉
        /// 自己寫反轉
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static string Reverse2(string str)
        {
            var sb = new StringBuilder();
            for(var i = str.Length - 1; i >= 0; i--)
            {
                sb.Append(str[i]);
            }

            return sb.ToString();
        }

    }
}
