using System.Text;

namespace 字串反轉
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "123ABC";
            Console.WriteLine("原始字串: " + str);
            Console.WriteLine("方法1: " + Reverse(str));
            Console.WriteLine("方法2: " + Reverse2(str));
            Console.WriteLine("方法3: " + Reverse3(str));

        }


        /// <summary>
        /// 字串反轉
        /// 字符串轉換為字符數組 String.ToCharArray()
        /// 
        /// ReverseString方法將字串轉換為字符陣列，
        /// 然後使用Array.Reverse方法反轉字符陣列，最後將其轉換回字串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }


        /// <summary>
        /// 不使用內建 Reverse反轉
        /// 自己寫反轉
        /// 
        /// 使用StringBuilder： StringBuilder在處理大量字串操作時更高效。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static string Reverse2(string str)
        {
            var sb = new StringBuilder();
            for (var i = str.Length - 1; i >= 0; i--)
            {
                sb.Append(str[i]);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 使用迴圈反轉
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse3(string str)
        {
            string reversed = "";
            for (int i = str.Length - 1; i >= 0; i--)
            {
                reversed += str[i];
            }
            return reversed;
        }
    }
}
