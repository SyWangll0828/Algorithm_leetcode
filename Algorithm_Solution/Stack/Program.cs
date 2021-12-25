using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例化可以访问类成员;直接访问需要添加static
            Common.Case testCase = new Common.Case();
            //problems.IntToRoman(testCase.MyProperty2);
            int[] res = new int[] { 3, 2, 4 };
            //bool isv = problems.IsValid("({})");
            Console.ReadKey();
        }
    }

    class Knowleage
    {
        #region 栈性质：先入后出
         // 栈的应用：1、反转顺序；2、先处理后来进入的元素

        // 反转栈中的元素
        // 返回 3 2 1 变成 返回 1 2 3
        public void Reverse(Stack<int> stack)
        {
            if (stack.Count == 0) return;
            int i = f(stack);
            Reverse(stack);
            stack.Push(i);
        }

        private int f(Stack<int> stack)
        {
            int result = stack.Pop();
            if (stack.Count == 0) return result;
            else
            {
                int last = f(stack);
                stack.Push(result);
                return last;
            }
        }

        #endregion

        #region 特殊的数据结构--单调栈
        // 单调栈专门解决Next Greater Number(模板一)
        public int[] NextGreaterNumberOne(int[] nums)
        {
            int[] ans = new int[nums.Length]; // 存放答案的数组
            Stack<int> s = new Stack<int>();
            for (int i = nums.Length - 1; i >= 0; i--)
            { // 倒着往栈⾥放
                while (s.Any() && s.Peek() <= nums[i])
                { // 判定个⼦⾼矮
                    s.Pop(); // 小的出栈
                }
                ans[i] = s.Any() ? -1 : s.Peek(); // 这个元素⾝后的第⼀个⾼个
                s.Push(nums[i]); // 进队，接受之后的⾝⾼判定吧！
            }
            return ans;
        }

        // 单调栈专门解决Next Greater Number(模板二-循环数组)
        public int[] NextGreaterNumberTwo(int[] nums)
        {
            /*计算机的内存都是线性的，没有真正意义上的环形数组，但是我们可以模拟出环形数组的效果，一般是通过 % 运算符求模（余数），获得环形特效：
            int[] arr = { 1, 2, 3, 4, 5 };
            int n = arr.length, index = 0;
            while (true) 
            {
                print(arr[index % n]);
                index++;
            }*/
            int n = nums.Length;
            int[] ans = new int[n]; // 存放答案的数组
            Stack<int> s = new Stack<int>();
            // 假装这个数组长度翻倍了
            for (int i = 2 * n - 1; i >= 0; i--)
            {
                while (s.Any() && s.Peek() <= nums[i % n])
                    s.Pop();
                ans[i % n] = s.Any() ? -1 : s.Peek();
                s.Push(nums[i % n]);
            }
            return ans;
        }

        // 496. 下一个更大元素 I
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            int[] ans = new int[nums1.Length];// 存放答案的数组
            Dictionary<int, int> keyValues = new Dictionary<int, int>();
            Stack<int> s = new Stack<int>();
            for (int i = nums2.Length - 1; i >= 0; i--)
            {// 倒着往栈⾥放
                while (s.Any() && s.Peek() <= nums2[i])
                {// 判定数字大小
                    s.Pop();
                }
                //先用字典存放nums2中个数字下一个更大的元素
                keyValues.Add(nums2[i], !s.Any() ? -1 : s.Peek());
                s.Push(nums2[i]);
            }
            //将nums1与对应字典key的value赋值给ans
            for (int i = 0; i < nums1.Length; i++)
            {
                ans[i] = keyValues[nums1[i]];
            }
            return ans;
        }

        // 503. 下一个更大元素 II ( 循环数组 )
        public int[] NextGreaterElements(int[] nums)
        {
            int n = nums.Length;
            int[] ans = new int[n]; // 存放答案的数组
            Stack<int> s = new Stack<int>();
            // 假装这个数组长度翻倍了
            for (int i = 2 * n - 1; i >= 0; i--)
            {
                while (s.Any() && s.Peek() <= nums[i % n])
                    s.Pop();
                ans[i % n] = !s.Any() ? -1 : s.Peek();
                s.Push(nums[i % n]);
            }
            return ans;
        }

        // 739. 每日温度
        public int[] DailyTemperatures(int[] temperatures)
        {
            int[] res = new int[temperatures.Length];
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < temperatures.Length; i++)
            {
                int temp = temperatures[i];
                if (stack.Any() && temp > temperatures[stack.Peek()])
                {
                    res[i] = i - stack.Peek();
                    stack.Pop();
                }
                //将下标入栈
                stack.Push(i);
            }
            return res;
        }

        // 84. 柱状图中最大的矩形
        public int LargestRectangleArea(int[] heights)
        {
            if (heights == null || heights.Length <= 0) return 0;
            int max = 0;
            var stack = new Stack<int>();
            for (int i = 0; i < heights.Length + 1; ++i)
            {
                while (stack.Count != 0 && ((i == heights.Length) || heights[i] < heights[stack.Peek()]))
                {
                    int height = heights[stack.Pop()];
                    int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                    max = Math.Max(max, width * height);
                }
                stack.Push(i);
            }
            return max;
        }
        #endregion

        #region 队列性质：先入先出
        // 单调队列 -- 队列的最大值版本
        public class MaxQueue
        {
            Queue<int> inQueue;
            LinkedList<int> maxQueue;
            public MaxQueue()
            {
                inQueue = new Queue<int>();
                maxQueue = new LinkedList<int>();
            }

            public int Max_value()
            {
                return maxQueue.Count == 0 ? -1 : maxQueue.Count;
            }

            // 在入队的时候构建好一个单调队列
            public void Push_back(int value)
            {
                inQueue.Enqueue(value);
                // 保持单调队列
                while (maxQueue.Count != 0 && maxQueue.Last.Value < value)
                {
                    maxQueue.RemoveLast();
                }
                maxQueue.AddLast(value);
            }

            public int Pop_front()
            {
                if (inQueue.Count == 0) return -1;
                int res = inQueue.Dequeue();
                // 值队列出队时看单调队列中对应值大小，相等的话需要出队
                if (maxQueue.First.Value == res) maxQueue.RemoveFirst();
                return res;
            }
        }
        #endregion

    }
}
