namespace BackTrack.Problems;

/// <summary>
/// 數獨求解（Sudoku Solver）：解 9×9 數獨。
/// 解法：以 rows / cols / boxes 三組 bool[9,10] 標記每行、每列、每 3×3 宮已使用的數字，遇衝突剪枝。
/// 時間複雜度 最壞 O(9^(空格數))，空間複雜度 O(1)（固定 9×9 棋盤）。
/// </summary>
public sealed class SudokuSolver : IBacktrackingProblem
{
    public string Title => "Sudoku Solver 數獨求解";

    public string Description => "回溯求解 9×9 數獨，三組布林標記避免衝突。";

    // 0 表示空格
    private static readonly int[,] DefaultPuzzle =
    {
        { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
        { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
        { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
        { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
        { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
        { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
        { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
        { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
        { 0, 0, 0, 0, 8, 0, 0, 7, 9 },
    };

    public void Run()
    {
        Console.WriteLine($"=== {Title} ===");

        var board = (int[,])DefaultPuzzle.Clone();
        var rows = new bool[9, 10];
        var cols = new bool[9, 10];
        var boxes = new bool[9, 10];

        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                int v = board[r, c];
                if (v != 0)
                {
                    rows[r, v] = cols[c, v] = boxes[BoxIndex(r, c), v] = true;
                }
            }
        }

        Console.WriteLine("輸入棋盤：");
        PrintBoard(board);

        if (Solve(board, rows, cols, boxes))
        {
            Console.WriteLine("解出來的棋盤：");
            PrintBoard(board);
        }
        else
        {
            Console.WriteLine("無解。");
        }
    }

    /// <summary>
    /// 遞迴主函式：找下一個空格嘗試填入 1-9。
    /// </summary>
    /// <returns>true 表示已成功解出整個棋盤。</returns>
    private static bool Solve(int[,] board, bool[,] rows, bool[,] cols, bool[,] boxes)
    {
        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                if (board[r, c] != 0)
                {
                    continue;
                }

                int b = BoxIndex(r, c);
                for (int v = 1; v <= 9; v++)
                {
                    // 剪枝：行 / 列 / 宮任一已有此數即跳過
                    if (rows[r, v] || cols[c, v] || boxes[b, v])
                    {
                        continue;
                    }

                    // 選擇
                    board[r, c] = v;
                    rows[r, v] = cols[c, v] = boxes[b, v] = true;

                    // 遞迴
                    if (Solve(board, rows, cols, boxes))
                    {
                        return true;
                    }

                    // 撤銷
                    board[r, c] = 0;
                    rows[r, v] = cols[c, v] = boxes[b, v] = false;
                }

                return false;
            }
        }

        return true;
    }

    private static int BoxIndex(int r, int c) => (r / 3) * 3 + (c / 3);

    private static void PrintBoard(int[,] board)
    {
        for (int r = 0; r < 9; r++)
        {
            if (r % 3 == 0 && r > 0)
            {
                Console.WriteLine("------+-------+------");
            }

            for (int c = 0; c < 9; c++)
            {
                if (c % 3 == 0 && c > 0)
                {
                    Console.Write("| ");
                }

                int v = board[r, c];
                Console.Write(v == 0 ? ". " : $"{v} ");
            }

            Console.WriteLine();
        }
    }
}
