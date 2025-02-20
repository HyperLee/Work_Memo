namespace 陣列計算最大_最小_平均值
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 10, 15, 50, 65, 34, 80, 90, 45 };

            // 找出最大值
            int max = array.Max();
            // 找出最小值
            int min = array.Min();
            // 計算平均值
            double average = array.Average();

            Console.WriteLine("陣列中的最大值是: " + max);
            Console.WriteLine("陣列中的最小值是: " + min);
            Console.WriteLine("陣列的平均值是: " + average);

            //------ 不使用API方式求解 ------
            // 初始化最大值、最小值和總和
            int maxValue = array[0];
            int minValue = array[0];
            int sum = 0;

            // 使用迴圈計算最大值、最小值和總和
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                }

                if (array[i] < minValue)
                {
                    minValue = array[i];
                }

                sum += array[i];
            }

            // 計算平均值
            double averageValue = (double)sum / array.Length;

            // 輸出結果
            Console.WriteLine($"最大值: {maxValue}");
            Console.WriteLine($"最小值: {minValue}");
            Console.WriteLine($"平均值: {averageValue:F2}");

        }
    }
}
