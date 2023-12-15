using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 身分證隱碼
{
    internal class Program
    {
        /// <summary>
        /// 身分證隱碼
        /// 身分證 只保留 前兩碼, 後三碼
        /// 中間五碼用 "*"取代
        /// 
        /// 因身分證長度固定
        /// 所以直接字串擷取處理即可
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string ID = "A123456789";

            // 身分證正常規格為10碼
            if (ID.Length >= 10)
            {
                ID = ID.Substring(0, 4) + "***" + ID.Substring(7, 3);
            }

            Console.WriteLine(ID);
            Console.ReadKey();

        }
    }
}
