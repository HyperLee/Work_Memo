using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maximum_even_sum
{
    class Program
    {
        // Function to find the
        // maximum even sum of any
        // subsequence of length K
        static int evenSumK(int[] arr, int N, int K)
        {
            // If count of elements
            // is less than K
            if (K > N)
            {
                return -1;
            }

            // Stores maximum even
            // subsequence sum
            int maxSum = 0;

            // Stores Even numbers
            List<int> Even = new List<int>();

            // Stores Odd numbers
            List<int> Odd = new List<int>();

            // Traverse the array
            for (int l = 0; l < N; l++)
            {
                // If current element
                // is an odd number
                if (arr[l] % 2 == 1)
                {
                    // Insert odd number
                    Odd.Add(arr[l]);
                }
                else
                {
                    // Insert even numbers
                    Even.Add(arr[l]);
                }
            }

            // Sort Odd[] array
            Odd.Sort();

            // Sort Even[] array
            Even.Sort();

            // Stores current index
            // Of Even[] array
            int i = Even.Count - 1;

            // Stores current index
            // Of Odd[] array
            int j = Odd.Count - 1;

            while (K > 0)
            {
                // If K is odd
                if (K % 2 == 1)
                {
                    // If count of elements
                    // in Even[] >= 1
                    if (i >= 0)
                    {
                        // Update maxSum
                        maxSum += Even[i];

                        // Update i
                        i--;
                    }

                    // If count of elements
                    // in Even[] array is 0.
                    else
                    {
                        return -1;
                    }

                    // Update K
                    K--;
                }

                // If count of elements
                // in Even[] and odd[] >= 2
                else if (i >= 1 && j >= 1)
                {
                    if (Even[i] + Even[i - 1]
                        <= Odd[j] + Odd[j - 1])
                    {
                        // Update maxSum
                        maxSum += Odd[j] + Odd[j - 1];

                        // Update j
                        j -= 2;
                    }
                    else
                    {
                        // Update maxSum
                        maxSum += Even[i] + Even[i - 1];

                        // Update i
                        i -= 2;
                    }

                    // Update K
                    K -= 2;
                }

                // If count of elements
                // in Even[] array >= 2.
                else if (i >= 1)
                {
                    // Update maxSum
                    maxSum += Even[i] + Even[i - 1];

                    // Update i
                    i -= 2;

                    // Update K
                    K -= 2;
                }

                // If count of elements
                // in Odd[] array >= 2
                else if (j >= 1)
                {
                    // Update maxSum
                    maxSum += Odd[j] + Odd[j - 1];

                    // Update i.
                    j -= 2;

                    // Update K.
                    K -= 2;
                }
            }
            return maxSum;
        }



        /// <summary>
        /// https://www.geeksforgeeks.org/maximum-even-sum-subsequence-of-length-k/
        /// Maximum even sum subsequence of length K
        /// 最大子數列問題
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] arr = { 4, 2, 6, 7, 8 };
            int N = arr.Length;
            int K = 3;
            Console.WriteLine(evenSumK(arr, N, K));
            Console.ReadKey();
        }
    }
}
