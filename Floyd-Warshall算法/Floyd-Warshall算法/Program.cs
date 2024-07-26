namespace Floyd_Warshall算法
{
    internal class Program
    {
        /// <summary>
        /// Floyd-Warshall算法
        /// 
        /// https://docs.google.com/document/d/1l7dlXP1bw4TpNH8_4GXGtsnSkPJ_S9hJqvEQfwTZNeY/edit
        /// 
        /// 參考Leetcode 1334. Find the City With the Smallest Number of Neighbors at a Threshold Distance
        /// https://leetcode.com/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/description/?envType=daily-question&envId=2024-07-26
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int INF = int.MaxValue / 2; // 使用一個足夠大的數來表示無限大
            int[,] graph = 
            {
                { 0, 3, INF, 7 },
                { 8, 0, 2, INF },
                { 5, INF, 0, 1 },
                { 2, INF, INF, 0 }
            };

            int[,] dist = FloydWarshall(graph);

            // 輸出結果
            for (int i = 0; i < dist.GetLength(0); i++)
            {
                for (int j = 0; j < dist.GetLength(1); j++)
                {
                    if (dist[i,j] == INF)
                    {
                        Console.Write("INF ");
                    }
                    else
                    {
                        Console.Write(dist[i,j] + " ");
                    }
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }


        /// <summary>
        /// Floyd-Warshall算法
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        static int[,] FloydWarshall(int[,] graph)
        {
            int INF = int.MaxValue / 2;
            int V = graph.GetLength(0);
            int[,] dist = new int[V, V];

            // 初始化距離矩陣
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    dist[i, j] = graph[i, j];
                }
            }

            // 動態規劃
            for (int k = 0; k < V; k++)
            {
                // 自己到自己距離為0
                dist[k, k] = 0;

                for (int i = 0; i < V; i++)
                {
                    for (int j = 0; j < V; j++)
                    {
                        /*
                        int a1 = dist[i, k];
                        int a2 = dist[j, k];
                        int a3 = dist[i, j];
                        */

                        dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);

                    }
                }
            }

            return dist;
        }
    }
}
