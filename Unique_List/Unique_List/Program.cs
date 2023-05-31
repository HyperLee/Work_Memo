using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unique_List
{
    class Program
    {
        /// <summary>
        /// list 去除重複值 Distinct
        /// unique list
        /// https://docs.microsoft.com/zh-tw/dotnet/api/system.linq.enumerable.distinct?view=net-6.0
        /// http://loveandcocoa.blogspot.com/2012/06/c-list.html
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<int> l1 = new List<int>();

            l1.Add(1);
            l1.Add(1);
            l1.Add(3);
            l1.Add(9);
            l1.Add(2);


            foreach(int value in l1)
            {
                Console.WriteLine("Before: " + value);
            }

            /* // 改成 function
            IEnumerable<int> distinctAges = l1.Distinct();

            Console.WriteLine("Distinct value:");

            foreach (int value2 in distinctAges)
            {
                Console.WriteLine("After: " + value2);
            }
            */

            s1(l1);


            Console.ReadKey();
        }

        public static void s1(List<int> l1)
        {
            IEnumerable<int> distinctAges = l1.Distinct();

            Console.WriteLine("Distinct value:");

            foreach (int value2 in distinctAges)
            {
                Console.WriteLine("After: " + value2);
            }
            //string s = "";
            //return s;
        }


    }
}
