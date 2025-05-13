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
        var testCases = new List<int[]>
        {
            new int[] {2, 3, 1, 0, 2, 5}, // 範例資料
            new int[] {0, 1, 2, 3, 4, 5}, // 無重複
            new int[] {1, 2, 3, 4, 5, 1}, // 重複在頭尾
            new int[] {5, 4, 3, 2, 1, 0, 3}, // 重複在中間
            new int[] {}, // 空陣列
            new int[] {0, 1, 2, 6} // 有不合法數字
        };
        foreach (var test in testCases)
        {
            // 測試 FindDuplicate
            var result1 = FindDuplicate(test);
            // 測試 FindDuplicateInPlace，需複製一份避免原地修改影響其他測資
            var testCopy = (int[])test.Clone();
            var result2 = FindDuplicateInPlace(testCopy);
            Console.WriteLine($"輸入: {{{string.Join(", ", test)}}} => FindDuplicate 輸出: {(result1.HasValue ? result1.ToString() : "無重複或輸入不合法")}, FindDuplicateInPlace 輸出: {(result2.HasValue ? result2.ToString() : "無重複或輸入不合法")}");
        }
    }
}
