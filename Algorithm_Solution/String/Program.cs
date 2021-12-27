using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Problem
    {
        #region StringBuilder API使用
        // 1047. 删除字符串中的所有相邻重复项
        public string RemoveDuplicates(string s)
        {
            //StringBuilder可以充当栈
            StringBuilder sb = new StringBuilder();
            foreach (var c in s)
            {
                int len = sb.Length;
                if (len > 0 && c == sb[len - 1])
                    sb.Remove(len - 1, 1);
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }
        #endregion

        // 通过自身旋转模拟指定长度字符旋转效果
        // 左旋转字符
        public string ReverseLeftWords(string s, int n)
        {
            int len = s.Length;
            n = n % len;
            char[] arr = s.ToArray();
            // 先全部旋转
            helper(0, len - 1);
            // 再旋转0-(n-1)
            helper(0, n - 1);
            // 再旋转n-(len-1)
            helper(n, len - 1);
            return new string(arr);

            void helper(int left, int right)
            {
                while (left < right)
                {
                    char t = arr[left];
                    arr[left] = s[right];
                    arr[right] = t;
                    left++;
                    right--;
                }
            }
        }
        // 类似的方法
        // 189. 旋转数组
        public void Rotate(int[] nums, int k)
        {
            k %= nums.Length;
            //先翻转原数组
            reverse(nums, 0, nums.Length - 1);
            //然后左 右部分数组在进行翻转
            reverse(nums, 0, k - 1);
            reverse(nums, k, nums.Length - 1);

            void reverse(int[] temp, int left, int right)
            {
                while (left < right)
                {
                    int t = temp[left];
                    temp[left] = temp[right];
                    temp[right] = t;
                    left++;
                    right--;
                }
            }
        }
    }
}

class Knowleage
{

}
}
