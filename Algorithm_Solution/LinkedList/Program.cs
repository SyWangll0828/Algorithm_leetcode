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
            int[] arr = { -3, 99 ,5};
            int[] arr1 = testCase.nums4;
            ListNode node1 = Knowleage.CreateListNodes(arr);
            ListNode node2 = Knowleage.CreateListNodes(arr1);
            //Knowleage.PrintListNodes(head);
            ListNode sdasd = problems.DeleteNode(node1,-3);
            //Knowleage.PrintListNodes(resHead);
        }
    }
    class Problems
    {
        public ListNode DeleteNode(ListNode head, int val)
        {
            if (head == null)
            {
                return head;
            }
            ListNode pre = new ListNode(0);
            ListNode cur = head;
            pre.next = head;
            while (pre.next != null)
            {
                if (pre.next.val == val)
                {
                    pre.next = pre.next.next;
                    break;
                }
                else
                {
                    pre = pre.next;
                }
            }
            return pre.next;
        }
        // 链表两数相加
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            // 反转链表+两数相加I的方法
            ListNode cur1 = reverse(l1);
            ListNode cur2 = reverse(l2);
            ListNode ans = AddTwoNodes(cur1, cur2);
            return reverse(ans);
            // 递归反转链表
            ListNode reverse(ListNode head)
            {
                return helper(null, head);
            }
            ListNode helper(ListNode pre, ListNode cur)
            {
                // 终止条件
                if (cur == null)
                {
                    return pre;
                }
                ListNode next = cur.next;
                cur.next = pre;
                return helper(cur, next);
            }
            // 两数相加
            ListNode AddTwoNodes(ListNode node1, ListNode node2)
            {
                int carry = 0;
                ListNode pre = new ListNode(0);
                ListNode cur = pre;
                while (node1 != null || node2 != null || carry != 0)
                {
                    int sum = carry;
                    // 结点后移，跳出循环
                    if (node1 != null)
                    {
                        sum += node1.val;
                        node1 = node1.next;
                    }
                    if (node2 != null)
                    {
                        sum += node2.val;
                        node2 = node2.next;
                    }
                    carry = sum / 10;
                    sum %= 10;
                    cur.next = new ListNode(sum);
                    cur = cur.next;
                }
                return pre.next;
            }
        }

        //链表常用方式，固定下来的解题：
        //cur = dummy, cur指向当前节点
        //cur = cur.next，进行链表遍历

        //删除链表结点
        //1、dummy.next = dummy.next.next;
        //2、找到要删除结点的下一个结点，将下一个结点的值赋值给删除结点，下一个结点删除

        //双指针概念（一个指针解决不了问题，可以考虑双指针，一个一次走一步，另一个走若干步）
        //求链表的中间节点（一个一次走一步，另一个一次走两步，第一个结点走到末尾时，第一个结点的位置就是中间节点）
        //判断是否环形链表（一个一次走一步，另一个一次走两步,在第一个走到末尾的时候第二个结点能否追上第一个结点）

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
        // 创建链表
        public static ListNode CreateListNodes(int[] nums)
        {
            ListNode pre = new ListNode(0);
            ListNode cur = pre;
            foreach (int num in nums)
            {
                cur.next = new ListNode(num);
                cur = cur.next;
            }
            return pre.next;
        }
        // 输出链表
        public static void PrintListNodes(ListNode head)
        {
            while (head != null)
            {
                if (head.next != null)
                {
                    Console.Write(head.val + "->");
                }
                else
                {
                    Console.Write(head.val);
                }
                head = head.next;
            }
            Console.WriteLine();
        }

        // 反转链表
        public ListNode ReverseList(ListNode head)
        {
            //if (head == null) return null;
            //ListNode preNode = null;
            //ListNode curNode = head;
            ////迭代
            //while (curNode != null)
            //{
            //    ListNode nextNode = curNode.next;
            //    curNode.next = preNode;
            //    preNode = curNode;
            //    curNode = nextNode;
            //}
            //return preNode;

            // 递归反转链表
            return helper(null, head);
            ListNode helper(ListNode pre, ListNode cur)
            {
                // 终止条件
                if (cur == null)
                {
                    return pre;
                }
                ListNode next = cur.next;
                cur.next = pre;
                return helper(cur, next);
            }
        }

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
            ListNode cur1 = node1;
            ListNode cur2 = node2;
            methodOne();
            ListNode methodOne()
            {
                // 两个链表分别走一遍
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
            // 浪漫相遇 方法
            // 走一遍两个链表，谁先走完走另外一个
            while (cur1 != cur2)
            {
                // 用cur1==null 的形式包含了两个链表不相交的情况
                // 不相交时两个节点各走一遍两个链表同时=null
                cur1 = cur1 == null ? node2 : cur1.next;
                cur2 = cur2 == null ? node1 : cur2.next;
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
        /// <summary>
        /// 快慢指针，套路方法;找到链表第一个入环节点，如果无环，返回null
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static ListNode GetLoopNode(ListNode node)
        {
            if (node == null || node.next == null || node.next.next == null)
            {
                return null;
            }
            // 快慢节点
            ListNode n1 = node.next;
            ListNode n2 = node.next.next;
            while (n1 != n2)
            {
                if (n2.next == null || n2.next.next == null)
                {
                    return null;
                }
                n1 = n1.next;
                n2 = n2.next.next;
            }
            // 当快节点与慢节点第一次相遇时，让快节点从头结点再次开始走
            n2 = node;
            // 两节点第二次相遇时就是第一个入环节点
            // 快指针与慢指针按照相同的速度走
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
