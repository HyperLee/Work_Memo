namespace 雙向廣度優先搜尋
{
    internal class Program
    {
        /// <summary>
        /// 雙向廣度優先搜尋（Bidirectional BFS）介紹
        /// 雙向廣度優先搜尋（Bidirectional BFS）是一種圖搜尋演算法，它同時從起點（Start）和終點（Goal）
        /// 進行 BFS 搜索，直到兩端搜尋相遇。這種方法適用於 無向圖 或 有向雙向可達圖，可大幅降低搜尋空間
        /// 與時間複雜度。
        /// 
        /// 為什麼使用雙向 BFS？
        /// 普通 BFS 從起點開始，一層層擴展節點，時間複雜度為 O(b^d)（其中 b 為分支因子，d 為目標距離）。
        /// 雙向 BFS 兩端同時擴展，平均僅需 O(b^(d/2) + b^(d/2))，大幅降低計算量。
        /// 
        /// 適用場景
        /// 最短路徑搜尋
        ///     例如：迷宮求解、社交網路最短連接、AI 路徑規劃
        /// 圖遍歷問題
        ///     例如：字詞變換（Word Ladder）、棋盤移動問題
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 建立測試圖
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 2, 3 } },
                { 2, new List<int> { 1, 4 } },
                { 3, new List<int> { 1, 4, 5 } },
                { 4, new List<int> { 2, 3, 6 } },
                { 5, new List<int> { 3, 6 } },
                { 6, new List<int> { 4, 5 } }
            };

            int start = 1;
            int target = 6;

            var bfs = new BidirectionalBFS();
            int distance = bfs.FindShortestPath(graph, start, target);

            Console.WriteLine($"從節點 {start} 到節點 {target} 的最短距離是: {distance}");
        }
    }

    public class BidirectionalBFS
    {
        public int FindShortestPath(Dictionary<int, List<int>> graph, int start, int target)
        {
            var startQueue = new Queue<int>();
            var endQueue = new Queue<int>();
            var startVisited = new Dictionary<int, int>();  // 節點 -> 距離
            var endVisited = new Dictionary<int, int>();    // 節點 -> 距離

            startQueue.Enqueue(start);
            endQueue.Enqueue(target);
            startVisited[start] = 0;
            endVisited[target] = 0;

            while (startQueue.Count > 0 && endQueue.Count > 0)
            {
                // 從起點擴展
                if (startQueue.Count <= endQueue.Count)
                {
                    int node = startQueue.Dequeue();
                    int dist = startVisited[node];

                    if (endVisited.ContainsKey(node))
                        return dist + endVisited[node];

                    foreach (var neighbor in graph[node])
                    {
                        if (!startVisited.ContainsKey(neighbor))
                        {
                            startVisited[neighbor] = dist + 1;
                            startQueue.Enqueue(neighbor);
                        }
                    }
                }
                // 從終點擴展
                else
                {
                    int node = endQueue.Dequeue();
                    int dist = endVisited[node];

                    if (startVisited.ContainsKey(node))
                        return dist + startVisited[node];

                    foreach (var neighbor in graph[node])
                    {
                        if (!endVisited.ContainsKey(neighbor))
                        {
                            endVisited[neighbor] = dist + 1;
                            endQueue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return -1; // 無法到達目標
        }
    }
}
