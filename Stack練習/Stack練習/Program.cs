namespace Stack練習
{
    internal class Program
    {
        /// <summary>
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.stack.peek?view=net-8.0
        /// Push(T item)：將元素 item 推入堆疊的頂部。
        /// Pop()：移除堆疊頂部的元素並返回該元素。
        /// Peek()：返回堆疊頂部的元素而不移除它。
        /// Count：返回堆疊中元素的個數。
        /// Clear()：清除堆疊中的所有元素。
        ///
        /// 
        /// 輸入順序
        /// 經過 push之後
        /// 會是相反的
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("LIFO 操作", "top=5; pop=5; peek=4; count=4; clear=0");
            passed += RunCase("空堆疊重新建立", "top=9; pop=9; peek=7; count=1; clear=0");

            Console.WriteLine($"Summary: {passed}/2 checks passed.");
            if (passed != 2)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 建立獨立堆疊並驗證 Push、Pop、Peek、Count 與 Clear 的狀態轉換。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期狀態文字。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string name, string expected)
        {
            Stack<string> numbers = new Stack<string>();
            if (name == "LIFO 操作")
            {
                foreach (string number in new[] { "1", "2", "3", "4", "5" })
                {
                    numbers.Push(number);
                }
            }
            else
            {
                numbers.Push("7");
                numbers.Push("9");
            }

            string top = numbers.Peek();
            string pop = numbers.Pop();
            string peek = numbers.Peek();
            int count = numbers.Count;
            numbers.Clear();
            string actual = $"top={top}; pop={pop}; peek={peek}; count={count}; clear={numbers.Count}";
            bool passed = expected == actual;
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }


    }
}