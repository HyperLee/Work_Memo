using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check_palindrome
{
    class Program
    {
        /// <summary>
        /// 回文判斷
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int s = 123321;

            Console.WriteLine(IsPalindrome(s));
            Console.ReadKey();

        }

        public static bool IsPalindrome(int x)
        {
            string s_input = x.ToString();
            string res = "";

            for (int i = s_input.Length - 1; i >= 0; i--)
            {
                res += s_input[i];
            }

            if (res == s_input)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
