namespace BitwiseSubsequence
{
    class Program
    {
        /// <summary>
        /// 題目：按位OR子序列
        /// 題目描述：
        /// 在一場為了招聘軟體開發人員而舉辦的程式競賽中，有一道有趣的題目涉及按位 OR（Bitwise-OR）運算。
        /// 一個序列的「良度」（goodness）定義為其元素的按位 OR 值。
        /// 給定一個長度為 n 的陣列 arr，你需要找出所有可能的不同「良度」值，這些值是透過選擇陣列中任何嚴格遞增的子序列計算得到的。
        /// 最後，返回的陣列應按照非遞減順序排序。
        /// 
        // 筆記：
        /// 子序列（subsequence）是指可以透過刪除原始序列中的零個或多個元素（但不改變剩餘元素的相對順序）所得到的序列。
        /// 嚴格遞增子序列（strictly increasing subsequence）是指該子序列中的每個元素都嚴格大於其前一個元素。
        /// 一個序列的「良度」（goodness）定義為其所有元素的按位 OR（Bitwise-OR）結果。
        /// 
        /// 例子：
        /// 考慮 n=4 且 arr=[4,2,4,1]。
        /// 可以選擇的嚴格遞增子序列及其對應的「良度」（goodness）值如下：
        /// 空子序列；良度 = 0
        /// [1]；良度 = 1
        /// [2]；良度 = 2
        /// [4]；良度 = 4
        /// [2,4]；良度 = 6
        /// 沒有其他嚴格遞增子序列能夠產生不同的良度值。
        /// 因此，返回的結果是 [0,1,2,4,6]。
        /// </summary>
        static void Main()
        {
            // Test Case 1: 基本案例
            int[] arr1 = { 4, 2, 4, 1 };
            Console.WriteLine("Test Case 1: " + string.Join(", ", GetDistinctGoodnessValues(arr1)));
            
            // Test Case 2: 空陣列
            int[] arr2 = { };
            Console.WriteLine("Test Case 2: " + string.Join(", ", GetDistinctGoodnessValues(arr2)));
            
            // Test Case 3: 單一元素
            int[] arr3 = { 5 };
            Console.WriteLine("Test Case 3: " + string.Join(", ", GetDistinctGoodnessValues(arr3)));
            
        }


        /// <summary>
        /// 計算數組中所有嚴格遞增子序列的按位OR運算結果
        /// 取得所有可能的不同「良度」值（按位 OR 結果）
        /// </summary>
        /// <param name="arr">輸入的整數數組</param>
        /// <returns>所有可能的按位OR結果（良度值）列表</returns>
        static List<int> GetDistinctGoodnessValues(int[] arr)
        {
            // 處理空陣列的情況
            if (arr.Length == 0)
            {
                return new List<int> { 0 };
            }
            
            // 使用HashSet儲存所有不重複的良度值（按位OR的結果）
            HashSet<int> goodnessValues = new HashSet<int>();
            // 用於儲存當前正在處理的遞增子序列
            List<int> currentList = new List<int>();
            
            // 開始遞迴搜索所有可能的子序列
            // 初始參數：數組、起始索引0、初始OR值0、當前序列、結果集合
            FindSubsequences(arr, 0, 0, currentList, goodnessValues);
            
            // 將結果轉換為排序後的列表返回
            List<int> sortedResults = goodnessValues.ToList();
            sortedResults.Sort();
            return sortedResults;
        }


        /// <summary>
        /// 使用回溯法尋找所有嚴格遞增子序列並計算它們的按位OR值
        /// </summary>
        /// <param name="arr">原始數組</param>
        /// <param name="index">當前處理的索引位置</param>
        /// <param name="currentOr">當前子序列的OR運算結果</param>  
        /// <param name="currentSequence">當前正在構建的子序列</param>
        /// <param name="goodnessValues">存儲所有不重複良度值的集合</param>
        static void FindSubsequences(int[] arr, int index, int currentOr, List<int> currentSequence, HashSet<int> goodnessValues)
        {
            // 檢查整數溢出的情況
            if (currentOr < 0)
            {
                throw new OverflowException("OR運算結果溢出");
            }

            // 邊界條件檢查
            if (index > arr.Length)
                return;

            // 將當前的OR值加入結果集合
            // 這包含了空序列的情況（OR值為0）
            goodnessValues.Add(currentOr);

            // 從當前索引開始，嘗試將每個後續元素加入子序列
            // 遍歷陣列中的每個元素，選擇是否加入子序列
            for (int i = index; i < arr.Length; i++)
            {
                // 檢查是否可以將當前元素加入子序列
                // 條件：序列為空 或 當前元素大於序列的最後一個元素（確保嚴格遞增）
                if (currentSequence.Count == 0 || arr[i] > currentSequence.Last())
                {
                    // 將當前元素加入序列
                    currentSequence.Add(arr[i]);
                    
                    // 遞迴處理下一個位置
                    // 更新OR值：將當前元素與之前的OR值進行按位OR運算
                    FindSubsequences(arr, i + 1, currentOr | arr[i], currentSequence, goodnessValues);
                    
                    // 回溯：移除最後加入的元素，以便嘗試其他可能的組合
                    currentSequence.RemoveAt(currentSequence.Count - 1);
                }
            }
        }
    }
}