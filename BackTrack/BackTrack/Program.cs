using BackTrack.Problems;

namespace BackTrack;

/// <summary>
/// 互動式選單入口：列出所有回溯法經典題目，使用者選擇後執行。
/// </summary>
internal static class Program
{
    private static readonly Dictionary<int, IBacktrackingProblem> Problems = new()
    {
        [1] = new NQueens(),
        [2] = new Permutations(),
        [3] = new Combinations(),
        [4] = new Subsets(),
        [5] = new SudokuSolver(),
        [6] = new GenerateParentheses(),
    };

    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            try
            {
                ShowMenu();
                Console.Write("請輸入編號：");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine($"[錯誤] 輸入「{input}」不是有效數字，請重試。\n");
                    continue;
                }

                if (choice == 0)
                {
                    Console.WriteLine("再見！");
                    return;
                }

                IBacktrackingProblem? problem = choice switch
                {
                    int n when Problems.TryGetValue(n, out var p) => p,
                    _ => null,
                };

                if (problem is null)
                {
                    Console.WriteLine($"[錯誤] 編號 {choice} 不存在，請重試。\n");
                    continue;
                }

                Console.WriteLine();
                problem.Run();
                Console.WriteLine();
                Console.WriteLine("按任意鍵返回選單...");
                if (Console.IsInputRedirected)
                {
                    Console.ReadLine();
                }
                else
                {
                    Console.ReadKey(intercept: true);
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[例外] {ex.GetType().Name}: {ex.Message}\n");
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("=========================================");
        Console.WriteLine("  Backtracking 回溯法 經典題目示範");
        Console.WriteLine("=========================================");
        foreach (var (key, problem) in Problems)
        {
            Console.WriteLine($"  {key}) {problem.Title}");
        }

        Console.WriteLine("  0) 離開");
        Console.WriteLine("-----------------------------------------");
    }
}
