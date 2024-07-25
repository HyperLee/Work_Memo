using System.Globalization;

namespace 插入排序
{
    internal class Program
    {
        /// <summary>
        /// 插入排序
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array = { 12, 11, 13, 5, 6 };
            InsertionSort(array);
            Console.WriteLine("Sorted array:");
            PrintArray(array);
            Console.ReadKey();
        }


        /// <summary>
        /// 插入排序
        /// 1. 初始狀態： 將第一個元素視為已經排序。
        /// 2. 取下未排序的元素： 從待排序的元素中取出第一個元素。
        /// 3. 插入到已排序的序列： 從後向前掃描已排序的序列，找到比它小的元素，將所有比它大的元素向後移一位。
        /// 4. 插入元素： 將新元素插入到找到的位置後。
        /// 5. 重複步驟2-4： 直到所有元素都被插入。
        /// 
        /// 
        /// 
        /// 外層迴圈 (i)：從第二個元素開始，逐一檢查每個元素。
        /// 內層迴圈 (j)：從當前元素的前一個位置開始，向左掃描，找到插入位置。
        /// key: 保存當前要插入的元素，以便在移動元素時不丟失。
        /// </summary>
        /// <param name="array"></param>
        static void InsertionSort(int[] array)
        {
            int n = array.Length;
            for(int i = 1; i < n; i++)
            {
                //  將第一個元素視為已經排序。
                int key = array[i];
                // 從待排序的元素中取出第一個元素。
                int j = i - 1;

                // 從後向前掃描已排序的序列，找到比它小的元素，將所有比它大的元素向後移一位。
                // 將 key 插入到已排序的 arr[1..i-1] 中
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                // 將新元素插入到找到的位置後。
                array[j + 1] = key;
            }
        }


        /// <summary>
        /// 輸出
        /// </summary>
        /// <param name="array"></param>
        static void PrintArray(int[] array)
        {
            foreach (int item in array)
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine();
        }

    }
}
