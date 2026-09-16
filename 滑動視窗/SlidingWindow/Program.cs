namespace SlidingWindow;

public class SlidingWindowAlgorithm
{
    /// <summary>
    /// 找出整數陣列中長度為 <paramref name="k"/> 的連續子陣列最大總和。
    /// </summary>
    /// <param name="arr">要搜尋的整數陣列。</param>
    /// <param name="k">固定視窗長度。</param>
    /// <returns>所有長度為 <paramref name="k"/> 的連續子陣列之最大總和。</returns>
    /// <exception cref="ArgumentException">輸入陣列為 null、空陣列，或視窗長度不合法。</exception>
    public static int MaxSumSubarray(int[] arr, int k)
    {
        if (arr == null || arr.Length == 0 || k <= 0 || k > arr.Length)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        int windowSum = 0;
        for (int i = 0; i < k; i++)
        {
            windowSum += arr[i];
        }

        int maxSum = windowSum;
        for (int i = k; i < arr.Length; i++)
        {
            // 移除左端舊值並加入右端新值，避免重新加總整個視窗。
            windowSum = windowSum - arr[i - k] + arr[i];
            maxSum = Math.Max(maxSum, windowSum);
        }

        return maxSum;
    }

    /// <summary>
    /// 找出字串中不含重複字元之最長連續子字串長度。
    /// </summary>
    /// <param name="str">要搜尋的字串。</param>
    /// <returns>最長不重複連續子字串的長度；null 或空字串回傳 0。</returns>
    public static int LongestUniqueSubstring(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return 0;
        }

        Dictionary<char, int> charIndex = new Dictionary<char, int>();
        int maxLength = 0;
        int start = 0;

        for (int end = 0; end < str.Length; end++)
        {
            if (charIndex.TryGetValue(str[end], out int previousIndex))
            {
                // start 只能向右移，不能被目前視窗左側的舊索引拉回去。
                start = Math.Max(start, previousIndex + 1);
            }

            charIndex[str[end]] = end;
            maxLength = Math.Max(maxLength, end - start + 1);
        }

        return maxLength;
    }
}

public class Program
{
    /// <summary>
    /// 執行兩種滑動視窗演算法的固定案例，並以結束碼回報結果。
    /// </summary>
    public static void Main()
    {
        int passed = 0;
        int total = 0;

        passed += RunCase("最大和：一般陣列", 24, () => SlidingWindowAlgorithm.MaxSumSubarray(new int[] { 1, 4, 2, 10, 2, 3, 1, 0, 20 }, 4));
        total++;
        passed += RunCase("最大和：全為負數", -5, () => SlidingWindowAlgorithm.MaxSumSubarray(new int[] { -8, -2, -3, -7 }, 2));
        total++;
        passed += RunCase("最大和：視窗等於陣列", 6, () => SlidingWindowAlgorithm.MaxSumSubarray(new int[] { 1, 2, 3 }, 3));
        total++;
        passed += RunCase("最大和：視窗為零", nameof(ArgumentException), () => CaptureException(() => SlidingWindowAlgorithm.MaxSumSubarray(new int[] { 1, 2, 3 }, 0)));
        total++;
        passed += RunCase("最長不重複：一般字串", 3, () => SlidingWindowAlgorithm.LongestUniqueSubstring("abcabcbb"));
        total++;
        passed += RunCase("最長不重複：重複字元跨視窗", 3, () => SlidingWindowAlgorithm.LongestUniqueSubstring("pwwkew"));
        total++;
        passed += RunCase("最長不重複：空字串", 0, () => SlidingWindowAlgorithm.LongestUniqueSubstring(string.Empty));
        total++;
        passed += RunCase("最長不重複：null", 0, () => SlidingWindowAlgorithm.LongestUniqueSubstring(null!));
        total++;

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 執行案例並輸出一致的 smoke-test 欄位。
    /// </summary>
    /// <typeparam name="T">預期值與實際值的型別。</typeparam>
    /// <param name="name">案例名稱。</param>
    /// <param name="expected">預期結果。</param>
    /// <param name="action">產生實際結果的動作。</param>
    /// <returns>通過時回傳 1，否則回傳 0。</returns>
    private static int RunCase<T>(string name, T expected, Func<T> action)
        where T : notnull
    {
        T actual = action();
        bool isPassed = EqualityComparer<T>.Default.Equals(expected, actual);
        Console.WriteLine($"Case: {name}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Actual: {actual}");
        Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();
        return isPassed ? 1 : 0;
    }

    /// <summary>
    /// 執行預期會失敗的呼叫並回傳例外型別名稱。
    /// </summary>
    /// <param name="action">要執行的動作。</param>
    /// <returns>捕捉到的例外型別名稱；未拋出例外時回傳 NoException。</returns>
    private static string CaptureException(Action action)
    {
        try
        {
            action();
            return "NoException";
        }
        catch (Exception exception)
        {
            return exception.GetType().Name;
        }
    }
}