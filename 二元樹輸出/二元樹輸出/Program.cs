namespace 二元樹輸出
{
    /// <summary>
    /// 
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// 226. Invert Binary Tree
        /// https://leetcode.com/problems/invert-binary-tree/description/?envType=study-plan-v2&envId=top-interview-150
        /// 
        /// 226. 翻转二叉树
        /// https://leetcode.cn/problems/invert-binary-tree/description/
        /// 
        /// 翻轉二元樹 root 不變 左右子樹翻轉
        /// 
        /// InvertTree 是 翻轉後的 答案
        /// 但是 tree 輸出顯示 要依靠
        /// 前中後序遍歷才能輸出樹狀結構
        /// 本題分別使用三種遍歷 展示輸出答案
        /// 
        /// 前中後序, 所謂的前中後是指根節點位置在哪裡
        /// 前序就是 前面
        /// 依此類蓷
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int total = 0;
            int passed = 0;

            RunCase(
                "翻轉完整二元樹",
                "inorder=9,7,6,4,3,2,1;preorder=4,7,9,6,2,3,1;postorder=9,6,7,3,1,2,4",
                () =>
                {
                    TreeNode root = CreateSampleTree();
                    TreeNode inverted = InvertTree(root);
                    return $"inorder={string.Join(",", CollectInOrder(inverted))};" +
                           $"preorder={string.Join(",", CollectPreOrder(inverted))};" +
                           $"postorder={string.Join(",", CollectPostOrder(inverted))}";
                },
                ref total,
                ref passed);

            RunCase(
                "翻轉空樹",
                "null",
                () => InvertTree(null) is null ? "null" : "not-null",
                ref total,
                ref passed);

            Console.WriteLine($"Summary: {passed}/{total} checks passed.");
            if (passed != total)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 建立每個測試案例獨立使用的完整二元樹。
        /// </summary>
        /// <returns>符合翻轉二元樹題目範例的根節點。</returns>
        private static TreeNode CreateSampleTree()
        {
            TreeNode root = new TreeNode(4)
            {
                left = new TreeNode(2),
                right = new TreeNode(7)
            };

            root.left.left = new TreeNode(1);
            root.left.right = new TreeNode(3);
            root.right.left = new TreeNode(6);
            root.right.right = new TreeNode(9);
            return root;
        }

        /// <summary>
        /// 將樹以中序順序收集成清單，供 smoke test 比對而不直接寫入主控台。
        /// </summary>
        /// <param name="node">目前拜訪的節點。</param>
        /// <returns>左、根、右順序的節點值。</returns>
        private static List<int> CollectInOrder(TreeNode node)
        {
            List<int> values = new List<int>();
            CollectInOrder(node, values);
            return values;
        }

        /// <summary>
        /// 遞迴收集中序遍歷結果。
        /// </summary>
        /// <param name="node">目前拜訪的節點。</param>
        /// <param name="values">累積節點值的清單。</param>
        private static void CollectInOrder(TreeNode node, List<int> values)
        {
            if (node == null)
            {
                return;
            }

            CollectInOrder(node.left, values);
            values.Add(node.val);
            CollectInOrder(node.right, values);
        }

        /// <summary>
        /// 將樹以 preorder 順序收集成清單。
        /// </summary>
        /// <param name="node">目前拜訪的節點。</param>
        /// <returns>根、左、右順序的節點值。</returns>
        private static List<int> CollectPreOrder(TreeNode node)
        {
            List<int> values = new List<int>();
            CollectPreOrder(node, values);
            return values;
        }

        /// <summary>
        /// 遞迴收集前序遍歷結果。
        /// </summary>
        /// <param name="node">目前拜訪的節點。</param>
        /// <param name="values">累積節點值的清單。</param>
        private static void CollectPreOrder(TreeNode node, List<int> values)
        {
            if (node == null)
            {
                return;
            }

            values.Add(node.val);
            CollectPreOrder(node.left, values);
            CollectPreOrder(node.right, values);
        }

        /// <summary>
        /// 將樹以 postorder 順序收集成清單。
        /// </summary>
        /// <param name="node">目前拜訪的節點。</param>
        /// <returns>左、右、根順序的節點值。</returns>
        private static List<int> CollectPostOrder(TreeNode node)
        {
            List<int> values = new List<int>();
            CollectPostOrder(node, values);
            return values;
        }

        /// <summary>
        /// 遞迴收集後序遍歷結果。
        /// </summary>
        /// <param name="node">目前拜訪的節點。</param>
        /// <param name="values">累積節點值的清單。</param>
        private static void CollectPostOrder(TreeNode node, List<int> values)
        {
            if (node == null)
            {
                return;
            }

            CollectPostOrder(node.left, values);
            CollectPostOrder(node.right, values);
            values.Add(node.val);
        }

        /// <summary>
        /// 執行一個固定案例並輸出統一的 Expected、Actual 與 PASS-FAIL 結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="expected">預期結果。</param>
        /// <param name="actualFactory">產生實際結果的函式。</param>
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
        /// https://ithelp.ithome.com.tw/articles/10227341
        /// https://leetcode.cn/problems/invert-binary-tree/solution/fan-zhuan-er-cha-shu-by-leetcode-solution/
        /// 
        /// 採用 遞迴 作法
        /// 將 tree 反轉, 
        /// 1. 判斷 root 是否為 null，若為 null 回傳 root;
        /// 2. 宣告 TreeNode tmpLeft 為 root.left;
        /// 3. 宣告 TreeNode tmpRight 為 root.right;
        /// 4. 此時使用遞迴將所有 TreeNode 對調 
        ///     root.left = InvertTree(tmpRight);
        ///     root.right = InvertTree(tmpLeft);
        ///     對調完成後回傳 root
        ///     
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                // return root, 同樣意思
                return null;
            }

            // 左子樹暫存
            TreeNode tmpleft = root.left;
            // 右子樹暫存
            TreeNode tmpright = root.right;

            // 遞迴 將左右子樹交換存放
            root.left = InvertTree(tmpright);
            root.right = InvertTree(tmpleft);

            return root;
        }


        /// <summary>
        /// 中序遍歷 InOrder Traversal
        /// 訪問順序：左子樹 -> 根節點 -> 右子樹
        /// 特點：對於二元搜尋樹，這種遍歷方式可以用來生成二叉樹的排序序列。
        /// 
        /// 前中後序, 所謂的前中後是指根節點位置在哪裡
        /// 前序就是 前面
        /// 依此類蓷
        /// </summary>
        /// <param name="node"></param>
        public static void InOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            InOrder(node.left);
            Console.Write(node.val + ", ");
            InOrder(node.right);
        }


        /// <summary>
        /// 前序遍歷 PreOrder Traversal
        /// 訪問順序：根節點 -> 左子樹 -> 右子樹
        /// 特點：通常用於複製樹結構或計算樹的高度。
        /// 
        /// 前中後序, 所謂的前中後是指根節點位置在哪裡
        /// 前序就是 前面
        /// 依此類蓷
        /// </summary>
        /// <param name="node"></param>
        public static void PreOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Console.Write(node.val + ", ");
            PreOrder(node.left);
            PreOrder(node.right);
        }


        /// <summary>
        /// 後序遍歷 PostOrder Traversal
        /// 訪問順序：左子樹 -> 右子樹 -> 根節點
        /// 特點：通常用於刪除樹或計算樹的高度。
        /// 
        /// 前中後序, 所謂的前中後是指根節點位置在哪裡
        /// 前序就是 前面
        /// 依此類蓷
        /// </summary>
        /// <param name="node"></param>
        public static void PostOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            PostOrder(node.left);
            PostOrder(node.right);
            Console.Write(node.val + ", ");
        }

    }
}