namespace 無意義的測試專案;

class Program
{
    /// <summary>
    /// 打 >聊天
    /// prompt:提示
    /// insatruction:指令
    /// mode:模式
    /// 
    /// 新功能 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 輸出歡迎訊息
        Console.WriteLine("Hello, World!");

        // 9*9 成法表
        // 外層 for 迴圈控制乘數 i，從 1 到 9
        for (int i = 1; i <= 9; i++)
        {
            // 內層 for 迴圈控制被乘數 j，從 1 到 9
            for (int j = 1; j <= 9; j++)
            {
                // 輸出當前的乘法結果，格式為 i*j=結果，並用 tab 分隔
                Console.Write($"{i}*{j}={i * j}\t");
            }
            // 每完成一列後換行
            Console.WriteLine();
        }
    }
}
