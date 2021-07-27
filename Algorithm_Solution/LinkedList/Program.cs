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
            Common.Case testCase = new Common.Case();
            //problems.copyRandomList(head);
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
            while (curNode != null)
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

    class Knowleage
    {
        // 获取两个链表的相交节点
        // 处理环形链表
        public static ListNode GetIntersectNode(ListNode node1, ListNode node2)
        {
            if (node1 == null || node2 == null)
            {
                return null;
            }
            ListNode loop1 = GetLoopNode(node1);
            ListNode loop2 = GetLoopNode(node2);
            // 两个链表皆无环
            if (loop1 == null && loop2 == null)
            {
                return NoLoop(loop1, loop2);
            }
            // 两个链表皆有环
            if (loop1 != null && loop2 != null)
            {
                return BothLoop(node1, node2, loop1, loop2);
            }
            // loop其中一个为null，另一个不为null，则两个链表一定不相交
            return null;
        }

        // 两个无环链表，返回第一个相交节点，如果不相交，返回null
        private static ListNode NoLoop(ListNode node1, ListNode node2)
        {
            if (node1 == null || node2 == null)
            {
                return null;
            }
            // 两个链表分别走一遍
            ListNode cur1 = node1;
            ListNode cur2 = node2;
            // 记录长度差值
            int n = 0;
            while (cur1.next != null)
            {
                cur1 = cur1.next;
                n++;
            }
            while (cur2.next != null)
            {
                cur2 = cur2.next;
                n--;
            }
            // 两个链表没有交点
            if (cur1 != cur2)
            {
                return null;
            }
            // 重新定义初始节点
            cur1 = n > 0 ? node1 : node2;
            cur2 = cur1 == node1 ? node2 : node1;
            n = Math.Abs(n);
            // 长的链表先走一段距离
            while (n != 0)
            {
                cur1 = cur1.next;
                n--;
            }
            while (cur1 != cur2)
            {
                cur1 = cur1.next;
                cur2 = cur2.next;
            }
            return cur1;
        }
        // 两个有环链表
        private static ListNode BothLoop(ListNode node1, ListNode node2, ListNode loop1, ListNode loop2)
        {
            ListNode cur1 = null;
            ListNode cur2 = null;
            if (loop1 == loop2)
            {
                cur1 = node1;
                cur2 = node2;
                int n = 0;
                while (cur1 != loop1)
                {
                    cur1 = cur1.next;
                    n++;
                }
                while (cur2 != loop2)
                {
                    cur2 = cur2.next;
                    n--;
                }
                // 重新定义初始节点
                cur1 = n > 0 ? node1 : node2;
                cur2 = cur1 == node1 ? node2 : node1;
                n = Math.Abs(n);
                // 长的链表先走一段距离
                while (n != 0)
                {
                    cur1 = cur1.next;
                    n--;
                }
                while (cur1 != cur2)
                {
                    cur1 = cur1.next;
                    cur2 = cur2.next;
                }
                return cur1;
            }
            else
            {
                cur1 = loop1.next;
                while (cur1 != loop1)
                {
                    if (cur1 == loop2)
                    {
                        return loop1;
                    }
                    cur1 = cur1.next;
                }
                return null;
            }
        }
        // 快慢指针，套路方法
        // 找到链表第一个入环节点，如果无环，返回null
        public static ListNode GetLoopNode(ListNode node)
        {
            if (node == null || node.next == null || node.next.next == null)
            {
                return null;
            }
            // 快慢节点
            ListNode n1 = node.next;
            ListNode n2 = node.next.next;
            // 当快节点与慢节点第一次相遇时，让快节点从头开始
            // 两节点第二次相遇时就是时第一个入环节点
            while (n1 != n2)
            {
                if (n2.next == null || n2.next.next == null)
                {
                    return null;
                }
                n1 = n1.next;
                n2 = n2.next.next;
            }
            n2 = node;
            while (n1 != n2)
            {
                n1 = n1.next;
                n2 = n2.next;
            }
            return n1;
        }
    }

    class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode random;
        public ListNode(int val = 0, ListNode next = null, ListNode random = null)
        {
            this.val = val;
            this.next = next;
            this.random = random;
        }
    }
}
