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
            int[] t2 = new int[] { 9, 3, 15, 20, 7 };

            TreeNode root = new TreeNode(3)
            {
                left = new TreeNode(9),
                right = new TreeNode(20)
                {
                    left = new TreeNode(15),
                    right = new TreeNode(7)
                }
            };
            problems.LevelOrder2(root);
            //problems.IntToRoman(testCase.MyProperty2);
            Console.ReadKey();
        }
    }

    class Problems
    {
        //二叉树的层序遍历并输出
        public int[] LevelOrder(TreeNode root)
        {
            //二叉树的层序遍历
            //特殊情况
            if (root == null) return new int[] { };
            //将根节点依次放到队列中去
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            List<int> list = new List<int>();
            while (q.Any())
            {
                TreeNode cur = q.Dequeue();
                list.Add(cur.val);
                if (cur.left != null)
                    q.Enqueue(cur.left);
                if (cur.right != null)
                    q.Enqueue(cur.right);
            }
            return list.ToArray();
        }
        //二叉树层序遍历 每层循环个节点 可用与求二叉树的深度、数组输出二叉树等
        public IList<IList<int>> LevelOrder2(TreeNode root)
        {
            List<IList<int>> list = new List<IList<int>>();
            //特殊情况
            if (root == null) return list;
            //将根节点依次放到队列中去
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            int n = 1;
            while (q.Any())
            {
                List<int> temp = new List<int>();
                int count = q.Count;
                for (int i = 0; i < count; i++)
                {
                    TreeNode cur = q.Dequeue();
                    temp.Add(cur.val);
                    if (cur.left != null)
                        q.Enqueue(cur.left);
                    if (cur.right != null)
                        q.Enqueue(cur.right);
                }
                if ((n & 1) == 0) temp.Reverse();
                list.Add(temp);
                n++;
            }
            return list;
        }
        //剑指 Offer 34. 二叉树中和为某一值的路径
        public IList<IList<int>> PathSum(TreeNode root, int target)
        {
            List<IList<int>> res = new List<IList<int>>();
            List<int> list = new List<int>();
            dfs(root, target);
            return res;

            void dfs(TreeNode node, int t)
            {
                if (node == null) return;
                //先序遍历 + 路径记录
                int temp = node.val;
                list.Add(temp);
                t -= temp;
                if (node.left == null && node.right == null && t == 0)
                    //需要使用new List<int>(),防止后续更改list，值跟随修改
                    res.Add(new List<int>(list));
                dfs(node.left, t);
                dfs(node.right, t);
                //回溯之前去掉list中最后的叶子节点
                list.RemoveAt(list.Count - 1);
            }
        }

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

        //104. 二叉树的最大深度
        public int MaxDepth(TreeNode root)
        {
            return root == null ? 0 : Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
        }

        //前序/后序+中序序列可以唯一确定一棵二叉树
        //对于任意一颗树而言
        //前序遍历的形式：[根节点, [左子树的前序遍历结果], [右子树的前序遍历结果]]
        //中序遍历的形式：[[左子树的中序遍历结果], 根节点, [右子树的中序遍历结果]]
        //后序遍历的形式：[[右子树的中序遍历结果], 根节点, [左子树的中序遍历结果]]
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            if (preorder.Length != inorder.Length) return null;
            Dictionary<int, int> dt = new Dictionary<int, int>();
            //将中序遍历存储记录对应下标
            for (int i = 0; i < inorder.Length; i++)
            {
                dt.Add(inorder[i], i);
            }
            return reBuildTree(0, preorder.Length - 1, 0, inorder.Length - 1);

            TreeNode reBuildTree(int perLeft, int perRight, int inLeft, int inRight)
            {
                //终止条件
                if (perLeft > perRight) return null;
                //根节点位置
                int rootIndex = dt[preorder[perLeft]];
                //左子树结点数
                int leftSubTreeNodes = rootIndex - inLeft;
                //定义根节点
                TreeNode node = new TreeNode(preorder[perLeft]);
                //前序遍历中[根节点之后leftSubTreeNodes个元素]对应[中序遍历中从左边界到根节点之间的元素]
                node.left = reBuildTree(perLeft + 1, perLeft + leftSubTreeNodes, inLeft, rootIndex);
                //前序遍历中[左边界+左结点数量+1到右边界元素]对应[中序遍历中从根节点+1到右边界元素]
                node.right = reBuildTree(perLeft + 1 + leftSubTreeNodes, perRight, rootIndex + 1, inRight);
                return node;
            }
        }

        #region 深度优先搜索
        //112. 路径总和
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
                return false;
            //叶子节点
            if (root.left == null && root.right == null)
                return targetSum == root.val;
            return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
        }
        //617. 合并二叉树
        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null)
                return null;
            if (root1 == null || root2 == null)
                return root1 == null ? root2 : root1;
            TreeNode node = new TreeNode(root1.val + root2.val);
            node.left = MergeTrees(root1.left, root2.left);
            node.right = MergeTrees(root1.right, root2.right);
            return node;
        }
        //反转链表
        public TreeNode InvertTree(TreeNode root)
        {
            //自底向上 后序遍历？先递归 在求解
            //自顶向下 前序遍历？先求解 在递归
            if (root == null) return null;
            TreeNode left = InvertTree(root.left);
            TreeNode right = InvertTree(root.right);
            root.left = right;
            root.right = left;
            return root;

        }
        #endregion

        #region 广度优先搜索

        #endregion

        #region BST 二叉搜索树 
        //中序遍历结果是按升序排列的
        //后序遍历定义： [左子树 | 右子树 | 根节点] ，即遍历顺序为 “左、右、根” 。
        //二叉搜索树定义： 左子树中所有节点的值 << 根节点的值；右子树中所有节点的值 >> 根节点的值；其左、右子树也分别为二叉搜索树
        //结点值:left<root<right
        public bool VerifyPostorder(int[] postorder)
        {
            return helper(0, postorder.Length - 1);
            bool helper(int left, int right)
            {
                //特殊情况
                if (left >= right) return true;
                //先找到左子树与右子树的分界点
                int mid = left;
                while (postorder[mid] < postorder[right]) mid++;
                //验证右子树中的值是不是比根结点的值大
                int temp = mid;
                for (int i = temp; i < right; i++)
                {
                    if (postorder[temp] < postorder[right]) return false;
                }
                //分别验证左子树与右子树
                return helper(0, mid - 1) && helper(mid, right - 1);
            }

        }
        //653. 两数之和 IV - 输入 BST
        public bool FindTarget(TreeNode root, int k)
        {
            //中序遍历
            var list = new List<int>();
            traversal(root, list);
            int low = 0, high = list.Count - 1;
            while (low < high)
            {
                int sum = list[low] + list[high];
                if (k == sum)
                    return true;
                else if (sum > k)
                    high--;
                else
                    low++;
            }
            return false;
            //局部函数
            void traversal(TreeNode node, List<int> resList)
            {
                if (node == null)
                    return;
                traversal(node.left, resList);
                resList.Add(node.val);
                traversal(node.right, resList);
            }
        }
        #endregion

    }

    //297. 二叉树的序列化与反序列化
    //class Codec
    //{
    //    // Encodes a tree to a single string.
    //    public string serialize(TreeNode root)
    //    {
    //    }

    //    // Decodes your encoded data to tree.
    //    public TreeNode deserialize(string data)
    //    {

    //    }
    //}

    class Knowleage
    {
        //先序遍历   根节点-左节点-右节点
        public void PreOrder(TreeNode root)
        {

            var list = new List<int>();
            if (root == null) return;
            //递归
            preOrder(root);
            void preOrder(TreeNode node)
            {
                if (node == null) return;
                list.Add(root.val);
                PreOrder(root.left);
                PreOrder(root.right);
            }

            //栈实现
            var stack = new Stack<int>();
            stack.Push(root.val);
            while (stack.Any())
            {
                list.Add(stack.Pop());
                //先右再左，利用栈的后进先出
                if (root.right != null)
                    stack.Push(root.right.val);
                if (root.left != null)
                    stack.Push(root.left.val);
            }
        }
        //中序遍历  左节点-根节点-右节点
        public void InOrder(TreeNode root)
        {

            var list = new List<int>();
            if (root == null) return;
            //递归
            inOrder(root);
            void inOrder(TreeNode node)
            {
                if (node == null) return;
                PreOrder(root.left);
                list.Add(root.val);
                PreOrder(root.right);
            }

            //栈实现
            var stack = new Stack<int>();
            while (stack.Any() || root != null)
            {
                //先将所有左节点入栈
                if (root != null)
                {
                    stack.Push(root.val);
                    root = root.left;
                } 
                else
                {
                    int temp = stack.Pop();
                    list.Add(temp);
                    root = root.right;
                }
            }
        }
        //后序遍历  左节点-右节点-根节点
        public void BackOrder(TreeNode root)
        {
            var list = new List<int>();
            if (root == null) return;
            //递归
            inOrder(root);
            void inOrder(TreeNode node)
            {
                if (node == null) return;
                PreOrder(root.left);
                list.Add(root.val);
                PreOrder(root.right);
            }

            //栈实现
            var stack = new Stack<int>();
            var help = new Stack<int>();
            stack.Push(root.val);
            while (stack.Any())
            {
                int temp = stack.Pop();
                help.Push(temp);
                //先左再右，利用栈的后进先出
                //根-右-左
                //辅助栈 -- 左-右-跟
                if (root.left != null)
                    stack.Push(root.left.val);
                if (root.right != null)
                    stack.Push(root.right.val);
            }
            while (help.Any()) list.Add(help.Pop());
        }
    }

    class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
