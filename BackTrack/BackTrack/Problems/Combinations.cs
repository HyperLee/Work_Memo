namespace BackTrack.Problems;

/// <summary>
/// 組合（Combinations）：從 1..n 中選 k 個的所有組合。
/// 解法：以 start 指標推進避免重複組合，path.Count == k 時收集。
/// 時間複雜度 O(C(n,k) · k)，空間複雜度 O(k)。
/// </summary>
public sealed class Combinations : IBacktrackingProblem
{
    public string Title => "Combinations 組合 (n=4, k=2)";

    public string Description => "從 1..n 中挑 k 個的所有組合，使用 start 指標避免重複。";

    private const int N = 4;
    private const int K = 2;

    public void Run()
    {
        Console.WriteLine($"=== {Title} ===");
        Console.WriteLine($"輸入：n = {N}, k = {K}");

        var results = new List<List<int>>();
        var path = new List<int>();
        Backtrack(1, N, K, path, results);

        Console.WriteLine($"找到 C({N},{K}) = {results.Count} 個組合：");
        foreach (var c in results)
        {
            Console.WriteLine($"  [{string.Join(", ", c)}]");
        }
    }

    /// <summary>
    /// 遞迴主函式。
    /// </summary>
    /// <param name="start">本層可選的最小數字。</param>
    /// <param name="n">範圍上界。</param>
    /// <param name="k">需選的元素個數。</param>
    /// <param name="path">當前已選元素。</param>
    /// <param name="results">收集所有解。</param>
    private static void Backtrack(int start, int n, int k, List<int> path, List<List<int>> results)
    {
        if (path.Count == k)
        {
            results.Add([.. path]);
            return;
        }

        // 剪枝：剩餘元素不足以填滿 k 個時提前結束
        int need = k - path.Count;
        int maxStart = n - need + 1;

        for (int i = start; i <= maxStart; i++)
        {
            // 選擇
            path.Add(i);

            // 遞迴
            Backtrack(i + 1, n, k, path, results);

            // 撤銷
            path.RemoveAt(path.Count - 1);
        }
    }
}
