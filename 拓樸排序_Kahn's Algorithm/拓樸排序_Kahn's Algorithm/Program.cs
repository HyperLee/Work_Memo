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
        int total = 0;
        int passed = 0;

        RunCase(
            "DAG",
            "valid",
            () =>
            {
                List<int>[] graph = CreateGraph(6, (5, 2), (5, 0), (4, 0), (4, 1), (2, 3), (3, 1));
                List<int> order = KahnTopologicalSort(graph.Length, graph);
                return IsValidTopologicalOrder(order, graph) ? "valid" : "invalid";
            },
            ref total,
            ref passed);

        RunCase(
            "含環圖",
            "cycle-detected",
            () =>
            {
                List<int>[] graph = CreateGraph(2, (0, 1), (1, 0));
                try
                {
                    _ = KahnTopologicalSort(graph.Length, graph);
                    return "invalid";
                }
                catch (InvalidOperationException)
                {
                    return "cycle-detected";
                }
            },
            ref total,
            ref passed);

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 建立固定的有向圖，供每個測試案例獨立使用。
    /// </summary>
    /// <param name="vertexCount">節點數量。</param>
    /// <param name="edges">由起點指向終點的邊。</param>
    /// <returns>鄰接清單。</returns>
    private static List<int>[] CreateGraph(int vertexCount, params (int From, int To)[] edges)
    {
        List<int>[] graph = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            graph[i] = new List<int>();
        }

        foreach ((int from, int to) in edges)
        {
            graph[from].Add(to);
        }

        return graph;
    }

    /// <summary>
    /// 驗證排序是否包含每個節點一次，且每條邊的起點都排在終點之前。
    /// </summary>
    /// <param name="order">待驗證的拓樸順序。</param>
    /// <param name="graph">原始鄰接清單。</param>
    /// <returns>若順序合法則回傳 true。</returns>
    private static bool IsValidTopologicalOrder(List<int> order, List<int>[] graph)
    {
        if (order is null || order.Count != graph.Length || order.Distinct().Count() != graph.Length)
        {
            return false;
        }

        int[] positions = new int[graph.Length];
        for (int i = 0; i < order.Count; i++)
        {
            positions[order[i]] = i;
        }

        for (int from = 0; from < graph.Length; from++)
        {
            foreach (int to in graph[from])
            {
                if (positions[from] >= positions[to])
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 執行一個 Kahn 拓樸排序案例並輸出統一的驗證結果。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="expected">預期狀態。</param>
    /// <param name="actualFactory">產生實際狀態的函式。</param>
    /// <param name="total">累積案例數。</param>
    /// <param name="passed">累積通過數。</param>
    private static void RunCase(string name, string expected, Func<string> actualFactory, ref int total, ref int passed)
    {
        total++;
        string actual;
        try
        {
            actual = actualFactory();
        }
        catch (Exception exception)
        {
            actual = $"EXCEPTION: {exception.GetType().Name}: {exception.Message}";
        }

        bool isPassed = actual == expected;
        if (isPassed)
        {
            passed++;
        }

        Console.WriteLine($"[{name}]");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Actual: {actual}");
        Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
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