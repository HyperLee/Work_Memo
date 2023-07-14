using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 常見數學除法題目用法
{
    internal class Program
    {
        /// <summary>
        /// 2520. Count the Digits That Divide a Number
        /// https://leetcode.com/problems/count-the-digits-that-divide-a-number/description/
        /// 2520. 统计能整除数字的位数
        /// https://leetcode.cn/problems/count-the-digits-that-divide-a-number/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 1248;
            Console.WriteLine(CountDigits(n));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/count-the-digits-that-divide-a-number/solution/2520-tong-ji-neng-zheng-chu-shu-zi-de-we-kso6/
        /// https://leetcode.cn/problems/count-the-digits-that-divide-a-number/solution/czhi-jie-qiu-jie-by-gao-shan-liu-shui-9j-g0av/
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int CountDigits(int num)
        {
            int temp = num;
            int count = 0;

            while (temp != 0)
            {
                // 枚舉尋找能整除
                if (num % (temp % 10) == 0)
                {
                    count++;
                }

                // 取出temp每個位數
                temp /= 10;
            }

            return count;
        }


    }
}
