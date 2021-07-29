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

            TreeNode root = new TreeNode(2)
            {
                left = new TreeNode(2),
                right = new TreeNode(2)
                //{
                //    left = new TreeNode(15),
                //    right = new TreeNode(7)
                //}
            };
            Knowleage.IsValidBST(root);
            problems.VerifyPostorder(t2);
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
            return valid(0, postorder.Length - 1);

            bool valid(int left, int right)
            {
                if (left >= right)
                {
                    return true;
                }
                // 从前找第一个大于根节点值的下标与最后一个小于根节点值的下标
                // 即分界点下标
                int index = left;
                while (postorder[index] < postorder[right])
                {
                    index++;
                }
                for (int i = index; i < right; i++)
                {
                    if (postorder[i] < postorder[right])
                    {
                        return false;
                    }
                }
                // 左右树递归返回结果
                return valid(left, index - 1) && valid(index, right - 1);
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
        public void BackOrder(TreeNode root)
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
            if (root == null)
            {
                return;
            }
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
                    if (root.left != null)
                    {
                        queue.Enqueue(root.left);
                    }
                    if (root.right != null)
                    {
                        queue.Enqueue(root.right);
                    }
                }
            }
        }
        // 判断一个树是二叉搜索树
        // 中序遍历从左到右递增
        static long preValue = long.MinValue;
        public static bool IsValidBST(TreeNode node)
        {
            if (node == null)
            {
                return true;
            }
            // 检查左树
            bool isLeftBst = IsValidBST(node.left);
            if (!isLeftBst)
            {
                return false;
            }
            // 根据二叉搜索树的中序遍历递增来判断
            if (node.val <= preValue)
            {
                return false;
            }
            else
            {
                preValue = node.val;
            }
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
        // todo
        public static bool IsBanlanceBT(TreeNode node)
        {
            return false;
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
        public string serialize(TreeNode root)
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
