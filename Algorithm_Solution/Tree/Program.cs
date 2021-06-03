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

    }

    class TreeNode
    {
        public int MyProperty;
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
