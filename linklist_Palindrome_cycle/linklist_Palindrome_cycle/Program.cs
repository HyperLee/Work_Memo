using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklist_Palindrome_cycle
{
    class Program
    {
        /// <summary>
        /// Link list node
        /// </summary>
        class Node
        {
            public int data;
            public Node next;
        }

        /// <summary>
        /// Function to find loop starting node.
        /// loop_node --> Pointer to one of
        /// the loop nodes head --> Pointer to
        /// the start node of the linked list
        /// </summary>
        /// <param name="loop_node"></param>
        /// <param name="head"></param>
        /// <returns></returns>
        static Node getLoopstart(Node loop_node,Node head)
        {
            Node ptr1 = loop_node;
            Node ptr2 = loop_node;
            // Count the number of nodes in loop
            int k = 1, i;
            while (ptr1.next != ptr2)
            {
                ptr1 = ptr1.next;
                k++;
            }
            // Fix one pointer to head
            ptr1 = head;
            // And the other pointer to k
            // nodes after head
            ptr2 = head;
            for (i = 0; i < k; i++)
            {
                ptr2 = ptr2.next;
            }

            /* Move both pointers at the same pace,
           they will meet at loop starting node */
            while (ptr2 != ptr1)
            {
                ptr1 = ptr1.next;
                ptr2 = ptr2.next;
            }
            return ptr1;
        }


        /// <summary>
        /// This function detects and find
        /// loop starting node in the list
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        static Node detectAndgetLoopstarting(Node head)
        {
            Node slow_p = head, fast_p = head, loop_start = null;
            //Start traversing list and detect loop
            while (slow_p != null && fast_p != null && fast_p.next != null)
            {
                slow_p = slow_p.next;
                fast_p = fast_p.next.next;
                /* If slow_p and fast_p meet then find the loop starting node*/
                if (slow_p == fast_p)
                {
                    loop_start = getLoopstart(slow_p, head);
                    break;
                }
            }
            // Return starting node of loop
            return loop_start;

        }


        /// <summary>
        /// Utility function to check if
        /// a linked list with loop is
        /// palindrome with given starting point.
        /// </summary>
        /// <param name="head"></param>
        /// <param name="loop_start"></param>
        /// <returns></returns>
        static bool isPalindromeUtil(Node head, Node loop_start)
        {
            Node ptr = head;
            Stack<int> s = new Stack<int>();
            // Traverse linked list until last node
            // is equal to loop_start and store the
            // elements till start in a stack
            int count = 0;
            while (ptr != loop_start || count != 1)
            {
                s.Push(ptr.data);
                if (ptr == loop_start)
                {
                    count = 1;
                }
                ptr = ptr.next;
            }
            ptr = head;
            count = 0;
            // Traverse linked list until last node is
            // equal to loop_start second time
            while (ptr != loop_start || count != 1)
            {
                // Compare data of node with the top of stack
                // If equal then continue
                if (ptr.data == s.Peek())
                {
                    s.Pop();
                }
                else
                {
                    return false;
                }

                if (ptr == loop_start)
                {
                    count = 1;
                }
                ptr = ptr.next;

            }
            // Return true if linked list is palindrome
            return true;

        }


        /// <summary>
        /// Function to find if linked list
        /// is palindrome or not
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        static bool isPalindrome(Node head)
        {
            // Find the loop starting node
            Node loop_start = detectAndgetLoopstarting(head);
            // Check if linked list is palindrome
            return isPalindromeUtil(head, loop_start);

        }


        static Node newNode(int key)
        {
            Node temp = new Node();
            temp.data = key;
            temp.next = null;
            return temp;
        }

        /// <summary>
        /// https://verytoolz.com/blog/666b4cb9e7/
        /// 给定一个带有循环的链表，任务是找出它是否是回文。不允许删除循环。 
        /// 
        /// 1. 使用弗洛伊德循环检测算法检测环路。
        /// 2. 然后找到循环的起始节点，如 this 中所述
        /// 3. 检查链接列表是否为回文，如this 中所述
        /// 
        /// this ==> https://verytoolz.com/blog/54290a1020/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Node head = newNode(50);

            head.next = newNode(20);

            head.next.next = newNode(15);

            head.next.next.next = newNode(20);

            head.next.next.next.next = newNode(40);

            /* Create a loop for testing */

            head.next.next.next.next.next = head.next.next;

            if (isPalindrome(head) == true)
            {
                Console.WriteLine("Palindrome");
            }
            else
            {
                Console.WriteLine("Not Palindrome");
            }

            Console.ReadKey();
                
        }


    }
}
