namespace 呼叫其他class
{
    internal class Program
    {
        /// <summary>
        /// 208. Implement Trie (Prefix Tree)
        /// https://leetcode.com/problems/implement-trie-prefix-tree/description/
        /// 
        /// 208. 实现 Trie (前缀树)
        /// https://leetcode.cn/problems/implement-trie-prefix-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Trie obj = new Trie();
            obj.Insert("apple");
            int passed = 0;
            passed += RunCase("插入後搜尋完整單字", true, obj.Search("apple"));
            passed += RunCase("搜尋既有前綴", true, obj.StartsWith("app"));
            passed += RunCase("前綴尚未成為單字", false, obj.Search("app"));
            obj.Insert("app");

            passed += RunCase("插入前綴後搜尋完整單字", true, obj.Search("app"));
            Console.WriteLine($"Summary: {passed}/4 checks passed.");
            if (passed != 4)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 比較 Trie 操作的預期結果與實際結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期布林值。</param>
        /// <param name="actual">Trie 操作回傳的實際布林值。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string name, bool expected, bool actual)
        {
            bool passed = expected == actual;
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }

    }

    /// <summary>
    /// 实现 Trie (前缀树)
    /// 
    /// ref:
    /// https://leetcode.cn/problems/implement-trie-prefix-tree/solutions/717239/shi-xian-trie-qian-zhui-shu-by-leetcode-ti500/
    /// https://leetcode.cn/problems/implement-trie-prefix-tree/solutions/2993894/cong-er-cha-shu-dao-er-shi-liu-cha-shu-p-xsj4/
    /// Copilot 產生的程式碼與註解
    /// </summary>
    public class Trie
    {
        // 表示当前节点是否是一个单词的结束节点
        private bool isEnd;
        // 子节点
        private readonly Trie[] children;


        /// <summary>
        /// Initialize your data structure here.
        /// </summary>
        public Trie()
        {
            // 初始化
            isEnd = false;
            // 26 个字母
            children = new Trie[26];
        }


        /// <summary>
        /// Inserts a word into the trie.
        /// </summary>
        /// <param name="word"></param>
        public void Insert(string word)
        {
            Trie node = this;
            int length = word.Length;
            for (int i = 0; i < length; i++)
            {
                char c = word[i];
                // 如果当前节点的子节点中不包含当前字符，则创建一个新的子节点
                int index = c - 'a';
                if (node.children[index] == null)
                {
                    // 创建一个新的子节点
                    node.children[index] = new Trie();
                }
                node = node.children[index];
            }
            node.isEnd = true;
        }


        /// <summary>
        /// Returns if the word is in the trie.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Search(string word)
        {
            // 搜索前缀，如果前缀存在，且是一个单词的结束节点，则返回 true
            Trie node = SearchPrefix(word);
            // 如果前缀存在，且是一个单词的结束节点，则返回 true
            return node != null && node.isEnd;
        }


        /// <summary>
        /// Returns if there is any word in the trie that starts with the given prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public bool StartsWith(string prefix)
        {
            // 搜索前缀，如果前缀存在，则返回 true
            Trie node = SearchPrefix(prefix);
            // 如果前缀存在，则返回 true
            return node != null;
        }


        /// <summary>
        /// 搜索前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private Trie SearchPrefix(string prefix)
        {
            Trie node = this;
            int length = prefix.Length;
            for (int i = 0; i < length; i++)
            {
                char c = prefix[i];
                int index = c - 'a';
                if (node.children[index] == null)
                {
                    return null!;
                }
                node = node.children[index];
            }

            return node;
        }
    }
}