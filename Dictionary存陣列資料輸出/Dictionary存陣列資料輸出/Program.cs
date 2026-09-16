namespace Dictionary存陣列資料輸出
{
    internal class Program
    {
        /// <summary>
        /// 49. Group Anagrams
        /// https://leetcode.com/problems/group-anagrams/
        /// 49. 字母异位词分组
        /// https://leetcode.cn/problems/group-anagrams/?envType=study-plan-v2&envId=top-interview-150
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("題目範例", "[ate,eat,tea]|[bat]|[nat,tan]", new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });
            passed += RunCase("空字串與相同字母", "[]|[ab,ba]", new string[] { "ab", "", "ba" });
            passed += RunCase("單一群組", "[abc,bca,cab]", new string[] { "abc", "bca", "cab" });

            Console.WriteLine($"Summary: {passed}/3 checks passed.");
            if (passed != 3)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 以正規化後的群組文字比較字母異位詞分組結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">排序後群組的預期文字。</param>
        /// <param name="input">本案例獨立的輸入字串。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string name, string expected, string[] input)
        {
            string actual = NormalizeGroups(GroupAnagrams([.. input]));
            bool passed = expected == actual;
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }

        /// <summary>
        /// 將群組內與群組間排序，消除 Dictionary 列舉順序對案例比對的影響。
        /// </summary>
        /// <param name="groups">字母異位詞群組。</param>
        /// <returns>以 `|` 連接的穩定群組文字。</returns>
        private static string NormalizeGroups(IList<IList<string>> groups)
        {
            return string.Join("|", groups
                .Select(group => $"[{string.Join(",", group.OrderBy(word => word, StringComparer.Ordinal))}]")
                .OrderBy(group => group, StringComparer.Ordinal));
        }


        /// <summary>
        /// 參考來源:
        /// https://leetcode.cn/problems/group-anagrams/solutions/520655/jie-by-long-yu-8-8zd0/?envType=study-plan-v2&envId=top-interview-150
        /// 
        /// 思路就是弄个字典，把每个字符串排序比较，排序的string作为key
        /// ,值为strs[i]，遍历完strs,在从dic取值
        /// 
        /// String.Join:
        /// https://dotblogs.com.tw/webber18/2020/06/12/154200
        /// https://ithelp.ithome.com.tw/articles/10105683
        /// 
        /// Key: 將輸入的strs經過排序過後的 str
        /// value: 排序前的輸入字串 strs[i]
        /// 
        /// 
        ///  字母异位词:同樣char, 不同排序組合而成的一個單字或是片段
        ///  題目要求很簡單, 將同樣的 字母异位词 進行排列
        ///  相同的放在一起即可
        ///  
        ///  所以做法就是
        ///  1.遍歷每個輸入的單字, 將單字從新排列 ( 字母异位词 具有相同的char)
        ///  2.判斷每個輸入的單字是不是相同的排列, 相同就加入, 不同就新增
        ///  3.輸出資料, 這裡要注意. 輸出資料是輸出原先輸入的單字,將相同的字母异位词放在一起輸出
        ///  
        /// 宣告部分需要注意, 是 IList<IList<string>> 輸入, 輸出
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>>();
            IList<IList<string>> res = new List<IList<string>>();

            for (int i = 0; i < strs.Length; i++)
            {
                // 遍歷每個輸入的單字
                char[] a = strs[i].ToArray();
                // 重新排序
                Array.Sort(a);
                // 暫存至 str中
                string str = new string(a);

                if (dic.ContainsKey(str))
                {
                    // 已存在就加入
                    dic[str].Add(strs[i]);
                }
                else
                {
                    // 不存在就新增
                    dic[str] = new List<string> { strs[i] };
                }

            }

            // 依序將dic.Keys裡面的value取出來, 放到res輸出
            foreach (var item in dic.Keys)
            {
                res.Add(dic[item]);
            }

            return res;

        }

    }
}