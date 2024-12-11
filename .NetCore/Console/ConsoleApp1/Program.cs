// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

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
/// <value></value>
ListNode l1 = new ListNode(2);
l1.next = new ListNode(4);
l1.next.next = new ListNode(3);

ListNode l2 = new ListNode(5);
l2.next = new ListNode(6);
l2.next.next = new ListNode(4);

var res = addTwoNumbers(l1, l2);
while (res != null)
{
    Console.WriteLine("function1:" + res.val);
    //Console.WriteLine(res.val);
    res = res.next;
}
Console.WriteLine("-----------------");

static ListNode addTwoNumbers(ListNode l1, ListNode l2)
{
    // 暫存
    ListNode l3 = new ListNode(0);
    // 輸出答案
    ListNode head = l3;
    int sum = 0;
    while (l1 != null || l2 != null)
    {
        // 判斷是否進位
        sum = sum > 9 ? 1 : 0;

        if (l1 != null)
        {
            sum += l1.val;
            l1 = l1.next;
        }

        if (l2 != null)
        {
            sum += l2.val;
            l2 = l2.next;
        }

        // 儲存在 l3 中
        l3.next = new ListNode(sum % 10);
        l3 = l3.next;
    }

    // 判斷最後一項是否和大於 9，大於則需要再添加一個 1.
    if (sum > 9)
    {
        l3.next = new ListNode(1);
    }

    return head.next;
}


/// <summary>
/// main 以外的 class 宣告
/// 要寫在下方
/// 直接放在最下方最快
/// 
/// 記住 上面給 main 使用
/// 下面給其他人用 
/// </summary> <summary>
class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int x) { val = x; }
}