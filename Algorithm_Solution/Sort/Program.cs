using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sort.Knowledge;

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
            int[] res = new int[] { 7, 5, 6, 4, 8, 10, 0, 6, 9 };
            SortSolution.RandomQuick(res,0,res.Length-1);
            if (BaseFunc.isSorted(res))
                BaseFunc.Print(res);
            else
                Console.WriteLine("数组未成功排序");


            SortSolution.Merge(test, 0, test.Length - 1);
            //int n = Knowledge.SortSolution.ReversePairs(res);
            //Console.WriteLine(problems.RestoreString(testCase.s, testCase.index));
            Console.ReadKey();
        }
    }
    class Knowledge
    {
        public class BaseFunc
        {
            //使用泛型实现的比较方法
            public static bool less(int low, int high)
            {
                IComparable iHigh = (IComparable)high;
                IComparable iLow = (IComparable)low;
                //CompareTo 返回-1 表示 iLow < iHigh
                return iLow.CompareTo(iHigh) < 0;
            }
            //交换
            public static void exch(int[] paras, int a, int b)
            {
                //a与b不能相同
                //paras[a] = paras[a] ^ paras[b];
                //paras[b] = paras[a] ^ paras[b];
                //paras[a] = paras[a] ^ paras[b];

                int temp = paras[a];
                paras[a] = paras[b];
                paras[b] = temp;
            }
            //打印数组
            public static void Print(int[] paras)
            {
                for (int i = 0; i < paras.Length; i++)
                    Console.Write(paras[i] + " ");
                Console.WriteLine();
            }
            public static bool isSorted(int[] paras)
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
            public static void SelectSort(int[] paras)
            {
                //特判
                if (paras == null || paras.Length < 2)
                {
                    return;
                }
                for (int i = 0; i < paras.Length - 1; i++)
                {
                    //最小值的下标
                    int min = i;
                    for (int j = i + 1; j < paras.Length; j++)
                    {
                        if (BaseFunc.less(paras[j], paras[min])) min = j;
                    }
                    BaseFunc.exch(paras, i, min);
                }
            }
            //冒泡排序
            //从左边开始比较，较大的往后移
            public static void BubbleSort(int[] paras)
            {
                //特判
                if (paras == null || paras.Length < 2)
                {
                    return;
                }
                //在0-->n-1上比较
                //在0-->n-2上比较
                for (int i = paras.Length - 1; i > 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (paras[j] > paras[j + 1])
                            BaseFunc.exch(paras, i, j);
                    }
                }
            }

            //插入排序
            //从下标1的元素开始与前一个元素进行比较，将元素插入到已经排序好的数组适当位置中
            public static void InsertSort(int[] paras)
            {
                //特判
                if (paras == null || paras.Length < 2)
                {
                    return;
                }
                //升序排序
                for (int i = 1; i < paras.Length; i++)
                {
                    for (int j = i; j > 0 && BaseFunc.less(paras[j], paras[j - 1]); j--)
                    {
                        BaseFunc.exch(paras, j, j - 1);
                    }
                }
            }
            //希尔排序
            //将已有数组进行分组,分别先进行插入排序
            public static void ShellSort(int[] paras)
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
            }
            #endregion

            #region 其他排序
            //归并排序
            public static void Merge(int[] paras, int left, int right)
            {
                //特判
                if (paras == null || paras.Length < 2)
                {
                    return;
                }
                if (left >= right) return;
                // 分成两半
                int mid = left + (right - left) / 2;
                // 左边排序
                Merge(paras, left, mid);
                // 右边排序
                Merge(paras, mid + 1, right);

                MergeSort(left, mid, right);

                // 原地归并
                void MergeSort(int l, int m, int r)
                {
                    int[] temp = new int[r - l + 1];
                    int i = 0;
                    int p1 = l;
                    int p2 = m + 1;
                    while (p1 <= m && p2 <= r)
                    {
                        temp[i++] = paras[p1] <= paras[p2] ? paras[p1++] : paras[p2++];
                    }
                    while (p1 <= m)
                    {
                        temp[i++] = paras[p1++];
                    }
                    while (p2 <= r)
                    {
                        temp[i++] = paras[p2++];
                    }
                    //从辅助数组输出到原数组
                    for (int k = 0; k < temp.Length; k++)
                    {
                        paras[l + k] = temp[k];
                    }
                }
            }

            // 快速排序 1.0 左边小于等于中心点的数；右边大于中心点的数
            public static void Quick(int[] paras, int l, int r)
            {
                if (l >= r) return;
                int mid = quickSort(l, r);
                Quick(paras, l, mid - 1);
                Quick(paras, mid + 1, r);

                int quickSort(int leftBound, int rightBound)
                {
                    //选取最右边元素为中心点
                    int pivot = paras[rightBound];
                    //从左边界从左向右寻找第一个比中心点大的数
                    int left = leftBound;
                    //从中心点之前的一个元素开始从右向左寻找第一个比中心点小的数
                    int right = rightBound - 1;
                    //注意要考虑左边界和右边界相等的情况
                    //当指向同一个下标时需要再次进入一次循环
                    while (left <= right)
                    {
                        //<=操作 便于跳出循环
                        while (left <= right && paras[left] <= pivot) left++;
                        while (left <= right && paras[right] > pivot) right--;
                        Console.WriteLine($"before:left={left},right={right}");
                        BaseFunc.Print(paras);
                        if (left < right)
                            BaseFunc.exch(paras, left, right);
                        Console.WriteLine("交换后");
                        BaseFunc.Print(paras);
                    }
                    //最后中心点的值和左右下标相聚时的值进行交换
                    BaseFunc.exch(paras, left, rightBound);
                    BaseFunc.Print(paras);
                    //返回新的中心点值
                    return left;
                }
            }

            // 随机快排 3.0
            public static void RandomQuick(int[] paras, int l, int r)
            {
                if (l >= r) return;
                Random random = new Random();
                // 随机选取一个数字和最右边值进行交换
                BaseFunc.exch(paras, (int)(random.NextDouble() * (r - l + 1)), r);
                int[] p = partition(l, r);
                Quick(paras, l, p[0] - 1);
                Quick(paras, p[1] + 1, r);

                int[] partition(int left, int right)
                {
                    // >划分值左边界
                    int less = left - 1;
                    int more = right;
                    while (left < more)
                    {
                        // 当前数<划分值
                        if (paras[left] < paras[right])
                        {
                            BaseFunc.exch(paras, ++less, left++);
                        }
                        else if (paras[left] > paras[right])
                        {
                            BaseFunc.exch(paras, --more, left);
                        }
                        else
                            left++;
                    }
                    BaseFunc.exch(paras, more, right);
                    return new int[] { less + 1, more };
                }
            }

            // 堆排序
            // 当前节点下标 index 父节点: (index-1)/2 左子: index*2+1 右子: index*2+2
            public static void HeapSort(int[] paras)
            {
                //特判
                if (paras == null || paras.Length < 2)
                {
                    return;
                }
                // 使数组变成大根堆 方式一 从头开始
                //for (int i = 0; i < paras.Length; i++)
                //{
                //    // O(logN)
                //    heapInsert(i);
                //}
                // 使数组变成大根堆 方式二 从叶子节点开始
                for (int i = paras.Length - 1; i >= 0; i--)
                {
                    // O(N)
                    heapify(paras, i, paras.Length);
                }
                int heapSize = paras.Length;
                BaseFunc.exch(paras, 0, --heapSize);
                while (heapSize > 0)
                {
                    // O(logN)
                    heapify(paras, 0, heapSize);
                    BaseFunc.exch(paras, 0, --heapSize);
                }

                // 子和父进行比较
                void heapInsert(int[] arr, int index)
                {
                    while (arr[index] > arr[(index - 1) / 2])
                    {
                        BaseFunc.exch(arr, index, (index - 1) / 2);
                        index = (index - 1) / 2;
                    }
                }

                // 堆化
                void heapify(int[] arr, int index, int size)
                {
                    // 左孩子的下标
                    int left = index * 2 + 1;
                    // 下方还有孩子的时候
                    while (left < size)
                    {
                        // 左右孩子中，把大的值下标给largest
                        int largest = left + 1 < size && arr[left + 1] > arr[left] ? left + 1 : left;
                        // 孩子的最大值与父比较
                        largest = arr[largest] > arr[index] ? largest : index;
                        if (largest == index)
                        {
                            break;
                        }
                        BaseFunc.exch(arr, index, largest);
                        index = largest;
                        left = index * 2 + 1;
                    }
                }
            }
        }
        #endregion
    }

}
