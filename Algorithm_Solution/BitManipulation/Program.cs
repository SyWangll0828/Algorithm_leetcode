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
            Knowledge k = new Knowledge();
            //静态方法直接访问
            //bool isPrime= Knowledge.isPrime(4);
            problems.HammingWeight(9);
            //problems.AddBinary("11", "1");
        }
    }

    class Problems
    {
        //技巧
        //(x & 1) == 1 ---等价---> (x % 2 == 1) 二进制最右一位为1
        //(x & 1) == 0 ---等价---> (x % 2 == 0)
        //n >>= 1 ： 将二进制数字 nn 无符号右移一位（ Java 中无符号右移为 ">>>>>>" ）
        //x / 2 ---等价---> x >> 1
        //x & (x - 1) ------> 把x最低位的二进制1给去掉
        //x & -x -----> 得到最低位的1
        //x & ~x -----> 0
        //x ^ 0 = x, x ^ x = 0    a ^ b = c, a ^ c = b, b ^ c = a
        //大写变小写、小写变大写：字符 ^= 32
        //大写变小写、小写变小写：字符 |= 32
        //大写变大写、小写变大写：字符 &= -33 
        //如果 n 是正整数并且 n & (n - 1) = 0 或者 n & (-n) = n，那么 n 就是 2 的幂。
        //4的幂-- n % 3 == 1

        //231. 2 的幂
        public bool IsPowerOfTwo(int n)
        {
            return n > 1 && (n & (n - 1)) == 0;
        }

        //位运算 
        public static int[] PlusOne(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                digits[i]++;
                digits[i] %= 10;
                if (digits[i] != 0)
                    return digits;
            }
            digits = new int[digits.Length + 1];
            digits[0] = 1;
            return digits;
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

        //191. 位1的个数
        public int HammingWeight(int n)
        {
            int res = 0;
            int n1 = n;

            //for (int i = 0; i < 32; i++)
            //{
            //    res += ((n >> i) & 1);
            //}
            //return res;

            //消除二进制末尾的1
            while (n != 0)
            {
                n = n & (n - 1);
                res++;
            }
            return res;
        }

        //338. 比特位计数
        public int[] CountBits(int n)
        {
            int[] res = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                //(i & 1)== 0 --偶数
                res[i] = res[i >> 1] + (i & 1);
            }
            return res;
        }
    }

    class Knowledge
    {
        //判断一个数是否是素数(质数)
        public static bool isPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

    }
}
