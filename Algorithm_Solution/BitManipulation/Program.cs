using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Problems
    {
        //231. 2 的幂
        public bool IsPowerOfTwo(int n)
        {
            //如果 n 是正整数并且 n & (n - 1) = 0 或者 n & (-n) = n，那么 n 就是 2 的幂。
            return n > 1 && (n & (n - 1)) == 0;
        }

        //lowbit运算
    }
}
