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
            int inf = int.MaxValue / 2;
            int[][,] graphs =
            {
                new int[,]
                {
                    { 0, 3, inf, 7 },
                    { 8, 0, 2, inf },
                    { 5, inf, 0, 1 },
                    { 2, inf, inf, 0 }
                },
                new int[,]
                {
                    { 0, 4, inf },
                    { inf, 0, 2 },
                    { inf, inf, 0 }
                }
            };
            string[] expected =
            {
                "0,3,5,6;5,0,2,3;3,6,0,1;2,5,7,0",
                "0,4,6;INF,0,2;INF,INF,0"
            };

            int total = 0;
            int passed = 0;
            for (int i = 0; i < graphs.Length; i++)
            {
                int caseIndex = i;
                RunCase(
                    $"graph={caseIndex + 1}",
                    expected[caseIndex],
                    () => MatrixToText(FloydWarshall(CloneMatrix(graphs[caseIndex]))),
                    ref total,
                    ref passed);
            }

            Console.WriteLine($"Summary: {passed}/{total} checks passed.");
            if (passed != total)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 複製距離矩陣，讓每個案例的動態規劃狀態彼此隔離。
        /// </summary>
        /// <param name="matrix">要複製的距離矩陣。</param>
        /// <returns>新的二維距離矩陣。</returns>
        private static int[,] CloneMatrix(int[,] matrix)
        {
            int[,] copy = new int[matrix.GetLength(0), matrix.GetLength(1)];
            Array.Copy(matrix, copy, matrix.Length);
            return copy;
        }

        /// <summary>
        /// 將距離矩陣轉成固定格式，並將無限距離標示為 INF。
        /// </summary>
        /// <param name="matrix">Floyd–Warshall 的結果矩陣。</param>
        /// <returns>以分號分隔列、逗號分隔欄的文字。</returns>
        private static string MatrixToText(int[,] matrix)
        {
            int inf = int.MaxValue / 2;
            List<string> rows = new List<string>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                List<string> values = new List<string>();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    values.Add(matrix[i, j] == inf ? "INF" : matrix[i, j].ToString());
                }

                rows.Add(string.Join(",", values));
            }

            return string.Join(";", rows);
        }

        /// <summary>
        /// 執行一個 Floyd–Warshall 案例並輸出統一的驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期距離矩陣。</param>
        /// <param name="actualFactory">產生實際距離矩陣的函式。</param>
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
        /// Floyd-Warshall算法
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        static int[,] FloydWarshall(int[,] graph)
        {
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