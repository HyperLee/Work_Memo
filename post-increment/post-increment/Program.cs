using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace post_increment
{
    internal class Program
    {
        /// <summary>
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/arithmetic-operators
        /// 
        /// 後置遞增運算子
        /// x++ 的結果為運算「之前」的 x 值
        /// ==> ++的下一行 數值才會變化
        /// 
        /// 
        /// 前置遞增運算子
        /// ++x 的結果為運算「之後」的 x 值
        /// ==> ++的那一行 數值就會變化
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int i = 3;
            Console.WriteLine("i:" + i);   // output: 3
            Console.WriteLine("i++:" + i++); // output: 3
            Console.WriteLine("i:" + i);   // output: 4
            Console.WriteLine(" ");
            double a = 1.5;
            Console.WriteLine("a:" + a);   // output: 1.5
            Console.WriteLine("++a:" + ++a); // output: 2.5
            Console.WriteLine("a:" + a);   // output: 2.5


            Console.ReadKey();

        }
    }
}
