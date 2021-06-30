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
            emptyT empty = new emptyT();
            empty.GetHashCode();
            //实例化可以访问类成员;带有static的可以直接访问
            Common.Case testCase = new Common.Case();
            //Console.WriteLine(problems.RestoreString(testCase.s, testCase.index));
            //problems.CuttingRope(120);
            //problems.checkSubarraySum(testCase.nums, 6);
            Console.ReadKey();
        }
    }
    class Problems
    {
        //168. Excel表列名称
        public string ConvertToTitle(int columnNumber)
        {
            if (columnNumber < 1) return null;
            string name = "";
            while (columnNumber >= 1)
            {
                columnNumber--;
                name = (char)(columnNumber % 26 + 'A') + name;
                columnNumber /= 26;
            }
            return name;
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

        //剑指 Offer 14- II.剪绳子 II
        public int CuttingRope(int n)
        {
            if (n < 4) return n - 1;
            int b = n % 3, p = 1000000007;
            long rem = 1, x = 3;
            //快速幂求余
            for (int a = n / 3 - 1; a > 0; a /= 2)
            {
                if (a % 2 == 1) rem = (rem * x) % p;
                x = (x * x) % p;
            }
            if (b == 0) return (int)(rem * 3 % p);
            if (b == 1) return (int)(rem * 4 % p);
            return (int)(rem * 6 % p);
        }

        //6. Z 字形变换
        //public string Convert(string s, int numRows)
        //{

        //}

        //149. 直线上最多的点数
        //public int MaxPoints(int[][] points)
        //{
        //    //同一条直线上的点斜率相同
        //    if (points.Length == 1) return 1;
        //    for (int i = 0; i < points.Length; i++)
        //    {
        //        if
        //    }
        //}

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

        #region 动态规划
        //剑指 Offer 62. 圆圈中最后剩下的数字 (约瑟夫环问题)
        public int LastRemaining(int n, int m)
        {
            int x = 0;
            for (int i = 2; i <= n; i++)
            {
                x = (x + m) % i;
            }
            return x;
        }
        //剑指 Offer 10- I. 斐波那契数列
        int[] cache = new int[101];
        public int Fib(int n)
        {
            //动态规划
            int a = 0, b = 1, sum;
            for (int i = 0; i < n; i++)
            {
                sum = (a + b) % 1000000007;
                a = b;
                b = sum;
            }
            return a;
            //递归超时
            //记忆化递归
            //if (n < 2) return n;
            //if (cache[n] == 0)
            //    cache[n] = (Fib(n - 1) + Fib(n - 2)) % 1000000007;
            //return cache[n];
        }
        //70. 爬楼梯
        //public int ClimbStairs(int n)
        //{

        //}

        #endregion

        #region 位运算
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
                    res |= (1 << i);
            }
            return res;
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
        #endregion 

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

    class emptyT
    {

    }

    struct MyStruct
    {
        public string MyProperty { get; set; }

    }
}
