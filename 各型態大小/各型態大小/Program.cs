using System.Runtime.InteropServices;

namespace 各型態大小
{
    internal class Program
    {
        /// <summary>
        /// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
        /// https://www.w3schools.com/cs/cs_data_types.php
        /// 
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/sizeof
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Size of int: {0}", sizeof(int));
            Console.WriteLine("Size of uint: {0}", sizeof(uint));
            Console.WriteLine("Size of double: {0}", sizeof(double));
            Console.WriteLine("Size of char: {0}", sizeof(char));
            Console.WriteLine("Size of long: {0}", sizeof(long));
            Console.WriteLine("Size of ulong: {0}", sizeof(ulong));
            Console.WriteLine("Size of float: {0}", sizeof(float));
            Console.WriteLine("Size of bool: {0}", sizeof(bool));
            Console.WriteLine("Size of byte: {0}", sizeof(byte));

            Console.ReadKey();
        }
    }
}
