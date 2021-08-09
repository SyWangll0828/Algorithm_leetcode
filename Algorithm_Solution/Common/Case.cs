using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Case
    {
        public static int MyProperty { get; set; }
        public int MyProperty2 { get; set; }

        public string s = "3[a]2[bc]";
        public int[] index = { 4, 5, 6, 7, 0, 2, 1, 3 };
        public int n = 3;
        public int[] traget = { 2, 3, 4 };
        public int[] nums = { 1, 0, 1, 1 };
        public int[] nums3 = { 2, 4, 3 };
        public int[] nums4 = { 5, 6, 4 };
        public int[] nums1 = { 7, 3, 3, 6, 6, 6, 10, 5, 9, 2 };
        public int[] nums2 = { 1, 2, 3, 4 };
        public string[] tokens = { "4", "13", "5", "/", "+" };
        public string[] tsetStr = { "4", "13", "5", "0", "8" };
        public char[] tsetChar = { '4', '3', '5', '0', '8' };
        public int[] sort = { 83, 4, 52, 6, 71, 0, 2, 1, 3 };

        public int[][] twoArrayOne = new int[6][] {
            new int[] { -1, -1, -1, -1, -1, -1 },
            new int[] { -1, -1, -1, -1, -1, -1 },
            new int[] { -1, -1, -1, -1, -1, -1 },
            new int[] { -1, 35, -1 ,-1, 13, -1 },
            new int[] { -1,-1,-1,-1,-1,-1 },
            new int[] { -1,15,-1,-1,-1,-1}
        };
        public int[][] twoArrayTwo = new int[5][] {
            new int[] {1,4,7,11,15},
            new int[] { 2,5,8 ,12,19,},
            new int[] { 3,6,9,16,22},
            new int[] { 10,13,14,17,24},
            new int[] { 18,21,23,26,30}
        };

        public int[][] twoArrayThree = new int[3][] {
            new int[] {1,1,0,0,0},
            new int[] {1,1,1,1,0},
            new int[] {1,1,1,0,0},
        };

        public int[][] twoArrayFour = new int[3][] {
            new int[] {1,2,3,4},
            new int[] {5,6,7,8},
            new int[] {9,10,11,12},
        };

        public int[] RandomArray()
        {
            Random ra = new Random();
            int[] arr = new int[10000];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = ra.Next();
            }
            return arr;
        }
        public ListNode head = new ListNode(1)
        {
            next = new ListNode(2)
            {
                next = new ListNode(3)
            }
        };

    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    //单例模式
    sealed class MyClass1
    {
        private MyClass1()
        {

        }
        //使用静态初始化语句
        private static readonly object syncObj = new object();
        private static MyClass1 myClass = null;
        private static MyClass1 MyClass
        {
            get
            {
                lock (syncObj)
                {
                    if (myClass == null)
                        myClass = new MyClass1();
                }
                return myClass;
            }
        }
        static MyClass1()
        {

        }
    }
}
