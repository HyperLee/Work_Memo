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
            Stack<string> numbers = new Stack<string>();
            numbers.Push("1");
            numbers.Push("2");
            numbers.Push("3");
            numbers.Push("4");
            numbers.Push("5");

            Console.WriteLine("原始輸入字串");
            foreach (string number in numbers) //依序印出
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\nPop後資料 : " + numbers.Pop());

            Console.WriteLine("\npeek後的資料 : " + numbers.Peek()); 
            foreach (string number in numbers) //依序印出
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\ncount筆數 : " + numbers.Count);

            numbers.Clear();
            Console.WriteLine("\n經過Clear資料 : " + numbers.Count);

            Console.ReadKey();
        }


    }
}
