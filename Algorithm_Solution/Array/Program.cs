using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Array
{
    class Program
    {
        static void Main(string[] args)
        {
            Problems problems = new Problems();
            Knowledge knowledge = new Knowledge();
            //实例化可以访问类成员;直接访问需要添加static
            Common.Case testCase = new Common.Case();
            int n = 10, m = 8;
            int mid = n + (n - m) >> 1;
            //problems.GetRow(5);
            //不等长交错数组
            int[][] aInt = new int[2][];
            //二维数组
            aInt[0] = new int[] { 5 };
            aInt[1] = new int[] { 6 };
            int[,] ab2 = new int[2, 1] { { 5 }, { 6 } };

            //problems.SearchInsert(new int[] { -1, 1, 0, -3, 3 });
            //int[] testNums = knowledge.Reverse(testCase.nums);
            //Console.WriteLine(string.Join("", testNums.ToArray()));
            //problems.IntToRoman(testCase.MyProperty2);
            //Knowledge.Rank(3, testCase.index);
            int[] test = new int[] { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 };
            //var rcptnos = new List<string>();
            //var n = problems.TwoSum2(new int[] { 2, 2, 7, 11, 15 }, 100);
            //problems.MyPow(2, 4);
            //problems.ContainsNearbyDuplicate(testCase.nums, 1);
            double a = 1.0 / 0.0;
            Console.ReadKey();
        }
    }

    class Problems
    {
        //4. 寻找两个正序数组的中位数
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int len1 = nums1.Length, len2 = nums2.Length;
            //if (len1 == 1 || len2 == 0) return
            int[] unionArr = new int[len1 + len2];
            //首先将两个数组按照递增排序
            int index1 = 0, index2 = 0;
            for (int m = 0; index1 < len1 && index2 < len2 && m < (len1 + len2); m++)
            {
                if (nums1[index1] < nums2[index2])
                {
                    unionArr[m] = nums1[index1];
                    index1++;
                }
                else
                {
                    unionArr[m] = nums2[index2];
                    index2++;
                }
            }
            //将剩余的元素填入到组合数组中
            while (index1 < len1)
            {
                unionArr[index1 + index2] = nums1[index1];
                index1++;
            }
            while (index2 < len2)
            {
                unionArr[index1 + index2] = nums2[index2];
                index2++;
            }
            int len3 = unionArr.Length;
            int mid3 = len3 / 2;
            if (len3 % 2 == 0) return (unionArr[mid3] + unionArr[mid3 - 1]) / 2f;
            else return unionArr[mid3];
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

        //119. 杨辉三角 II
        public IList<IList<int>> GetRow(int numRows)
        {
            //为什么要这么写？
            int[][] res = new int[numRows][];
            for (int i = 0; i < numRows; i++)
            {
                res[i] = new int[i + 1];
            }
            res[0][0] = 1;
            for (int i = 1; i < numRows; i++)
            {
                res[i][0] = res[i][i] = 1;
                for (int j = i - 1; j >= 1; j--)
                {
                    res[i][j] = res[i - 1][j] + res[i - 1][j - 1];
                }
            }
            return res;
        }

        //435. 无重叠区间
        public int EraseOverlapIntervals(int[][] intervals)
        {
            // 先按照区间起始位置排序
            System.Array.Sort(intervals, (v1, v2) => v1[0] - v2[0]);

            return 9;
        }

        //56. 合并区间
        public int[][] Merge(int[][] intervals)
        {
            // 先按照区间起始位置排序
            System.Array.Sort(intervals, (v1, v2) => v1[0] - v2[0]);
            // 遍历区间
            int[][] res = new int[intervals.Length][];
            int idx = -1;
            foreach (int[] interval in intervals)
            {
                // 如果结果数组是空的，或者当前区间的起始位置 > 结果数组中最后区间的终止位置，
                // 则不合并，直接将当前区间加入结果数组。
                if (idx == -1 || interval[0] > res[idx][1])
                {
                    res[++idx] = interval;
                }
                else
                {
                    // 反之将当前区间合并至结果数组的最后区间
                    res[idx][1] = System.Math.Max(res[idx][1], interval[1]);
                }
            }
            return res.Where(o => o != null).ToArray();
        }

        //189. 旋转数组
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

        //5. 最长回文子串
        /* public string LongestPalindrome(string s)
        {
            int length = s.Length;
            if (length == 1)
                return s;

        } */

        #region 二分查找
        //模板--在取左边区间还是右边区间时需要分析问题中可以通过收缩*边界，锁定最值的范围
        //temp[mid] > target  
        //mid在右边区间：将数组分割为[left,mid-1]和[mid,right]
        //往左查 right=mid-1; 往右查 left=mid;
        //需要上取整- mid=left+(right-left+1)/2; 最后一个出现的元素

        //temp[mid] < target
        //mid在左边区间：将数组分割为[left,mid]和[mid+1,right]
        //往左查 right=mid; 往右查 left=mid+1;
        //下取整- mid更靠近left  在循环中left <= mid，mid < right; 第一个出现的元素

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

        //167. 两数之和 II - 输入有序数组
        public int[] TwoSum2(int[] numbers, int target)
        {
            //for (int i = 0; i < numbers.Length; ++i)
            //{
            //    int low = i + 1, high = numbers.Length - 1;
            //    while (low <= high)
            //    {
            //        int mid = (high - low) / 2 + low;
            //        if (numbers[mid] == target - numbers[i])
            //            return new int[] { i + 1, mid + 1 };
            //        else if (numbers[mid] > target - numbers[i])
            //            high = mid - 1;
            //        else
            //            low = mid + 1;
            //    }
            //}
            //return null;

            //双指针
            int low = 0, high = numbers.Length - 1;
            while (low < high)
            {
                int sum = numbers[low] + numbers[high];
                if (target == sum)
                    return new int[] { low + 1, high + 1 };
                else if (sum > target)
                    high--;
                else
                    low++;
            }
            return null;
        }
        //4. 寻找两个正序数组的中位数
        //public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        //{

        //}
        //374. 猜数字大小
        //public int GuessNumber(int n)
        //{
        //    int left = 1, right = n;
        //    while (left <= right)
        //    {
        //        int mid = (right - left) / 2 + left;
        //        //所选数字在[mid,right]之间
        //        if (guess(mid) <= 0)
        //            right = mid - 1;
        //        //所选数字在[left,mid]之间
        //        else
        //            left = mid + 1;
        //    }
        //    return left;
        //}

        //278. 第一个错误的版本
        //public int FirstBadVersion(int n)
        //{
        //    int left = 1, right = n;
        //    while (left < right)
        //    { // 循环直至区间左右端点相同
        //        int mid = left + (right - left) / 2; // 防止计算时溢出
        //        if (IsBadVersion(mid))
        //            right = mid; // 答案在区间 [left, mid] 中
        //        else
        //            left = mid + 1; // 答案在区间 [mid+1, right] 中
        //    }
        //    // 此时有 left == right，区间缩为一个点，即为答案
        //    return left;
        //}

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

        //633. 平方数之和
        public bool JudgeSquareSum(int c)
        {
            int left = 0, right = (int)Math.Sqrt(c);
            while (left <= right)
            {
                int res = left * left + right * right;
                if (res == c)
                    return true;
                else if (res > c)
                    right--;
                else
                    left++;
            }
            return false;
        }

        //33. 搜索旋转排序数组
        public int Search(int[] nums, int target)
        {
            int len = nums.Length;
            //特殊情况
            if (len == 1) return nums[0] == target ? 0 : -1;
            //第一次「二分」：从中间开始找，找到满足最后一个 >=nums[0] 的分割点（旋转点的下标）
            int left = 0, right = len - 1;
            while (left < right)
            {
                //上取整
                int mid = left + ((right - left + 1) >> 1);
                if (nums[mid] >= nums[0]) left = mid;
                else right = mid - 1;
            }

            //第二次「二分」：通过和 nums[0] 进行比较，得知 target 是在旋转点的左边还是右边
            //目标值在旋转点的左边
            if (target >= nums[0])
            {
                left = 0;
            }
            else
            {
                left = left + 1;
                right = len - 1;
            }
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                if (nums[mid] >= target)
                    right = mid;
                else
                    left = mid + 1;
            }
            //到这时left>=right 用right去比较
            return nums[right] == target ? right : -1;
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
        public int Search3(int[] arr, int target)
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
        #endregion

        #region 滑动窗口
        //滑动窗口:
        //看作是在数组中拖入一个窗口(队列)，并进行窗口的移动(加减)
        //3. 无重复字符的最长子串
        public int LengthOfLongestSubstring(String s)
        {
            //if (s.Length == 0) return 0;
            //Dictionary<char, int> dict = new Dictionary<char, int>();
            //int max = 0;
            //int left = 0;
            //for (int i = 0; i < s.Length; i++)
            //{
            //    if (dict.ContainsKey(s[i]))
            //    {
            //        left = Math.Max(left, dict[s[i]] + 1);
            //    }
            //    dict[s[i]] = i;
            //    max = Math.Max(max, i - left + 1);
            //}
            //return max;
            if (s.Length == 0) return 0;
            int max = 0;
            Queue<char> queue = new Queue<char>();
            for (int i = 0; i < s.Length; i++)
            {
                while (queue.Contains(s[i]))
                    queue.Dequeue();
                queue.Enqueue(s[i]);
                if (queue.Count > max) max = queue.Count;
            }
            return max;
        }

        #endregion

        #region 动态规划
        //1、确定dp数组（dp table）以及下标的含义
        //2、确定递推公式
        //3、dp数组如何初始化
        //4、确定遍历顺序
        //5、举例推导dp数组

        //10. 正则表达式匹配
        //public bool IsMatch(string s, string p)
        //{

        //}

        //55. 跳跃游戏
        public bool CanJump(int[] nums)
        {
            //可以跳到的地方理解位可以覆盖的范围
            int len = nums.Length;
            if (len == 1) return true;
            int coverRange = 0;
            for (int i = 0; i <= coverRange; i++)
            {
                coverRange = Math.Max(coverRange, i + nums[i]);
                if (coverRange >= len - 1) return true;
            }
            return false;
        }
        //public int Jump(int[] nums)
        //{
        //}

        //198. 打家劫舍
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
        //打家劫舍Ⅱ 首位相邻
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

        //public IList<IList<string>> DisplayTable(IList<IList<string>> orders)
        //{
        //    var order = orders.Select(o => new { tableNumber, foodItem }).OrderBy(o => int.Parse(o.tableNumber)).GroupBy(o => (o.tableNumber, o.foodItem), (item, result) => new { item.tableNumber, item.foodItem, foodItemNumber = result.Count() });
        //    List<string> title = order.GroupBy(o => o.foodItem, (item) => new { item.foodItem }).OrderBy(o => o.Key, StringComparer.Ordinal).Select(o => o.Key).ToList();
        //    title.Insert(0, "Table");
        //    List<List<string>> result = new List<List<string>> { title };

        //    foreach (var o in order)
        //    {
        //        List<string> r;
        //        r = result.FirstOrDefault(m => m[0].Equals(o.tableNumber));
        //        if (r == null)
        //        {
        //            r = new List<string>(title.Select(t => "0"))
        //            {
        //                [0] = o.tableNumber
        //            };
        //            result.Add(r);
        //        }
        //        r[title.IndexOf(o.foodItem)] = o.foodItemNumber.ToString();
        //    }

        //    return result.ToArray();
        //}

        //53. 最大子序和
        public int MaxSubArray(int[] nums)
        {
            int pre = 0;
            int sum = nums[0];
            foreach (var num in nums)
            {
                pre = Math.Max(pre + num, num);
                sum = Math.Max(sum, pre);
            }
            return sum;
        }
        //62. 不同路径  杨辉三角问题
        public int UniquePaths(int m, int n)
        {
            int[,] f = new int[m, n];
            f[0, 0] = 1;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //当前位置既能往下也能往右
                    if (i > 0 && j > 0)
                    {
                        f[i, j] = f[i - 1, j] + f[i, j - 1];
                    }
                    //只能往下
                    else if (i > 0)
                    {
                        f[i, j] = f[i - 1, j];
                    }
                    //只能往右
                    else if (j > 0)
                    {
                        f[i, j] = f[i, j - 1];
                    }
                }
            }
            return f[m - 1, n - 1];
            int[] counts = new int[n];
            counts[0] = 1;

            for (int i = 0; i < m; i++)
                for (int j = 1; j < n; j++)
                    counts[j] += counts[j - 1];

            return counts[n - 1];
        }
        //63. 不同路径 II
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int m = obstacleGrid.Length, n = obstacleGrid[0].Length;
            int[,] f = new int[m, n];
            f[0, 0] = obstacleGrid[0][0] == 1 ? 0 : 1;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (obstacleGrid[i][j] != 1)
                    {
                        //当前位置既能往下也能往右
                        if (i > 0 && j > 0)
                        {
                            f[i, j] = f[i - 1, j] + f[i, j - 1];
                        }
                        //只能往下
                        else if (i > 0)
                        {
                            f[i, j] = f[i - 1, j];
                        }
                        //只能往左
                        else if (j > 0)
                        {
                            f[i, j] = f[i, j - 1];
                        }
                    }
                }
            }
            return f[m - 1, n - 1];
        }
        //64. 最小路径和
        public int MinPathSum(int[][] grid)
        {
            int n = grid.Length;
            int m = grid[0].Length;
            int[,] dp = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 && j == 0)
                        dp[i, j] = grid[i][j];
                    else if (i == 0)
                        dp[i, j] = dp[i, j - 1] + grid[i][j];
                    else if (j == 0)
                        dp[i, j] = dp[i - 1, j] + grid[i][j];
                    else
                        dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i][j];
                }
            }
            return dp[n - 1, m - 1];
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    //第一个位置处理
                    if (i == 0 && j == 0) continue;
                    //下移
                    if (i > 0 && j == 0) grid[i][j] += grid[i - 1][j];
                    //左移
                    else if (j > 0 && i == 0) grid[i][j] += grid[i][j - 1];
                    else grid[i][j] += Math.Min(grid[i - 1][j], grid[i][j - 1]);
                }
            }
            return grid[grid.Length - 1][grid[0].Length - 1];
        }
        //120. 三角形最小路径和
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            int n = triangle[triangle.Count - 1].Count;
            int[] counts = new int[n];
            counts[0] = triangle[0][0];

            for (int i = 0; i < n; i++)
                for (int j = 1; j < n; j++)
                    counts[j] += counts[j - 1];

            return counts.Min();
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
        #endregion

        #region 贪心
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

        //169. 多数元素 摩尔投票法
        public int MajorityElement(int[] nums)
        {
            //候选人初始化为nums[0]，票数count初始化为1。
            //当遇到与候选人相同的数，则票数++，否则票数--。
            //当票数count为0时，更换候选人，并将票数count重置为1。
            //遍历完数组后，候选人即为最终答案。
            int index = nums[0], count = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == index) count++;
                else if (--count == 0)
                {
                    index = nums[i];
                    count = 1;
                }
            }
            return index;
        }
        #endregion

        #region 前缀和
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

        //快速幂 -- 分治思想
        //50. Pow(x, n)
        public double MyPow(double x, int n)
        {
            //折半考虑
            double res = 1.0;
            for (int i = n; i != 0; i /= 2)
            {
                if (i % 2 != 0) res *= x;
                x *= x;
            }
            return n < 0 ? 1 / res : res;

            long N = n;
            return N >= 0 ? quickMul(x, N) : 1.0 / quickMul(x, -N);
            //x→x^2→x^4→x^9→x^19→x^38→x^77
            //n为奇数需再乘一个x 
            //递归
            double quickMul(double xTemp, long NTemp)
            {
                if (NTemp == 0) return 1.0;
                double y = quickMul(xTemp, NTemp >> 1);
                return NTemp % 2 == 0 ? y * y : y * y * xTemp;
            }
            //迭代
            double quickMul2(double xTemp, long NTemp)
            {
                double res1 = 1;
                double xMul = xTemp;
                while (NTemp > 0)
                {
                    if (NTemp % 2 == 1) res *= xMul;
                    xMul *= xMul;
                    NTemp >>= 1;
                }
                return res1;
            }
        }
        #endregion 


    }

    class Knowledge
    {
        //翻转数组
        public int[] Reverse(int[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i < n / 2; i++)
            {
                int temp = nums[i];
                nums[i] = nums[n - 1 - i];
                nums[n - 1 - i] = temp;
            }
            return nums;
        }

        //二分查找的递归实现
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

        //判断字符串是否是回文
        public static bool isPlindRome(string s)
        {
            int n = s.Length;
            for (int i = 0; i < n / 2; i++)
            {
                if (s[i] != s[n - i - 1])
                    return false;
            }
            foreach (var c in s)
            {

            }
            return true;
        }

    }
}
