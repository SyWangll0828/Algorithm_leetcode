using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Problems problems = new Problems();
            //实例化可以访问类成员;直接访问需要添加static
            Common.Case testCase = new Common.Case();
            int[] t1 = new int[] { 3, 9, 20, 15, 7 };
            int[] t2 = new int[] { 1, 3, 2, 6, 5 };

            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(4),
                    right = new TreeNode(5)
                },
                right = new TreeNode(3)
                {
                    right = new TreeNode(7)
                }
            };
            TreeNode root2 = new TreeNode(2)
            {
                right = new TreeNode(3)
                {
                },
            };
            //Knowleage.serialize(root2);
            Knowleage.IsComBT(root);
            int[] arr1 = new int[] { 1, 2, 3, 4, 5, 0, 7 };
            int[] arr2 = new int[] { 4, 5, 2, 6, 7, 3, 1 };
            problems.Flatten(root);


            Console.ReadKey();
        }
    }

    class Problems
    {
        public void Flatten(TreeNode root)
        {
            if (root == null)
            {
                return;
            }
            // 前序遍历根结点
            // 对遍历生成的数组进行迭代生成对应的结点
            List<int> list = new List<int>();
            preOrder(root);
            TreeNode node = new TreeNode();
            for (int i = 1; i < list.Count; i++)
            {
                TreeNode pre = new TreeNode(list[i - 1]);
                TreeNode cur = new TreeNode(list[i]);
                pre.left = null;
                pre.right = cur;
            }

            void preOrder(TreeNode head)
            {
                if (head == null)
                {
                    return;
                }
                list.Add(head.val);
                preOrder(head.left);
                preOrder(head.right);
            }
        }

        // 根据二叉树遍历结果重建二叉树
        public TreeNode ConstructFromPrePost(int[] preorder, int[] postorder)
        {
            int preLen = preorder.Length;
            int postLen = postorder.Length;
            if (preLen != postLen)
            {
                return null;
            }
            return bulidBT(0, preLen - 1, 0, postLen - 1);

            TreeNode bulidBT(int preLeft, int preRight, int postLeft, int postRight)
            {
                // 递归终止条件
                if (preLeft > preRight || postLeft > postRight)
                {
                    return null;
                }
                TreeNode head = new TreeNode(preorder[preLeft]);
                if (preLeft == preRight)
                {
                    return head;
                }
                // 找到左子树和右子树分界点
                int pivotIndex = postLeft;
                while (postorder[pivotIndex] != preorder[preLeft + 1] && pivotIndex <= postRight)
                {
                    pivotIndex++;
                }
                int preL = pivotIndex - postLeft + preLeft + 1;
                head.left = bulidBT(preLeft + 1, preL, postLeft, pivotIndex);
                head.right = bulidBT(preL + 1, preRight, pivotIndex + 1, postRight - 1);
                return head;
            }
        }

        // 二叉树转为双向链表？？
        public TreeNode TreeToDoublyList(TreeNode root)
        {
            if (root == null) return null;
            TreeNode head = null;
            //指定当前节点的前一个节点，用于节点间连接
            TreeNode pre = null;
            //中序遍历的同时将头和尾之间的节点相连
            midOrder(root);
            //将头和尾相连
            head.left = pre;
            pre.right = head;
            return head;

            void midOrder(TreeNode node)
            {
                if (node == null) return;
                //中序遍历
                midOrder(node.left);
                //只有第一进入时执行,及中序遍历的第一个节点
                if (pre == null) head = node;
                //指定前一节点的右节点为当前节点
                else pre.right = node;
                //反向连接
                node.left = pre;
                //指定当前节点
                pre = node;
                midOrder(node.right);
            }
        }
    }

    class Knowleage
    {
        // 先序遍历  根节点-左节点-右节点
        public void PreOrder(TreeNode root)
        {
            if (root != null)
            {
                var list = new List<int>();
                if (root == null) return;
                // 递归
                preOrder(root);
                void preOrder(TreeNode node)
                {
                    if (node == null)
                        return;
                    list.Add(root.val);
                    PreOrder(root.left);
                    PreOrder(root.right);
                }

                // 栈实现
                var stack = new Stack<TreeNode>();
                // 先将头结点放入栈中
                stack.Push(root);
                while (stack.Any())
                {
                    // 头结点出栈
                    root = stack.Pop();
                    list.Add(root.val);
                    // 先右再左，利用栈的后进先出
                    if (root.right != null)
                        stack.Push(root.right);
                    if (root.left != null)
                        stack.Push(root.left);
                }
            }

        }

        // 中序遍历  左节点-根节点-右节点
        public void InOrder(TreeNode root)
        {
            if (root != null)
            {
                var list = new List<int>();
                // 递归
                inOrder(root);
                void inOrder(TreeNode node)
                {
                    if (node == null)
                        return;
                    inOrder(root.left);
                    list.Add(root.val);
                    inOrder(root.right);
                }

                // 栈实现
                var stack = new Stack<TreeNode>();
                while (stack.Any() || root != null)
                {
                    // 先将所有左节点入栈
                    if (root != null)
                    {
                        stack.Push(root);
                        root = root.left;
                    }
                    else
                    {
                        root = stack.Pop();
                        list.Add(root.val);
                        root = root.right;
                    }
                }
            }
        }

        // 后序遍历  左节点-右节点-根节点
        public void PostOrder(TreeNode root)
        {
            if (root != null)
            {
                var list = new List<int>();
                if (root == null) return;
                // 递归
                inOrder(root);
                void inOrder(TreeNode node)
                {
                    if (node == null)
                        return;
                    PreOrder(root.left);
                    list.Add(root.val);
                    PreOrder(root.right);
                }

                // 栈实现
                Stack<TreeNode> stack = new Stack<TreeNode>();
                Stack<TreeNode> help = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Any())
                {
                    root = stack.Pop();
                    help.Push(root);
                    if (root.left != null)
                        stack.Push(root.left);
                    if (root.right != null)
                        stack.Push(root.right);
                }
                while (help.Any())
                {
                    root = help.Pop();
                    list.Add(root.val);
                }
            }
        }

        // 深度遍历 == 先序遍历
        // 宽度遍历 == 层序遍历
        public void LevelOrder(TreeNode root)
        {
            List<int> list = new List<int>();
            if (root == null) return;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            // 头结点先入队
            queue.Enqueue(root);
            while (queue.Any())
            {
                int len = queue.Count;
                // 队列中每个节点出队的时候将左右子节点入队
                for (int i = 0; i < len; i++)
                {
                    root = queue.Dequeue();
                    list.Add(root.val);
                    // 先左再右
                    if (root.left != null) queue.Enqueue(root.left);
                    if (root.right != null) queue.Enqueue(root.right);
                }
            }
        }

        // 判断一个树是二叉搜索树
        // 中序遍历递增
        static long preValue = long.MinValue;
        public static bool IsValidBST(TreeNode node)
        {
            if (node == null) return true;
            // 检查左树
            bool isLeftBst = IsValidBST(node.left);
            if (!isLeftBst) return false;
            // 根据二叉搜索树的中序遍历递增来判断
            if (node.val <= preValue) return false;
            else preValue = node.val;
            return IsValidBST(node.right);

            // 非递归
            if (node != null)
            {
                var stack = new Stack<TreeNode>();
                while (stack.Any() || node != null)
                {
                    // 先将所有左节点入栈
                    if (node != null)
                    {
                        stack.Push(node);
                        node = node.left;
                    }
                    else
                    {
                        node = stack.Pop();
                        if (node.val <= preValue)
                        {
                            return false;
                        }
                        else
                        {
                            preValue = node.val;
                        }
                        node = node.right;
                    }
                }
            }
            return true;
        }

        // 验证所给的后续遍历数组能否构成二叉搜索树
        public bool VerifyPostorder(int[] postorder)
        {
            return valid(0, postorder.Length - 1);

            bool valid(int left, int right)
            {
                if (left >= right) return true;
                // 从前找第一个大于根节点值的下标与最后一个小于根节点值的下标
                // 即分界点下标
                int index = left;
                while (postorder[index] < postorder[right]) index++;
                for (int i = index; i < right; i++)
                {
                    if (postorder[i] < postorder[right]) return false;
                }
                // 左右树递归返回结果
                return valid(left, index - 1) && valid(index, right - 1);
            }
        }

        // 判断一个树是完全二叉树
        // 从左往右依次填满
        public static bool IsComBT(TreeNode node)
        {
            if (node == null)
            {
                return true;
            }
            // 是否遇到过左右节点不双全
            bool leaf = false;
            TreeNode left = null;
            TreeNode right = null;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(node);
            while (queue.Any())
            {
                node = queue.Dequeue();
                left = node.left;
                right = node.right;
                // 是没有双全子节点的节点之后的节点（应该为叶节点），但是有左右节点
                // 有右没有左
                if ((leaf && (left != null || right != null)) || (right != null && left == null))
                {
                    return false;
                }
                if (left != null)
                {
                    queue.Enqueue(left);
                }
                if (right != null)
                {
                    queue.Enqueue(right);
                }
                // 遇到左右不双全的情况
                if (left == null || right == null)
                {
                    leaf = true;
                }
            }
            return true;
        }

        #region 树形DP套路
        // 左右两边各自需要提供一些信息来解决问题

        // 判断一个树是满二叉树
        // 深度 depth 节点数 count => count= 2^depth-1
        public static bool IsFullBT(TreeNode node)
        {
            if (node == null)
            {
                return true;
            }
            ReturnType res = valid(node);
            return res.nodeCount == (1 << res.depth - 1);
            ReturnType valid(TreeNode head)
            {
                // 两边分别需要节点个数和深度的信息
                if (head == null)
                {
                    return new ReturnType(0, 0);
                }
                ReturnType leftDate = valid(head.left);
                ReturnType rightDate = valid(head.right);
                int depth = Math.Max(leftDate.depth, rightDate.depth) + 1;
                int nodes = leftDate.nodeCount + rightDate.nodeCount + 1;
                return new ReturnType(depth, nodes);
            }
        }

        public class ReturnType
        {
            public int depth;
            public int nodeCount;
            public ReturnType(int d, int nodes)
            {
                depth = d;
                nodeCount = nodes;
            }
        }
        #endregion

        // 判断一个树是平衡二叉树
        public static bool IsBalanced(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }
            return Math.Abs(maxDepth(root.left) - maxDepth(root.right)) <= 1 ? IsBalanced(root.left) && IsBalanced(root.right) : false;
            // 递归
            int maxDepth(TreeNode node)
            {
                return node == null ? 0 : Math.Max(maxDepth(node.left), maxDepth(node.right)) + 1;
            }
        }

        // 二叉树的最低公共节点
        // 两个节点在同一个左树或者右树返回在上面的一个节点
        // 两个节点分别在根节点的左右
        // 容易理解的方法，将节点与父节点存到键值对字典中
        // 定义一个节点用set来存储p到根节点的路径
        // 然后用q去走，检查set是否存在q走过的节点
        TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null || root == p || root == q)
            {
                return root;
            }
            TreeNode left = lowestCommonAncestor(root.left, p, q);
            TreeNode right = lowestCommonAncestor(root.right, p, q);
            // 两个节点分别在根节点的左右
            if (left != null && right != null)
            {
                return root;
            }
            // 两个节点中一个节点为null，返回不为空的一个
            return left != null ? left : right;
        }

        // 前驱节点; 中序遍历中节点的前一个节点
        // 后继节点: 中序遍历中节点的后一个节点
        public TreeNode getSuccessorNode(TreeNode head)
        {
            if (head == null)
            {
                return null;
            }
            // 一个节点有右树的时候，它的最左侧节点就是其后驱节点
            if (head.right != null)
            {
                return getLeftMost(head.right);
            }
            else
            {
                TreeNode parent = head.parent;
                // 一个节点是不是父节点的左孩子，不是继续往上，是则该节点的后驱节点是该父节点
                // 左子树最右的节点
                while (parent != null && parent.left != head)
                {
                    head = parent;
                    parent = head.parent;
                }
                return parent;
            }
            // 获取该节点右树的最左侧节点
            TreeNode getLeftMost(TreeNode node)
            {
                if (node == null)
                {
                    return null;
                }
                while (node.left != null)
                {
                    node = node.left;
                }
                return node;
            }
        }

        // 二叉树的序列化和反序列化
        // 此处采用先序遍历
        public static string serialize(TreeNode root)
        {
            if (root == null)
            {
                return "#_";
            }
            // 先序遍历：根-左-右
            string res = root.val.ToString() + "_";
            res += serialize(root.left);
            res += serialize(root.right);
            return res;
        }
        public TreeNode deserialize(string data)
        {
            string[] arr = data.Split('_');
            Queue<string> queue = new Queue<string>();
            // 先将所有序列化的值添加到队列中
            foreach (var item in arr)
            {
                queue.Enqueue(item);
            }
            return desNodes();

            TreeNode desNodes()
            {
                string s = queue.Dequeue();
                if (s == "#")
                {
                    return null;
                }
                // 先建立根节点
                TreeNode head = new TreeNode(int.Parse(s));
                head.left = desNodes();
                head.right = desNodes();
                return head;
            }
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
