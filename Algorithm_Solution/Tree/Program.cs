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
            problems.BuildTree(t1, t2);
            //problems.IntToRoman(testCase.MyProperty2);
            Console.ReadKey();
        }
    }

    class Problems
    {
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
        {   //递归 -- 栈的实现
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

        //BST 二叉搜索树 
        //中序遍历结果是按升序排列的
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

        


    }

    //297. 二叉树的序列化与反序列化
    class Codec
    {
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {

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
