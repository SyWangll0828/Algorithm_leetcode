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
            //Knowledge.SortSolution.SelectSort(testCase.tsetStr);
            //Knowledge.SortSolution.InsertSort(testCase.sort);
            //Knowledge.SortSolution.ShellSort(testCase.sort);
            int[] test = testCase.RandomArray();
            //Knowledge.SortSolution.InsertSort(test);
            //Knowledge.SortSolution.ShellSort(test);
            Knowledge.SortSolution.Merge(test, 0, test.Length - 1);
            //Console.WriteLine(problems.RestoreString(testCase.s, testCase.index));
            Console.ReadKey();
        }
    }
    class Knowledge
    {
        class BaseFunc
        {
            //使用泛型实现的比较方法
            public static bool less<T>(T low, T high)
            {
                IComparable iHigh = (IComparable)high;
                IComparable iLow = (IComparable)low;
                //CompareTo 返回-1 表示 iLow < iHigh
                return iLow.CompareTo(iHigh) < 0;
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
            //比较算法所需时间
            //public static double Time(string str,double[] a)
            //{

            //}
            //public static double timeRandomInput(string str, int n,int t)
            //{

            //}


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
            //将元素插入到已经排序好的数组适当位置中，
            public static void InsertSort<T>(T[] paras)
            {
                //升序排序
                for (int i = 1; i < paras.Length; i++)
                {
                    for (int j = i; j > 0 && BaseFunc.less(paras[j], paras[j - 1]); j--)
                    {
                        BaseFunc.exch(paras, j, j - 1);
                    }
                }
                if (BaseFunc.isSorted(paras))
                    BaseFunc.Print(paras);
                else
                    Console.WriteLine("数组未成功排序");
            }
            //希尔排序
            //将已有数组进行分组,分别先进行插入排序
            public static void ShellSort<T>(T[] paras)
            {
                int h = 1;
                while (h < paras.Length / 3) h = 3 * h + 1;
                while (h >= 1)
                {
                    for (int i = h; i < paras.Length; i++)
                    {
                        for (int j = i; j >= h && BaseFunc.less(paras[j], paras[j - h]); j -= h)
                        {
                            BaseFunc.exch(paras, j, j - 1);
                        }
                    }
                    h /= 3;
                }
                if (BaseFunc.isSorted(paras))
                    BaseFunc.Print(paras);
                else
                    Console.WriteLine("数组未成功排序");
            }
            #endregion
            #region 其他排序
            //归并排序
            public static void Merge<T>(T[] paras, int left, int right)
            {
                if (left == right) return;
                //分成两半
                int mid = left + (right - left) / 2;
                //左边排序
                Merge(paras, left, mid);
                //右边排序
                Merge(paras, mid + 1, right);

                MergeSort(paras, left, mid + 1, right);

            }
            public static void MergeSort<T>(T[] paras, int leftPrt, int rigthPtr, int rightBound)
            {
                int mid = rigthPtr - 1;
                T[] temp = new T[rightBound - leftPrt + 1];
                int i = leftPrt, j = rigthPtr, k = 0;
                while (i <= mid && j <= rightBound)
                {
                    temp[k++] = BaseFunc.less(paras[i], paras[j]) ? paras[i++] : paras[j++];
                }
                while (i <= mid) temp[k++] = paras[i++];
                while (j <= rightBound) temp[k++] = paras[j++];
                for (int i = 0; i < temp.Length-1; i++) paras[i] == temp[i];
            }


            //快速排序
            public static void QuickSort<T>(T[] paras)
            { }

            //堆排序
            public static void HeapSort<T>(T[] paras)
            { }
            #endregion
        }

    }
}
