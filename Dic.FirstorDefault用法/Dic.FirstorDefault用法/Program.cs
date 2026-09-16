namespace Dic.FirstorDefault用法
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
            passed += RunCase("中間出現的唯一值", 5, new int[] { 1, 1, 5, 7, 7 });
            passed += RunCase("負數也可使用 XOR", -3, new int[] { -3, 4, 4, 8, 8 });
            passed += RunCase("只有一個元素", 42, new int[] { 42 });

            Console.WriteLine($"Summary: {passed}/9 checks passed.");
            if (passed != 9)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 用同一組新陣列比較三種 Single Number 解法。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">唯一出現數字的預期值。</param>
        /// <param name="input">符合每個其他數字出現兩次的輸入。</param>
        /// <returns>三個方法通過數量。</returns>
        private static int RunCase(string name, int expected, int[] input)
        {
            int[] first = [.. input];
            int[] second = [.. input];
            int[] third = [.. input];
            int[] actuals = { SingleNumber(first), SingleNumber2(second), SingleNumber3(third) };
            int passed = 0;

            for (int i = 0; i < actuals.Length; i++)
            {
                bool isPassed = expected == actuals[i];
                Console.WriteLine($"Case: {name} / 方法 {i + 1}");
                Console.WriteLine($"Expected: {expected}");
                Console.WriteLine($"Actual: {actuals[i]}");
                Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
                passed += isPassed ? 1 : 0;
            }

            return passed;
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10219111
        /// 
        /// FirstOrDefault
        /// https://vimsky.com/examples/detail/csharp-ex---Dictionary-FirstOrDefault-method.html
        /// ContainsValue 
        /// https://vimsky.com/zh-tw/examples/usage/c-sharp-dictionary-containsvalue-method.html
        /// 
        /// 本方法是透過 function 去找出 次數包含 1 者(可能會有多個, 但是本題目輸入只會有一次而已)
        /// 然後再輸出第一個 當作答案
        /// return dic.FirstOrDefault(x => x.Value == 1).Key;
        /// 
        /// 題目有說明 出現一次者 只有一個
        /// 所以取 第一個 沒問題
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber(int[] nums)
        {
            // key: num, Value: times
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (dic.ContainsKey(num))
                {
                    dic[num] = dic[num] + 1;
                }
                else
                {
                    dic.Add(num, 1);
                }
            }

            // 找出 dic 中 Value 有為 1 者
            if (dic.ContainsValue(1))
            {
                // 取出第一個的 key 值
                return dic.FirstOrDefault(x => x.Value == 1).Key;
            }
            else
            {
                return 0;
            }

        }


        /// <summary>
        /// 方法2:
        /// 差別在於 找尋次數為 1者 不同而已
        /// 這邊是透過 遍歷 dic 
        /// 去找出  num.Value == 1
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber2(int[] nums)
        {
            // key: num, Value: times
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]]++;
                }
                else
                {
                    dic.Add(nums[i], 1);
                }
            }

            // 從 dic 中遍歷
            foreach (var num in dic)
            {
                // 取出 value 數值為 1
                if (num.Value == 1)
                {
                    return num.Key;
                }
            }

            return 0;
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