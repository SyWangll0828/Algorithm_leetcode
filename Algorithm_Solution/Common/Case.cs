﻿using System;
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
        public int[] nums1 = { 1, 2, 1 };
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
        public int[][] twoArrayTwo = new int[2][] {
            new int[] { 0, 11, 12 },
            new int[] { 1, 2, 3, 4, 5 }
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
    }

    //单例模式
    sealed class MyClass1
    {
        private MyClass1()
        {

        }

        private static readonly object syncObj = new object();
        private static MyClass1 myClass = null;
        private static MyClass1 MyClass
        {
            get{
                lock (syncObj)
                {
                    if (myClass == null)
                        myClass = new MyClass1();
                }
                return myClass;
            }
        }
    }
}
