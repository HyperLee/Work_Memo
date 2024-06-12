namespace 計數排序
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 4, 2, 2, 8, 3, 3, 1 };
            Console.WriteLine("Original array: ");
            PrintArray(array);
            CountingSortAlgorithm(array);
            Console.WriteLine("\nSorted array: ");
            PrintArray(array);

            Console.ReadKey();
        }


        /// <summary>
        /// 計數排序
        /// </summary>
        /// <param name="arr"></param>
        public static void CountingSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            int[] output = new int[n];

            // Find the maximum element of the array
            int max = arr[0];
            for (int i = 1; i < n; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }

            // Create a count array to store count of individual elements
            int[] count = new int[max + 1];

            // Initialize count array with all zeros
            for (int i = 0; i <= max; ++i)
            {
                count[i] = 0;
            }

            // Store count of each character
            for (int i = 0; i < n; ++i)
            {
                ++count[arr[i]];
            }

            // Change count[i] so that count[i] now contains actual position of this element in output array
            // 修改計數數組
            for (int i = 1; i <= max; ++i)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            // 構建排序後的數組
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[arr[i]] - 1] = arr[i];
                --count[arr[i]];
            }

            // Copy the output array to arr, so that arr now contains sorted characters
            // 複製回原始數組
            for (int i = 0; i < n; ++i)
            {
                arr[i] = output[i];
            }
        }


        /// <summary>
        /// print
        /// </summary>
        /// <param name="arr"></param>
        public static void PrintArray(int[] arr)
        {
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
        }
    }
}
