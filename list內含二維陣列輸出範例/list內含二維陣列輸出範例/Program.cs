namespace list內含二維陣列輸出範例
{
    internal class Program
    {
        /// <summary>
        /// 786. K-th Smallest Prime Fraction
        /// https://leetcode.com/problems/k-th-smallest-prime-fraction/?envType=daily-question&envId=2024-05-10
        /// 786. 第 K 个最小的质数分数
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("四個分母候選值", "2/5", new int[] { 1, 2, 3, 5 }, 3);
            passed += RunCase("三個分母候選值", "1/3", new int[] { 1, 2, 3 }, 1);
            passed += RunCase("第二小分數", "1/2", new int[] { 1, 2, 3, 5 }, 4);

            Console.WriteLine($"Summary: {passed}/3 checks passed.");
            if (passed != 3)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 取得第 k 小質數分數並與穩定化文字預期值比較。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期的分數文字。</param>
        /// <param name="input">嚴格遞增且以 1 開頭的候選陣列。</param>
        /// <param name="k">分數排名，從 1 開始。</param>
        /// <returns>檢查通過時回傳 1，否則回傳 0。</returns>
        private static int RunCase(string name, string expected, int[] input, int k)
        {
            int[] fraction = KthSmallestPrimeFraction([.. input], k);
            string actual = $"{fraction[0]}/{fraction[1]}";
            bool passed = expected == actual;
            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
            return passed ? 1 : 0;
        }


        /// <summary>
        /// ref: 方法一：自定义排序
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/solutions/1127103/di-k-ge-zui-xiao-de-su-shu-fen-shu-by-le-argw/
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/solutions/1127751/gong-shui-san-xie-yi-ti-shuang-jie-you-x-8ymk/
        /// https://leetcode.cn/problems/k-th-smallest-prime-fraction/solutions/2726838/786-di-k-ge-zui-xiao-de-zhi-shu-fen-shu-pu5wt/
        /// 
        /// 此方法需要注意
        /// 正常比較方法是
        /// a/b 與 c/d 比較大小
        /// 但是這邊用
        /// a * d < b * c 來取代上述方法計算比較
        /// 因浮點數計算會有誤差問題
        /// 詳細推導方式 要去看上述ref連結說明
        /// 
        /// 以长度为 2 的整数数组返回你的答案, 这里 answer[0] == arr[i] 且 answer[1] == arr[j] 。
        /// </summary>
        /// <param name="arr">輸入資料, array</param>
        /// <param name="k">第K個最小分數</param>
        /// <returns></returns>
        public static int[] KthSmallestPrimeFraction(int[] arr, int k)
        {
            int n = arr.Length;
            List<int[]> list = new List<int[]>();

            // 枚舉arr中 所有排列組合資料, 塞入 list裡面
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    list.Add(new int[] { arr[i], arr[j] });
                }
            }

            // 分數; 排序 遞增排序 小至大; a * d < b * c
            list.Sort((x, y) => x[0] * y[1] - y[0] * x[1]);

            // 回傳第 k 個
            return list[k - 1];
        }
    }
}