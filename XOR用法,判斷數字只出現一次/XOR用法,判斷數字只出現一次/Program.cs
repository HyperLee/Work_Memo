namespace XOR用法_判斷數字只出現一次
{
    internal class Program
    {
        /// <summary>
        /// 136. Single Number
        /// https://leetcode.com/problems/single-number/
        /// 
        /// 136. 只出现一次的数字
        /// https://leetcode.cn/problems/single-number/description/
        /// 
        /// 與 leetcode 540 類似  解法共用
        /// https://leetcode.com/problems/single-element-in-a-sorted-array/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("中間的唯一值", 5, new int[] { 1, 1, 5, 7, 7 });
            passed += RunCase("負數唯一值", -3, new int[] { -3, 4, 4, 8, 8 });
            passed += RunCase("只有一個元素", 42, new int[] { 42 });

            Console.WriteLine($"Summary: {passed}/3 checks passed.");
            if (passed != 3)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 以 XOR 的消去性質找出只出現一次的整數。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">唯一數字的預期值。</param>
        /// <param name="input">除唯一值外其餘值均出現兩次的陣列。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string name, int expected, int[] input)
        {
            int actual = SingleNumber3([.. input]);
            bool passed = expected == actual;
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }


        /// <summary>
        /// 用邏輯運算 xor
        /// 1 ⊕ 0 = 1
        /// 0 ⊕ 0 = 0
        /// 註記:
        /// 1. 任何数和 0 做异或运算，结果仍然是原来的数，即 a⊕0=a。
        /// 2. 任何数和其自身做异或运算，结果是 0，即 a⊕a=0。
        /// 3. 异或运算满足交换律和结合律，即 a⊕b⊕a=b⊕a⊕a=b⊕(a⊕a)=b⊕0=b。
        /// 
        /// ref:
        /// https://leetcode.cn/problems/single-number/solutions/242211/zhi-chu-xian-yi-ci-de-shu-zi-by-leetcode-solution/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber3(int[] nums)
        {
            int res = 0;
            foreach (var num in nums)
            {
                res ^= num;
            }

            return res;
        }
    }
}