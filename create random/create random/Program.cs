using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace create_random
{
    class Program
    {
        /// <summary>
        /// 要注意 
        /// Random rnd = new Random();
        /// 宣告 要寫在
        /// for 迴圈外面
        /// 不然同一梯次執行
        /// 產生的資料 都會相同
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //int month = rnd.Next(1, 4);  // 1 <= month < 13
            //int dice = rnd.Next(1, 7);    // 1 <= dice < 7
            //int card = rnd.Next(52);      // 0 <= card < 52

            //Console.WriteLine(month);

            for(int i = 0; i <20; i++)
            {
                int month = rnd.Next(1, 5);
                //Console.WriteLine(month);

                Console.WriteLine("第" + i + "次 亂數結果為: " + month);
            }


            Console.ReadKey();
        }
    }
}
