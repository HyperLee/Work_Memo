using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 西元年轉民國年
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "2020/1/1";
            Console.WriteLine(ADToROC(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 輸入 西元年
        /// 輸入範例:
        /// 2000-01-01
        /// 2020/01/01
        /// 2020/1/1
        /// 
        /// 輸出格式:  年-月-日
        /// 可自行修改
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ADToROC(string date)
        {
            string ADYear = "";
            string temps1 = "-";
            string temps2 = "\"";
            string temps3 = "/";

            try
            {
                string d1 = date;
                string[] ary = d1.Split('-');

                if (date.IndexOf(temps1) > -1)
                {
                    ary = d1.Split('-');
                }

                if (date.IndexOf(temps2) > -1)
                {
                    ary = d1.Split('\'');
                }

                if (date.IndexOf(temps3) > -1)
                {
                    ary = d1.Split('/');
                }

                string _year = "";
                string _MM = "";
                string _dd = "";
                for (int i = 0; i < ary.Length; i++)
                {
                    if (i == 0)
                    {
                        if (ary[0].ToString() != "")
                        {
                            _year = (int.Parse(ary[0].ToString()) - 1911).ToString();
                        }

                    }
                    if (i == 1)
                    {
                        if (ary[1].ToString() != "")
                        {
                            _MM = ary[1].ToString();

                            if (_MM.Length == 1)
                            {
                                _MM = _MM.Insert(0, "0");
                            }
                        }

                    }
                    if (i == 2)
                    {
                        if (ary[2].ToString() != "")
                        {
                            _dd = ary[2].ToString();

                            if (_dd.Length == 1)
                            {
                                _dd = _dd.Insert(0, "0");
                            }
                        }

                    }
                }

                // 輸出格式:  年-月-日
                ADYear = _year + "-" + _MM + "-" + _dd;

            }
            catch (Exception ee)
            {
               Console.WriteLine(ee.Message);
            }

            return ADYear;
        }
    }
}
