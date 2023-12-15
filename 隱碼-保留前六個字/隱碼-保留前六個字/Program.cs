using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 隱碼_保留前六個字
{
    internal class Program
    {
        /// <summary>
        ///  隱碼_保留前六個字
        ///  使用地址當作範例
        ///  
        /// 縣市之後以* 字取代，如：台北市中正區********
        /// 地址保留前六個字
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string address = "臺北市中正區台北火車站";

            if (address.Length > 6)
            {
                int t1 = address.Length;
                int t2 = t1 - 6;
                string maskchar = "";
                for (int i1 = 0; i1 < t2; i1++)
                {
                    maskchar = maskchar + "*";
                }

                address = address.Substring(0, 6);
                address = address + maskchar;
            }

            Console.WriteLine(address);
            Console.ReadKey();

        }
    }
}
