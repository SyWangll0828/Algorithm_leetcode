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



    }
    class Knowleage
    {
        #region StringBuilder API使用
        // 1047. 删除字符串中的所有相邻重复项
        // remove 可以模拟缩小字符长度
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

        // 简化路径
        // 与Append 相反 Insert 可以在头部插入 与栈搭配使用模拟反转效果
        public string SimplifyPath(string path)
        {
            if (path == null || path.Length == 0) return "";
            var stack = new Stack<string>();
            var array = path.Split('/');
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] == ".." && stack.Any()) stack.Pop();
                else if (array[i] != "." && array[i] != "" && array[i] != "..") stack.Push(array[i]);
            }
            var res = new StringBuilder();
            while (stack.Any()) res.Insert(0, stack.Pop()).Insert(0, '/');
            return res.Length == 0 ? "/" : res.ToString();
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

        // 所给字符串是否是数值
        public bool IsNumber(string s)
        {
            if (s == null || s.Length == 0) return false;
            var array = s.Trim().ToArray();
            // 定义标签
            bool numFlag = false;
            bool eFlag = false;
            bool dotFlag = false;
            for (int i = 0; i < array.Length; i++)
            {
                // 遇到数字
                if (array[i] >= '0' && array[i] <= '9') numFlag = true;
                else if (array[i] == '.')
                {
                    // .之前不能出现.和e
                    if (dotFlag || eFlag) return false;
                    dotFlag = true;
                }
                else if (array[i] == 'e' || array[i] == 'E')
                {
                    // 小数可以是 46. / 46.7 / .46
                    // e之前不能出现e,一定要出现数字（e后面可以跟+ - 1e-1表示1*0.1）
                    if (eFlag || !numFlag) return false;
                    eFlag = true;
                    // 数字标记重置 (123e不是数值)
                    numFlag = false;
                }
                else if (array[i] == '-' || array[i] == '+')
                {
                    // + - 只能出现在0位置或者e后面（出现在数字后面就变成表达式了！）
                    if (i > 0 && array[i - 1] != 'e' && array[i - 1] != 'E') return false;
                }
                else
                    // 其他不符合条件的情况
                    return false;
            }
            return numFlag;
        }

    }

}


