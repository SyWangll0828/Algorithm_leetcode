using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Test1
{
    public class MinStack
    {
        Stack<int> ST ;
        Stack<int> min_st;

        public MinStack()
        {
            ST = new Stack<int>();
            min_st = new Stack<int>();
            string s = "";
        }

        public void Push(int val)
        {
            ST.Push(val);
            if (min_st.Count == 0 || min_st.Peek() >= val)
            {
                min_st.Push(val);
            }
            else
            {
                min_st.Push(min_st.Peek());
            }
        }

        public void Pop()
        {
            try
            {
                ST.Pop();
                min_st.Pop();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public int Top()
        {
            return ST.Peek();
        }

        public int GetMin()
        {
            return min_st.Peek();
        }
    }
}
