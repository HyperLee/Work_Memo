using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 今天_星期幾
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetDayName();
            Console.ReadKey();
        }

        /// <summary>
        /// 今天 星期幾
        /// </summary>
        /// <returns></returns>
        private static string GetDayName()
        {
            string result = "";

            DateTime today = DateTime.Now;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                result = "星期一";
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                result = "星期二";
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                result = "星期三";
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                result = "星期四";
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                result = "星期五";
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                result = "星期六";
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                result = "星期日";
            }

            Console.Write(result);

            return result;
        }
    }
}
