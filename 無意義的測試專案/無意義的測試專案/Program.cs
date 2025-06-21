namespace 無意義的測試專案;

// 主程式類別，負責程式進入點與主要執行流程
class Program
{
    /// <summary>
    /// Main 函式為程式進入點，負責顯示歡迎訊息並輸出 9*9 乘法表。
    /// </summary>
    /// <param name="args">命令列參數陣列</param>
    static void Main(string[] args)
    {
        // 輸出歡迎訊息到主控台
        Console.WriteLine("Hello, World!");

        // 9*9 乘法表
        // 外層 for 迴圈，i 代表乘數，從 1 執行到 9
        for (int i = 1; i <= 9; i++)
        {
            // 內層 for 迴圈，j 代表被乘數，從 1 執行到 9
            for (int j = 1; j <= 9; j++)
            {
                // 輸出目前的乘法運算結果，格式為 i*j=結果，並以 tab 分隔
                Console.Write($"{i}*{j}={i * j}\t");
            }
            // 每完成一列乘法後換行
            Console.WriteLine();
        }
    }
}
