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
            Problems problems = new Problems();
            //实例化可以访问类成员;直接访问需要添加static
            ListNode head = new ListNode(1)
            {
                next = new ListNode(2)
                {
                    next = new ListNode(3)
                }
            };
            Common.Case testCase = new Common.Case();
            problems.ReverseList(head);
        }
    }
    class Problems
    {
        //链表常用方式，固定下来的解题：
        //方便对头节点的操作，创建哑节点dummy.next
        //cur = dummy, cur指向当前节点
        //cur = cur.next，进行链表遍历

        //删除链表结点
        //1、dummy.next = dummy.next.next;
        //2、找到要删除结点的下一个结点，将下一个结点的值赋值给删除结点，下一个结点删除

        //双指针概念（一个指针解决不了问题，可以考虑双指针，一个一次走一步，另一个走若干步）
        //求链表的中间节点（一个一次走一步，另一个一次走两步，第一个结点走到末尾时，第一个结点的位置就是中间节点）
        //判断是否环形链表（一个一次走一步，另一个一次走两步,在第一个走到末尾的时候第二个结点能否追上第一个结点）

        //206. 反转链表
        public ListNode ReverseList(ListNode head)
        {
            if (head == null) return null; 
            ListNode preNode = null;
            ListNode curNode = head;
            //迭代
            while (curNode!=null)
            {
                ListNode nextNode = curNode.next;
                curNode.next = preNode;
                preNode = curNode;
                curNode = nextNode;
            }
            return preNode;

            //递归 建议画图 从尾节点开始理解
            if (head == null || head.next == null) return head;
            //定义返回节点（及尾节点）
            ListNode cur = ReverseList(head.next);
            //head.next为尾节点，head为尾节点前驱节点
            //2->3 变成  2<-3
            head.next.next = head;
            //前驱节点的next先置空
            head.next = null;
            return cur;


        }

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

        //19. 删除链表的倒数第 N 个结点
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dummy = head;
            int sum = 0, count = 0;
            //循环有多少个结点
            while (dummy != null)
            {
                dummy = dummy.next;
                sum++;
            }
            if (sum == n) return head.next;
            var cur = head;
            while (count < sum - n - 1)
            {
                cur = cur.next;
                count++;
            }
            cur.next = cur.next.next;
            return head;
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
