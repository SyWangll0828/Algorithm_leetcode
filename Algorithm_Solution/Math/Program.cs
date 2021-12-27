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
            int n1 = 1, n2 = 2;
            int n3 = 1 << 2;
            n3 = n1 & n2;
            int[] test = new int[] { 3, 6, 3, 3 };
            char[] res = new char[] { 'h', 'e', 'l', 'l', 'o' };
            int[] res1 = new int[] { 1, 3 };
            int[] res2 = new int[] { 1, 2, 5, 2 };


            Console.ReadKey();
        }
    }
    class Problems
    {
        //645. 错误的集合
        public int[] FindErrorNums(int[] nums)
        {
            int n = nums.Length;
            int[] cnts = new int[n + 1];
            //用数组下标与对应的值模拟哈希表
            foreach (int x in nums) cnts[x]++;
            int[] ans = new int[2];
            for (int i = 1; i <= n; i++)
            {
                if (cnts[i] == 0) ans[1] = i;
                if (cnts[i] == 2) ans[0] = i;
            }
            return ans;
            //桶排序？
        }


        // 剑指 Offer 17. 打印从1到最大的n位数
        // 考虑大数问题
        public int[] PrintNumbers(int n)
        {
            int maxValue = (int)System.Math.Pow(10, n) - 1;
            int[] res = new int[maxValue];
            for (int i = 0; i < maxValue; i++)
            {
                res[i] = i + 1;
            }
            return res;
        }

        // 丑数
        public int NthUglyNumber(int n)
        {
            // 丑数都是由 2 3 5 的乘积得到的
            if (n <= 0) return 0;
            var dp = new int[n];
            dp[0] = 1;
            // 三指针，分别指向从起始位置*2、*3、*5的索引，
            int a = 0, b = 0, c = 0;
            for (int i = 1; i < n; i++)
            {
                int a2 = dp[a] * 2, b2 = dp[b] * 3, c2 = dp[c] * 5;
                // 对小的丑数计算求大的丑数，取最小值
                dp[i] = System.Math.Min(System.Math.Min(a2, b2), c2);
                // 指针移动
                if (dp[i] == a2) a++;
                if (dp[i] == b2) b++;
                if (dp[i] == c2) c++;
            }
            return dp[n - 1];
        }

        //任何大于1的数都可由2和3相加组成（根据奇偶证明）
        //因为2*2=1*4，2*3>1*5, 所以将数字拆成2和3，能得到的积最大
        //因为2*2*2<3*3, 所以3越多积越大
        //343. 整数拆分
        public int IntegerBreak(int n)
        {
            //n = 3a + b 数学推导
            //if (n < 4) return n - 1;
            //int a = n / 3, b = n % 3;
            //if (b == 0) return (int)System.Math.Pow(3, a);
            ////当 b=1,要将一个 1+3 转换为 2+2，因此返回 3^(a−1) * 4（例如 10=3*3*1 转换为10=3*2*2）
            //if (b == 1) return (int)System.Math.Pow(3, a - 1) * 4;
            ////当 b=2, 返回 3^a * 4 (例如 20=2*6*6*6)
            //return (int)System.Math.Pow(3, a) * 2;
            //贪心
            if (n < 4) return n - 1;
            long res = 1;
            while (n > 4)
            {
                res = res * 3;
                n -= 3;
            }
            return (int)(res * n);
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

        #region 位运算
        //技巧
        //1、与（&）
        //x&1=1 其余都为0 --> (x % 2 == 1) 二进制最右一位为1 是奇数
        //x & (x - 1) --> 把x最低位的二进制1给去掉 0&1=0
        //x & ~x --> 0
        // -x 取反加一
        //x & (-x) --> 得到最低位1  

        //2、或（|）
        // 有1则为1 1|0=1 1|1=1 0|0=0

        //3、异或（^）  可表示无进位相加
        //1^1=0  不同为1，相同为0
        //x ^ 0 = x, x ^ x = 0    a ^ b = c, a ^ c = b, b ^ c = a

        //4、左移（<<）
        //5、右移（>>） -->  x >> 2 = x / 4 右移2位
        //如果数字原先是负数，则右移之后在最左边补n个1。
        //n >>= 1 ： 将二进制数字 n 无符号右移一位

        //大写变小写、小写变大写：字符 ^= 32
        //大写变小写、小写变小写：字符 |= 32
        //大写变大写、小写变大写：字符 &= -33 

        //如果 n 是正整数并且 n & (n - 1) = 0 或者 n & (-n) = n，那么 n 就是 2 的幂。


        //剑指 Offer 65. 位运算做加法
        public int Add(int a, int b)
        {
            //模拟二进制位相加得
            //进位 carry=(a&b)<<1
            //不考虑进位 sum=a^b
            int sum = a ^ b;
            int carry = (a & b) << 1;
            while (carry != 0)
            {
                a = sum;
                b = carry;
                sum = a ^ b;
                carry = (a & b) << 1;
            }
            return sum;
        }

        //剑指 Offer 56 - II. 数组中数字出现的次数 II
        public int SingleNumber(int[] nums)
        {
            //n 是数字出现的次数
            int res = 0;
            for (int i = 0; i < 32; i++)
            {
                int oneCount = 0;
                for (int j = 0; j < nums.Length; j++)
                {
                    oneCount += (nums[j] >> i) & 1;
                }
                //如果1的个数不是3的倍数，说明那个只出现一次的数字的二进制位中在这一位是1
                if (oneCount % 3 == 1)
                    res = res | (1 << i);
            }
            return res;
        }

        // 位运算 
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

        //67. 二进制求和
        public string AddBinary(string a, string b)
        {
            int i = a.Length - 1, j = b.Length - 1, add = 0;
            StringBuilder ans = new StringBuilder();
            while (i >= 0 || j >= 0 || add != 0)
            {
                int x = i >= 0 ? a[i] - '0' : 0;
                int y = j >= 0 ? b[j] - '0' : 0;
                int result = x + y + add;
                ans.Append(result % 10);
                add = result / 10;
                i--;
                j--;
            }
            // 计算完以后的答案需要翻转过来
            StringBuilder res = new StringBuilder();
            for (int m = ans.Length - 1; m >= 0; m--)
            {
                res.Append(ans[m]);
            }
            return res.ToString();
        }

        #endregion

    }
}
