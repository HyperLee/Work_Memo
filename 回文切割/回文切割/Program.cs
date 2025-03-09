namespace 回文切割
{
    internal class Program
    {
        /// <summary>
        /// 對於字串s和整數k，如果滿足以下條件，則子字串的選擇是有效的： 
        /// – 每個子字串的長度大於或等於k。
        /// – 每個子字串都是回文。
        /// – 沒有兩個子字串重疊。
        /// – 確定s可以形成的有效子字串的最大數量。
        /// 
        /// 功能說明
        /// 使用以下參數完成函數getMaxSubstring： 
        /// – string s：給定的字串。
        /// – int k：有效子字串的最小長度。
        /// 返回值
        /// – int：可以形成的最大有效子字串數。
        ///  
        /// s = "aababaabce"
        /// k = 3
        /// 對於這個例子，長度至少為k的非重疊回文子串的一些可能選擇是：“ababa”和“abce”，“aba”和“baab”，此類有效子字串的最大數量是2。
        /// "aba" 和 "baab" 確實是其中一個最佳解。這是因為這個切割方式能得到最多數量（2個）的符合所有條件的回文子字串。
        /// 1. 符合長度要求：每個子字串的長度至少為k。
        /// 2. 符合回文要求：每個子字串都是回文。
        /// 3. 符合不重疊要求：沒有兩個子字串重疊。
        /// 
        /// 這是一個經典的回文切割（Palindrome Partitioning）問題的變形，我們的目標是找出最多的不重疊回文子字串，並且這些子字串的長度至少為 k。
        /// <param name="args"></param>
        /// </summary>
        private static void Main(string[] args)
        {
            
            // 測試案例 1: 原始測試案例
            Console.WriteLine("測試案例 1:");
            string s1 = "aababaabce";
            int k1 = 3;
            int result1 = Solution.GetMaxSubstring(s1, k1);
            Console.WriteLine($"輸入: s = {s1}, k = {k1}");
            Console.WriteLine($"結果: {result1}\n");
            
            // 測試案例 2: 較短的字串
            Console.WriteLine("測試案例 2:");
            string s2 = "abacbc";
            int k2 = 2;
            int result2 = Solution.GetMaxSubstring(s2, k2);
            Console.WriteLine($"輸入: s = {s2}, k = {k2}");
            Console.WriteLine($"結果: {result2}\n");
            
            // 測試案例 3: 較長的回文字串
            Console.WriteLine("測試案例 3:");
            string s3 = "abbaabbacc";
            int k3 = 4;
            int result3 = Solution.GetMaxSubstring(s3, k3);
            Console.WriteLine($"輸入: s = {s3}, k = {k3}");
            Console.WriteLine($"結果: {result3}\n");

            // 測試案例 4: 無回文的情況
            Console.WriteLine("測試案例 4:");
            string s4 = "abcde";
            int k4 = 2;
            int result4 = Solution.GetMaxSubstring(s4, k4);
            Console.WriteLine($"輸入: s = {s4}, k = {k4}");
            Console.WriteLine($"結果: {result4}\n");

            // 測試案例 5: k值等於字串長度
            Console.WriteLine("測試案例 5:");
            string s5 = "racecar";
            int k5 = 7;
            int result5 = Solution.GetMaxSubstring(s5, k5);
            Console.WriteLine($"輸入: s = {s5}, k = {k5}");
            Console.WriteLine($"結果: {result5}\n");
            
        }
    }


    /// <summary>
    /// 回文切割問題求解類
    /// 
    /// 問題描述：
    /// - 給定字串 s 和整數 k
    /// - 找出最多數量的不重疊回文子字串
    /// - 每個子字串長度至少為 k
    /// 
    /// 解題思路：
    /// 1. 動態規劃方法
    ///    - 定義 dp[i] 表示到位置 i 為止能形成的最大有效子字串數量
    ///    - dp 數組大小為 n+1，其中 n 為輸入字串長度
    /// 
    /// 2. 狀態轉移策略
    ///    - 對於每個位置 i，先繼承 i-1 位置的結果，確保考慮之前的最優解
    ///    - 枚舉所有以 i 為起點的子字串，檢查是否能形成新的回文
    ///    - 當找到有效回文時，更新 dp[j+1] = max(dp[j+1], dp[i] + 1)
    /// 
    /// 3. 回文檢查優化
    ///    - 使用雙指針法進行回文檢查
    ///    - 只有當子字串長度 >= k 時才進行回文檢查，避免不必要的運算
    /// 
    /// 4. 時間複雜度分析
    ///    - 外層迴圈：O(n)，遍歷所有可能的起點
    ///    - 內層迴圈：O(n)，遍歷所有可能的終點
    ///    - 回文檢查：O(n)，最壞情況下需要檢查整個子字串
    ///    - 總時間複雜度：O(n³)
    /// 
    /// 5. 空間複雜度
    ///    - dp 數組：O(n)
    ///    - 額外空間：O(1)
    ///    - 總空間複雜度：O(n)
    /// </summary>
    public class Solution 
    {
        /// <summary>
        /// 取得最大不重疊回文子字串數量
        /// </summary>
        /// <param name="s">輸入字串</param>
        /// <param name="k">最小子字串長度</param>
        /// <returns>最大有效子字串數量</returns>
        public static int GetMaxSubstring(string s, int k) 
        {
            // 獲取字串長度
            int n = s.Length;
            // dp[i] 表示到位置i為止能形成的最大有效子字串數量
            int[] dp = new int[n + 1];

            // 外層迴圈: 遍歷所有可能的子字串起點
            for (int i = 0; i < n; i++)
            {
                // 繼承前一個位置的最大值，確保不重疊
                if (i > 0)
                {
                    dp[i] = dp[i - 1];
                }

                // 內層迴圈: 檢查所有以i為起點的子字串結尾位置
                for (int j = i; j < n; j++)
                {
                    // 條件檢查：
                    // 1. j-i+1 >= k 確保子字串長度符合最小要求
                    // 2. IsPalindrome 檢查是否為回文
                    if (j - i + 1 >= k && IsPalindrome(s, i, j))
                    {
                        // 更新dp數組：
                        // 如果是從字串開頭開始(i=0)，直接設為1
                        // 否則，取當前值和 dp[i]+1 的最大值
                        if (i == 0)
                        {
                            dp[j + 1] = Math.Max(dp[j + 1], 1);
                        }   
                        else
                        {
                            dp[j + 1] = Math.Max(dp[j + 1], dp[i] + 1);
                        }
                    }
                }
            }

            // 返回整個字串的最大有效子字串數量
            return dp[n];
        }


        /// <summary>
        /// 檢查字串區間是否為回文
        /// </summary>
        /// <param name="s">原始字串</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">結束位置</param>
        /// <returns>true表示是回文，false表示不是回文</returns>
        private static bool IsPalindrome(string s, int start, int end)
        {
            // 使用雙指針法從兩端向中間檢查
            while (start < end)
            {
                // 如果對應位置字符不相同，則不是回文
                if (s[start] != s[end])
                {
                    return false;
                }

                // 移動指針
                start++;
                end--;
            }

            // 所有字符都對應相同，是回文
            return true;
        }
    }
}