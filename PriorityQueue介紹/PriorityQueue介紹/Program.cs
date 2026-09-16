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
    /// </summary>
    /// <param name="args">程式執行參數；固定 smoke test 不需額外參數。</param>
    static void Main(string[] args)
    {
        QueueStep[] expectedSteps =
        [
            new(3, 2, 2),
            new(2, 3, 3),
            new(1, 1, 1)
        ];
        QueueStep[] actualSteps = DrainDemoQueue(CreateDemoQueue());

        int passedChecks = 0;
        Console.WriteLine("Case: 預設比較器依數字優先權由小到大出隊");
        for (int i = 0; i < expectedSteps.Length; i++)
        {
            passedChecks += PrintCheck($"Step {i + 1} Count", expectedSteps[i].Count, actualSteps[i].Count);
            passedChecks += PrintCheck($"Step {i + 1} Peek", expectedSteps[i].Peek, actualSteps[i].Peek);
            passedChecks += PrintCheck($"Step {i + 1} Dequeue", expectedSteps[i].Dequeued, actualSteps[i].Dequeued);
        }

        int totalChecks = expectedSteps.Length * 3;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    private sealed record QueueStep(int Count, int Peek, int Dequeued);

    /// <summary>
    /// 建立包含原始示範資料的全新優先佇列。
    /// </summary>
    /// <returns>元素為 1、2、3，對應優先權為 100、10、50 的佇列。</returns>
    private static PriorityQueue<int, int> CreateDemoQueue()
    {
        PriorityQueue<int, int> priorityQueue = new();

        // 預設比較器以較小的數字優先權先出隊。
        priorityQueue.Enqueue(1, 100);
        priorityQueue.Enqueue(2, 10);
        priorityQueue.Enqueue(3, 50);

        return priorityQueue;
    }

    /// <summary>
    /// 逐步記錄佇列在出隊前的 Count、Peek 與實際出隊元素。
    /// </summary>
    /// <param name="priorityQueue">要完整取出的非空整數優先佇列。</param>
    /// <returns>依出隊順序排列的觀察結果；輸入佇列會被清空。</returns>
    private static QueueStep[] DrainDemoQueue(PriorityQueue<int, int> priorityQueue)
    {
        List<QueueStep> steps = [];
        while (priorityQueue.Count > 0)
        {
            // Peek 與 Dequeue 必須指向同一個當前最小優先權元素。
            int count = priorityQueue.Count;
            int peek = priorityQueue.Peek();
            int dequeued = priorityQueue.Dequeue();
            steps.Add(new(count, peek, dequeued));
        }

        return [.. steps];
    }

    /// <summary>
    /// 輸出單一整數檢查的 Expected、Actual 與 PASS/FAIL 結果。
    /// </summary>
    /// <param name="name">可辨識檢查階段與欄位的名稱。</param>
    /// <param name="expected">手動推導的預期整數。</param>
    /// <param name="actual">執行佇列操作得到的實際整數。</param>
    /// <returns>檢查通過時回傳 1，否則回傳 0，供總結累計。</returns>
    private static int PrintCheck(string name, int expected, int actual)
    {
        bool passed = expected == actual;
        Console.WriteLine($"Check: {name}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Actual: {actual}");
        Console.WriteLine($"PASS-FAIL: {(passed ? "PASS" : "FAIL")}");
        Console.WriteLine();
        return passed ? 1 : 0;
    }
}