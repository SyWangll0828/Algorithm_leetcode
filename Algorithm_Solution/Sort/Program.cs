using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Problems problems = new Problems();
            //实例化可以访问类成员;带有static的可以直接访问
            Common.Case testCase = new Common.Case();
            //Console.WriteLine(problems.RestoreString(testCase.s, testCase.index));
            Console.ReadKey();
        }
    }
    class Problems
    {

        //位运算 
        public static int[] PlusOne(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                digits[i]++;
                digits[i] %= 10;
                if (digits[i] != 0)
                    return digits;
            }
            digits = new int[digits.Length + 1];
            digits[0] = 1;
            return digits;
        }
    }
}
