namespace SlidingWindow;

public class SlidingWindowAlgorithm
{
    // 找出陣列中連續 k 個元素的最大和
    public static int MaxSumSubarray(int[] arr, int k)
    {
        if (arr == null || arr.Length == 0 || k <= 0 || k > arr.Length)
            throw new ArgumentException("Invalid input parameters");

        int maxSum = 0;
        int windowSum = 0;

        // 計算第一個窗口的和
        for (int i = 0; i < k; i++)
        {
            windowSum += arr[i];
        }
        maxSum = windowSum;

        // 滑動窗口並更新最大和
        for (int i = k; i < arr.Length; i++)
        {
            windowSum = windowSum - arr[i - k] + arr[i];
            maxSum = Math.Max(maxSum, windowSum);
        }

        return maxSum;
    }

    // 找出字串中最長的不重複字元子字串長度
    public static int LongestUniqueSubstring(string str)
    {
        if (string.IsNullOrEmpty(str))
            return 0;

        Dictionary<char, int> charIndex = new();
        int maxLength = 0;
        int start = 0;

        for (int end = 0; end < str.Length; end++)
        {
            if (charIndex.ContainsKey(str[end]))
            {
                start = Math.Max(start, charIndex[str[end]] + 1);
            }

            charIndex[str[end]] = end;
            maxLength = Math.Max(maxLength, end - start + 1);
        }

        return maxLength;
    }
}

public class Program
{
    public static void Main()
    {
        // 示範使用滑動視窗演算法
        int[] numbers = { 1, 4, 2, 10, 2, 3, 1, 0, 20 };
        int k = 4;
        int maxSum = SlidingWindowAlgorithm.MaxSumSubarray(numbers, k);
        Console.WriteLine($"最大連續 {k} 個元素的和為: {maxSum}");

        string text = "abcabcbb";
        int length = SlidingWindowAlgorithm.LongestUniqueSubstring(text);
        Console.WriteLine($"最長不重複字元子字串長度為: {length}");
    }
}
