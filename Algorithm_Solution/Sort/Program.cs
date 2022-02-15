using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
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

            MyTest testT = new MyTest();
            int ant = testT.FindPalindrome();


            int[] test = testCase.RandomArray();
            int[] res = new int[] { 7, 5, 6, 4, 8, 10, 0, 6, 9 };
            BubbleSort.bubbleSort(res);
            if (BaseFunc.isSorted(res))
                BaseFunc.Print(res);
            else
                Console.WriteLine("数组未成功排序");

            Type t = typeof(Knowledge);
            Type t2 = typeof(BaseFunc);

            Console.ReadKey();

        }
    }

    class MyTest
    {
        public int FindPalindrome()
        {
            int res = 11;
            while (true)
            {
                if (Convert.ToString(res, 2) == new string(Convert.ToString(res, 2).Reverse().ToArray()) &&
                    Convert.ToString(res, 8) == new string(Convert.ToString(res, 8).Reverse().ToArray()) &&
                    Convert.ToString(res, 10) == new string(Convert.ToString(res, 10).Reverse().ToArray()))
                {
                    break;
                }
                res += 2;
            }
            return res;
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

            // 桶排序
            public static void bucketSort(int[] paras)
            {
                int left = 0;
                int right = paras.Length - 1;
            }
        }
    }

    class BubbleSort
    {
        public static void bubbleSort(int[] para)
        {
            for (int i = 0; i < para.Length; i++)
            {
                for (int j = para.Length - 1; j >= i; j--)
                {
                    if (para[j] > para[j + 1])
                    {
                        BaseFunc.exch(para, j, j + 1);
                    }
                }
            }
        }
    }

    class MergeSort
    {
        // 归并排序
        public void merge(int[] nums, int left, int right)
        {
            if (left >= right) return;
            int mid = left + ((right - left) >> 1);
            // 分为左右两半，分到只剩两个数为止（分到一个数时直接返回）
            merge(nums, left, mid);
            merge(nums, mid + 1, right);
            // 进入排序，开始合并
            mergeSort(nums, left, mid, right);
        }

        public void mergeSort(int[] nums, int left, int mid, int right)
        {
            // 定义一个存放排序结果的辅助数组
            var temp = new int[right - left + 1];
            // p1 p2双指针移动比较大小
            // p2为上一步分之后的第一个起点,mid为上一步分开的位置
            // p1的范围为上一步分的长度
            int index = 0, p1 = left, p2 = mid + 1;
            while (p1 <= mid && p2 <= right)
            {
                if (nums[p1] <= nums[p2])
                {
                    temp[index++] = nums[p1++];
                }
                else // nums[p1]>nums[p2]
                {
                    // 逆序对个数统计（p1位于左边一端，p2位于右边一端）
                    // 在左边排序好的一段中，p1指向的大于p2指向的数，则p1到mid之间的数都大于p2指向的数
                    // maxCount += (mid - p1 + 1);
                    temp[index++] = nums[p2++];
                }
            }
            while (p1 <= mid)
            {
                temp[index++] = nums[p1++];
            }
            while (p2 <= right)
            {
                temp[index++] = nums[p2++];
            }
            // 用辅助数组排序原数组
            for (int k = 0; k < temp.Length; k++)
            {
                nums[left + k] = temp[k];
            }
        }
    }

    class QuickSort
    {
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
                    //Console.WriteLine($"before:left={left},right={right}");
                    //BaseFunc.Print(paras);
                    if (left < right)
                        BaseFunc.exch(paras, left, right);
                    //Console.WriteLine("交换后");
                    //BaseFunc.Print(paras);
                }
                //最后中心点的值和左右下标相聚时的值进行交换
                BaseFunc.exch(paras, left, rightBound);
                //BaseFunc.Print(paras);
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
    }

    class HeapSort
    {
        // 堆排序
        // 当前节点下标 index 父节点: (index-1)/2 左子: index*2+1 右子: index*2+2
        public static void heapSort(int[] paras)
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
}
