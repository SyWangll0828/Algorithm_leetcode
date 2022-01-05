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
    class Problem
    {
        static int[,] dir = new[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
        // 542. 01 矩阵
        public int[][] UpdateMatrix(int[][] mat)
        {
            // 将所有0位置入队，1位置置-1
            int m = mat.Length;
            int n = mat[0].Length;
            Queue<int[]> queue = new Queue<int[]>();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i][j] == 0) queue.Enqueue(new int[] { i, j });
                    else mat[i][j] = -1;
                }
            }
            // 模板
            while (queue.Any())
            {
                int[] point = queue.Dequeue();
                int x = point[0], y = point[1];
                for (int i = 0; i < 4; i++)
                {
                    int newX = x + dir[i, 0];
                    int newY = y + dir[i, 1];
                    // 如果四邻域的点是 -1，表示这个点是未被访问过的 1
                    // 所以这个点到 0 的距离就可以更新成 mat[x][y] + 1。
                    if (newX >= 0 && newX < m && newY >= 0 && newY < n && mat[newX][newY] == -1)
                    {
                        mat[newX][newY] = mat[x][y] + 1;
                        queue.Enqueue(new int[] { newX, newY });
                    }
                }
            }
            return mat;
        }
    }

    class Knowleage
    {
        // 求存在过的元素；没有存在过的使用哈希表
        // key唯一

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
        // 寻找任一个元素的右边或者左边第一个比自己大或者小的元素的位置

        // 739. 每日温度
        public int[] DailyTemperatures(int[] temperatures)
        {
            int[] res = new int[temperatures.Length];
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < temperatures.Length; i++)
            {
                // 构建下标索引对应温度的递减栈（从栈顶到栈底）
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

        // 503. 下一个更大元素 II ( 循环数组 ) 
        public int[] NextGreaterElements(int[] nums)
        {
            // [1,2,1] => [1,2,1,1,2,1]
            int n = nums.Length;
            int[] ans = new int[n];
            Stack<int> s = new Stack<int>();
            // 假装这个数组长度翻倍了  模拟环形数组
            for (int i = 2 * n - 1; i >= 0; i--)
            {
                // 只留比自己大的
                while (s.Any() && s.Peek() <= nums[i % n])
                    s.Pop();
                ans[i % n] = !s.Any() ? -1 : s.Peek();
                s.Push(nums[i % n]);
            }
            return ans;
        }

        // 接雨水 动态规划  按每列求值
        public int Trap(int[] height)
        {
            // 动态规划 先求好每个下标对应左右两边的最高高度
            if (height == null || height.Length == 0) return 0;
            int len = height.Length;
            // dp数组
            var maxLeftH = new int[len];
            var maxRightH = new int[len];
            // 求每列左边最高的柱子高度
            maxLeftH[0] = height[0];
            for (int i = 1; i < height.Length - 1; i++)
            {
                maxLeftH[i] = Math.Max(height[i], maxLeftH[i - 1]);
            }
            // 求每列右边最高的柱子高度
            maxRightH[len - 1] = height[len - 1];
            for (int i = len - 2; i >= 1; i--)
            {
                maxRightH[i] = Math.Max(height[i], maxRightH[i + 1]);
            }
            // 首尾两端漏水，无法接水
            int res = 0;
            for (int i = 1; i < len - 1; i++)
            {
                int h = Math.Min(maxLeftH[i], maxRightH[i]) - height[i];
                // 两边较小的一端高于当前列才能接雨水
                if (h > 0) res += h;
            }
            return res;
        }
        // 接雨水 单调栈
        public int TrapStack(int[] height)
        {
            int res = 0;
            return res;
        }

        // 柱状图中最大的矩形 动态规划
        public int LargestRectangleArea(int[] heights)
        {
            if (heights == null || heights.Length == 0) return 0;
            int len = heights.Length;
            var minLeftIndex = new int[len];
            var minRightIndex = new int[len];
            // 记录每个柱子 左边第一个小于该柱子的下标
            // 初始化第一个柱子
            minLeftIndex[0] = -1;
            for (int i = 1; i < len; i++)
            {
                int t = i - 1;
                // 这里不是用if，而是不断向左寻找的过程
                // 遇到左边比当前的柱子高的，就左移继续查找
                while (t >= 0 && heights[t] >= heights[i]) t = minLeftIndex[t];
                minLeftIndex[i] = t;
            }
            // 初始化最后一个柱子
            minRightIndex[len - 1] = len;
            // 记录每个柱子 右边第一个小于该柱子的下标
            for (int i = len - 2; i >= 0; i--)
            {
                int k = i + 1;
                while (k < len && heights[k] >= heights[i]) k = minRightIndex[k];
                minRightIndex[i] = k;
            }
            // 计算和
            int res = 0;
            for (int i = 0; i < len; i++)
            {
                // 为什么要用右边最小-左边最小？
                // 因为前面进行的初始化 右边数组的值永远大于0
                // 面积 = 当前柱子的高度 * 能形成的最大宽度
                int area = heights[i] * (minRightIndex[i] - minLeftIndex[i] - 1);
                res = Math.Max(res, area);
            }
            return res;
        }
        // 84. 柱状图中最大的矩形 单调栈
        public int LargestRectangleAreaStack(int[] heights)
        {
            if (heights == null || heights.Length <= 0) return 0;
            int max = 0;
            var stack = new Stack<int>();
            for (int i = 0; i < heights.Length + 1; ++i)
            {
                while (stack.Any() && ((i == heights.Length) || heights[i] < heights[stack.Peek()]))
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
