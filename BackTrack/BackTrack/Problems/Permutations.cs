namespace BackTrack.Problems;

/// <summary>
/// 全排列（Permutations）：給定不重複整數陣列，輸出所有排列。
/// 解法：使用 used[] 標記已選元素，遞迴深度等於 nums.Length 時收集解。
/// 時間複雜度 O(n · n!)，空間複雜度 O(n)（不含輸出）。
/// </summary>
public sealed class Permutations : IBacktrackingProblem
{
    public string Title => "Permutations 全排列 ([1,2,3])";

    public string Description => "輸出陣列所有排列，使用 used[] 避免重複選擇。";

    private static readonly int[] DefaultInput = [1, 2, 3];

    public void Run()
    {
        Console.WriteLine($"=== {Title} ===");
        Console.WriteLine($"輸入：[{string.Join(", ", DefaultInput)}]");

        var results = new List<List<int>>();
        var path = new List<int>();
        var used = new bool[DefaultInput.Length];
        Backtrack(DefaultInput, used, path, results);

        Console.WriteLine($"找到 {results.Count} 個排列：");
        foreach (var p in results)
        {
            Console.WriteLine($"  [{string.Join(", ", p)}]");
        }
    }

    /// <summary>
    /// 遞迴主函式。
    /// </summary>
    /// <param name="nums">輸入陣列。</param>
    /// <param name="used">標記元素是否已被選取。</param>
    /// <param name="path">當前部分排列。</param>
    /// <param name="results">收集所有解。</param>
    private static void Backtrack(int[] nums, bool[] used, List<int> path, List<List<int>> results)
    {
        if (path.Count == nums.Length)
        {
            results.Add([.. path]);
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // 剪枝：已使用的元素跳過
            if (used[i])
            {
                continue;
            }

            // 選擇
            used[i] = true;
            path.Add(nums[i]);

            // 遞迴
            Backtrack(nums, used, path, results);

            // 撤銷
            path.RemoveAt(path.Count - 1);
            used[i] = false;
        }
    }
}
