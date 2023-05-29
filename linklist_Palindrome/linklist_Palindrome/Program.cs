using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklist_Palindrome
{
    class Program
    {
        /// <summary>
        /// Structure of node
        /// </summary>
        public class Node
        {
            public char data;
            public Node next;
            public Node prev;
        };


        /// <summary>
        /// Given a reference (pointer to pointer) to
        /// the head of a list and an int, inserts a
        /// new node on the front of the list.
        /// </summary>
        /// <param name="head_ref"></param>
        /// <param name="new_data"></param>
        /// <returns></returns>
        static Node push(Node head_ref, char new_data)
        {
            Node new_node = new Node();
            new_node.data = new_data;
            new_node.next = head_ref;
            new_node.prev = null;
            if (head_ref != null)
                head_ref.prev = new_node;

            head_ref = new_node;
            return head_ref;
        }


        /// <summary>
        /// Function to check if list is palindrome or not
        /// 此為雙向 linklist
        /// 1. 创建一个双向链表，其中每个节点只包含字符串的一个字符
        /// 2. 初始化列表开头左侧和列表末尾右侧的两个指针。
        /// 3. 检查左节点的数据是否等于右节点。如果相等，则向左递增并向右递减直到列表的中间。
        ///    如果在任何阶段都不相等，则返回 false。
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        static bool isPalindrome(Node left)
        {
            if (left == null)
                return true;

            // Find rightmost node
            Node right = left;
            while (right.next != null)
            {
                right = right.next;
            }

            while (left != right)
            {
                if (left.data != right.data)
                    return false;

                left = left.next;
                right = right.prev;
            }
            return true;
        }


        /// <summary>
        /// linklist 回文 範例程式
        /// 给定一个字符的双向链表，编写一个函数，如果给定的双向链表是回文，则返回 true，否则返回 false。 
        /// 
        /// ref:
        /// https://verytoolz.com/blog/ca18cacb6e/
        /// 
        /// 資料夾裡面有附上 圖片說明
        /// 此為雙向 linklist
        /// 1. 创建一个双向链表，其中每个节点只包含字符串的一个字符
        /// 2. 初始化列表开头左侧和列表末尾右侧的两个指针。
        /// 3. 检查左节点的数据是否等于右节点。如果相等，则向左递增并向右递减直到列表的中间。
        ///    如果在任何阶段都不相等，则返回 false。
        /// 
        /// 下列為參考類似題目
        /// https://verytoolz.com/blog/77bbc50698/
        /// 單向linklist
        /// 
        /// https://verytoolz.com/blog/666b4cb9e7/
        /// 循環 cycle linklist
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Node head = null;

            head = push(head, '1');
            head = push(head, '3');
            head = push(head, '4');
            //head = push(head, '1');
            //head = push(head, 'l');

            if (isPalindrome(head))
            {
                Console.Write("It is Palindrome");
            }
            else
            {
                Console.Write("Not Palindrome");
            }

            Console.ReadKey();
        }

        
    }

}
