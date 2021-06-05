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
            //problems.IntToRoman(testCase.MyProperty2);
            Console.ReadKey();
        }
    }

    class Problems
    {
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
