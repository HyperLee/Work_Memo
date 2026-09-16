// 最上層陳述式必須放在本檔案所有型別宣告之前；編譯器會產生隱含的 Main。
Environment.ExitCode = TopLevelEntry.Run();

/// <summary>
/// 提供由最上層陳述式呼叫的預設入口流程與鏈結串列加法示範。
/// </summary>
internal static class TopLevelEntry
{
    /// <summary>
    /// Console 專案路徑
    /// D:\.NetCore\Console
    ///
    /// 紀錄 不需要寫 main{}
    /// 副程式 function 不需要寫 public 這種宣告方式
    /// using 也都不用寫
    ///
    /// function 上方寫呼叫即可使用
    ///
    /// 點選左方 執行與偵錯(欄位)執行(綠色箭頭) 下方(欄位)偵錯主控台就會顯示 console 輸出訊息
    /// </summary>
    /// <returns>所有案例通過時回傳 0，否則回傳 1。</returns>
    internal static int Run()
    {
        int passed = 0;
        int total = 0;

        passed += RunCase("題目範例", new int[] { 7, 0, 8 }, new int[] { 2, 4, 3 }, new int[] { 5, 6, 4 });
        total++;
        passed += RunCase("兩個零", new int[] { 0 }, new int[] { 0 }, new int[] { 0 });
        total++;
        passed += RunCase("連續進位與不同長度", new int[] { 8, 9, 9, 9, 0, 0, 0, 1 }, new int[] { 9, 9, 9, 9, 9, 9, 9 }, new int[] { 9, 9, 9, 9 });
        total++;
        passed += RunCase("尾端產生進位", new int[] { 0, 1 }, new int[] { 5 }, new int[] { 5 });
        total++;

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        return passed == total ? 0 : 1;
    }

    /// <summary>
    /// 建立新鏈結串列輸入、執行加法並輸出案例結果。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="expectedDigits">預期的反向位數。</param>
    /// <param name="leftDigits">第一個加數的反向位數。</param>
    /// <param name="rightDigits">第二個加數的反向位數。</param>
    /// <returns>通過時回傳 1，否則回傳 0。</returns>
    private static int RunCase(string name, int[] expectedDigits, int[] leftDigits, int[] rightDigits)
    {
        ListNode actualList = addTwoNumbers(BuildList(leftDigits), BuildList(rightDigits));
        string expected = FormatDigits(expectedDigits);
        string actual = FormatList(actualList);
        bool isPassed = expected == actual;

        Console.WriteLine($"Case: {name}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Actual: {actual}");
        Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();
        return isPassed ? 1 : 0;
    }

    /// <summary>
    /// 將兩條以反向位數儲存的鏈結串列相加。
    /// </summary>
    /// <param name="l1">第一個非空鏈結串列。</param>
    /// <param name="l2">第二個非空鏈結串列。</param>
    /// <returns>代表總和的新鏈結串列。</returns>
    private static ListNode addTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode dummy = new ListNode(0);
        ListNode tail = dummy;
        int carry = 0;
        ListNode? left = l1;
        ListNode? right = l2;

        while (left != null || right != null)
        {
            int sum = carry;
            if (left != null)
            {
                sum += left.val;
                left = left.next;
            }

            if (right != null)
            {
                sum += right.val;
                right = right.next;
            }

            // 每個節點只保留個位數，十位數留給下一輪。
            carry = sum / 10;
            tail.next = new ListNode(sum % 10);
            tail = tail.next;
        }

        if (carry > 0)
        {
            tail.next = new ListNode(carry);
        }

        return dummy.next!;
    }

    /// <summary>
    /// 由反向位數建立一條新的鏈結串列。
    /// </summary>
    /// <param name="digits">由個位數開始排列的位數。</param>
    /// <returns>新建立的非空鏈結串列。</returns>
    private static ListNode BuildList(int[] digits)
    {
        ListNode head = new ListNode(digits[0]);
        ListNode tail = head;
        for (int i = 1; i < digits.Length; i++)
        {
            tail.next = new ListNode(digits[i]);
            tail = tail.next;
        }

        return head;
    }

    /// <summary>
    /// 將位數陣列轉為便於比對的文字。
    /// </summary>
    /// <param name="digits">反向位數陣列。</param>
    /// <returns>以箭頭連接的位數。</returns>
    private static string FormatDigits(int[] digits)
    {
        return string.Join(" -> ", digits);
    }

    /// <summary>
    /// 將鏈結串列轉為便於比對的文字。
    /// </summary>
    /// <param name="node">鏈結串列首節點。</param>
    /// <returns>以箭頭連接的位數。</returns>
    private static string FormatList(ListNode node)
    {
        List<int> digits = new List<int>();
        for (ListNode? current = node; current != null; current = current.next)
        {
            digits.Add(current.val);
        }

        return FormatDigits(digits.ToArray());
    }
}

/// <summary>
/// 其他 class 要寫在最下方
/// </summary>
internal class ListNode
{
    /// <summary>取得或設定目前節點儲存的位數。</summary>
    public int val;

    /// <summary>取得或設定下一個節點；尾端為 null。</summary>
    public ListNode? next;

    /// <summary>
    /// 建立具有指定數值的節點。
    /// </summary>
    /// <param name="x">節點數值。</param>
    public ListNode(int x)
    {
        val = x;
    }
}