using System;
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
            //实例化可以访问类成员;直接访问需要添加static
            Common.Case testCase = new Common.Case();
            //problems.IntToRoman(testCase.MyProperty2);
            //var rcptnos = new List<string>();
            //problems.Test(rcptnos);
            var n = problems.TwoSum2(new int[] { 2, 2, 7, 11, 15 }, 100);
            //problems.ContainsNearbyDuplicate(testCase.nums, 1);
            Console.ReadKey();
        }
    }

    class Problems
    {
        public void Test(List<string> str)
        {
            foreach (var s in str)
            {
                Console.WriteLine(s);
            }
        }

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

        //167. 两数之和 II - 输入有序数组
        public int[] TwoSum2(int[] numbers, int target)
        {
            //二分查找
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
        #endregion



    }
}
