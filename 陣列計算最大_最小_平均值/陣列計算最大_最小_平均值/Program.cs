namespace 陣列計算最大_最小_平均值
{
    internal class Program
    {
        /// <summary>
        /// 使用 LINQ 與手動迴圈計算固定整數陣列的最大值、最小值與平均值。
        /// </summary>
        /// <param name="args">程式執行參數；固定 smoke test 不需額外參數。</param>
        private static void Main(string[] args)
        {
            int passed = 0;
            passed += RunCase("題目範例", new int[] { 10, 15, 50, 65, 34, 80, 90, 45 }, "max=90; min=10; average=48.625", "max=90; min=10; average=48.63");
            passed += RunCase("包含負數", new int[] { -5, 0, 5 }, "max=5; min=-5; average=0", "max=5; min=-5; average=0.00");

            Console.WriteLine($"Summary: {passed}/4 checks passed.");
            if (passed != 4)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 同時比較 LINQ API 與手動迴圈計算的最大值、最小值與平均值。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="input">本案例獨立的整數陣列。</param>
        /// <param name="expectedApi">API 計算的預期文字。</param>
        /// <param name="expectedManual">手動計算的預期文字。</param>
        /// <returns>兩種計算各自通過時各計 1 分。</returns>
        private static int RunCase(string name, int[] input, string expectedApi, string expectedManual)
        {
            int max = input.Max();
            int min = input.Min();
            double average = input.Average();
            string actualApi = $"max={max}; min={min}; average={average}";

            int maxValue = input[0];
            int minValue = input[0];
            int sum = 0;
            foreach (int value in input)
            {
                maxValue = Math.Max(maxValue, value);
                minValue = Math.Min(minValue, value);
                sum += value;
            }

            double roundedAverage = Math.Round((double)sum / input.Length, 2, MidpointRounding.AwayFromZero);
            string actualManual = $"max={maxValue}; min={minValue}; average={roundedAverage.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}";
            int passed = 0;
            passed += PrintResult($"{name} / LINQ", expectedApi, actualApi);
            passed += PrintResult($"{name} / 迴圈", expectedManual, actualManual);
            return passed;
        }

        /// <summary>
        /// 輸出字串計算結果的固定 smoke-test 欄位。
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
    }
}