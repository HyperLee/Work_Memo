namespace DictionaryEquals用法
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            // 測試案例 1: pattern = "abba", s = "dog cat cat dog" → true
            // a->dog, b->cat, b->cat, a->dog，對應關係一致
            Console.WriteLine($"測試 1: {program.WordPattern("abba", "dog cat cat dog")}"); // true

            // 測試案例 2: pattern = "abba", s = "dog cat cat fish" → false
            // a->dog, b->cat, b->cat, a->fish，a 先對應 dog 後又對應 fish，不一致
            Console.WriteLine($"測試 2: {program.WordPattern("abba", "dog cat cat fish")}"); // false

            // 測試案例 3: pattern = "aaaa", s = "dog cat cat dog" → false
            // a->dog, a->cat，a 先對應 dog 後又對應 cat，不一致
            Console.WriteLine($"測試 3: {program.WordPattern("aaaa", "dog cat cat dog")}"); // false

            // 測試案例 4: pattern = "abba", s = "dog dog dog dog" → false
            // a->dog, b->dog，不同 pattern 字元對應到相同 word，不符合雙射
            Console.WriteLine($"測試 4: {program.WordPattern("abba", "dog dog dog dog")}"); // false

            // 測試案例 5: pattern 與 s 長度不同 → false
            Console.WriteLine($"測試 5: {program.WordPattern("abc", "dog cat")}"); // false
        }

        /// <summary>
        /// LeetCode 290. Word Pattern
        /// <para>
        /// 題目：給定一個 pattern 字串與一個以空白分隔的字串 s，
        /// 判斷 s 中的單字是否與 pattern 中的字元構成「雙射 (bijection)」對應關係。
        /// </para>
        /// <para>
        /// 解題思路：使用兩個 Dictionary 建立雙向對應，
        /// dic1 (char → string) 確保同一個 pattern 字元只對應到同一個 word，
        /// dic2 (string → char) 確保同一個 word 只對應到同一個 pattern 字元。
        /// 若任一方向出現衝突，即回傳 false。
        /// </para>
        /// <example>
        /// <code>
        /// WordPattern("abba", "dog cat cat dog"); // true
        /// WordPattern("abba", "dog cat cat fish"); // false
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="pattern">由小寫英文字母組成的 pattern 字串。</param>
        /// <param name="s">以空白分隔的單字字串。</param>
        /// <returns>若 pattern 與 s 的單字存在雙射對應則回傳 true，否則回傳 false。</returns>
        public bool WordPattern(string pattern, string s)
        {
            // dic1: pattern 字元 → 對應的 word（確保同一字元只對應同一 word）
            Dictionary<char, string> dic1 = new Dictionary<char, string>();
            // dic2: word → 對應的 pattern 字元（確保同一 word 只對應同一字元）
            Dictionary<string, char> dic2 = new Dictionary<string, char>();

            int length = pattern.Length;
            string[] arr = s.Split(new char[] { ' ' });

            // 若 pattern 長度與單字數量不同，直接回傳 false
            if (arr.Length != length)
            {
                return false;
            }

            for (int i = 0; i < length; i++)
            {
                char c = pattern[i];
                string word = arr[i];

                // 檢查 pattern 字元 → word 方向
                if (!dic1.ContainsKey(c))
                {
                    // 第一次遇到此字元，建立對應
                    dic1.Add(c, word);
                }
                else if (!dic1[c].Equals(word))
                {
                    // 該字元已有對應的 word，但與當前 word 不同 → 衝突
                    return false;
                }

                // 檢查 word → pattern 字元方向（反向驗證）
                if (!dic2.ContainsKey(word))
                {
                    // 第一次遇到此 word，建立對應
                    dic2.Add(word, c);
                }
                else if (!dic2[word].Equals(c))
                {
                    // 該 word 已有對應的字元，但與當前字元不同 → 衝突
                    return false;
                }
            }

            // 所有位置皆通過雙向檢查，回傳 true
            return true;
        }
    }
}
