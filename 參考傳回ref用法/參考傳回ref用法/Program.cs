namespace 參考傳回ref用法
{
    internal class Program
    {
        /// <summary>
        /// 方法1:
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/statements/jump-statements#code-try-6
        /// 參考傳回
        /// 請使用含 ref 關鍵字的 return 陳述式，如下列範例所示：
        /// 
        /// 
        /// 參考傳回值 (或 ref 傳回值) 是方法以傳參考方式傳回給呼叫者的值。 也就是說，呼叫端可以藉由方法修改所傳回的值，而且該變更會反映在呼叫方法的物件狀態中。
        /// 將30 變成 0 回傳值
        /// 
        /// 方法2:
        /// https://csharpkh.blogspot.com/2017/09/c-ref-out.html
        /// https://ad57475747.medium.com/c-%E9%9B%9C%E8%A8%98-%E5%8F%83%E6%95%B8%E4%BF%AE%E9%A3%BE%E8%A9%9E-in-out-ref-5e4d83816c3a
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /* // 方法1: 官方範例
            int[] xs = new int[] { 10, 20, 30, 40 };
            ref int found = ref FindFirst(xs, s => s == 30);
            // 30用0取代
            found = 0;

            Console.WriteLine(string.Join(" ", xs));  // output: 10 20 0 40
            */

            // 方法2:
            int normal = 20;
            int ref_int = 10;
            int out_int;
            Method_normal(normal);
            Method_ref(ref ref_int);
            Method_out(out out_int);

            Console.WriteLine($"normal的值為 {normal}");
            Console.WriteLine("Method_normal_return的值為 " + Method_normal_return(normal));
            Console.WriteLine($"ref_int的值為 {ref_int}");
            Console.WriteLine($"out_int的值為 {out_int}");
            Console.WriteLine($"Press any key for continuing...{Environment.NewLine}");

            Console.ReadKey();
        }


        /// <summary>
        /// ref 關鍵字會導致引數由參考加以傳遞，而非透過值
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        static ref int FindFirst(int[] numbers, Func<int, bool> predicate)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (predicate(numbers[i]))
                {
                    return ref numbers[i];
                }
            }
            throw new InvalidOperationException("No element satisfies the given condition.");
        }


        /// <summary>
        /// 正常function
        /// 帶入 normal數值
        /// function 不會修改異動
        /// 
        /// 如果是有 return 的 就可以更改了
        /// </summary>
        /// <param name="normal"></param>
        private static void Method_normal(int normal)
        {
            normal = 999;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="notmal"></param>
        /// <returns></returns>
        private static string Method_normal_return(int normal)
        {
            normal = 1234;
            return normal.ToString();
        }


        /// <summary>
        /// 呼叫時候不用宣告
        /// 但是這邊裡面 要給值
        /// 
        /// 調用方法之前，可以不用初始化傳入的參數。方法內必須對參數進行賦值。
        /// </summary>
        /// <param name="out_int"></param>
        private static void Method_out(out int out_int)
        {
            out_int = 888;
        }


        /// <summary>
        /// 宣告時候要給預設數值,
        /// 然後方法裡面會可以再變更
        /// 
        /// 
        /// 調用方法之前，必須先初始化傳入的參數。
        /// 方法內可以讀取和修改參數的值。
        /// </summary>
        /// <param name="ref_int"></param>
        private static void Method_ref(ref int ref_int)
        {
            ref_int = 777;
        }

    }
}
