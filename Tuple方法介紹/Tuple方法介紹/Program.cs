﻿using System.Text;

namespace Tuple方法介紹
{
    internal class Program
    {
        /// <summary>
        /// 12. Integer to Roman
        /// https://leetcode.com/problems/integer-to-roman/description/
        /// 
        /// 12. 整数转罗马数字
        /// https://leetcode.cn/problems/integer-to-roman/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int input = 3749;

            Console.WriteLine("res: " + IntToRoman(input));
            Console.ReadKey();
        }


        /// <summary>
        /// c# Tuple 介紹
        /// 元組型別 (C# 參考)
        /// Tuple 是具有特定專案數目和序列的資料結構。
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/value-tuples
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.tuple?view=net-8.0
        /// https://www.huanlintalk.com/2017/04/c-7-tuple-syntax.html
        /// https://blog.darkthread.net/blog/cs-in-depth-notes-8/
        /// 
        /// 建立新的 2-Tuple 或雙重 (Pair) 物件。
        /// 
        /// 簡單說就是建立類似表格
        /// , 之後可以查表對應
        /// 
        /// T1: 數字 int
        /// T2: 羅馬數字 string
        /// 
        /// 羅馬數字規則需要注意, 尤其是 4, 9開頭轉換
        /// ex:
        /// 5 => V
        /// 4 => IV, 小的數字後面接上一個大數. 代表減法
        /// 
        /// 10 => X
        ///  9 => IX
        /// </summary>
        static Tuple<int, string>[] valueSymbols = {
            new Tuple<int, string>(1000, "M"),
            new Tuple<int, string>(900, "CM"),
            new Tuple<int, string>(500, "D"),
            new Tuple<int, string>(400, "CD"),
            new Tuple<int, string>(100, "C"),
            new Tuple<int, string>(90, "XC"),
            new Tuple<int, string>(50, "L"),
            new Tuple<int, string>(40, "XL"),
            new Tuple<int, string>(10, "X"),
            new Tuple<int, string>(9, "IX"),
            new Tuple<int, string>(5, "V"),
            new Tuple<int, string>(4, "IV"),
            new Tuple<int, string>(1, "I")
        };


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/integer-to-roman/solutions/774611/zheng-shu-zhuan-luo-ma-shu-zi-by-leetcod-75rs/
        /// https://leetcode.cn/problems/integer-to-roman/solutions/87905/tan-xin-ha-xi-biao-tu-jie-by-ml-zimingmeng/
        /// https://leetcode.cn/problems/integer-to-roman/solutions/2848775/jian-ji-xie-fa-pythonjavaccgojsrust-by-e-kmp6/
        /// https://leetcode.cn/problems/integer-to-roman/solutions/1458311/by-stormsunshine-cvsa/
        /// 
        /// 1. 先把每個數字對應的羅馬文字, 透過 tuple 建立陣列表格(建立越詳細越方便查詢)
        /// 2. 接下來把要轉換的 num, 對應到表格中查詢
        /// 3. num 查到就扣除該數字
        /// 4. 持續步驟2, 3. num 數字會由大變小
        /// 
        /// 搞懂 Tuple 這題就好解很多
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string IntToRoman(int num)
        {
            StringBuilder sb = new StringBuilder();

            // 從 valueStmbols 取出 每一組 tuple 給 num 查詢對應使用
            // 數字 會由大開始到小
            foreach (Tuple<int, string> tuple in valueSymbols)
            {
                int value = tuple.Item1;
                string symbol = tuple.Item2;

                // value 不超過 num 範圍去找出有多少個
                while (num >= value)
                {
                    // 表格查詢到, 就扣減該數字
                    num -= value;
                    // 加入 結果
                    sb.Append(symbol);
                }

                if (num == 0)
                {
                    break;
                }
            }

            return sb.ToString();
        }

    }
}
