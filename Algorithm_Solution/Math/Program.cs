using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    class Program
    {
        static void Main(string[] args)
        {
            Problems problems = new Problems();
            //实例化可以访问类成员;带有static的可以直接访问
            Common.Case testCase = new Common.Case();
            //Console.WriteLine(problems.RestoreString(testCase.s, testCase.index));
            problems.checkSubarraySum(testCase.nums, 6);
            Console.ReadKey();
        }
    }
    class Problems
    {
        #region 前缀和问题
        //523. 连续的子数组和
        public bool checkSubarraySum(int[] nums, int k)
        {
            //同余定理：如果两个整数m、n满足n-m能被k整除，那么n和m对k同余
            int n = nums.Length;
            int[] sum = new int[n + 1];
            //处理前缀和数组
            for (int i = 1; i <= n; i++) sum[i] = sum[i - 1] + nums[i - 1];
            HashSet<int> set = new HashSet<int>();
            for (int i = 2; i <= n; i++)
            {
                set.Add(sum[i - 2] % k);
                if (set.Contains(sum[i] % k)) return true;
            }
            return false;
        }
        //525. 连续数组
        public int FindMaxLength(int[] nums)
        {
            //求最长一段区间和为 00 的子数组
            int n = nums.Length;
            int[] sum = new int[n + 1];
            //处理前缀和数组
            for (int i = 1; i <= n; i++) sum[i] = sum[i - 1] + (nums[i - 1] == 1 ? 1 : -1);
            int ans = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(0, 0);
            //
            for (int i = 2; i <= n; i++)
            {
                if (!dict.ContainsKey(sum[i - 2])) dict.Add(sum[i - 2], i - 2);
                if (dict.ContainsKey(sum[i])) ans = System.Math.Max(ans, i - dict[sum[i]]);
            }
            return ans;
        }
        #endregion
        


        //约瑟夫环问题，动态规划
        //剑指 Offer 62. 圆圈中最后剩下的数字
        public int LastRemaining(int n, int m)
        {
            int x = 0;
            for (int i = 2; i <= n; i++)
            {
                x = (x + m) % i;
            }
            return x;
        }

        

    }
}
