﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //int[] testNums = knowledge.Reverse(testCase.nums);
            //Console.WriteLine(string.Join("", testNums.ToArray()));
            //problems.IntToRoman(testCase.MyProperty2);
            Knowledge.Rank(3, testCase.index);
            int[] test = new int[] { 3, 1 };
            //var rcptnos = new List<string>();
            problems.Search(test, 1);
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
        //模板
        //left<right
        //左边区间：将数组分割为[left,mid-1]和[mid,right]
        //需要上取整- mid=left+(right-left+1)/2; 
        //left=mid;right=mid-1;
        //右边区间：将数组分割为[left,mid]和[mid+1,right]
        //left=mid+1;right=mid;

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
        //33. 搜索旋转排序数组
        public int Search(int[] nums, int target)
        {
            int len = nums.Length;
            if (len == 0) return -1;
            int left = 0, right = len - 1;
            while (left < right)
            {
                int mid = left + (right - left + 1) / 2;
                if (nums[mid] == target) return mid;
                if (nums[mid] > nums[left])
                {
                    if (target >= nums[left] && target <= nums[mid - 1])//将数组分割为[left,mid-1]和[mid,right]
                        right = mid - 1;
                    else
                        left = mid;
                }
                else
                {
                    if (target >= nums[mid] && target <= nums[right])
                        left = mid;
                    else
                        right = mid - 1;
                }
            }
            return nums[left] != target ? -1 : left;
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
