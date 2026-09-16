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
            int passed = 0;
            passed += RunCase("int", 4, sizeof(int));
            passed += RunCase("uint", 4, sizeof(uint));
            passed += RunCase("double", 8, sizeof(double));
            passed += RunCase("char", 2, sizeof(char));
            passed += RunCase("long", 8, sizeof(long));
            passed += RunCase("ulong", 8, sizeof(ulong));
            passed += RunCase("float", 4, sizeof(float));
            passed += RunCase("bool", 1, sizeof(bool));
            passed += RunCase("byte", 1, sizeof(byte));

            Console.WriteLine($"Summary: {passed}/9 checks passed.");
            if (passed != 9)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 比較指定數值型別的預期與實際位元組大小。
        /// </summary>
        /// <param name="typeName">要顯示的型別名稱。</param>
        /// <param name="expected">依 C# 型別契約推導的大小。</param>
        /// <param name="actual">由 <c>sizeof</c> 運算子取得的大小。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string typeName, int expected, int actual)
        {
            bool passed = expected == actual;
            Console.WriteLine($"Case: {typeName} 大小");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }
    }
}