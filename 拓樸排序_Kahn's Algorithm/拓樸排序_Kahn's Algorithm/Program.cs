namespace 拓樸排序_Kahn_s_Algorithm;

class Program
{
    /// <summary>
    /// 拓樸排序 Kahn's Algorithm
    /// Repo 另一個解法是使用 DFS 深度優先搜尋。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 示範圖節點數為 6
        int numVertices = 6;

        // 建立鄰接清單：每個節點的出邊（它指向誰）
        List<int>[] adjList = new List<int>[numVertices];
        for (int i = 0; i < numVertices; i++)
        { 
            adjList[i] = new List<int>();
        }

        // 加入邊：例如 5 → 2, 5 → 0, 4 → 0, 4 → 1, 2 → 3, 3 → 1
        adjList[5].Add(2);
        adjList[5].Add(0);
        adjList[4].Add(0);
        adjList[4].Add(1);
        adjList[2].Add(3);
        adjList[3].Add(1);

        // 執行拓扑排序
        var sorted = KahnTopologicalSort(numVertices, adjList);

        // 輸出結果
        Console.WriteLine("拓扑排序結果: " + string.Join(", ", sorted));
    }


    /// <summary>
    /// 拓扑排序函式，傳入節點數和鄰接清單
    /// </summary>
    /// <param name="numVertices"></param>
    /// <param name="adjList"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static List<int> KahnTopologicalSort(int numVertices, List<int>[] adjList)
    {
        // 步驟 1：初始化每個節點的入度
        int[] inDegree = new int[numVertices];
        foreach (var neighbors in adjList)
        {
            foreach (var neighbor in neighbors)
            {
                // 每個節點被其他節點指向一次，入度就加一
                inDegree[neighbor]++;
            }
        }

        // 步驟 2：建立一個佇列，放入所有入度為 0 的節點
        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < numVertices; i++)
        {
            if (inDegree[i] == 0)
            { 
                // 如果入度為 0，就加入佇列
                queue.Enqueue(i);  
            }
        }

        // 用來儲存拓扑排序結果
        List<int> topOrder = new List<int>();

        // 步驟 3-5：處理佇列中的節點
        while (queue.Count > 0)
        {
            // 取出入度為 0 的節點
            int u = queue.Dequeue();
            // 加入排序結果中
            topOrder.Add(u);  

            // 遍歷這個節點的所有相鄰節點（也就是它指向誰）
            foreach (int neighbor in adjList[u])
            {
                // 將相鄰節點的入度減 1
                inDegree[neighbor]--;  

                // 如果這個相鄰節點入度變成 0，也加入佇列
                if (inDegree[neighbor] == 0)
                    queue.Enqueue(neighbor);
            }
        }

        // 步驟 6：檢查是否有環（結果數量不等於節點數代表有環）
        if (topOrder.Count != numVertices)
        {
            throw new InvalidOperationException("圖中存在環，無法進行拓扑排序");
        }

        return topOrder;
    }
}
