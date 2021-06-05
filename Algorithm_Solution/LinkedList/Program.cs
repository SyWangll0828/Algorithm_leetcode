using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class Problems
    {
        //160. 相交链表
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            HashSet<ListNode> hash = new HashSet<ListNode>();
            var curNode = headA;
            while (curNode != null)
            {
                hash.Add(curNode);
                curNode = curNode.next;
            }
            curNode = headB;
            while (curNode != null)
            {
                if (!hash.Add(curNode))
                    return curNode;
                else
                    curNode = curNode.next;
            }
            return null;
        }
        

        #region 双指针
        //876. 链表的中间结点
        public ListNode MiddleNode(ListNode head)
        {
            //int n = 0, k = 0;
            //var curNode = head;
            ////两次遍历链表
            //while (curNode != null)
            //{
            //    n++;
            //    curNode = curNode.next;
            //}
            //curNode = head;
            //while (k < n / 2)
            //{
            //    k++;
            //    curNode = curNode.next;
            //}
            //return curNode;
            ListNode fast = head, slow = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            return slow;
        }

        //203. 移除链表元素
        public ListNode RemoveElements(ListNode head, int val)
        {
            ////定义一个哑节点
            //var dummy = new ListNode();
            //dummy.next = head;
            ////定义一个
            //var temp = dummy;
            //while (temp.next != null)
            //{
            //    if (temp.next.val == val)
            //        temp.next = temp.next.next;
            //    else
            //        temp = temp.next;
            //}
            //return dummy.next;

            //递归
            //终止条件
            if (head == null)
                return null;
            head.next= RemoveElements(head.next, val);
            return head.val == val ? head.next : head;

        }
        #endregion

    }

    class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
