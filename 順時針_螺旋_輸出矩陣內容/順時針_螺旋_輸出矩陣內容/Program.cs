namespace 順時針_螺旋_輸出矩陣內容;

class Program
{
    static void Main(string[] args)
    {
        // 建立測試用的矩陣
        int[][] matrix = new int[][] {
            new int[] {1, 2, 3},
            new int[] {4, 5, 6},
            new int[] {7, 8, 9}
        };
        
        // 執行螺旋順序輸出，取得結果
        IList<int> result = SpiralOrder(matrix);
        // 輸出結果到主控台
        Console.WriteLine("res: " + string.Join(", ", result));
    }

    /// <summary>
    /// 方向陣列，依序為：右、下、左、上
    /// {0, 1}: 向右移動
    /// {1, 0}: 向下移動
    /// {0, -1}: 向左移動
    /// {-1, 0}: 向上移動
    /// </summary>
    private static readonly int[,] DIRS = {{0, 1}, {1, 0}, {0, -1}, {-1, 0}};

    /// <summary>
    /// 以順時針螺旋順序回傳矩陣中的所有元素
    /// 
    /// Returns the elements of the matrix in spiral order.
    /// 解題概念：直接依據提目描述方式,(模擬)螺旋走矩陣
    /// 使用方向數組 DIRS 來表示四個方向（右、下、左、上），
    /// 並用 di 來追蹤當前方向。每次走一步後，檢查下一步是否出界或已訪問過，
    /// 如果是，則右轉 90°。重複這個過程直到遍歷完整個矩陣。
    /// 
    /// 根據題意，我們從左上角( 0 ,0 )出發，依照「右下左上」(螺旋, 順時鐘)的順序前進：
    /// 首先向右走，如果到達矩陣邊界，則向右轉9 0 度，前進方向變為向下。
    /// 然後向下走，如果到達矩陣邊界，則向右轉9 0 度，前進方向變為向左。
    /// 然後向左走，如果到達矩陣邊界，則向右轉9 0 度 ，前進方向變為向上。
    /// 每次走到底(邊界位置)就轉向，直到所有元素都被訪問過。
    /// 遇到標記過的元素或者邊界時，就轉向。
    /// 
    /// nextRow < 0: 檢查是否會超出矩陣的上邊界
    /// nextRow >= m: 檢查是否會超出矩陣的下邊界
    /// nextCol < 0: 檢查是否會超出矩陣的左邊界
    /// nextCol >= n: 檢查是否會超出矩陣的右邊界
    /// matrix[nextRow][nextCol] == int.MaxValue: 檢查下一個位置是否已被訪問過
    /// 我們使用 int.MaxValue 作為已訪問的標記
    /// </summary>
    /// <param name="matrix">輸入的二維整數陣列</param>
    /// <returns>依螺旋順序排列的一維整數串列</returns>
    public static IList<int> SpiralOrder(int[][] matrix)
    {
        int m = matrix.Length; // 矩陣的列數
        if(m == 0)
        {
            // 若矩陣為空，直接回傳空串列
            return new List<int>();
        }

        int n = matrix[0].Length; // 矩陣的行數

        List<int> res = new List<int>(m * n); // 儲存結果

        int i = 0; // 當前列索引
        int j = 0; // 當前行索引
        int di = 0; // 當前方向索引

        // 走訪所有元素
        for(int k = 0; k < m * n; k++)
        {
            res.Add(matrix[i][j]); // 加入目前元素
            matrix[i][j] = int.MaxValue; // 標記已拜訪過

            // 計算下一步的位置
            int newRow = i + DIRS[di, 0];
            int newCol = j + DIRS[di, 1];

            // 若超出邊界或已拜訪過，則換方向
            if (newRow < 0 || newRow >= m || newCol < 0 || newCol >= n || matrix[newRow][newCol] == int.MaxValue)
            {
                di = (di + 1) % 4; // 方向循環切換
            }

            // 依照目前方向移動
            i += DIRS[di, 0];
            j += DIRS[di, 1];
        }
        return res;
    }
}
