namespace BackTrack.Problems;

/// <summary>
/// 括號生成（Generate Parentheses）：產生所有 n 對合法括號組合。
/// 解法：以 open / close 兩計數做剪枝，open &lt; n 才能加 '('，close &lt; open 才能加 ')'。
/// 時間複雜度 O(4^n / sqrt(n))（卡塔蘭數），空間複雜度 O(n)。
/// </summary>
public sealed class GenerateParentheses : IBacktrackingProblem
{
    public string Title => "Generate Parentheses 括號生成 (n=3)";

    public string Description => "產生 n 對合法括號，open<n 與 close<open 雙重剪枝。";

    private const int N = 3;

    public void Run()
    {
        Console.WriteLine($"=== {Title} ===");
        Console.WriteLine($"輸入：n = {N}");

        var results = new List<string>();
        var sb = new System.Text.StringBuilder();
        Backtrack(0, 0, N, sb, results);

        Console.WriteLine($"找到 {results.Count} 種合法字串：");
        foreach (var s in results)
        {
            Console.WriteLine($"  {s}");
        }
    }

    /// <summary>
    /// 遞迴主函式。
    /// </summary>
    /// <param name="open">已使用的左括號數。</param>
    /// <param name="close">已使用的右括號數。</param>
    /// <param name="n">目標對數。</param>
    /// <param name="sb">當前字串。</param>
    /// <param name="results">收集所有解。</param>
    private static void Backtrack(int open, int close, int n, System.Text.StringBuilder sb, List<string> results)
    {
        if (sb.Length == n * 2)
        {
            results.Add(sb.ToString());
            return;
        }

        // 剪枝：左括號還沒用完才能加 '('
        if (open < n)
        {
            sb.Append('(');                               // 選擇
            Backtrack(open + 1, close, n, sb, results);   // 遞迴
            sb.Length--;                                  // 撤銷
        }

        // 剪枝：右括號數量必須小於左括號才能加 ')'
        if (close < open)
        {
            sb.Append(')');                               // 選擇
            Backtrack(open, close + 1, n, sb, results);   // 遞迴
            sb.Length--;                                  // 撤銷
        }
    }
}
