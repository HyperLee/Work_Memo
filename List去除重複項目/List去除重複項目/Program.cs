using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List去除重複項目
{
    internal class Program
    {
        /// <summary>
        /// 工作上 專案  新增公告 碰到之 問題
        /// 解法 紀錄
        /// 
        /// input1: 新增名單
        /// input2: 刪除名單
        /// => 新增 要扣除 與 刪除 重複 之項目
        ///    以及 只有 刪除 才出現之項目 也要扣除
        ///    
        /// 最終得到的 才是所需要的
        /// 
        /// 參考: 
        /// https://www.itread01.com/article/1481080283.html
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<string> Ashuzu = new List<string>();
            Ashuzu.Add("1");
            Ashuzu.Add("2");
            Ashuzu.Add("3");
            Ashuzu.Add("5");
            List<string> Bshuzu = new List<string>();
            Bshuzu.Add("1");
            Bshuzu.Add("2");
            Bshuzu.Add("4");
            List<string> Cshuzu = new List<string>();
            foreach (string aitem in Ashuzu)
            {
                if (!Bshuzu.Contains(aitem))
                {
                    Cshuzu.Add(aitem);
                }
            }

            foreach( var value in Cshuzu)
            {
                Console.WriteLine(value);
            }

            Console.ReadKey();

        }
    }
}
