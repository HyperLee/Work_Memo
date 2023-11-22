using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compareint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string a = "0";
            string b = "0";
            

            if(int.Parse(a) > int.Parse(b))
            {
                Console.WriteLine("a > b");
            }
            else
            {
                Console.WriteLine("a <= b");
            }

            Console.ReadKey();
        }
    }
}
