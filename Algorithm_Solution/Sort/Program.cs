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
            Knowledge knowledge = new Knowledge();
            //实例化可以访问类成员;带有static的可以直接访问
            Common.Case testCase = new Common.Case();
            Knowledge.SortSolution.SelectSort(testCase.tsetStr);
            //Console.WriteLine(problems.RestoreString(testCase.s, testCase.index));
            Console.ReadKey();
        }
    }
    class Knowledge
    {
        class BaseFunc
        {
            //使用泛型实现的比较方法
            public static bool less<T>(T high, T low)
            {
                IComparable iHigh = (IComparable)high;
                IComparable iLow = (IComparable)low;
                //CompareTo 返回1 表示当前实例在排序后将后移及 iHigh > iLow
                return iHigh.CompareTo(iLow) < 0;
            }
            //交换
            public static void exch<T>(T[] paras, int a, int b)
            {
                T temp = paras[a];
                paras[a] = paras[b];
                paras[b] = temp;
            }
            //打印数组
            public static void Print<T>(T[] paras)
            {
                for (int i = 0; i < paras.Length; i++)
                    Console.WriteLine(paras[i]);
            }
            public static bool isSorted<T>(T[] paras)
            {
                for (int i = 1; i < paras.Length - 1; i++)
                {
                    if (less(paras[i], paras[i - 1])) return false;
                }
                return true;
            }
        }

        public class SortSolution
        {
            #region 基础排序
            //选择排序
            //从第一个元素开始，与剩余元素比较，将最小的放到第一个，循环剩下的元素。
            public static void SelectSort<T>(T[] paras)
            {
                for (int i = 0; i < paras.Length - 1; i++)
                {
                    int min = i;
                    for (int j = i + 1; j < paras.Length; j++)
                    {
                        if (BaseFunc.less(paras[j], paras[min])) min = j;
                    }
                    BaseFunc.exch(paras, i, min);
                }
                if (BaseFunc.isSorted(paras))
                    BaseFunc.Print(paras);
                else
                    Console.WriteLine("数组未成功排序");

            }
            //插入排序

            //希尔排序


            #endregion
            #region 其他排序
            //归并排序

            //快速排序

            //堆排序
            #endregion
        }

    }
}
