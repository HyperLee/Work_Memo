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
            int[] array1 = new int[] { 1, 1, 5, 7, 7 };
            Console.WriteLine("方法3: " + SingleNumber3(array1));
            Console.ReadKey();
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
