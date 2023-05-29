using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklist_Palindrome_single
{
    class LinkedList
    {
        Node head; // head of list
        Node slow_ptr, fast_ptr, second_half;

        /// <summary>
        /// Linked list Node
        /// </summary>
        public class Node
        {
            public char data;
            public Node next;
            public Node(char d)
            {
                data = d;
                next = null;
            }
        }


        /// <summary>
        /// Function to check if given linked list is
        /// palindrome or not
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        Boolean isPalindrome(Node head)
        {
            slow_ptr = head;
            fast_ptr = head;
            Node prev_of_slow_ptr = head;
            Node midnode = null; // To handle odd size list
            Boolean res = true; // initialize result
            if (head != null && head.next != null)
            {
                /* Get the middle of the list. Move slow_ptr by 1

                   and fast_ptr by 2, slow_ptr will have the middle

                   node */

                while (fast_ptr != null && fast_ptr.next != null)
                {
                    fast_ptr = fast_ptr.next.next;

                    /*We need previous of the slow_ptr for

                     linked lists  with odd elements */

                    prev_of_slow_ptr = slow_ptr;
                    slow_ptr = slow_ptr.next;

                }

                /* fast_ptr would become NULL when there are even elements

                  in the list and not NULL for odd elements. We need to skip 

                  the middle node for odd case and store it somewhere so that

                  we can restore the original list */

                if (fast_ptr != null)
                {
                    midnode = slow_ptr;
                    slow_ptr = slow_ptr.next;
                }

                // Now reverse the second half and compare it with first half

                second_half = slow_ptr;
                prev_of_slow_ptr.next = null; // NULL terminate first half
                reverse(); // Reverse the second half
                res = compareLists(head, second_half); // compare

                /* Construct the original list back */

                reverse(); // Reverse the second half again

                if (midnode != null)
                {
                    // If there was a mid node (odd size case) which
                    // was not part of either first half or second half.
                    prev_of_slow_ptr.next = midnode;
                    midnode.next = second_half;
                }
                else
                {
                    prev_of_slow_ptr.next = second_half;
                }
            }
            return res;
        }


        /// <summary>
        /// Function to reverse the linked list  Note that this function may change the head
        /// </summary>
        void reverse()
        {
            Node prev = null;
            Node current = second_half;
            Node next;

            while (current != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }
            second_half = prev;

        }


        /// <summary>
        /// Function to check if two input lists have same data
        /// </summary>
        /// <param name="head1"></param>
        /// <param name="head2"></param>
        /// <returns></returns>
        Boolean compareLists(Node head1, Node head2)
        {
            Node temp1 = head1;
            Node temp2 = head2;

            while (temp1 != null && temp2 != null)
            {
                if (temp1.data == temp2.data)
                {
                    temp1 = temp1.next;
                    temp2 = temp2.next;
                }
                else
                {
                    return false;
                }
            }

            /* Both are empty return 1*/
            if (temp1 == null && temp2 == null)
            {
                return true;
            }

            /* Will reach here when one is NULL
              and other is not */

            return false;

        }


        /// <summary>
        /// Push a node to linked list. Note that this function changes the head
        /// </summary>
        /// <param name="new_data"></param>
        public void push(char new_data)
        {
            /* Allocate the Node & Put in the data */
            Node new_node = new Node(new_data);
            /* link the old list off the new one */
            new_node.next = head;
            /* Move the head to point to new Node */
            head = new_node;
        }


        /// <summary>
        /// A utility function to print a given linked list
        /// </summary>
        /// <param name="ptr"></param>
        void printList(Node ptr)
        {
            while (ptr != null)
            {
                Console.Write(ptr.data + "->");
                ptr = ptr.next;
            }
            Console.WriteLine("NULL");
        }


        /// <summary>
        /// Driver program to test the above functions
        /// 
        /// https://verytoolz.com/blog/77bbc50698/
        /// single linklist Palindrome  單向linklist回文
        /// 给定一个字符的单链表，编写一个函数，如果给定的列表是回文则返回 true，否则返回 false。
        /// 
        /// 1. 一个简单的解决方案是使用一堆列表节点。这主要涉及三个步骤。
        /// 2. 从头到尾遍历给定列表，并将每个访问过的节点推送到堆栈。
        /// 3. 再次遍历列表。对于每个访问的节点，从堆栈中弹出一个节点，并将弹出节点的数据与当前访问的节点进行比较。
        /// 4. 如果所有节点都匹配，则返回 true，否则返回 false。
        /// 資料夾裡面有附上 圖片說明
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /* Start with the empty list */
            LinkedList llist = new LinkedList();
            char[] str = { 'a', 'b', 'a', 'c', 'a', 'b', 'a' };

            for (int i = 0; i < 7; i++)
            {
                llist.push(str[i]);
                llist.printList(llist.head);

                if (llist.isPalindrome(llist.head) != false)
                {
                    Console.WriteLine("Is Palindrome");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Not Palindrome");
                    Console.WriteLine("");
                }
            }
            Console.ReadKey();
        }
    }
}
