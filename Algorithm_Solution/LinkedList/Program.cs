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
            int[] arr = { 1, 4, 3, 0, 5, 2 };
            int[] arr1 = testCase.nums4;
            ListNode node1 = Knowleage.CreateListNodes(arr);
            ListNode node2 = Knowleage.CreateListNodes(arr1);
            problems.ReverseList(node1);
            //Knowleage.PrintListNodes(head);
            //Knowleage.PrintListNodes(resHead);
        }
    }
    class Problems
    {
        public TreeNode SortedListToBST(ListNode head)
        {
            if (head == null)
            {
                return null;
            }
            int len = 0;
            ListNode cur = head;
            while (cur != null)
            {
                len++;
                cur = cur.next;
            }
            int[] nums = new int[len];
            cur = head;
            for (int i = 0; i < len; i++)
            {
                nums[i] = cur.val;
                cur = cur.next;
            }
            return creatrBST(0, len - 1);

            TreeNode creatrBST(int left, int right)
            {
                if (right > left)
                {
                    return null;
                }
                int mid = left + ((right - left) >> 1);
                TreeNode node = new TreeNode(nums[mid]);
                node.left = creatrBST(left, mid - 1);
                node.right = creatrBST(mid + 1, right);
                return node;
            }
        }
        // 92. 反转链表 II
        public ListNode ReverseBetween(ListNode head, int left, int right)
        {
            if (left == right || head == null)
            {
                return head;
            }
            // 头插法
            // 原地一趟遍历
            ListNode dummy = new ListNode(0);
            dummy.next = head;
            ListNode pre = dummy;
            for (int i = 1; i < left; i++)
            {
                pre = pre.next;
            }
            ListNode cur = pre.next;
            // pre,cur,next三个指针进行原地反转
            for (int i = 0; i < right - left; i++)
            {
                ListNode next = cur.next;
                cur.next = next.next;
                next.next = pre.next;
                pre.next = next;
            }
            return dummy.next;

            //ListNode dummy = new ListNode(0);
            //dummy.next = head;
            //// 头结点可能发生变化，定义哑节点
            //ListNode pre = dummy;
            //// 先找到left位置的结点
            //for (int i = 1; i < left; i++)
            //{
            //    pre = pre.next;
            //}
            //ListNode rightPre = pre;
            //// 然后找到right位置的结点
            //// 此时rightPre指向right结点
            //for (int i = 1; i <= (right - left + 1); i++)
            //{
            //    rightPre = rightPre.next;
            //}
            //// 断开连接
            //ListNode leftNode = pre.next;
            //// 后继结点
            //ListNode succ = rightPre.next;
            //pre.next = null;
            //rightPre.next = null;
            //reverse(leftNode);
            //// 重新连接
            //leftNode.next = succ;
            //pre.next = rightPre;
            //return dummy.next;
            //// 反转链表
            //void reverse(ListNode node)
            //{
            //    ListNode preNode = null;
            //    ListNode n = node;
            //    while (n != null)
            //    {
            //        ListNode ne = n.next;
            //        n.next = preNode;
            //        preNode = n;
            //        n = ne;
            //    }
            //}


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

        // 链表常用方式，固定下来的解题：
        // head头结点要做操作的时候，通常定义一个dummy结点，其next结点指向头结点

        //删除链表结点
        //1、dummy.next = dummy.next.next;
        //2、找到要删除结点的下一个结点，将下一个结点的值赋值给删除结点，下一个结点删除

        //双指针概念（一个指针解决不了问题，可以考虑双指针，一个一次走一步，另一个走若干步）
        //求链表的中间节点（一个一次走一步，另一个一次走两步，第一个结点走到末尾时，第一个结点的位置就是中间节点）
        //判断是否环形链表（一个一次走一步，另一个一次走两步,在第一个走到末尾的时候第二个结点能否追上第一个结点）

        #region 双指针


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
            //ListNode nextNode = curNode.next;
            //curNode.next = preNode;
            //preNode = curNode;
            //curNode = nextNode;
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

        // 143. 重排链表
        // 首先想到用栈来处理中点之后的结点插入，结果超时
        // 中点之后的结点进行反转，然后合并两个链表
        public void ReorderList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return;
            }
            ListNode mid = findMidNode(head);
            ListNode l1 = head;
            ListNode l2 = mid.next;
            // 断开连接
            mid.next = null;
            reverse(l2);
            // 合并两个链表
            mergeTwoList(l1, l2);

            ListNode findMidNode(ListNode head1)
            {
                ListNode slow = head, fast = head;
                while (fast != null && fast.next != null)
                {
                    slow = slow.next;
                    fast = fast.next.next;
                }
                return slow;
            }

            // p,cur,next 反转链表
            void reverse(ListNode head2)
            {
                ListNode p = null;
                ListNode cur = head2;
                while (cur != null)
                {
                    ListNode n = cur.next;
                    cur.next = p;
                    p = cur;
                    cur = n;
                }
                l2 = p;
            }

            // 合并两个链表（交叉合并）
            void mergeTwoList(ListNode n1, ListNode n2)
            {
                ListNode temp1;
                ListNode temp2;
                while (n1 != null && n2 != null)
                {
                    temp1 = n1.next;
                    temp2 = n2.next;
                    n1.next = n2;
                    n1 = temp1;
                    // 交换插入
                    n2.next = n1;
                    n2 = temp2;
                }
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

    class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode parent;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null, TreeNode par = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
            this.parent = par;
        }
    }
}
