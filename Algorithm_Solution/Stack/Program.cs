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
            Problems problems = new Problems();
            //实例化可以访问类成员;直接访问需要添加static
            Common.Case testCase = new Common.Case();
            //problems.IntToRoman(testCase.MyProperty2);
            //problems.ReverseWords("Let's take LeetCode contest");
            //problems.DecodeString(testCase.s);
            //problems.NextGreaterElement(testCase.nums1, testCase.nums2);
            //problems.NextGreaterElements(testCase.nums1);
            //problems.BuildArray(testCase.traget, testCase.n);
            //problems.EvalRPN(testCase.tokens);
            Console.ReadKey();
        }
    }

    class Problems
    {
        //栈的特性：后入先出
        readonly Tuple<string, int>[] tuples = {
        //从大到小，便于显示
        new Tuple<string, int>("M",1000),
        new Tuple<string, int>("D",500),
        new Tuple<string, int>("CD",400),
        new Tuple<string, int>("C",100),
        new Tuple<string, int>("L",50),
        new Tuple<string, int>("XL",40),
        new Tuple<string, int>("X",10),
        new Tuple<string, int>("V",5),
        new Tuple<string, int>("IV",4),
        new Tuple<string, int>("I",1),
        };

        //344. 反转字符串
        //反转字符串的时候可以只循环一半 i 与 len-i-1 交换

        //1711. 大餐计数
        public int CountPairs(int[] deliciousness)
        {
            int mod = 1000000007;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int len = deliciousness.Length;
            int count = 0;
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    int sum = deliciousness[i] + deliciousness[j];
                    if (sum > 0 && (sum & (sum - 1)) == 0)
                    {
                        if (dict.ContainsKey(deliciousness[i])) dict[deliciousness[i]]++;
                        else dict.Add(deliciousness[i], 1);
                        int value = 0;
                        dict.TryGetValue(i, out value);
                        count = (count + value) % mod;
                    }
                    continue;
                }
            }
            return count;
        }
        public string IntToRoman(int num)
        {
            StringBuilder builder = new StringBuilder();
            //罗马数字组成都是先去可以表示的最大值
            //140 -> CXL
            foreach (var item in tuples)
            {
                string key = item.Item1;
                int value = item.Item2;
                while (num >= value)
                {
                    num -= value;
                    builder.Append(key);
                }
                if (num == 0)
                    break;
            }
            return builder.ToString();
        }

        //1047. 删除字符串中的所有相邻重复项
        public string RemoveDuplicates(string s)
        {
            //StringBuilder可以充当栈
            StringBuilder sb = new StringBuilder();
            foreach (var c in s)
            {
                int len = sb.Length;
                if (len > 0 && c == sb[len - 1])
                    sb.Remove(len - 1, 1);
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        //1441. 用栈操作构建数组
        public IList<string> BuildArray(int[] target, int n)
        {
            List<string> list = new List<string>();
            for (int i = 1, index = 0; i <= n && index < target.Length; i++)
            {
                if (target[index] == i)
                {
                    list.Add("Push");
                    index++;
                }
                else
                {
                    //目标数组值已经经过了一次入栈出栈
                    list.Add("Push");
                    list.Add("Pop");
                }
            }
            return list;
        }

        //1544. 整理字符串
        public string MakeGood(string s)
        {
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                //栈里有无相应的大小写字母;有的话将其出栈。
                if (stack.Count != 0 && (char.ToUpper(stack.Peek()) == s[i] || char.ToLower(stack.Peek()) == s[i]) && stack.Peek() != s[i])
                    stack.Pop();
                else
                    //将元素添加到栈
                    stack.Push(s[i]);
            }
            //栈后进先出 在赋值一个新栈
            Stack<char> reverseStack = new Stack<char>();
            while (stack.Count > 0)
            {
                reverseStack.Push(stack.Pop());
            }
            return string.Join("", reverseStack.ToArray());
        }

        //150.  逆波兰表达式求值
        public int EvalRPN(string[] tokens)
        {
            Stack<int> s = new Stack<int>();
            int temp;
            foreach (var c in tokens)
            {
                if (int.TryParse(c, out temp))
                    s.Push(temp);
                else
                {
                    //将前两个数字出栈
                    int nums1 = s.Pop();
                    int nums2 = s.Pop();
                    switch (c)
                    {
                        case "+":
                            s.Push(nums2 + nums1);
                            break;
                        case "-":
                            s.Push(nums2 - nums1);
                            break;
                        case "*":
                            s.Push(nums2 * nums1);
                            break;
                        //整数除法只保留整数部分
                        case "/":
                            s.Push(nums2 / nums1);
                            break;
                        default:
                            break;
                    }
                }
            }
            return s.Pop();

        }

        #region 特殊的数据结构--单调栈
        //单调栈专门解决Next Greater Number(模板一)
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

        //单调栈专门解决Next Greater Number(模板二-循环数组)
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

        //496. 下一个更大元素 I
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

        //503. 下一个更大元素 II ( 循环数组 )
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

        //739. 每日温度
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

        //84. 柱状图中最大的矩形
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

        public static IList<string> TopKFrequent(string[] words, int k)
        {
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (keyValues.ContainsKey(word))
                    keyValues[word] += 1;
                else
                    keyValues.Add(word, 1);
            }
            List<string> res = new List<string>();
            foreach (var item in keyValues)
            {
                res.Add(item.Key);
            }
            res.Sort((a, b) =>
            {
                return keyValues[b] - keyValues[a];
            });
            return res.GetRange(0, k);
        }

        //394.  字符串解码
        public string DecodeString(string s)
        {
            Stack<string> letter = new Stack<string>();
            Stack<int> nums = new Stack<int>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (var c in s)
            {
                //处理连续数字的情况
                if (char.IsDigit(c))
                {
                    num = num * 10 + int.Parse(c.ToString());
                }
                //遇到‘[’，将当前倍数以及组成的字符串入栈并置空
                else if (c == '[')
                {
                    letter.Push(builder.ToString());
                    builder = new StringBuilder();
                    nums.Push(num);
                    num = 0;
                }
                //栈顶出栈并
                else if (c == ']')
                {
                    int count = nums.Pop();
                    StringBuilder temp = new StringBuilder();
                    for (int i = 0; i < count; i++)
                    {
                        temp.Append(builder);
                    }
                    builder = new StringBuilder(letter.Pop() + temp);
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        public void Test2()
        {
            List<Pzhy> pzhies = new List<Pzhy>()
            {
                new Pzhy { phcode = "h-1", remake = "123",c=1,d=DateTime.Now },
                new Pzhy { phcode = "h-2", remake = "345",c=2,d=DateTime.Now },
            };
            Pzhy p = new Pzhy();
            PropertyInfo[] myPropertyInfo;
            var t = p.GetType();
            // Get the properties of 'Type' class object.
            myPropertyInfo = t.GetProperties();
            foreach (var property in myPropertyInfo)
            {
                Console.WriteLine(property.Name);
                Console.WriteLine(property.PropertyType.Name);
            }
            var newType = p.GetType();
            foreach (var m in pzhies)
            {
                foreach (var item in newType.GetRuntimeProperties())
                {
                    var type = item.PropertyType.Name;
                    Console.WriteLine($"属性名称：{item.Name}，类型：{type}，值：{item.GetValue(m)}");
                }
            }

        }
    }

    //232. 用（两个）栈实现队列
    public class MyQueue
    {
        //后入先出的输出栈模拟先入先出的队列
        Stack<int> inStack;
        Stack<int> outStack;
        /** Initialize your data structure here. */
        public MyQueue()
        {
            inStack = new Stack<int>();
            outStack = new Stack<int>();
        }

        /** Push element x to the back of queue. */
        public void Push(int x)
        {
            inStack.Push(x);
        }

        /** Removes the element from in front of queue and returns that element. */
        public int Pop()
        {
            //输出栈没有元素，则将输入栈倒插到输出栈
            if (!outStack.Any())
            {
                inOutStack(inStack);
            }
            return outStack.Pop();
        }

        /** Get the front element. */
        public int Peek()
        {
            if (!outStack.Any())
            {
                inOutStack(inStack);
            }
            return outStack.Peek();
        }

        /** Returns whether the queue is empty. */
        public bool Empty()
        {
            return inStack.Count == 0 && outStack.Count == 0;
        }

        public void inOutStack(Stack<int> stack)
        {
            while (stack.Any())
            {
                outStack.Push(inStack.Pop());
            }
        }
    }

    class Pzhy
    {
        public string phcode { get; set; }
        public string remake { get; set; }
        public int c { get; set; }
        public DateTime d { get; set; }
    }
}
