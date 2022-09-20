using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Array
{
    class Program
    {
        static void Main()
        {
            Problems problems = new Problems();
            Knowledge knowledge = new Knowledge();
            //实例化可以访问类成员;直接访问需要添加static
            Common.Case testCase = new Common.Case();
            //不等长交错数组
            char[][] aInt = new char[1][];
            //二维数组
            aInt[0] = new char[] { 'a' };
            //aInt[1] = new int[] { 6 };
            int[,] ab2 = new int[,] { { 5, 4, 4 }, { 6, 5, 6 } };
            int[] test = new int[] { 1, 2, 3, 3, 4, 5 };
            int[] test2 = new int[] { 4, 3, 2, 7, 8, 2, 3, 1 };

            problems.findDuplicates(test2);
            Console.ReadKey();
        }
    }

    class Problems
    {
        #region 原地修改数组
        public List<int> findDuplicates(int[] nums)
        {
            List<int> duplicates = new List<int>();
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                int num = nums[i];
                int index = Math.Abs(num) - 1;
                if (nums[index] > 0)
                {
                    nums[index] = -nums[index];
                }
                else
                {
                    duplicates.Add(index + 1);
                }
            }
            return duplicates;
        }

        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            List<int> res = new List<int>();
            res.Sort((x, y) => x.CompareTo(y));
            int len = nums.Length;
            // 原地将正数所对应的下标值置为负数
            for (int i = 0; i < len; i++)
            {
                nums[Math.Abs(nums[i]) - 1] = -Math.Abs(nums[Math.Abs(nums[i]) - 1]);
            }
            for (int i = 0; i < len; i++)
            {
                if (nums[i] > 0)
                    res.Add(i + 1);
            }
            return res;
        }
        #endregion

        // 重复次数最多的数 的最短子数组长度
        public int FindShortestSubArray(int[] nums)
        {
            // 求数组的度
            // 左右字典存各数字出现得首位置和尾位置
            var dic = new Dictionary<int, int>();
            var left = new Dictionary<int, int>();
            var right = new Dictionary<int, int>();
            int len = nums.Length;
            for (int i = 0; i < len; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]]++;
                    right[nums[i]] = i;
                }
                else
                {
                    dic.Add(nums[i], 1);
                    left.Add(nums[i], i);
                    right.Add(nums[i], i);
                }
            }
            // 与数组具有相同度的最短子数组的长度
            // 求相同度得数字出现得首位置和尾位置得最小长度
            int res = len;
            foreach (var item in dic)
            {
                if (item.Value == dic.Values.Max())
                    res = Math.Min(right[item.Key] - left[item.Key] + 1, res);
            }
            return res;
        }

        public int NumDecodings(string s)
        {
            int len = s.Length;
            s = " " + s;
            int[] dp = new int[len + 1];
            dp[0] = 1;
            for (int i = 1; i < len; i++)
            {
                int a = s[i] - '0';
                int b = (s[i - 1] - '0') * 10 + (s[i] - '0');
                if (a > 0 && a < 10)
                {
                    dp[i] = dp[i - 1];
                }
                if (b > 9 && b < 27)
                {
                    dp[i] += dp[i - 2];
                }
            }
            return dp[len];
        }

        public int MinSubArrayLen(int target, int[] nums)
        {
            int min = int.MaxValue;
            int len = nums.Length;
            int left = 0;
            int right = 0;
            int sum = 0;
            while (right < len)
            {
                sum += nums[right++];
                while (sum >= target)
                {
                    min = Math.Min(right - left + 1, min);
                    sum -= nums[left++];
                }
            }
            return min == int.MaxValue ? 0 : min;
        }

        public int Compress(char[] chars)
        {
            List<char> list = new List<char>();
            int len = chars.Length;
            int left = 0;
            for (int right = 0; right < len;)
            {
                list.Add(chars[left]);
                right++;
                while (right < len && chars[right] == chars[right - 1])
                {
                    right++;
                }
                if (right - left > 1)
                {
                    string arr = (right - left).ToString();
                    list.AddRange(arr.ToArray());
                }
                left = right;
            }
            return list.Count;
        }

        

        public int[] SearchInsert(int[] nums)
        {
            int len = nums.Length;
            int[] res = new int[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = 1;
            }
            for (int i = 0; i < len; i++)
            {
                int temp = nums[i];
                nums[i] = 1;
                for (int j = 0; j < len; j++)
                {
                    res[i] *= nums[j];
                }
                nums[i] = temp;
            }
            return res;
        }

        // 矩阵模拟
        // 数据范围小的，可以考虑直接用范围+1的长度定义数据结构
        public IList<int> LuckyNumbers(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            // 开两个足够空间的数组
            var min = new int[51];
            var max = new int[51];
            for (int i = 0; i < m; i++)
            {
                // 行中最小，列中最大
                // 比较最小需要先设置最大值
                min[i] = 100001;
                for (int j = 0; j < n; j++)
                {
                    // 用数组更新记录行最小值与列最大值
                    min[i] = Math.Min(min[i], matrix[i][j]);
                    max[j] = Math.Max(max[j], matrix[i][j]);
                }
            }
            var res = new List<int>();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int t = matrix[i][j];
                    if (t == min[i] && t == max[i])
                        res.Add(t);
                }
            }
            return res;
        }

    }

    class Knowledge
    {
        #region 哈希表
        // 缺失的第一个正数
        // 哈希表也是数组
        public int FirstMissingPositive(int[] nums)
        {
            // 原地哈希，把数值为i的数映射到i-1的索引处
            int len = nums.Length;
            for (int i = 0; i < len; i++)
            {
                // 值在[1,len]区间内才需要进行交换
                while (nums[i] >= 1 && nums[i] <= len && nums[i] != nums[nums[i] - 1])
                {
                    int num = nums[i];
                    nums[i] = nums[num - 1];
                    nums[num - 1] = num;
                }
            }
            for (int i = 0; i < len; i++)
            {
                if (nums[i] != i + 1)
                    return i + 1;
            }
            return len + 1;
        }
        #endregion

        #region 双指针
        // 56. 合并区间
        public int[][] Merge(int[][] intervals)
        {
            var res = new List<int[]>();
            if (intervals == null || intervals[0] == null || intervals[0].Length == 0) return null;
            System.Array.Sort(intervals, (x, y) => x[0] - y[0]);
            // 设置左边界
            int start = intervals[0][0];
            // 从第二个元素开始比较
            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i][0] > intervals[i - 1][1])
                {
                    res.Add(new int[] { start, intervals[i - 1][1] });
                    // 更新左边界
                    start = intervals[i][0];
                }
                else
                {
                    intervals[i][1] = Math.Max(intervals[i][1], intervals[i - 1][1]);
                }
            }
            // 加上最后一个剩下的区间
            res.Add(new int[] { start, intervals[intervals.Length - 1][1] });
            return res.ToArray();
        }

        // 双指针维护最大值与次大值模板
        public int DominantIndex(int[] nums)
        {
            if (nums == null || nums.Length == 0) return -1;
            if (nums.Length == 1) return 0;
            // 遍历维护最大值和次大值的下标
            int a = -1, b = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                // 遇到比b下标还大的数，则同时更新a,b下标的位置
                if (nums[i] > nums[b])
                {
                    a = b;
                    b = i;
                }
                // a下标都是比b下标小的数
                else if (a == -1 || nums[i] > nums[a]) a = i;
            }
            return nums[a] * 2 > nums[b] ? -1 : b;
        }


        #endregion

        #region 数组模拟
        // 矩阵
        public int[][] GenerateMatrix(int n)
        {
            int count = n * n;
            var res = new int[n][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new int[n];
            }
            int left = 0, right = n - 1, top = 0, down = n - 1;
            int index = 1;
            while (index <= count)
            {
                for (int i = left; i <= right && index <= count; i++)
                {
                    res[top][i] = index++;
                }
                top++;
                for (int i = top; i <= down && index <= count; i++)
                {
                    res[i][right] = index++;
                }
                right--;
                for (int i = right; i >= left && index <= count; i--)
                {
                    res[down][i] = index++;
                }
                down--;
                for (int i = down; i >= top && index <= count; i--)
                {
                    res[i][left] = index++;
                }
                left++;
            }
            return res;
        }
        #endregion

        #region 二分查找
        //模板--在取左边区间还是右边区间时需要分析问题中可以通过收缩*边界，锁定最值的范围
        //temp[mid] > target  
        //mid在右边区间：将数组分割为[left,mid-1]和[mid,right]
        //往左查 right=mid-1; 往右查 left=mid;
        //需要上取整- mid=left+(right-left+1)/2;   元素出现的最后一个位置

        //temp[mid] < target
        //mid在左边区间：将数组分割为[left,mid]和[mid+1,right]
        //往左查 right=mid; 往右查 left=mid+1;
        //下取整- mid更靠近left  在循环中left <= mid，mid < right;   元素出现的第一个位置

        //注意事项：边界情况;
        //单调递增线性区间：arr[mid]>=traget 取上限 第一个出现的元素
        //                  arr[mid]<=traget 取下限 最后一个出现的元素
        //永远思考，当什么什么条件成立的时候，目标元素的值在哪个区间里。

        //35. 搜索插入位置
        public int SearchInsert(int[] nums, int target)
        {

            int len = nums.Length;
            // 特殊判断
            if (nums[len - 1] < target)
            {
                return len;
            }
            int left = 0, right = len - 1;
            // 在区间 nums[left..right] 里查找第 1 个大于等于 target 的元素的下标
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                if (nums[mid] < target) left = mid + 1;
                else right = mid;
            }
            return left;
        }

        //852. 山脉数组的峰顶索引(找到数组中最大值的下标)
        public int PeakIndexInMountainArray(int[] arr)
        {
            int left = 0, right = arr.Length - 1;
            while (left < right)
            { // 循环直至区间左右端点相同
                int mid = left + (right - left) / 2; // 防止计算时溢出
                if (arr[mid] > arr[mid + 1])
                    right = mid; // 答案在区间 [left, mid] 中
                else
                    left = mid + 1; // 答案在区间 [mid+1, right] 中
            }
            // 此时有 left == right，区间缩为一个点，即为最大值的下标
            return left;

        }

        //154. 寻找旋转排序数组中的最小值II
        public int FindMin(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                //左边大、右边小
                if (nums[mid] > nums[right])
                    left = mid + 1;
                else if (nums[mid] < nums[right])
                    right = mid;
                else
                    right--;
            }
            return left > nums.Length ? -1 : nums[left];
        }

        //面试题 10.03. 搜索旋转数组
        public int Search(int[] arr, int target)
        {
            int left = 0, right = arr.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                //
                if (arr[mid] > arr[right])
                {
                    if (arr[mid] >= target && target <= arr[right])
                        right = mid;
                    else
                        left = mid - 1;

                }
                else
                {
                    if (arr[left] <= target && target < arr[mid])
                        right = mid;
                    else
                        left = mid - 1;
                }
            }
            return left > arr.Length ? -1 : left;
        }

        // 二分查找的递归实现
        public static int Rank(int key, int[] nums)
        {
            return Rank(key, nums, 0, nums.Length - 1);
        }

        public static int Rank(int key, int[] nums, int left, int right)
        {
            //二分模板(适用于有序数组)
            if (left > right) return -1;
            //取中位数
            int mid = left + (right - left) / 2;
            if (key >= nums[mid]) return Rank(key, nums, mid + 1, right);
            else if (key <= nums[mid]) return Rank(key, nums, left, mid - 1);
            else return key;
        }

        #endregion

        #region 滑动窗口
        // 看作是在数组中拖入一个窗口(队列)，并进行窗口的移动(加减)
        // 567. 字符串的排列
        public bool CheckInclusion(string s1, string s2)
        {
            //固定活动窗口的长度为s1的长度
            int n = s1.Length, m = s2.Length;
            if (n > m) return false;
            //char[] s1Arr = new char[26];
            ////记录s1的词频
            //for (int i = 0; i < n; i++) s1Arr[s1[i] - 'a']++;
            //char[] s2Arr = new char[26];
            ////双指针同时右移 
            //int left = 0, right = n - 1;
            //for (int i = 0; i < right; i++) s2Arr[s2[i] - 'a']++;
            //while (right < m)
            //{
            //    s2Arr[s2[right] - 'a']++;
            //    if (check(s1Arr, s2Arr)) return true;
            //    s2Arr[s2[left] - 'a']--;
            //    //移动窗口
            //    left++;
            //    right++;
            //}
            //return false;

            //双指针交替右移 模板
            Dictionary<char, int> dict = new Dictionary<char, int>();
            Dictionary<char, int> window = new Dictionary<char, int>();
            //记录s1的词频
            for (int i = 0; i < n; i++)
            {
                int val = 0;
                dict.TryGetValue(s1[i], out val);
                dict[s1[i]] = val + 1;
            }
            int left = 0, right = 0, valid = 0;
            while (right < m)
            {
                char c = s2[right];
                //右移窗口
                right++;
                // 进行窗口内数据的更新
                if (dict.ContainsKey(c))
                {
                    int val = 0;
                    window[c] = window.TryGetValue(c, out val) ? val + 1 : 1;
                    if (window[c] == dict[c])
                        valid++;
                }
                /*** debug 输出的位置 ***/
                Console.WriteLine("window: [%d, %d)\n", left, right);
                /********************/
                // 判断左侧窗口是否要收缩
                while (right - left >= n)
                {
                    // 在这里判断是否找到了合法的子串
                    if (valid == dict.Count)
                        return true;
                    char d = s2[left];
                    left++;
                    // 进行窗口内数据的更新
                    if (dict.ContainsKey(d))
                    {
                        if (window[d] == dict[d])
                            valid--;
                        window[d]--;
                    }
                }
            }
            return false;

            bool check(char[] arr1, char[] arr2)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (arr1[i] != arr2[i]) return false;
                }
                return true;
            }
        }

        #endregion

        #region 动态规划

        // 简单构建杨辉三角
        public IList<IList<int>> Generate(int numRows)
        {
            var res = new List<IList<int>>();
            for (int i = 0; i < numRows; i++)
            {
                var list = new List<int>();
                for (int j = 0; j < i + 1; j++)
                {
                    if (j == 0 || j == i) list.Add(1);
                    else
                    {
                        int num = res[i - 1][j - 1] + res[i - 1][j];
                        list.Add(num);
                    }

                }
                res.Add(list);
            }
            return res;
        }


        //1、确定dp数组（dp table）以及下标的含义
        //2、确定状态转移方程
        //3、dp数组如何初始化
        //4、确定遍历顺序
        //5、返回值

        //剑指 Offer 10- I. 斐波那契数列
        int[] cache = new int[101];
        public int FibTwo(int n)
        {
            //动态规划 从下往上计算
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

        //剑指 Offer 10- II. 青蛙跳台阶问题
        public int NumWays(int n)
        {
            //分析过后 依然是斐波那契数列问题
            int[] res = { 1, 1, 2 };
            if (n < 3) return res[n];
            int a = 1, b = 2, sum = 0;
            for (int i = 3; i <= n; i++)
            {
                sum = (a + b) % 1000000007;
                a = b;
                b = sum;
            }
            return sum;
        }

        //
        public int MinCostClimbingStairs(int[] cost)
        {
            int len = cost.Length;
            //dp表示下标为i结尾的花费值
            int[] dp = new int[len + 1];
            dp[0] = 0;
            dp[1] = 0;
            //递推公式
            //dp[i] = Math.Min(dp[i - 2] + cost[i - 2], dp[i - 1] + cost[i - 1]);
            for (int i = 2; i <= len; i++)
            {
                dp[i] = Math.Min(dp[i - 2] + cost[i - 2], dp[i - 1] + cost[i - 1]);
            }
            return dp[len];
        }

        // 152. 乘积最大子数组
        public int MaxProduct(int[] nums)
        {
            int len = nums.Length;
            if (len < 1) return 0;
            //dp表示下标为i结尾的最大乘积
            int[] dpMax = new int[len];
            //dp1表示下标为i结尾的最小值
            int[] dpMin = new int[len];
            dpMax[0] = dpMin[0] = nums[0];
            //递推公式
            for (int i = 1; i < len; i++)
            {
                if (nums[i] >= 0)
                {
                    dpMax[i] = Math.Max(nums[i], nums[i] * dpMax[i - 1]);
                    dpMin[i] = Math.Min(nums[i], nums[i] * dpMin[i - 1]);
                }
                else
                {
                    //有负数时最大值最小值反转
                    dpMax[i] = Math.Max(nums[i], nums[i] * dpMin[i - 1]);
                    dpMin[i] = Math.Min(nums[i], nums[i] * dpMax[i - 1]);
                }
            }
            return dpMax.Max();
        }

        // 198. 打家劫舍
        public int Rob(int[] nums)
        {
            //int len = nums.Length;
            //if (len == 1) return nums[0];
            //int[] dp = new int[len];
            //dp[0] = nums[0];
            //dp[1] = Math.Max(nums[0], nums[1]);
            //for (int i = 2; i < len; i++)
            //{
            //    dp[i] = Math.Max(dp[i - 2] + nums[i], dp[i - 1]);
            //}
            //return dp[len - 1];

            int a = 0, b = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                int t = b;
                b = Math.Max(a + nums[i], b);
                a = t;
            }
            return b;
        }
        // 打家劫舍Ⅱ 首位相邻
        public int Rob2(int[] nums)
        {
            //int len = nums.Length;
            //if (len == 1) return nums[0];
            ////抢第一个
            //int[] dp1 = new int[len];
            //dp1[0] = nums[0];
            //dp1[1] = Math.Max(nums[0], nums[1]);
            //for (int i = 2; i < len - 1; i++)
            //{
            //    dp1[i] = Math.Max(dp1[i - 2] + nums[i], dp1[i - 1]);
            //}

            ////不抢第一个
            //int[] dp2 = new int[len];
            //dp2[0] = 0;
            //dp2[1] = nums[0];
            //for (int i = 2; i < len; i++)
            //{
            //    dp2[i] = Math.Max(dp2[i - 2] + nums[i], dp2[i - 1]);
            //}
            //return Math.Max(dp1[len - 2], dp2[len - 1]);

            int len = nums.Length;
            if (len == 1) return nums[0];
            //抢第一家
            int rob1 = robRange(0, len - 2);
            //不抢第一家
            int rob2 = robRange(1, len - 1);
            return Math.Max(rob1, rob2);
            int robRange(int start, int end)
            {
                if (start == end) return nums[start];
                int[] dp = new int[len];
                dp[start] = nums[start];
                dp[start + 1] = Math.Max(nums[start], nums[start + 1]);
                for (int i = 2; i <= end; i++)
                {
                    dp[i] = Math.Max(nums[i] + dp[i - 2], dp[i - 1]);
                }
                return dp[end];
            }
        }

        //740. 删除并获得点数
        public int DeleteAndEarn(int[] nums)
        {
            int[] trans = new int[10001];
            for (int i = 0; i < nums.Length; i++) trans[nums[i]] += nums[i];
            int[] dp = new int[10001];

            dp[0] = 0;
            dp[1] = trans[1];
            for (int i = 2; i < trans.Length; i++)
            {
                dp[i] = Math.Max(dp[i - 1], dp[i - 2] + trans[i]);
            }
            return dp[dp.Length - 1];
        }

        //LCP 07. 传递信息
        public int NumWays(int n, int[][] relation, int k)
        {
            int[,] f = new int[k + 1, n];
            f[0, 0] = 1;
            for (int i = 1; i <= k; i++)
            {
                foreach (var r in relation)
                {
                    int a = r[0], b = r[1];
                    f[i, b] += f[i - 1, a];
                }
            }
            return f[k, n - 1];
        }



        #endregion

        #region 回溯 DFS
        // 回溯思想
        // 深度优先遍历，遍历枚举所有情况（遇到重复得情况进行剪枝）

        //全排列问题，讲究顺序，因此已经选过的元素还有可能再次被选中放置在不同的位置上，构成不同的排列；
        //组合问题与子集问题，因为不计算元素顺序，一个元素选还是没有选过很重要，因此需要设置搜索起点，搜索起点之前的元素不再考虑，这样才能做到不重不漏；
        //不建议去记忆上面的规则，事实上应该根据问题的特点自行推导出来；
        //编码之前先根据具体的用例画出树形图，图和代码是一一对应的关系，先画图再编码是建议的方式；
        //如果不是很熟悉，不用苛求一下子写对，编写测试用例调试正确即可。
        List<IList<int>> res = new List<IList<int>>();
        // 存放结果
        List<int> path = new List<int>();
        // 访问数组，便于剪枝
        bool[] visited;

        #region 排列问题  每个元素必须得选
        // 排列问题不需要考虑起始位置
        // 46. 全排列   回溯基础模板
        public IList<IList<int>> Permute(int[] nums)
        {
            int len = nums.Length;
            if (len == 1) return res;
            visited = new bool[nums.Length];
            // 排列问题 
            permuteDfs(0, nums);
            return res;
        }

        public void permuteDfs(int depth, int[] nums)
        {
            if (depth == nums.Length)
            {
                res.Add(new List<int>(path));
                return;
            }
            // 遍历数组，找出所有符合条件的排列
            for (int i = 0; i < nums.Length; i++)
            {
                // 剪枝
                if (visited[i]) continue;
                path.Add(nums[i]);
                visited[i] = true;
                permuteDfs(depth + 1, nums);
                // 状态复位
                path.RemoveAt(path.Count - 1);
                visited[i] = false;
            }
        }

        // 47. 全排列 II  含有重复元素 需要在回溯递归前先排序
        // 需要在递归时进行重复选择剪枝，排序是剪枝的前提
        public void permuteUniqueDfs(int depth, int[] nums)
        {
            if (depth == nums.Length)
            {
                res.Add(new List<int>(path));
                return;
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (visited[i]) continue;
                // 上一步刚刚撤销相同元素的访问
                if (i > 0 && nums[i - 1] == nums[i] && !visited[i - 1]) continue;
                visited[i] = true;
                path.Add(nums[i]);
                permuteUniqueDfs(depth + 1, nums);
                visited[i] = false;
                path.RemoveAt(path.Count - 1);
            }
        }

        // 应用题
        // 剑指 Offer 38. 字符串的排列
        public string[] PermutationT(string s)
        {
            int len = s.Length;
            if (len == 0) return new string[] { };

            char[] array = s.ToArray();
            // 排序（升序或者降序都可以），排序是剪枝的前提
            System.Array.Sort(array);

            //存放每次查找的结果
            var path = new List<string>();
            StringBuilder param = new StringBuilder();

            //定义元素是否已经访问过
            bool[] used = new bool[len];
            backTrack(0);
            //存放符合条件结果的集合
            string[] res = new string[path.Count];
            int index = 0;
            //将列表结果转换成数组
            foreach (var str in path) res[index++] = str;
            return res;

            void backTrack(int depth)
            {
                //找到了一组满足条件的数组，添加到结果集合中
                if (depth == len)
                {
                    path.Add(param.ToString());
                    return;
                }
                for (int i = 0; i < len; i++)
                {
                    //剪枝，不访问已经访问过的
                    if (used[i]) continue;
                    //剪枝，结果集合重复
                    if (i > 0 && array[i] == array[i - 1] && !used[i - 1]) continue;
                    //标记已经遍历过
                    used[i] = true;
                    param.Append(array[i]);
                    //回溯 
                    backTrack(depth + 1);
                    //状态重置
                    param.Length--;
                    used[i] = false;
                }
            }
        }

        #endregion

        #region 子集问题 每个元素可以有选或者不选两种情况
        // 78. 子集
        public IList<IList<int>> Subsets(int[] nums)
        {
            int len = nums.Length;
            if (len == 0) return res;
            subSetsDfs(0, nums);
            return res;
        }
        // 在回溯的过程中记录节点
        public void subSetsDfs(int begin, int[] nums)
        {
            res.Add(new List<int>(path));
            if (begin >= nums.Length) return;
            for (int i = begin; i < nums.Length; i++)
            {
                path.Add(nums[i]);
                // 选择当前数字
                subSetsDfs(i + 1, nums);
                path.RemoveAt(path.Count - 1);
            }
        }

        // 90. 子集 II 含有重复元素
        // 需要进行元素重复剪枝；排序依然是前提，同时需要一个访问数组，对上一次刚取消选择的相同元素进行剪枝
        public void subSetUniqueDfs(int begin, int[] nums)
        {
            // 直接添加
            res.Add(new List<int>(path));
            // 递归基线条件
            if (begin >= nums.Length) return;
            for (int i = begin; i < nums.Length; i++)
            {
                // 重复剪枝
                if (i > 0 && nums[i] == nums[i - 1] && !visited[i - 1]) continue;
                path.Add(nums[i]);
                visited[i] = true;
                subSetUniqueDfs(i + 1, nums);
                path.RemoveAt(path.Count - 1);
                visited[i] = false;
            }
        }

        #endregion

        #region 组合问题
        // 39. 组合总和
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            int len = candidates.Length;
            if (len == 0) return res;
            visited = new bool[len];
            combinationDfs(0, candidates, target);
            return res;
        }

        public void combinationDfs(int begin, int[] nums, int target)
        {
            if (target < 0) return;
            if (target == 0)
            {
                res.Add(new List<int>(path));
                return;
            }
            for (int i = begin; i < nums.Length; i++)
            {
                // 如果要在调用递归之前进行去重需要在主函数中先对数组进行排序
                // if (target - nums[i] < 0) break;
                path.Add(nums[i]);
                combinationDfs(i, nums, target - nums[i]);
                path.RemoveAt(path.Count - 1);
            }
        }

        // 40. 组合总和 II 每个元素只能使用一遍，递归时begin+1；
        public void combinationDfsTwo(int begin, int[] nums, int target)
        {
            // if (target < 0) return;
            if (target == 0)
            {
                res.Add(new List<int>(path));
                return;
            }
            for (int i = begin; i < nums.Length; i++)
            {
                // 如果要在调用递归之前进行去重需要在主函数中先对数组进行排序
                if (target - nums[i] < 0) break;
                // 起始位置之后的相同元素
                if (i > begin && nums[i] == nums[i - 1]) continue;
                path.Add(nums[i]);
                // 每个元素只能使用一次，所以传入i+1
                combinationDfsTwo(i + 1, nums, target - nums[i]);
                path.RemoveAt(path.Count - 1);
            }
        }


        // 组合
        public IList<IList<int>> Combine(int n, int k)
        {
            List<IList<int>> list = new List<IList<int>>();
            if (k < 0 || n < 0) return list;
            // 记录
            List<int> path = new List<int>();
            backTrack(1);
            return list;

            void backTrack(int begin)
            {
                if (path.Count == k)
                {
                    list.Add(new List<int>(path));
                    return;
                }
                for (int i = begin; i <= n - (k - path.Count) + 1; i++)
                {
                    path.Add(i);
                    backTrack(i + 1);
                    path.RemoveAt(path.Count - 1);
                }
            }
        }

        #endregion


        #endregion

        #region 贪心 没有固定的模板
        //可以购买
        public int MaxIceCream(int[] costs, int coins)
        {
            //排序+贪心？
            //System.Array.Sort(costs);
            //int count = 0;
            //for (int i = 0; i < costs.Length; i++)
            //{
            //    int cost = costs[i];
            //    if (cost <= coins)
            //    {
            //        count++;
            //        coins -= cost;
            //    }
            //    else
            //        break;
            //}
            //return count;
            //计数排序+贪心 适用于元素较少
            int[] freq = new int[100001];
            //记录每个元素出现的次数
            foreach (int cost in costs)
            {
                freq[cost]++;
            }
            int count = 0;
            for (int i = 1; i <= 100000; i++)
            {
                if (coins >= i)
                {
                    // coins / i 表示可以购买价格为i的雪糕的数量
                    // 题目要求的是可以购买最多种类的雪糕的数量
                    // 所以价格为i的雪糕最多可以购买freq[i]个
                    // 所以是取freq[i]和coins/i的最小值
                    int curCount = Math.Min(freq[i], coins / i);
                    count += curCount;
                    coins -= i * curCount;
                }
                else
                    break;
            }
            return count;
        }

        // 最长回文串

        // 跳跃游戏
        public int CanJump(int[] nums)
        {
            // 每次到达边界的时候再开始跳
            int len = nums.Length;
            // 当前能到达的右边界
            int reach = nums[0];
            // 下一步能跳跃的最远边界
            int nextReach = 0;
            int count = 0;
            // 贪心 需要最少的跳跃数
            // 每次到边界的时候 直接去到下一步可以到达的最远边界
            for (int i = 0; i < len; i++)
            {
                nextReach = Math.Max(nextReach, i + nums[i]);
                if (nextReach >= len - 1)
                {
                    return count + 2;
                }
                if (i == reach)
                {
                    reach = nextReach;
                    count++;
                }
            }
            return count;

            // 方法二 贪心经典题
            // 下标+值与最后一个元素进行比较
            //if (nums == null || nums.Length == 0) return false;
            //if (nums.Length == 1) return true;
            //int coverRange = 0;
            //for (int i = 0; i <= coverRange; i++)
            //{
            //    coverRange = Math.Max(i + nums[i], coverRange);
            //    if (coverRange >= nums.Length - 1) return true;
            //}
            //return false;
        }

        // 盛最多水的容器
        // 贪心 + 双指针
        public int MaxArea(int[] height)
        {
            // 首先考虑最大宽度
            int left = 0, right = height.Length - 1, res = 0;
            while (left < right)
            {
                int h = Math.Min(height[left], height[right]), w = right - left;
                res = Math.Max(res, h * w);
                // 收缩宽度，考虑高度
                if (height[left] < height[right]) left++;
                else right--;
            }
            return res;
        }


        // 零钱兑换
        // BFS版本
        public int CoinChangeBFS(int[] coins, int amount)
        {
            if (coins == null || coins.Length == 0 || amount <= 0) return 0;
            System.Array.Sort(coins);
            // 访问标记
            var visited = new bool[amount + 1];
            // 辅助队列，将每次换完钱的amount入队
            var queue = new Queue<int>();
            // 初始化
            queue.Enqueue(amount);
            int count = 1;
            while (queue.Any())
            {
                // 将每一种可能进行零钱兑换
                // 相当于一次扩散
                int len = queue.Count;
                for (int i = 0; i < len; i++)
                {
                    int val = queue.Dequeue();
                    foreach (var item in coins)
                    {
                        int next = val - item;
                        // 后续都是用更大的面值换，直接返回
                        if (next < 0) break;
                        // 找到图中的最短路径
                        else if (next == 0) return count;
                        else
                        {
                            // 可以换,打上标记，入队
                            if (!visited[next])
                            {
                                visited[next] = true;
                                queue.Enqueue(next);
                            }
                        }
                    }
                }
                // 记录次数
                count++;
            }
            // 无法将amount全部兑换
            return -1;
        }
        #endregion

        #region 前缀和
        // 计算前缀和数组

        // 寻找中心坐标的位置（左右两侧数组和相同，返回该索引下标）
        public int PivotIndex(int[] nums)
        {
            // 将数组分成左右两部分，遍历一遍数组查找是否存在中心坐标
            int sumLeft = 0, sumRight = nums.Sum();
            for (int i = 0; i < nums.Length; i++)
            {
                sumRight -= nums[i];
                if (sumLeft == sumRight) return i;
                sumLeft += nums[i];
            }
            return -1;
        }

        // 和为K的子数组
        public int SubarraySum(int[] nums, int k)
        {
            int res = 0;
            if (nums == null || nums.Length == 0) return res;
            int sum = nums[0];
            int sumLeft = 0, sumRight = nums.Length - 1;
            while (sumLeft < nums.Length)
            {

            }

            return res;
        }

        // 前缀和记录元素乘积
        public int[] ConstructArr(int[] array)
        {
            int len = array.Length;
            var res = new int[len];
            // 如何使用前缀和
            // 从左往右乘(不包含自己，首位置用1代替)
            for (int i = 0, cur = 1; i < len; i++)
            {
                // 用cur来计算前n-1个元素的乘积
                res[i] = cur;
                cur *= array[i];
            }
            // 数组中中已经存储了元素左边的乘积和
            // 从右往左乘
            for (int i = len - 1, cur = 1; i >= 0; i--)
            {
                res[i] *= cur;
                cur *= array[i];
            }
            return res;
        }

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
        //930. 和相同的二元子数组
        public int NumSubarraysWithSum(int[] nums, int goal)
        {
            // 最大的和的值为nums.length
            int[] map = new int[nums.Length + 1];
            int res = 0, sum = 0;
            foreach (int num in nums)
            {
                // 先存入前缀和
                map[sum]++;
                // 计算当前sum
                sum += num;
                // 更新res
                if (sum - goal >= 0) res += map[sum - goal];
            }
            return res;
        }
        #endregion

        #region 快速幂
        //剑指 Offer 14- 剪绳子 I
        public int CuttingRope(int n)
        {
            if (n < 4)
                return n - 1;
            int res = 1;
            while (n > 4)
            {
                res *= 3;
                n -= 3;
            }
            return res * n;
        }

        // 50. Pow(x, n)  实现自己的Pow函数
        // 快速幂
        public double MyPow(double x, int n)
        {
            //折半考虑
            //double res = 1.0;
            //for (int i = n; i != 0; i /= 2)
            //{
            //    if (i % 2 != 0) res *= x;
            //    x *= x;
            //}
            //return n < 0 ? 1 / res : res;

            // int取值范围问题 此处要用-t来取到整形最小负数的取反数
            long tempN = n;
            if (tempN < 0)
            {
                tempN = -tempN;
                x = 1 / x;
            }
            double res = 1.0;
            // 模拟求指数次幂函数
            while (tempN > 0)
            {
                //奇数，乘以一个x
                if ((tempN & 1) == 1)
                    res *= x;
                x *= x;
                tempN >>= 1;
            }
            return res;
        }

        //快速幂 -- 分治思想

        #endregion

        #region 背包问题

        #region 完全背包


        #endregion

        #region 01背包


        #endregion


        #endregion

    }
}
