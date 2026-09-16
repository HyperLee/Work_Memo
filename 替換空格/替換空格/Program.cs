using System;
using System.Text;

namespace 替換空格
{
    /// <summary>
    /// 實現 ReplaceSpace functions，將字串中的空格替換為「%20」。
    /// </summary>
    class Program
    {
        /// <summary>
        /// 主程式進入點。讀取輸入字串，呼叫 ReplaceSpace 函式，並輸出結果。
        /// </summary>
        /// <param name="args">命令列參數</param>
        static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("題目範例", "We%20Are%20Happy");
            passed += RunCase("單一空格", "%20");
            passed += RunCase("前後空格", "%20%20Leading%20and%20trailing%20%20");
            passed += RunCase("連續空格", "A%20B%20%20C%20%20%20D");
            passed += RunCase("沒有空格", "NoSpace");
            passed += RunCase("空字串", string.Empty);

            Console.WriteLine($"Summary: {passed}/12 checks passed.");
            if (passed != 12)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 使用兩種空格替換方法處理同一個固定字串。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期替換結果。</param>
        /// <returns>兩種方法各自通過時各計 1 分。</returns>
        private static int RunCase(string name, string expected)
        {
            string input = name switch
            {
                "題目範例" => "We Are Happy",
                "單一空格" => " ",
                "前後空格" => "  Leading and trailing  ",
                "連續空格" => "A B  C   D",
                "沒有空格" => "NoSpace",
                _ => string.Empty
            };
            int passed = 0;
            passed += PrintResult($"{name} / 手動走訪", expected, ReplaceSpace(input));
            passed += PrintResult($"{name} / StringBuilder 逆向", expected, new Program().ReplaceSpace2(input));
            return passed;
        }

        /// <summary>
        /// 輸出字串替換結果的固定 smoke-test 欄位。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期字串。</param>
        /// <param name="actual">實際字串。</param>
        /// <returns>通過時回傳 1，否則回傳 0。</returns>
        private static int PrintResult(string name, string expected, string actual)
        {
            bool passed = expected == actual;
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }


        /// <summary>
        /// 將字串中的每個空格替換成「%20」。
        /// 解法說明：
        /// 本函式不使用 C# 內建 Replace 方法，改以手動演算法實作。
        /// 1. 建立一個 StringBuilder 物件以儲存結果。
        /// 2. 逐字檢查輸入字串的每個字元：
        ///    - 若為空格，則加入 "%20"。
        ///    - 否則加入原字元。
        /// 3. 最後回傳 StringBuilder 轉成的字串。
        /// 時間複雜度 O(n)，空間複雜度 O(n)。
        /// </summary>
        /// <param name="s">輸入字串</param>
        /// <returns>替換後的新字串</returns>
        static string ReplaceSpace(string s)
        {
            // 建立 StringBuilder 以儲存結果
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            // 逐字檢查輸入字串的每個字元
            foreach (char c in s)
            {
                if (c == ' ')
                {
                    // 若為空格，則加入 "%20"
                    sb.Append("%20");
                }
                else
                {
                    // 否則加入原字元
                    sb.Append(c);
                }
            }
            // 回傳替換後的新字串
            return sb.ToString();
        }

        /// <summary>
        /// 將字串中的每個空格替換成「%20」。
        /// 解法說明：
        /// 本方法採用「雙指標逆向填充」演算法，先計算空格數並擴展字串長度，
        /// 再從尾端往前逐字搬移與替換，能有效減少多餘搬移，適合大字串處理。
        /// 時間複雜度 O(n)，空間複雜度 O(n)。
        /// 
        /// 注意:
        /// sb 加入的空格是在最尾端，並不會影響原字串的內容。
        /// 這是因為我們要在原字串的尾端補上兩個額外空間，
        /// 以便能夠在原字串的尾端進行替換。
        /// 這樣做的好處是可以避免在字串中間進行多次搬移操作，
        /// 而是只需在尾端進行一次搬移操作。
        /// 這樣可以提高效率，特別是在處理長字串時。
        /// 
        /// 步驟:
        /// 1. 計算原字串的長度，並在 StringBuilder 中擴展空間。
        /// 2. 使用兩個指標 P1 和 P2，分別指向原字串的尾端和擴展後字串的尾端。
        /// 3. 從尾端開始遍歷原字串，逐字檢查每個字元。
        /// 4. 如果遇到空格，則在擴展後字串中填入「%20」。
        /// 5. 如果遇到非空格字元，則直接搬移到擴展後字串中。
        /// 6. 最後回傳擴展後字串的內容。
        /// </summary>
        /// <param name="s">輸入字串</param>
        /// <returns>所有空格被替換為「%20」的新字串</returns>
        public string ReplaceSpace2(string s)
        {
            // 將輸入字串轉為 StringBuilder 以便原地修改
            StringBuilder sb = new StringBuilder(s);
            int P1 = sb.Length - 1; // P1 指向原字串尾端

            // 先計算空格數量，並在"字串尾端"為每個空格補上兩個額外空間
            for (int i = 0; i <= P1; i++)
            {
                if (sb[i] == ' ')
                {
                    // 每個空格需多兩個字元空間
                    sb.Append("  ");
                }
            }

            int P2 = sb.Length - 1; // P2 指向擴展後字串尾端

            // 從尾端往前搬移與替換
            // P1 與 P2 持續右往左移動(逆向)
            while (P1 >= 0 && P2 > P1)
            {
                // 逐字檢查原字串的每個字元
                // 且 P1 持續往左移動
                char c = sb[P1--];
                if (c == ' ')
                {
                    // 遇到空格，逆向填入 '0', '2', '%'
                    sb[P2--] = '0';
                    sb[P2--] = '2';
                    sb[P2--] = '%';
                }
                else
                {
                    // 非空格直接搬移
                    // P2 持續往左移動
                    sb[P2--] = c;
                }
            }

            // 回傳替換後的新字串
            return sb.ToString();
        }

    }
}