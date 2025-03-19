namespace PriorityQueue介紹;

class Program
{
    /// <summary>
    /// 在 C# 的 PriorityQueue<TElement, TPriority> 中，數字越小的優先級越高。
    /// 讓我們看看程式碼中加入的元素和它們的優先級：
    /// 1. 數字 1 的優先級是 100
    /// 2. 數字 2 的優先級是 10
    /// 3. 數字 3 的優先級是 50
    ///  當我們執行 Dequeue() 操作時：
    ///  優先級為 10 的元素 (值為 2) 會最先出隊列
    ///  優先級為 50 的元素 (值為 3) 會第二個出隊列
    ///  優先級為 100 的元素 (值為 1) 會最後出隊列
    /// 
    /// 如果您想要改變這個行為，讓數字越大優先級越高，可以將優先級的比較邏輯反轉。
    /// 在建立 PriorityQueue 時使用反向比較器：
    /// 1. var priorityQueue = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
    /// 2. 或是在設定優先級時使用負數：priorityQueue.Enqueue(1, -100);
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 創建一個優先級隊列，存儲整數並按優先級排序
        PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();

        // 添加元素到隊列中，第二個參數是優先級
        priorityQueue.Enqueue(1, 100);
        priorityQueue.Enqueue(2, 10);
        priorityQueue.Enqueue(3, 50);

        // 取出並移除優先級最高的元素
        while (priorityQueue.Count > 0) 
        {
            var item = priorityQueue.Dequeue();
            Console.WriteLine(item);
        }
    }
}
