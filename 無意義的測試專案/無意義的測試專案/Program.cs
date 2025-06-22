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
        // 測試資料陣列 1，內容為 9, 5, 2, 7, 1, 8, 3, 6, 4
        int[] arr1 = { 9, 5, 2, 7, 1, 8, 3, 6, 4 };
        // 測試資料陣列 2，內容為 10, 20, 15, 3, 8
        int[] arr2 = { 10, 20, 15, 3, 8 };
        // 測試資料陣列 3，內容為 1
        int[] arr3 = { 1 };
        // 對 arr1 執行氣泡排序
        BubbleSort(arr1);
        // 對 arr2 執行氣泡排序
        BubbleSort(arr2);
        // 對 arr3 執行氣泡排序
        BubbleSort(arr3);
        // 輸出 arr1 排序結果
        Console.WriteLine("氣泡排序結果1：" + string.Join(", ", arr1));
        // 輸出 arr2 排序結果
        Console.WriteLine("氣泡排序結果2：" + string.Join(", ", arr2));
        // 輸出 arr3 排序結果
        Console.WriteLine("氣泡排序結果3：" + string.Join(", ", arr3));
    }

    /// <summary>
    /// 氣泡排序演算法，將整數陣列由小到大排序。
    /// </summary>
    /// <param name="arr">要排序的整數陣列</param>
    static void BubbleSort(int[] arr)
    {
        // 取得陣列長度
        int n = arr.Length;
        // 外層 for 迴圈，控制排序回合數
        for (int i = 0; i < n - 1; i++)
        {
            // 內層 for 迴圈，進行相鄰元素比較與交換
            for (int j = 0; j < n - i - 1; j++)
            {
                // 如果前一個元素大於後一個元素，則交換兩者
                if (arr[j] > arr[j + 1])
                {
                    // 暫存 arr[j] 的值
                    int temp = arr[j];
                    // 將 arr[j+1] 的值賦給 arr[j]
                    arr[j] = arr[j + 1];
                    // 將暫存的值賦給 arr[j+1]
                    arr[j + 1] = temp;
                }
            }
        }
    }
}
