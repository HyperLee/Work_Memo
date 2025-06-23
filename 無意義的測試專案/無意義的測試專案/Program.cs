namespace 無意義的測試專案;

// 主程式類別，負責程式進入點與主要執行流程
class Program
{
    /// <summary>
    /// 拓樸排序_DFS
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 節點數量
        int numVertices = 6;
        // 建立鄰接清單，初始化每個節點的鄰接串列
        List<int>[] adjList = new List<int>[numVertices];
        for (int i = 0; i < numVertices; i++)
        {
            adjList[i] = new List<int>();
        }

        // 建立圖的邊：5→2, 5→0, 4→0, 4→1, 2→3, 3→1
        adjList[5].Add(2);
        adjList[5].Add(0);
        adjList[4].Add(0);
        adjList[4].Add(1);
        adjList[2].Add(3);
        adjList[3].Add(1);

        // 執行 DFS 拓樸排序
        var sorted = DfsTopologicalSort(numVertices, adjList);
        if (sorted == null)
        {
            // 若有環，顯示錯誤訊息
            Console.WriteLine("圖中有環，無法進行拓扑排序");
        }
        else
        {
            // 輸出拓樸排序結果
            Console.WriteLine("拓扑排序結果 (DFS): " + string.Join(", ", sorted));
        }
    }


    /// <summary>
    /// 以 DFS 執行拓樸排序，並偵測有無環
    /// </summary>
    /// <param name="numVertices">節點數</param>
    /// <param name="adjList">鄰接清單</param>
    /// <returns>拓樸排序結果，若有環則回傳 null</returns>
    public static List<int> DfsTopologicalSort(int numVertices, List<int>[] adjList)
    {
        // 記錄哪些節點已訪問
        bool[] visited = new bool[numVertices];
        // 用來偵測遞迴路徑上的環
        bool[] recursionStack = new bool[numVertices];
        // 用來儲存結果（先完成的節點會後進）
        Stack<int> stack = new Stack<int>();

        // 遍歷所有節點，避免遺漏孤立節點
        for (int i = 0; i < numVertices; i++)
        {
            if (!visited[i])
            {
                // 若偵測到環，直接回傳 null
                if (DFS(i, visited, recursionStack, stack, adjList))
                {
                    return null;
                }
            }
        }

        // 將 stack 轉為 list 並反轉，即為拓扑排序結果
        List<int> topOrder = new List<int>(stack);
        topOrder.Reverse();
        return topOrder;
    }


    /// <summary>
    /// 遞迴 DFS 函式，並偵測有無環
    /// </summary>
    /// <param name="node">目前節點</param>
    /// <param name="visited">已訪問標記</param>
    /// <param name="recursionStack">遞迴路徑標記</param>
    /// <param name="stack">結果堆疊</param>
    /// <param name="adjList">鄰接清單</param>
    /// <returns>若偵測到環則回傳 true</returns>
    private static bool DFS(int node, bool[] visited, bool[] recursionStack, Stack<int> stack, List<int>[] adjList)
    {
        // 標記目前節點已訪問，並加入遞迴路徑
        visited[node] = true;
        recursionStack[node] = true;

        // 遍歷所有相鄰節點
        foreach (int neighbor in adjList[node])
        {
            if (!visited[neighbor])
            {
                // 遞迴處理相鄰節點，若偵測到環則回傳 true
                if (DFS(neighbor, visited, recursionStack, stack, adjList))
                    return true;
            }
            else if (recursionStack[neighbor])
            {
                // 若相鄰節點已在遞迴路徑上，表示有環
                return true;
            }
        }

        // 離開遞迴路徑，並將節點推入 stack
        recursionStack[node] = false;
        stack.Push(node);
        return false;
    }
}
