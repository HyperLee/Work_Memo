namespace PriorityQueue介紹;

class Program
{
    /// <summary>
    /// 在 C# 的 PriorityQueue<TElement, TPriority> 中，數字越小的優先級越高。
    /// 讓我們看看程式碼中加入的元素和它們的優先級：
    /// 1. 數字 1 的優先級是 100
    /// 2. 數字 2 的優先級是 10
    /// 3. 數字 3 的優先級是 50
    /// 
    ///  當我們執行 Dequeue() 操作時：
    ///  優先級為 10 的元素 (值為 2) 會最先出隊列
    ///  優先級為 50 的元素 (值為 3) 會第二個出隊列
    ///  優先級為 100 的元素 (值為 1) 會最後出隊列
    /// 
    /// Dequeue 輸出是根據優先級從小到大排序的。
    /// 一開始初始化時候預設決定, 要修改排序的邏輯，必須在建立 PriorityQueue 時使用自定義的比較器。
    /// 
    /// 如果您想要改變這個行為，讓數字越大優先級越高，可以將優先級的比較邏輯反轉。
    /// 在建立 PriorityQueue 時使用反向比較器：
    /// 1. var priorityQueue = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
    /// 2. 或是在設定優先級時使用負數：priorityQueue.Enqueue(1, -100);
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 創建一個優先級隊列，存儲整數並按優先級排序
        // 預設排序是從小到大，數字越小優先級越高 
        // 如果想要數字越大優先級越高，可以使用 Comparer<int>.Create((a, b) => b.CompareTo(a))
        // TElement 你想存的元素（例如字串、物件等），TPriority 用來排序的「優先權」值。通常是 int 或其他可比大小的類型（如 double、DateTime 等）。
        PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();

        // 添加元素到隊列中，第二個參數是優先級
        priorityQueue.Enqueue(1, 100);
        priorityQueue.Enqueue(2, 10);
        priorityQueue.Enqueue(3, 50);

        // 取出並移除優先級最高的元素
        while (priorityQueue.Count > 0) 
        {
            Console.WriteLine("Count:" + priorityQueue.Count);
            Console.WriteLine("Peek(): " + priorityQueue.Peek());
            var item = priorityQueue.Dequeue();
            Console.WriteLine("Dequeue(): " + item);
            Console.WriteLine("-----");
        }
    }
}
