using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace String
{
    class Program
    {
        static void Main(string[] args)
        {

            string pattern = "(Mr\\.?|Mrs\\.?|Miss|Ms\\.?)";
            string[] names = { "Mr. Henry Hunt", "Ms. Sara Samuels",
                         "Abraham Adams", "Ms. Nicole Norris" };

            foreach (var item in names)
            {
                Console.WriteLine(Regex.Replace(item, pattern, string.Empty));
            }

            var know = new Knowleage();

            var arr = new int[] { 3, 4, -1, 1 };
            know.ReverseWords("a good   example");

        }
    }

    class Problem
    {
        // 根据条件模拟
        // 将字符串转换为整数
        public int StrToInt(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            int len = str.Length;
            int index = 0;
            int res = 0;
            bool isNegtive = false;
            // 去除左边的空格
            while (index < len && str[index] == ' ') index++;
            if (index == len - 1) return 0;

            // 判断正负
            if (str[index] == '-') isNegtive = true;
            // 判断完之后需要进一位
            if (str[index] == '-' || str[index] == '+') index++;

            // 读取适当大小范围的数字
            while (index < len && str[index] >= '0' && str[index] <= '9')
            {
                int temp = str[index] - '0';
                // 最大值 2147483647  最小值 -2147483648
                // 通过 214748364 来判断下一步是否越界
                if (!isNegtive && (res > 214748364 || (res == 214748364 && temp >= 7))) return int.MaxValue;
                if (isNegtive && (-res < -214748364 || (-res == -214748364 && temp >= 8))) return int.MinValue;
                res = res * 10 + temp;
                index++;
            }
            if (isNegtive) return -res;
            return res;
        }
    }
    class Knowleage
    {

        #region 正则表达式
        public int CountValidWords(string sentence)
        {
            var tokens = sentence.Split(' ');
            int ans = 0;
            foreach (var item in tokens)
            {
                // 其中用 () 和 | 把正则中间部分分成两种情况，实际可以当成两个正则：^[,.!]$ 与 ^[a-z]+(-[a-z]+)?[,.!]?$
                // 要匹配完整的token：用 ^ 表示匹配到字符串起始位置，用 $ 表示匹配到字符串末尾
                // token只有1个标点符号，即第一种情况 ^[,.!]$ ，表示整个字符串是3个标点中任意1个
                // token有字母的情况下，即第二种情况 ^[a - z] + (-[a - z] +)?[,.!] ?$
                // 一定是1个或多个字母开头即 ^[a - z] +，后面可能有连字符 - 和字母即(-[a - z] +) ?，末尾可能有标点即[,.!] ?$

                if (Regex.IsMatch(item, "^([,.!]|[a-z]+(-[a-z]+)?[,.!]?)$")) ans++;
            }
            return ans;
        }
        #endregion

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

        // 验证回文串
        public bool IsPalindrome(string s)
        {
            var str = new StringBuilder();
            var arr = s.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (char.IsLetter(arr[i]) || char.IsDigit(arr[i])) str.Append(char.ToLower(arr[i]));
            }
            for (int left = 0, right = str.Length - 1; left < right; left++, right--)
            {
                if (str[left] != str[right]) return false;
            }
            return true;
        }

        // 反转字符串
        public void ReverseString(char[] s)
        {
            for (int left = 0, right = s.Length - 1; left < right; left++, right--)
            {
                var item = s[left];
                s[left] = s[right];
                s[right] = item;
            }
        }

        // 反转字符串中的单词
        public string ReverseWords(string s)
        {
            var wordArr = s.Trim().Split(' ');
            var res = new StringBuilder();
            for (int i = 0, j = wordArr.Length - 1; i < j; i++, j--)
            {
                // 收缩边界
                while (i < j && wordArr[i] == "") i++;
                while (j > i && wordArr[j] == "") j--;
                if (i >= j) break;
                var item = wordArr[i];
                wordArr[i] = wordArr[j];
                wordArr[j] = item;
            }
            for (int i = 0; i < wordArr.Length; i++)
            {
                if (wordArr[i] != "")
                {
                    res.Append(wordArr[i]);
                    res.Append(" ");
                }
            }
            return res.ToString().TrimEnd();
        }
    }

}


