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
            // 建立一個無向圖的鄰接表表示法
            // 每個節點都存儲了與其相連的所有節點列表
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 2, 3 } },   // 節點1連接到節點2和3
                { 2, new List<int> { 1, 4 } },   // 節點2連接到節點1和4
                { 3, new List<int> { 1, 4, 5 } },// 節點3連接到節點1,4,5
                { 4, new List<int> { 2, 3, 6 } },// 節點4連接到節點2,3,6
                { 5, new List<int> { 3, 6 } },   // 節點5連接到節點3和6
                { 6, new List<int> { 4, 5 } }    // 節點6連接到節點4和5
            };

            // 定義搜尋的起點和終點節點
            int start = 1;  // 從節點1開始
            int target = 6; // 到節點6結束

            // 初始化雙向BFS搜尋器並執行搜尋
            var bfs = new BidirectionalBFS();
            int distance = bfs.FindShortestPath(graph, start, target);

            // 輸出搜尋結果，如果返回-1表示沒有找到路徑
            Console.WriteLine($"從節點 {start} 到節點 {target} 的最短距離是: {distance}");
        }
    }

    /// <summary>
    /// 實作雙向廣度優先搜尋演算法的類別
    /// </summary>
    public class BidirectionalBFS
    {
        /// <summary>
        /// 尋找從起點到終點的最短路徑距離
        /// </summary>
        /// <param name="graph">圖的鄰接表表示</param>
        /// <param name="start">起始節點</param>
        /// <param name="target">目標節點</param>
        /// <returns>最短路徑的長度，如果無法到達則返回-1</returns>
        public int FindShortestPath(Dictionary<int, List<int>> graph, int start, int target)
        {
            if (graph == null || !graph.ContainsKey(start) || !graph.ContainsKey(target))
            {
                throw new ArgumentException("無效的圖形或起始/終點節點");
            }

            if (start == target)
            {
                return 0; // 起點和終點相同，距離為0
            }

            // 初始化兩個方向的搜尋隊列
            var startQueue = new Queue<int>();    // 從起點開始搜尋的隊列
            var endQueue = new Queue<int>();      // 從終點開始搜尋的隊列

            // 記錄已訪問的節點，避免重複訪問
            var startVisited = new HashSet<int>(); // 從起點方向已訪問的節點集合
            var endVisited = new HashSet<int>();   // 從終點方向已訪問的節點集合

            // 將起點和終點加入各自的隊列和已訪問集合
            startQueue.Enqueue(start);
            endQueue.Enqueue(target);
            startVisited.Add(start);
            endVisited.Add(target);

            int level = 0;  // 記錄當前搜尋的層數（距離）

            // 當兩個方向的搜尋隊列都不為空時繼續搜尋
            while (startQueue.Count > 0 && endQueue.Count > 0)
            {
                level++; // 每次迭代增加一層

                // 優化：總是從較小的隊列開始搜尋，以減少搜尋空間
                if (startQueue.Count > endQueue.Count)
                {
                    // 交換兩個方向的隊列和已訪問集合
                    (startQueue, endQueue) = (endQueue, startQueue);
                    (startVisited, endVisited) = (endVisited, startVisited);
                }

                // 處理當前層的所有節點
                int levelSize = startQueue.Count;
                for (int i = 0; i < levelSize; i++)
                {
                    int node = startQueue.Dequeue(); // 取出當前要處理的節點

                    // 檢查當前節點的所有相鄰節點
                    foreach (var neighbor in graph[node])
                    {
                        // 如果在另一個方向的搜尋中已經訪問過這個節點
                        // 表示兩個方向的搜尋相遇，找到了最短路徑
                        if (endVisited.Contains(neighbor))
                            return level;

                        // 如果這個節點在當前方向還沒有被訪問過
                        if (!startVisited.Contains(neighbor))
                        {
                            startVisited.Add(neighbor);     // 標記為已訪問
                            startQueue.Enqueue(neighbor);    // 加入隊列等待處理
                        }
                    }
                }
            }

            return -1; // 如果搜尋結束仍未找到路徑，返回-1
        }
    }
}
