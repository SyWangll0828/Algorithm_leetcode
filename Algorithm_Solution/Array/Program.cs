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

            int[,] f = new int[3, 7];

            problems.UniquePaths(3, 7);
            //int[] testNums = knowledge.Reverse(testCase.nums);
            //Console.WriteLine(string.Join("", testNums.ToArray()));
            //problems.IntToRoman(testCase.MyProperty2);
            Knowledge.Rank(3, testCase.index);
            int[] test = new int[] { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 };
            //var rcptnos = new List<string>();
            problems.Search3(test, 5);
            //var n = problems.TwoSum2(new int[] { 2, 2, 7, 11, 15 }, 100);
            //problems.LengthOfLongestSubstring("pwwkew");
            //problems.ContainsNearbyDuplicate(testCase.nums, 1);
            double a = 1.0 / 0.0;
            Console.ReadKey();
        }
    }

    class Problems
    {
        //414. 第三大的数
        public int ThirdMax(int[] nums)
        {
            //LINQ
            var query = nums.GroupBy(o => o)
                            .OrderByDescending(m => m.Key)
                            .ElementAtOrDefault(2);
            if (query == null)
                return (from num in nums select num).Max();
            return query.First();
        }

        //219. 存在重复元素 II
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                //维护一个k位的散列表
                if (set.Contains(nums[i])) return true;
                set.Add(nums[i]);
                if (set.Count > k)
                {
                    set.Remove(nums[i - k]);
                }
            }
            return false;
        }
        //1. 两数之和
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            //将数组每个元素及下标值记录到dict中
            for (int i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(target - nums[i]))
                {
                    return new int[] { dict[target - nums[i]], i };
                }
                else
                    dict.Add(nums[i], i);
            }
            return null;
        }

        #region 二分查找
        //模板--在取左边区间还是右边区间时需要分析问题中可以通过收缩*边界，锁定最值的范围
        //left<right
        //mid在右边区间：将数组分割为[left,mid-1]和[mid,right]
        //需要上取整- mid=left+(right-left+1)/2; 最后一个出现的元素
        //往左查 right=mid-1; 往右查 left=mid;
        //mid在左边区间：将数组分割为[left,mid]和[mid+1,right]
        //下取整- mid更靠近left  在循环中left <= mid，mid < right; 第一个出现的元素
        //往左查 right=mid; 往右查 left=mid+1;
        //注意事项：边界情况;
        //单调递增线性区间：arr[mid]>=traget 取上限 第一个出现的元素
        //                  arr[mid]<=traget 取下限 最后一个出现的元素
        //永远思考，当什么什么条件成立的时候，目标元素的值在哪个区间里。

        public int MySqrt(int x)
        {
            if (x == 0) return 0;
            int left = 1, right = x / 2;
            while (left < right)
            {
                int mid = left + (right - left + 1) / 2;
                // 调试语句开始
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("left = " + left + ", right = " + right + ", mid = " + mid); ;
                //将数组分为[left,mid-1]与[mid,right]
                //最值在[left,mid-1]中
                int y = x / mid;
                if (y == mid) return mid;
                if (mid > y)
                    left = mid;
                else
                    right = mid - 1;
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
        //34. 在排序数组中查找元素的第一个和最后一个位置
        public int[] SearchRange(int[] nums, int target)
        {
            int[] res = new int[] { -1, -1 };
            if (nums.Length == 0 || nums == null) return res;

            res[0] = binarySearch(nums, target, true);
            res[1] = binarySearch(nums, target, false);
            return res;
        }

        public int binarySearch(int[] nums, int target, bool flag)
        {
            int n = nums.Length;
            int left = 0, right = n - 1;
            int ans = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] > target)
                    right = mid - 1;
                else if (nums[mid] < target)
                    left = mid + 1;
                else
                {
                    ans = mid;
                    if (flag) right = mid - 1;
                    else left = mid + 1;
                }
            }
            return ans;
        }
        //81. 搜索旋转排序数组II
        public bool Search(int[] nums, int target)
        {
            if (nums.Length == 1 && nums[0] == target) return true;
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left + 1) / 2;
                if (nums[mid] == target) return true;
                //数组有重复项，缩小边界（影响时间复杂度为O(n)）
                if (nums[left] == nums[mid])
                {
                    left++;
                    continue;
                }
                //[left,mid-1]有序
                if (nums[mid] > nums[left])
                {
                    if (nums[left] <= target && target <= nums[mid - 1])
                        right = mid - 1;
                    else
                        left = mid;
                }
                else
                {
                    if (nums[mid] <= target && target <= nums[right])
                        left = mid;
                    else
                        right = mid - 1;
                }
            }
            return nums[left] == target ? true : false;
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
        //状态定义
        //转移方程
        //边界条件

        //10. 正则表达式匹配
        public bool IsMatch(string s, string p)
        {

        }

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
