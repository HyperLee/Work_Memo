using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 資料隱碼中間三位
{
    internal class Program
    {
        /// <summary>
        /// 資料隱碼中間三位
        /// 可以給個資使用
        /// 
        /// 範例為輸入電話號碼
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "0987654321";

            if (input.Length > 3)
            {
                string left = "";
                left = (((input.Length) / 2) - 1).ToString();
                string right = "";
                right = (((input.Length - 3 - int.Parse(left)))).ToString();

                if (input.Length == 11)
                {
                    left = "5";
                    right = "3";
                }

                // 長度超過14 當作是 使用者輸入兩組電話號碼
                if (input.Length > 13)
                {
                    left = (((input.Length) / 3) - 1).ToString();
                    right = (((input.Length) / 4) - 1).ToString();
                }

                try
                {
                    if (input.Length > 3)
                    {
                        input = HideSensitiveInfo(input, int.Parse(left), int.Parse(right));
                        Console.WriteLine(input);
                    }
                }
                catch (Exception ee)
                {

                    Console.WriteLine(ee.ToString());
                }
            }
            Console.ReadKey();
        }


        /// <summary>
        /// 隱藏敏感信息
        /// https://www.itread01.com/articles/1476857155.html
        /// </summary>
        /// <param name="info">信息實體</param>
        /// <param name="left">左邊保留的字符數</param>
        /// <param name="right">右邊保留的字符數</param>
        /// <param name="basedOnLeft">當長度異常時，是否顯示左邊 
        /// <code>true</code>顯示左邊，<code>false</code>顯示右邊
        /// </param>
        /// <returns></returns>
        public static string HideSensitiveInfo(string info, int left, int right, bool basedOnLeft = true)
        {
            if (String.IsNullOrEmpty(info))
            {
                return "";
            }
            StringBuilder sbText = new StringBuilder();
            int hiddenCharCount = info.Length - left - right;
            if (hiddenCharCount > 0)
            {
                string prefix = info.Substring(0, left), suffix = info.Substring(info.Length - right);
                sbText.Append(prefix);
                for (int i = 0; i < hiddenCharCount; i++)
                {
                    sbText.Append("*");
                }
                sbText.Append(suffix);
            }
            else
            {
                if (basedOnLeft)
                {
                    if (info.Length > left && left > 0)
                    {
                        sbText.Append(info.Substring(0, left) + "****");
                    }
                    else
                    {
                        sbText.Append(info.Substring(0, 1) + "****");
                    }
                }
                else
                {
                    if (info.Length > right && right > 0)
                    {
                        sbText.Append("****" + info.Substring(info.Length - right));
                    }
                    else
                    {
                        sbText.Append("****" + info.Substring(info.Length - 1));
                    }
                }
            }
            return sbText.ToString();
        }

    }
}
