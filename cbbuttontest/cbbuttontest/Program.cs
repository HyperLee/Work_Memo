using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cbbuttontest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cb = "1,2,";

            cb = cb.TrimEnd(',').Trim();
            string[] ary = cb.Split(',');
            for (int i = 0; i < ary.Length; i++)
            {
                //Console.WriteLine(ary[i]);

                /*
                if (i == 0)
                {
                    Console.WriteLine("i = 0:" + ary[i]);
                }
                if (i == 1)
                {
                    Console.WriteLine("i = 1:" + ary[i]);
                }
                if (i == 2)
                {
                    Console.WriteLine("i = 2:" + ary[i]);
                }
                */

                if (ary[i].ToString().Trim() != "")
                {
                    Console.WriteLine(ary[i]);
                }
            }


            string aa = "\"123\"";
            Console.WriteLine(aa.Replace("\"", ""));

            Console.ReadKey();

        }
    }
}
