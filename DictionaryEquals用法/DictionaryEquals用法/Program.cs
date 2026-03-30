namespace DictionaryEquals用法
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        /// <summary>
        /// Dictionary.Equals 用法
        /// 比對 value 是否相同
        /// 感覺很實用
        /// 需要特別紀錄
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="s"></param>
        /// <returns></returns>
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
}
