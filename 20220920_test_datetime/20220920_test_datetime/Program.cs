using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20220920_test_datetime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            DateTime LDs, LDe;
            LDe = DateTime.Today;
            LDs = LDe.AddDays(-1);

            Console.WriteLine("s:" + LDs);

            Console.WriteLine("e:" + LDe);
            


            // 測試 取收件者行為
            string mm = "test20230529@gmail.com,test20230528@gmail.com";

            string[] mail_to = mm.Split(',');

            for (int mails = 0; mails < mail_to.Length; mails++)
            {
                Console.WriteLine(mail_to[mails]);
            }

            Console.ReadKey();

        }
    }
}
