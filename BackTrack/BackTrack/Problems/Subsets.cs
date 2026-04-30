namespace BackTrack.Problems;

/// <summary>
/// 子集（Subsets）：給定不重複整數陣列，輸出所有子集（冪集）。
/// 解法：以 start 指標走過陣列，每進入節點就收一次解（前序）。
/// 時間複雜度 O(n · 2^n)，空間複雜度 O(n)。
/// </summary>
public sealed class Subsets : IBacktrackingProblem
{
    public string Title => "Subsets 子集 ([1,2,3])";

    public string Description => "輸出陣列所有子集，使用 start 指標進行二元決策。";

    private static readonly int[] DefaultInput = [1, 2, 3];

    public void Run()
    {
        Console.WriteLine($"=== {Title} ===");
        Console.WriteLine($"輸入：[{string.Join(", ", DefaultInput)}]");

        var results = new List<List<int>>();
        var path = new List<int>();
        Backtrack(0, DefaultInput, path, results);

        Console.WriteLine($"找到 2^{DefaultInput.Length} = {results.Count} 個子集：");
        foreach (var s in results)
        {
            Console.WriteLine($"  [{string.Join(", ", s)}]");
        }
    }

    /// <summary>
    /// 遞迴主函式：每進入函式即視為一個子集。
    /// </summary>
    /// <param name="start">本層可選的最小索引。</param>
    /// <param name="nums">輸入陣列。</param>
    /// <param name="path">當前子集。</param>
    /// <param name="results">收集所有解。</param>
    private static void Backtrack(int start, int[] nums, List<int> path, List<List<int>> results)
    {
        results.Add([.. path]);

        for (int i = start; i < nums.Length; i++)
        {
            // 選擇
            path.Add(nums[i]);

            // 遞迴
            Backtrack(i + 1, nums, path, results);

            // 撤銷
            path.RemoveAt(path.Count - 1);
        }
    }
}
