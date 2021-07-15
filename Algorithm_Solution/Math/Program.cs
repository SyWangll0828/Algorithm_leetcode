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
            int n1 = 1, n2 = 2;
            int n3 = 3;
            n3 = n1 & n2;
            //Console.WriteLine(problems.RestoreString(testCase.s, testCase.index));
            char[] res = new char[] { 'h', 'e', 'l', 'l', 'o' };
            int[] res1 = new int[] { 1, 3 };
            int[] res2 = new int[] { 1, 2, 5, 2 };
            problems.NthUglyNumber(10);
            Console.ReadKey();
        }
    }
    class Problems
    {
        public int NthUglyNumber(int n)
        {
            //一个数m是另一个数n的因子，是指n能被m整除
            //一个大丑数可以由小丑数*2/3/5 得到
            //三指针
            //第一个丑数乘以2后大于M的结果记为M2。
            //同样，我们把已有的每一个一个丑数乘以3和5，
            //能得到第一个大于M的结果M3和M5。那么下一个丑数应该是M2、M3和M5这3个数的最小者。
            if (n <= 0) return 0;
            int[] dp = new int[n];
            dp[0] = 1;
            int a = 0, b = 0, c = 0;
            for (int i = 1; i < n; i++)
            {
                int a2 = dp[a] * 2, b2 = dp[b] * 3, c2 = dp[c] * 5;
                dp[i] = System.Math.Min(System.Math.Min(a2,b2),c2 );
                if (dp[i] == a2) a++;
                if (dp[i] == b2) b++;
                if (dp[i] == c2) c++;
            }
            return dp[n - 1];
        }
        //移动零
        public void MoveZeroes(int[] nums)
        {
            //双指针
            int len = nums.Length;
            int slow = 0, quick = 1;
            while (slow < len && quick < len)
            {
                if (nums[slow] == 0 && nums[quick] != 0)
                {
                    swap(slow, quick);
                    slow++;
                    quick++;
                }
                else if (nums[slow] == 0 && nums[quick] == 0)
                    quick++;
                else
                    slow++;
                quick++;
            }

            void swap(int low, int high)
            {
                int temp = nums[high];
                nums[high] = nums[low];
                nums[low] = temp;
            }
        }

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

        //451. 根据字符出现频率排序
        public string FrequencySort(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (var c in s)
            {
                dict[c] = dict.TryGetValue(c, out var n) ? n + 1 : 1;
            }
            //List<char> list = new List<char>(dict.Keys);
            //list.Sort((a, b) => dict[b].CompareTo(dict[a]));
            var arr = dict.Keys.ToArray();
            //按照字符从小到大进行排序
            Array.Sort(arr, (a, b) => dict[b].CompareTo(dict[a]));
            StringBuilder sb = new StringBuilder();
            foreach (var c in arr)
            {
                sb.Append(c, dict[c]);
            }
            return sb.ToString();
        }


        //剑指 Offer 17. 打印从1到最大的n位数
        //考虑大数问题
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

        #region 模拟 找规律
        //240. 搜索二维矩阵 II
        public bool SearchMatrix(int[][] matrix, int target)
        {
            //右上角的数
            //行中最大，列中最小
            //int row = 0;
            //int col = matrix[0].Length - 1;
            //while (row < matrix.Length && col >= 0)
            //{
            //    if (matrix[row][col] == target) return true;
            //    else if (matrix[row][col] > target) col--;
            //    else row++;
            //}
            //return false;

            //左下角的数
            //行中最小，列中最大
            int row = matrix.Length - 1;
            int col = 0;
            while (row >= 0 && col < matrix[0].Length)
            {
                if (matrix[row][col] == target) return true;
                else if (matrix[row][col] > target) row--;
                else col++;
            }
            return false;
        }

        //48. 旋转图像
        public void Rotate(int[][] matrix)
        {
            //90°翻转 == 先水平再对角线翻转
            int n = matrix.Length;
            //顺时针旋转90°
            //先水平翻转
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[n - i - 1][j];
                    matrix[n - i - 1][j] = temp;
                }
            }
            //再对角线翻转
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; ++j)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = temp;
                }
            }

            //原地翻转  找规律
            //int n = matrix.length;
            for (int i = 0; i < n / 2; ++i)
            {
                for (int j = 0; j < (n + 1) / 2; ++j)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[n - j - 1][i];
                    matrix[n - j - 1][i] = matrix[n - i - 1][n - j - 1];
                    matrix[n - i - 1][n - j - 1] = matrix[j][n - i - 1];
                    matrix[j][n - i - 1] = temp;
                }
            }
        }
        //54. 螺旋矩阵
        public IList<int> SpiralOrder(int[][] matrix)
        {
            //模拟螺旋走位
            List<int> res = new List<int>();
            int up = 0, down = matrix.Length - 1, left = 0, right = matrix[0].Length - 1, index = matrix.Length * matrix[0].Length;
            while (index >= 1)
            {
                //从左到右
                for (int i = left; i <= right && index >= 1; i++)
                {
                    res.Add(matrix[up][i]);
                    index--;
                }
                up++;
                //从上到下
                for (int i = up; i <= down && index >= 1; i++)
                {
                    res.Add(matrix[i][right]);
                    index--;
                }
                right--;
                //从右到左
                for (int i = right; i >= left && index >= 1; i--)
                {
                    res.Add(matrix[down][i]);
                    index--;
                }
                down--;
                //从下到上
                for (int i = down; i >= up && index >= 1; i--)
                {
                    res.Add(matrix[i][left]);
                    index--;
                }
                left++;
            }
            return res;
        }

        //59. 螺旋矩阵Ⅱ
        public int[][] GenerateMatrix(int n)
        {
            int[][] res = new int[n][];
            for (int i = 0; i < n; i++)
            {
                if (res[i] == null) res[i] = new int[n];
            }
            int up = 0, down = n - 1, left = 0, right = n - 1, index = 1;
            while (index <= n * n)
            {
                for (int i = left; i <= right; i++)
                {
                    res[up][i] = index++;
                }
                up++;
                for (int i = up; i <= down; i++)
                {
                    res[i][right] = index++;
                }
                right--;
                for (int i = right; i >= left; i--)
                {
                    res[down][i] = index++;
                }
                down--;
                for (int i = down; i >= up; i--)
                {
                    res[i][left] = index++;
                }
                left++;
            }
            return res;
        }
        #endregion

        #region 位运算
        //技巧
        //1、与（&）
        //1&1=1 其余都为0 --> (x % 2 == 1) 二进制最右一位为1
        //x & (x - 1) --> 把x最低位的二进制1给去掉 0&1=0
        //x & -x --> 得到最低位的1  
        //x & ~x --> 0
        //2、或（|）
        //3、异或（^）
        //x ^ 0 = x, x ^ x = 0    a ^ b = c, a ^ c = b, b ^ c = a
        //4、左移（<<）
        //5、右移（>>） -->  x >> 2 = x / 4 右移2位
        //如果数字原先是负数，则右移之后在最左边补n个1。
        //n >>= 1 ： 将二进制数字 n 无符号右移一位

        //大写变小写、小写变大写：字符 ^= 32
        //大写变小写、小写变小写：字符 |= 32
        //大写变大写、小写变大写：字符 &= -33 
        //如果 n 是正整数并且 n & (n - 1) = 0 或者 n & (-n) = n，那么 n 就是 2 的幂。

        public int FindDuplicate(int[] nums)
        {
            int len = nums.Length;
            //位运算 异或
            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                int temp = sum;
                sum ^= nums[i];
                if (temp == sum) return nums[i];
            }
            return -1;
        }

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
            //int lengthA = a.Length;
            //int lengthB = b.Length;
            ////先保持两字符串长度一致
            //while (lengthA > lengthB)
            //{
            //    b = '0' + b;
            //    lengthB++;
            //}
            //while (lengthB > lengthA)
            //{
            //    a = '0' + a;
            //    lengthA++;
            //}
            //string res = "";
            ////模拟进位
            //int carry = 0;//进位
            //for (int i = lengthA - 1; i >= 0; i--)
            //{
            //    int sum = a[i] - '0' + b[i] - '0' + carry;
            //    res = ((char)(sum % 2 + '0')).ToString() + res;
            //    carry = sum / 2;//进位更新
            //}
            //res = carry > 0 ? '1' + res : res;
            //return res;

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

        //剑指 Offer 56 - I. 数组中数字出现的次数
        public int[] SingleNumbers(int[] nums)
        {
            int len = nums.Length;
            if (len == 0) return new int[2];
            int sum = 0;
            //先获得所有数异或的结果
            //x^0=x;x^x=0;
            foreach (var num in nums) sum ^= num;
            //怎么获得异或之前的两个数
            //得到最低位的1
            int lowbit = sum & (-sum);
            int x = 0;
            foreach (var num in nums)
            {
                if ((lowbit & num) != 0) x ^= num;
            }
            return new int[] { x, x ^ sum };
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
