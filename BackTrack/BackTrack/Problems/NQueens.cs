namespace BackTrack.Problems;

/// <summary>
/// 八皇后（N-Queens）：在 N×N 棋盤放置 N 個皇后使其互不攻擊。
/// 解法：逐列放置，以三組布林陣列分別標記「行」「主對角線 row-col」「副對角線 row+col」是否被佔。
/// 時間複雜度 O(N!)，空間複雜度 O(N)。
/// </summary>
public sealed class NQueens : IBacktrackingProblem
{
    public string Title => "N-Queens 八皇后 (n=8)";

    public string Description => "於 N×N 棋盤放置 N 個皇后，使其互不攻擊。";

    private const int N = 8;

    public void Run()
    {
        Console.WriteLine($"=== {Title} ===");
        Console.WriteLine($"輸入：n = {N}");

        var results = new List<int[]>();
        var queens = new int[N];                  // queens[row] = col
        var cols = new bool[N];
        var diag1 = new bool[2 * N - 1];          // row - col + (N-1)
        var diag2 = new bool[2 * N - 1];          // row + col

        Backtrack(0, queens, cols, diag1, diag2, results);

        Console.WriteLine($"共找到 {results.Count} 組解，列印前 3 組棋盤：");
        for (int i = 0; i < Math.Min(3, results.Count); i++)
        {
            Console.WriteLine($"--- 解 #{i + 1} ---");
            PrintBoard(results[i]);
        }
    }

    /// <summary>
    /// 遞迴主函式：逐列嘗試放置皇后。
    /// </summary>
    /// <param name="row">當前要放置皇后的列。</param>
    /// <param name="queens">queens[r] 表示第 r 列的皇后所在行。</param>
    /// <param name="cols">行佔用標記。</param>
    /// <param name="diag1">主對角線（row-col）佔用標記。</param>
    /// <param name="diag2">副對角線（row+col）佔用標記。</param>
    /// <param name="results">收集所有解。</param>
    private static void Backtrack(
        int row,
        int[] queens,
        bool[] cols,
        bool[] diag1,
        bool[] diag2,
        List<int[]> results)
    {
        if (row == N)
        {
            results.Add((int[])queens.Clone());
            return;
        }

        for (int col = 0; col < N; col++)
        {
            int d1 = row - col + (N - 1);
            int d2 = row + col;

            // 剪枝：行 / 主對角線 / 副對角線任一被占用即跳過
            if (cols[col] || diag1[d1] || diag2[d2])
            {
                continue;
            }

            // 選擇
            queens[row] = col;
            cols[col] = diag1[d1] = diag2[d2] = true;

            // 遞迴
            Backtrack(row + 1, queens, cols, diag1, diag2, results);

            // 撤銷
            cols[col] = diag1[d1] = diag2[d2] = false;
        }
    }

    private static void PrintBoard(int[] queens)
    {
        for (int r = 0; r < queens.Length; r++)
        {
            var row = new char[queens.Length];
            for (int c = 0; c < queens.Length; c++)
            {
                row[c] = queens[r] == c ? 'Q' : '.';
            }

            Console.WriteLine(string.Join(' ', row));
        }
    }
}
