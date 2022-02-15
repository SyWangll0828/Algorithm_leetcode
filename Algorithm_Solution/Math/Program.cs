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

        //剑指 Offer 62. 圆圈中最后剩下的数字 (约瑟夫环问题)
        public int LastRemaining(int n, int m)
        {
            int ans = 0;
            // 最后一轮剩下2个人，所以从2开始反推
            for (int i = 2; i <= n; i++)
            {
                ans = (ans + m) % i;
            }
            // 返回下标
            return ans;
        }

        // 最小时间差(用记录隔天的时间来处理时间差)
        public int FindMinDifference(IList<string> timePoints)
        {
            // 特判 总共就1439种时间 不然就是存在重复时间 
            if (timePoints.Count >= 1440) return 0;
            int[] temp = new int[timePoints.Count * 2];
            // 记录当天时间分钟和隔天之后的分钟
            for (int i = 0, index = 0; i < timePoints.Count; i++, index += 2)
            {
                var arr = timePoints[i].Split(':');
                int h = int.Parse(arr[0]), min = int.Parse(arr[1]);
                temp[index] = h * 60 + min;
                temp[index + 1] = temp[index] + 1440;
            }
            Array.Sort(temp);
            int res = temp[1] - temp[0];
            for (int i = 1; i < temp.Length; i++) res = System.Math.Min(res, temp[i] - temp[i - 1]);
            return res;
        }

    }

    class Knowledge
    {
        // 判断一个数是否是素数(质数)
        public static bool isPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        // 求一个数的所有正因数和
        public static int getFactorSum(int num)
        {
            // 不包括自己本身
            int res = 1;
            for (int i = 2; i <= num / i; i++)
            {
                if ((num % i) == 0) res += i + num / i;
            }
            return res;
        }

        // 求0-n的比特位计数  
        public int[] CountBits(int n)
        {
            var res = new int[n + 1];
            // 偶数 为更小的数左移一位，增加了0的个数，1的个数不变
            // 奇数，为比该奇数小一位的偶数1的个数+1
            for (int i = 1; i <= n; i++)
            {
                if ((i & 1) == 0)
                    res[i] = res[i / 2];
                else
                    res[i] = res[i - 1] + 1;
            }
            return res;
        }

        // 整数拆分最大乘积
        // 任何大于1的数都可由2和3相加组成（根据奇偶证明）
        // 因为2*2=1*4，2*3>1*5, 所以将数字拆成2和3，能得到的积最大
        // 因为2*2*2<3*3, 所以3越多积越大
        // 343. 整数拆分
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

        // 第 N 位数字
        public int findNthDigit(int n)
        {
            if (n <= 9)
                return n;
            int digits = 1;
            long begin = 1;
            long count = 10;
            long num = n;
            while (num > count)
            {
                num -= count;
                digits++;
                begin *= 10;
                count = digits * begin * 9;
            }
            // 找到是在哪个digits位数的数上
            long res = begin + num / digits;
            // 在当前数上看是第几个位置的字符
            return res.ToString()[(int)num % digits] - '0';
        }

        // 1～n 整数中 1 出现的次数
        public int CountDigitOne(int n)
        {
            // 高低位
            int high = n;
            int low = 0;
            // 当前位上的值
            int cur = 0;
            int res = 0;
            // 位数 个位 十位 百位...
            int num = 1;
            // 高位与当前位从右往左移动
            while (high != 0 || cur != 0)
            {
                cur = high % 10;
                // 高位移动
                high /= 10;
                // 当前位与要统计的值（1）进行比较
                if (cur == 0) res += high * num;
                else if (cur == 1) res += (high * num + low + 1);
                else res += (high + 1) * num;
                low = cur * num + low;
                num *= 10;
            }
            return res;
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
