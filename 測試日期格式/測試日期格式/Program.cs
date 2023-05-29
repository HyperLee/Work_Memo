using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 測試日期格式
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string strDate = "2020/06/01";

            //DateTime t2 = DateTime.Parse(strDate);
            //t2 = DateTime.Today.AddMonths(+1);
            DateTime dtDate;
            string  tt;

            if (DateTime.TryParse(strDate, out dtDate))
            {
                Console.WriteLine(dtDate);
                //tt = dtDate;
            }
            else
            {
                //throw new Exception("不是正确的日期格式类型！");
                Console.WriteLine("不是正确的日期格式类型！");
            }


            tt = DateTime.Parse(DateTime.Now.ToString(strDate)).AddMonths(1).ToShortDateString();
            */

            //string aa = "\"0610800\",\"0610799\",\"94H0248\"";

            //string[] arr = aa.Split(',');



            ////string[] arr = { aa };
            ////string[] arr = { "0610800", "0610799", "94H0248" };
            //for (int i = 0; i < arr.Length - 1; i++)
            //{
            //    arr[i] = arr[i].Replace("\\","");
            //    arr[i] = arr[i].Replace("\"", "");
            //    Console.Write(arr[i]);
            //}



            //////////// 測試字串正規化  保留 "-" 符號
            //string aa = "123-lg";
            // aa = Regex.Replace(aa, @"[^a-zA-Z0-9-]", "");

            //Console.WriteLine(aa);
            //////////////////////////////////////////////////////////////
            /*
            DateTime LastSalaryDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5);
            DateTime NextSalaryDay = new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 5);
            System.Console.WriteLine("上次發薪日: {0} ", LastSalaryDay.ToString("yyyy/MM/dd"));
            TimeSpan ts1 = DateTime.Now - LastSalaryDay;
            Console.WriteLine("距離上次發薪日已過了{0}天", ts1.Days);
            System.Console.WriteLine("下次發薪日: {0} ", NextSalaryDay.ToString("yyyy/MM/dd"));
            //用大的日期 減小的日期
            // TimeSpan ts2 =NextSalaryDay - DateTime.Now;
            TimeSpan ts2 = DateTime.Now - NextSalaryDay;   //小的日期減大的日期         
            Console.WriteLine("距離下次發薪日還有{0}天", Math.Abs(ts2.Days)); //距離幾天一定是正的 用Math.Abs取絕對值
            System.Console.ReadLine();

            */

            //DateTime d1 = DateTime.Now;

            //d1.Year.ToString();//2005


            /*
            DateTime dt = DateTime.Now;
            DateTime startYear = new DateTime(dt.Year, 1, 1);
            //dt.ToString("yyyy");
            string a1 = dt.ToString("yyyy");
            DateTime.Today.AddMonths(-12);
            //a1 = a1 + "-01-01 00:00:00";

            string aa = "";
            aa = DateTime.Today.AddMonths(-12).ToString("yyyyMMdd") + "000000";
            Console.WriteLine(aa);

            */



            /*
            string updateDate = "2021-1-4";


            string temp_name =  "2010-01-04";
            string temps1 = "-";
            string dt_birthDate = "";
       
            try
            {
                if (temp_name.Length >= 8)
                {
                    //dt_birthDate.Text = dt_member.Rows[0]["BirthDate"].ToString();

                    if (temp_name.Length == 8)
                    {
                        if (temp_name.IndexOf(temps1) > -1)
                        {
                            // 包含指定的字串，執行相應的程式碼
                            // 長度8 日期有 "-" 隔開新增的
                            // 2020-01-4
                            string insert1 = temp_name.Insert(5, "0");
                            string insert2 = insert1.Insert(8, "0");
                            dt_birthDate = insert2;
                            //dt_birthDate = temp_name;
                        }
                        else
                        {
                            // 長度8 但是日期中間沒有 "-" 隔開格式的資料, 以前disk版日期
                            string insert1 = temp_name.Insert(4, "-");
                            string insert2 = insert1.Insert(7, "-");
                            dt_birthDate = insert2;
                        }
                    }
                    else
                    {
                        dt_birthDate = temp_name;
                    }
                }
                else
                {
                    string insert1 = temp_name.Insert(4, "-");
                    string insert2 = insert1.Insert(7, "-");
                    dt_birthDate = insert2;
                }
            }
            catch
            {
                dt_birthDate = "1900-01-01";
            }




            string formatDateTime = DateTime.ParseExact(dt_birthDate, "yyyy-MM-dd",
                null, System.Globalization.DateTimeStyles.AllowWhiteSpaces).ToString("yyyyMMdd");
            Console.WriteLine(formatDateTime);
            System.Console.ReadLine();
            */

            /*
          string  mm1 = (DateTime.Now.Month - 1).ToString("D2");
            Console.WriteLine(mm1);
            System.Console.ReadLine();
            */

            string updateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string d2 = DateTime.Now.ToString("yyyyMMddHHmmss");
            Console.WriteLine(updateDate + ", " + d2);
            //System.Console.ReadLine();
            Console.ReadKey();

        }
    }
}
