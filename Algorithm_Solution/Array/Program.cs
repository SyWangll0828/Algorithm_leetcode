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
            problems.ThirdMax(testCase.nums);
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
    }
}
