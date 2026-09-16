namespace 陣列中重複的數字;

class Program
{
    /// <summary>
    /// 尋找陣列中任意一個重複的數字，若無重複或輸入不合法則回傳 null
    /// 限制：陣列中的數字必須都在 0 ~ n-1 範圍內
    /// 若無重複或輸入不合法則回傳 null
    /// 
    /// 時間複雜度：O(n)
    /// 空間複雜度：O(n)
    /// 需要一個 HashSet 來儲存已出現過的數字，最壞情況下 HashSet 會存放 n 個元素，因此空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    static int? FindDuplicate(int[] nums)
    {
        // 檢查輸入是否為 null 或空陣列
        if (nums == null || nums.Length == 0)
        {
            return null;
        }
        int n = nums.Length;
        // 驗證每個數字是否都在 0 ~ n-1 範圍內
        foreach (var num in nums)
        {
            if (num < 0 || num >= n)
            {
                return null; // 若有數字超出範圍，視為不合法
            }
        }
        // 使用 HashSet 來記錄已出現過的數字
        var seen = new HashSet<int>();
        foreach (var num in nums)
        {
            // 若數字已經出現過，代表找到重複數字，直接回傳
            if (seen.Contains(num))
            {
                return num;
            }
            // 否則加入已出現集合
            seen.Add(num);
        }
        // 若都沒找到重複數字，回傳 null
        return null;
    }

    /// <summary>
    /// 尋找陣列中任意一個重複的數字，空間複雜度 O(1)，時間複雜度 O(n)
    /// 限制：陣列中的數字必須都在 0 ~ n-1 範圍內
    /// 若無重複或輸入不合法則回傳 null
    /// 
    /// 原地交換法使空間複雜度降到 O(1)，但時間複雜度仍然是 O(n)
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    static int? FindDuplicateInPlace(int[] nums)
    {
        // 檢查輸入是否為 null 或空陣列
        if (nums == null || nums.Length == 0)
        {
            return null;
        }
        int n = nums.Length;
        // 驗證每個數字是否都在 0 ~ n-1 範圍內
        foreach (var num in nums)
        {
            if (num < 0 || num >= n)
            {
                return null; // 若有數字超出範圍，視為不合法
            }
        }
        // 原地交換法，空間 O(1)
        for (int i = 0; i < n; i++)
        {
            while (nums[i] != i)
            {
                if (nums[i] == nums[nums[i]])
                {
                    return nums[i]; // 找到重複
                }
                // 交換 nums[i] 與 nums[nums[i]]
                int temp = nums[i];
                nums[i] = nums[temp];
                nums[temp] = temp;
            }
        }
        // 若都沒找到重複數字，回傳 null
        return null;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        int passed = 0;
        passed += RunCase("題目範例", 2, new int[] { 2, 3, 1, 0, 2, 5 });
        passed += RunCase("沒有重複", null, new int[] { 0, 1, 2, 3, 4, 5 });
        passed += RunCase("重複在頭尾", 1, new int[] { 1, 2, 3, 4, 5, 1 });
        passed += RunCase("重複在中間", 3, new int[] { 5, 4, 3, 2, 1, 0, 3 });
        passed += RunCase("空陣列", null, Array.Empty<int>());
        passed += RunCase("含不合法數字", null, new int[] { 0, 1, 2, 6 });

        Console.WriteLine($"Summary: {passed}/12 checks passed.");
        if (passed != 12)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以 HashSet 與原地交換兩種方法檢查重複值結果。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="expected">預期的重複值；沒有結果時為 null。</param>
    /// <param name="input">本案例獨立的輸入陣列。</param>
    /// <returns>兩種方法各自通過時各計 1 分。</returns>
    static int RunCase(string name, int? expected, int[] input)
    {
        int? first = FindDuplicate([.. input]);
        int? second = FindDuplicateInPlace([.. input]);
        int passed = 0;
        passed += PrintResult($"{name} / HashSet", expected, first);
        passed += PrintResult($"{name} / 原地交換", expected, second);
        return passed;
    }

    /// <summary>
    /// 輸出 nullable 整數結果的固定 smoke-test 欄位。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="expected">預期值。</param>
    /// <param name="actual">實際值。</param>
    /// <returns>通過時回傳 1，否則回傳 0。</returns>
    static int PrintResult(string name, int? expected, int? actual)
    {
        bool passed = expected == actual;
        Console.WriteLine($"Case: {name}");
        Console.WriteLine($"Expected: {expected?.ToString() ?? "null"}");
        Console.WriteLine($"Actual: {actual?.ToString() ?? "null"}");
        Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
        Console.WriteLine();
        return passed ? 1 : 0;
    }
}