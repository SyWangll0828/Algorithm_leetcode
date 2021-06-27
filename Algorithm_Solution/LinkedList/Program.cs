using System;
using System.Collections;
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
        //剑指 Offer 06. 从尾到头打印链表
        public int[] ReversePrint(ListNode head)
        {
            ArrayList array = new ArrayList();
            reverse(head);
            int[] res = new int[array.Count];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = (int)array[i];
            }
            return res;
            void reverse(ListNode node)
            {
                //自底向上
                if (node == null) return;
                reverse(node.next);
                array.Add(node.val);
            }
            //Stack<int> stack = new Stack<int>();
            //while (head != null)
            //{
            //    stack.Push(head.val);
            //    head = head.next;
            //}
            //int[] res = new int[stack.Count];
            //for (int i = 0; i < res.Length; i++)
            //{
            //    res[i] = stack.Pop();
            //}
            //return res;
        }

        //21. 合并两个有序链表
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null || l2 == null) return l1 == null ? l2 == null ? null : l2 : l1;
            if (l1.val <= l2.val)
            {
                l1.next = MergeTwoLists(l1.next, l2);
                return l1;
            }
            else
            {
                l2.next = MergeTwoLists(l1, l2.next);
                return l2;
            }
        }

        //83. 删除排序链表中的重复元素
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null) return null;
            ListNode dummy = new ListNode();
            dummy = head;
            while (dummy != null && dummy.next != null)
            {
                if (dummy.val == dummy.next.val)
                    dummy.next = dummy.next.next;
                else
                    dummy = dummy.next;
            }
            //要注意返回的是经过遍历过后的头结点
            return head;
        }

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
            //定义一个哑节点
            var dummy = new ListNode();
            dummy.next = head;
            //定义一个
            var temp = dummy;
            while (temp.next != null)
            {
                if (temp.next.val == val)
                    temp.next = temp.next.next;
                else
                    temp = temp.next;
            }
            return dummy.next;

            //递归
            //终止条件
            if (head == null)
                return null;
            head.next = RemoveElements(head.next, val);
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
