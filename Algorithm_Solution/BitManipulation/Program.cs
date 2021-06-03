using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.Case testCase = new Common.Case();
            Problems problems = new Problems();
            problems.AddBinary("11", "1");
        }
    }

    class Problems
    {
        //231. 2 的幂
        public bool IsPowerOfTwo(int n)
        {
            //如果 n 是正整数并且 n & (n - 1) = 0 或者 n & (-n) = n，那么 n 就是 2 的幂。
            //4的幂-- n % 3 == 1
            return n > 1 && (n & (n - 1)) == 0;
        }

        //lowbit运算

        //67. 二进制求和
        public string AddBinary(string a, string b)
        {
            int lengthA = a.Length;
            int lengthB = b.Length;
            //先保持两字符串长度一致
            while (lengthA > lengthB)
            {
                b = '0' + b;
                lengthB++;
            }
            while (lengthB > lengthA)
            {
                a = '0' + a;
                lengthA++;
            }
            string res = "";
            //模拟进位
            int carry = 0;//进位
            for (int i = lengthA - 1; i >= 0; i--)
            {
                int sum = a[i] - '0' + b[i] - '0' + carry;
                res = ((char)(sum % 2 + '0')).ToString() + res;
                carry = sum / 2;//进位更新
            }
            res = carry > 0 ? '1' + res : res;
            return res;
        }
        //https://leetcode-cn.com/problems/power-of-two/solution/5chong-jie-fa-ni-ying-gai-bei-xia-de-wei-6x9m/

        //
        public string ToLowerCase(string s)
        {
            var build = new StringBuilder();
            build.Append(s);
            for (int i = 0; i < build.Length; i++)
            {
                build[i] = build[i] >= 'A' && build[i] <= 'Z' ? (char)(build[i] + 32) : build[i];

            }
            return build.ToString();

        }

        public int[] FindErrorNums(int[] nums)
        {
            int sum = 0, temp = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += i;
                temp += nums[i];
            }
            return new int[2];
        }

        //461. 汉明距离
        //计算 x 和 y 之间的汉明距离，可以先计算 x异或y，然后统计结果中等于1的位数。
        public int HammingDistance(int x, int y)
        {
            //return Convert.ToString(x ^ y, 2).Count(c => c == '1');
            //异或
            int s = x ^ y, res = 0;
            //移位操作实现位计数
            while (s != 0)
            {
                res += s & 1;
                //右移一位
                s >>= 1;
            }
            return res;
        }
    }
}
