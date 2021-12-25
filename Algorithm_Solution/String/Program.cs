using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Problem
    {
        #region StringBuilder API使用
        // 1047. 删除字符串中的所有相邻重复项
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
        #endregion
    }

    class Knowleage
    {
        
    }
}
