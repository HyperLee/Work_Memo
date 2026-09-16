namespace DictionaryEquals用法;

internal class Program
{
    /// <summary>
    /// 執行 Word Pattern 的固定案例，並以結束碼回報整體結果。
    /// </summary>
    private static void Main()
    {
        Program program = new Program();
        int passed = 0;
        int total = 0;

        passed += RunCase("標準雙射", true, program.WordPattern("abba", "dog cat cat dog"));
        total++;
        passed += RunCase("同一字元對應不同單字", false, program.WordPattern("abba", "dog cat cat fish"));
        total++;
        passed += RunCase("重複字元產生衝突", false, program.WordPattern("aaaa", "dog cat cat dog"));
        total++;
        passed += RunCase("不同字元對應同一單字", false, program.WordPattern("abba", "dog dog dog dog"));
        total++;
        passed += RunCase("字元數與單字數不同", false, program.WordPattern("abc", "dog cat"));
        total++;
        passed += RunCase("單一對應", true, program.WordPattern("a", "dog"));
        total++;
        passed += RunCase("全部位置維持同一對應", true, program.WordPattern("aaaa", "dog dog dog dog"));
        total++;

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 輸出單一案例的預期值、實際值與通過狀態。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="expected">預期布林值。</param>
    /// <param name="actual">實際布林值。</param>
    /// <returns>通過時回傳 1，否則回傳 0。</returns>
    private static int RunCase(string name, bool expected, bool actual)
    {
        bool isPassed = expected == actual;
        Console.WriteLine($"Case: {name}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Actual: {actual}");
        Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();
        return isPassed ? 1 : 0;
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
        Dictionary<char, string> dic1 = new Dictionary<char, string>();
        Dictionary<string, char> dic2 = new Dictionary<string, char>();

        int length = pattern.Length;
        string[] arr = s.Split(new char[] { ' ' });

        if (arr.Length != length)
        {
            return false;
        }

        for (int i = 0; i < length; i++)
        {
            char c = pattern[i];
            string word = arr[i];

            // 兩個方向都必須維持唯一映射，任一方向衝突就不是雙射。
            if (!dic1.ContainsKey(c))
            {
                dic1.Add(c, word);
            }
            else if (!dic1[c].Equals(word))
            {
                return false;
            }

            if (!dic2.ContainsKey(word))
            {
                dic2.Add(word, c);
            }
            else if (!dic2[word].Equals(c))
            {
                return false;
            }
        }

        return true;
    }
}