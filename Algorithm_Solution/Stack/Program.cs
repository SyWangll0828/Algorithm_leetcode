using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            StackProblems problems = new StackProblems();
            //实例化可以访问类成员
            Common.Case testCase = new Common.Case();
            problems.IntToRoman(testCase.MyProperty2);
            //直接访问需要添加static
            problems.IntToRoman(Common.Case.MyProperty);
            Console.ReadKey();
        }
    }

    class StackProblems
    {
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

        //单调栈专门解决Next Greater Number，
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
    }
}
