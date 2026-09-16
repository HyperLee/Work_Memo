namespace 拓樸排序_DFS;

class Program
{
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
                List<int> order = DfsTopologicalSort(graph.Length, graph);
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
                return DfsTopologicalSort(graph.Length, graph) is null ? "cycle-detected" : "invalid";
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
    /// 執行一個 DFS 拓樸排序案例並輸出統一的驗證結果。
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

        // Stack 由完成時間倒序列舉，該順序正好符合每條邊的起點先於終點。
        List<int> topOrder = new List<int>(stack);
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