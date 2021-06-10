using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class Form1 : Form
    {
        string[] strs = new string[4] { "aaaaaaa", "aab", "aaaa", "aaaaaaaa" };
        int sum = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            int[] nums1 = new int[8] {73, 74, 75, 71, 69, 72, 76, 73};
            int[] nums = new int[4] { 0,1,0,1};
            int[] nums2 = new int[7] { 1, 1, 1 ,1,1,1,1};
            int[] num3 = new int[4] { 137, 59, 92, 236 };
            int[] num4 = new int[4] { 137, 59, 92, 236 };
            string[] st1 = new string[5]{"5", "2", "C", "D", "+"};
            //txt3.Text = AddBinary(txt2.Text, txt1.Text);
            //txt3.Text = RomanToInt(txt2.Text).ToString();
            //txt3.Text = LongestCommonPrefix(strs);
            //txt3.Text = IsValid(txt2.Text).ToString();
            //txt3.Text = intToRoman(int.Parse(txt2.Text));
            //Merge(nums1, 3, nums2, 3);
            //txt3.Text = TrailingZeroes(int.Parse(txt2.Text)).ToString();
            //txt3.Text = LengthOfLastWord(txt2.Text).ToString();
            //txt3.Text = StrStr(txt2.Text, txt1.Text).ToString();
            //txt3.Text = IsUgly(int.Parse(txt2.Text)).ToString();
            //txt3.Text = ContainsNearbyDuplicate(nums2,2).ToString();
            //txt3.Text = AddDigits(int.Parse(txt1.Text)).ToString();
            //txt2.Text = AddDigits(int.Parse(txt1.Text), 1).ToString();
            //txt2.Text = ReplaceSpace(txt1.Text);
            //txt2.Text = SumOfUnique(nums2).ToString();
            //txt2.Text = CalPoints(st1).ToString();
            //txt2.Text = NextGreaterElement(num3, num4).ToString();
            //txt2.Text = BuildArray(nums1, 3).ToString();
            //txt2.Text = MakeGood2(txt1.Text);
            //txt2.Text = MaxProduct(nums1).ToString();
            //txt2.Text = TruncateSentence("Hello how are you", 3);
            //FindErrorNums(nums1);
            //txt3.Text = FindLUSlength(txt1.Text, txt2.Text).ToString();
            //txt2.Text = Maximum69Number(int.Parse(txt1.Text)).ToString();
            //txt2.Text = GenerateTheString(int.Parse(txt1.Text));
            //txt2.Text = SortSentence(txt1.Text);
            //txt2.Text = CheckPerfectNumber(int.Parse(txt1.Text)).ToString();
            //txt2.Text = CountStudents(nums1, nums).ToString();
            //txt2.Text = CanPlaceFlowers(nums1, 1).ToString();
            DailyTemperatures(nums1);
        }

        //67 二进制求和
        public static string AddBinary(string a, string b)
        {
            int c = 0;
            int i = a.Length - 1;
            int j = b.Length - 1;
            StringBuilder stb = new StringBuilder();

            while (i >= 0 && j >= 0)
            {
                int sum = 0;
                if (a.Length >= 1 && b.Length >= 1)
                {
                    sum += int.Parse(a.Substring(i--, 1));
                    sum += int.Parse(b.Substring(j--, 1));
                }
                if (sum + c > 2 || (sum == 1 && c == 1))
                {
                    stb.Append((c + sum) % 2);
                }
                else if (sum < 2 && c == 1)
                {
                    stb.Append(c);
                }
                else
                {
                    stb.Append((c + sum) % 2);
                }
                c = (sum + c) / 2;
            }
            while (i >= 0 && j != 0)
            {
                int sum = c + int.Parse(a.Substring(i--, 1));
                c = sum / 2;
                stb.Append(sum % 2);
            }
            while (j >= 0 && i != 0)
            {
                int sum = c + int.Parse(b.Substring(j--, 1));
                c = sum / 2;
                stb.Append(sum % 2);
            }
            if (c == 1)
            {
                stb.Append(c);
            }

            string str = stb.ToString();

            char[] arr = str.ToCharArray();

            Array.Reverse(arr);

            return new string(arr);
        }


        //13 罗马字符转数字
        #region
        public int RomanToInt(string s)
        {
            char[] str = s.ToCharArray();
            int sum = 0;

            if (s.Length >= 2)
            {
                for (int i = s.Length - 2; i >= 0; i--)
                {
                    if (Romanint(str[i]) >= Romanint(str[i + 1]))
                    {
                        sum += Romanint(str[i]);
                    }
                    else
                    {
                        sum -= Romanint(str[i]);
                    }
                }
                sum += Romanint(str[s.Length - 1]);
            }
            else
                sum = Romanint(str[0]);

            return sum;
        }

        public int Romanint(char m)
        {
            switch (m)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                case 'C': return 100;
                case 'D': return 500;
                default: return 1000;
            }
        }
        #endregion

        //12 整数转罗马数字
        public string intToRoman(int num)
        {
            // 把阿拉伯数字与罗马数字可能出现的所有情况和对应关系，放在两个数组中，并且按照阿拉伯数字的大小降序排列
            int[] nums = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] romans = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            StringBuilder sb = new StringBuilder();
            int index = 0;
            while (index < 13)
            {
                // 特别注意：这里是等号
                while (num >= nums[index])
                {
                    sb.Append(romans[index]);
                    num -= nums[index];
                }
                index++;
            }
            return sb.ToString();
        }

        //14 最长公共前缀
        #region
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 1)
            {
                return strs[0].ToString();
            }
            if (strs.Length == 0)
            {
                return "";
            }
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] == "")
                    return "";
            }
            int Sum = 0;
            int flag = 0;
            int minlength = strs[0].Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < strs.Length; i++)
            {
                if (strs[i].Length < minlength)
                {
                    minlength = strs[i].Length;
                    flag = i;
                }
                if (minlength == 1)
                {
                    break;
                }
            }

            for (int i = 0; i < SUM(strs, minlength); i++)
            {
                sb.Append(strs[flag][i]);
            }
            return sb.ToString();
        }

        public int SUM(string[] strs, int length)
        {
            List<int> list = new List<int>();
            int minNum = 0;
            for (int i = 1; i < strs.Length; i++)
            {
                int sum = 0;
                for (int j = 0; j < length; j++)
                {
                    if (strs[i][j] == strs[0][j])
                    {
                        sum++;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                list.Add(sum);
            }
            if (list.Count == 1)
            {
                return list[0];
            }
            minNum = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] < minNum)
                {
                    minNum = list[i];
                }
            }
            return minNum;
        }
        #endregion

        //20 有效的括号
        #region
        public bool IsValid(string s)
        {
            if (s == "")
            {
                return false;
            }
            char[] kh = s.ToCharArray();

            Stack<char> temp = new Stack<char>();

            temp.Push(kh[0]);
            if (khlx(kh[0]) % 2 == 0)
            {
                return false;
            }
            for (int i = 0; i < kh.Length - 1; i++)
            {
                if (kh[i + 1] == '(' || kh[i + 1] == '{' || kh[i + 1] == '[')
                {
                    temp.Push(kh[i + 1]);
                }
                else
                {
                    if (temp.Count > 0)
                    {
                        if (khlx(temp.Peek()) != kh[i + 1])
                        {
                            return false;
                        }
                        if (khlx(temp.Peek()) == kh[i + 1])
                        {
                            temp.Pop();
                        }
                    }
                    else
                    {
                        if (khlx(kh[i + 1]) == ' ')
                        {
                            return false;
                        }
                    }
                }
            }
            if (temp.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public char khlx(char m)
        {
            switch (m)
            {
                case '(': return ')';
                case '[': return ']';
                case '{': return '}';
                default: return ' ';
            }
        }
        #endregion

        //704 二分查找(递归算法)
        #region
        public int search(int[] nums, int target)
        {
            int len = nums.Length;

            int left = 0;
            int right = len - 1;
            // 目标元素可能存在在区间 [left, right]
            return index(left, right, nums, target);
        }

        public int index(int left, int right, int[] nums, int target)
        {
            int mid = left + (right - left) / 2;
            while (left <= right)
            {
                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
                return index(left, right, nums, target);
            }
            return -1;
        }
        #endregion

        //374 猜数字大小
        #region
        //public int GuessNumber(int n)
        //{
        //    int left = 0;
        //    int right = n;
        //    while (left <= right)
        //    {
        //        int mid = left + (right - left) / 2;
        //        int result = guess(mid);
        //        if (result == 0)
        //        {
        //            return mid;
        //        }
        //        else if (result < 0)
        //        {
        //            right = mid - 1;
        //        }
        //        else
        //        {
        //            left = mid + 1;
        //        }
        //    }
        //    return -1;
        //}
        #endregion

        //88 合并两个有序数组
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int index = 0;
            int[] num = new int[m + n];
            for (int i = 0, j = 0; i < m || j < n;)
            {
                if (i < m && j < n)
                {
                    num[index++] = nums1[i] < nums2[j] ? nums1[i++] : nums2[j++];
                }
                else if (i < m)
                {
                    num[index++] = nums1[i++];
                }
                else if (j < n)
                {
                    num[index++] = nums2[j++];
                }
            }
            int s = 0;
            //string str = "";
            //for(int i=0;i<num.Length;i++)
            //{
            //    str += num[i].ToString();
            //}
            //txt3.Text = str;
            for (int i = 0; i < num.Length; i++)
            {
                for (int j = i + 1; j < num.Length - 1; j++)
                {
                    if (num[i] > num[j])
                    {
                        num[i] = s;
                        s = num[j];
                        num[j] = s;
                    }
                }
            }
            num.CopyTo(nums1, 0);
        }

        //172 阶乘后的0
        public int TrailingZeroes(int n)
        {
            int count = 0;
            while (n >= 5)
            {
                count += n / 5;
                n /= 5;
            }
            return count;
        }

        //58. 最后一个单词的长度
        public int LengthOfLastWord(string s)
        {
            string[] str = s.Split(' ');
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(str[i]))
                {
                    return str[i].Length;
                }
            }

            return 0;
        }

        //169. 多数元素
        public int MajorityElement(int[] num)
        {
            int count = 1;
            int hxr = num[0];
            for (int i = 1; i < num.Length; i++)
            {
                if (count == 0)
                {
                    count = 1;
                    hxr = num[i];
                    continue;
                }
                if (num[i] != hxr)
                {
                    count--;
                }
                else
                {
                    count++;
                }
            }
            return hxr;
        }

        //	231. 2的幂
        public bool IsPowerOfTwo(int n)
        {
            if (n < 1)
            {
                return false;
            }
            while (n % 2 == 0)
            {
                n /= 2;
            }
            return n == 1;
        }

        //326. 3的幂
        public bool IsPowerOfThree(int n)
        {
            if (n < 1)
            {
                return false;
            }
            while (n % 3 == 0)
            {
                n /= 3;
            }
            return n == 1;
        }


        //342. 4的幂
        public bool IsPowerOfFour(int n)
        {
            if (n < 1)
            {
                return false;
            }
            while (n % 4 == 0)
            {
                n /= 4;
            }
            return n == 1;
        }

        //263. 丑数 (丑数 就是只包含质因数 2、3 和/或 5 的正整数。)
        public bool IsUgly(int n)
        {
            if (n < 1)
            {
                return false;
            }
            while (n % 2 == 0) n /= 2;
            while (n % 3 == 0) n /= 3;
            while (n % 5 == 0) n /= 5;
            return n == 1;
        }

        //217. 存在重复元素
        public bool ContainsDuplicate(int[] nums)
        {
            int first = nums[0];
            Hashtable hs = new Hashtable();
            for (int i = 0; i < nums.Length; i++)
            {
                if (hs.Contains(nums[i]))
                {
                    return true;
                }
                else
                {
                    hs.Add(nums[i], i);
                }
            }
            return false;
        }

        //219. 存在重复元素 II
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            HashSet<int> hs = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (hs.Contains(nums[i]))
                {
                    return true;
                }
                hs.Add(nums[i]);
                if (hs.Count() > k)
                {
                    hs.Remove(nums[i - k]);
                }
            }
            return false;
        }

        //258. 各位相加(两种解法)
        #region
        public int AddDigits(int num)
        {
            while (num >= 10)
            {
                num = num / 10 + num % 10;
            }
            return num;
        }

        public int AddDigits(int num, int two)
        {
            return (num - 1) % 9 + 1;
        }
        #endregion

        //191. 位1的个数
        public int HammingWeight(uint n)
        {
            int res = 0;
            for (int i = 0; i < 32; i++)
            {
                res += int.Parse(((n >> i) & 1).ToString());
            }
            return res;
        }

        //204. 计数质数
        public int CountPrimes(int n)
        {
            return 0;
        }

        //1748. 唯一元素的和
        public int SumOfUnique(int[] nums)
        {
            int sum=0;
            Hashtable hs = new Hashtable();
            for (int i = 0; i < nums.Length; i++)
            {
                if (hs.Contains(nums[i]))
                    hs[nums[i]] = 0;
                else
                {
                    hs.Add(nums[i], 1);
                }
            }
            foreach(DictionaryEntry de in hs)
            {
                int m =int.Parse(de.Value.ToString());
                if (m == 1)
                    sum += int.Parse(de.Key.ToString());
            }
            return sum;
        }

        //804. 唯一摩尔斯密码词
        public int UniqueMorseRepresentations(string[] words)
        {
            Dictionary<char, string> dct = new Dictionary<char, string>()
            {
                {'a', ".-"},{'b', "-..."},{'c',"-.-." },{'d',"-.." },{'e',"." },{'f',"..-." },{'g', "--."},
                {'h', "...."},{'i',".." },{'j', ".---"},{'k', "-.-"},{'l', ".-.."},{'m',"--" },{'n', "-."},
                {'o',"---" },{'p', ".--."},{'q',"--.-"},{'r',".-."},{ 's',"..."},{ 't',"-"},{'u',"..-"},
                {'v', "...-"},{'w',".--"},{'x',"-..-"},{'y',"-.--"},{'z',"--.."}
            };
            StringBuilder sb = new StringBuilder();
            Dictionary<string, int> dct1 = new Dictionary<string, int>();

            foreach (string i in words)//遍历words里的所有单词。
            {
                for (int j = 0; j < i.Length; j++)//遍历单词的字母
                {
                    //将每个字母所对应的摩斯密码连起来。
                    sb.Append(dct[i[j]]);
                }
                if (!dct1.ContainsKey(sb.ToString()))
                {
                    dct1.Add(sb.ToString(), 1);
                }

                sb.Clear();//最后，将StringBuilder清空，因为遍历到下一串单词还用用上它。
            }
            return dct1.Count;
        }

        //1021. 删除最外层的括号
        public string RemoveOuterParentheses(string s)
        {
            int count = 0;
            string ans = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' && count++ > 0) ans += '(';
                if (s[i] == ')' && count-- > 1) ans += ')';
            }
            return ans;
            
        }

        //682. 棒球比赛
        public int CalPoints(string[] ops)
        {
            Stack<int> st = new Stack<int>();
            for(int i=0;i<ops.Length;i++)
            {
                switch(ops[i])
                {
                    case "D":
                        int k = st.Peek() * 2;
                        st.Push(k);
                        break;
                    case "C":
                        st.Pop();
                        break;
                    case "+":
                        int m = st.Pop();
                        int n = st.Peek();
                        st.Push(m);
                        st.Push(m + n);
                        break;
                    default:
                        st.Push(int.Parse(ops[i]));
                        break;
                }
            }
            return st.Sum();
        }

        //496. 下一个更大元素 I
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            int m = 0;
            int flag = 0;
            List<int> num = new List<int>();
            Dictionary<int, int> dt = new Dictionary<int, int>();
            for(int i=0;i<nums2.Length;i++)
            {
                dt.Add(nums2[i], i);
            }
            for (int i = 0; i < nums1.Length; i++)
            {
                dt.TryGetValue(nums1[i],out m);
                while (m < nums2.Length)
                {
                    flag = 0;
                    if (nums2[m] > nums1[i])
                    {
                        num.Add(nums2[m]);
                        flag = 1;
                        break;
                    }
                    m++;
                }
                if (flag == 0)
                    num.Add(-1);
            }
            return num.ToArray();
        }

        //1441. 用栈操作构建数组
        public IList<string> BuildArray(int[] target, int n)
        {
            List<string> li = new List<string>();
            List<int> hs = new List<int>();
            hs = target.ToList();
            if (target[0] == 1)
            {
                li.Add("Push");
            }
            if (target[0] != 1)
            {
                for (int m = 0; m < target[0] - 1; m++)
                {
                    li.Add("Push");
                    li.Add("Pop");
                }
                li.Add("Push");
            }
            hs.Remove(target[0]);
            for (int j = 1; j < target.Length - 1; j++)
            {
                if (target[j + 1] - target[j] == 1)
                {
                    li.Add("Push");
                }
                else
                {
                    for (int m = 0; m < target[j + 1] - target[j] - 1; m++)
                    {
                        li.Add("Push");
                        li.Add("Pop");
                    }
                    li.Add("Push");
                }
            }
            return li;
        }

        //155. 最小栈
        public void min_stack()
        {
            MinStack ms = new MinStack();
        }

        //1544. 整理字符串
        //1.不用栈
        public string MakeGood(string s)
        {
            int flag = 0;
            List<char> w = new List<char>();
            StringBuilder sb = new StringBuilder();
            w = s.ToList();
            for(int i=0;i<w.Count-1;i++)
            {
                if (Math.Abs(w[i + 1] - w[i]) == 32)
                {
                    w.RemoveAt(i);
                    w.RemoveAt(i);
                    flag = 1;
                }
            }
            for (int i = 0; i < w.Count; i++)
            {
                sb.Append(w[i]);
            }
            if(sb.ToString()==""||sb.Length==1)
            {
                return sb.ToString();
            }
            if (flag == 1)
            {
                return MakeGood(sb.ToString());
            }
            return sb.ToString();
        }
        //2.用栈
        public string MakeGood2(string s)
        {
            char m = ' ';
            Stack st = new Stack();
            StringBuilder sb = new StringBuilder();
            char[] ch = s.ToArray();
            st.Push(ch[0]);
            for (int i = 1; i < ch.Length; i++)
            {
                char.TryParse(st.Peek().ToString(), out m);
                if (Math.Abs(ch[i] - m) == 32)
                {
                    st.Pop();
                }
                else
                {
                    st.Push(ch[i]);
                }
            }
            for(int i=0;i<st.Count;i++)
            {
                sb.Append(st.Pop());
            }
            string str = sb.ToString();
            return str.Reverse().ToString();
            //未完成
        }


        //1464. 数组中两元素的最大乘积
        public int MaxProduct(int[] nums)
        {
            int op = 0;
            for(int i=0;i<nums.Length-1;i++)
            {
                for(int j=0;j<nums.Length-i-1;j++)
                {
                    if(nums[j]>nums[j+1])
                    {
                        op = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = op;
                    }
                }
            }
            return (nums[nums.Length-1]-1)*(nums[nums.Length-2]-1);
        }

        //1816. 截断句子
        public string TruncateSentence(string s, int k)
        {
            string[] str = s.Split(' ');
            StringBuilder sb = new StringBuilder();
            sb.Append(str[0]);
            for(int i=1;i<k;i++)
            {
                sb.Append(" " + str[i]);
            }

            return sb.ToString();
        }

        //645. 错误的集合
        public int[] FindErrorNums(int[] nums)
        {
            Array.Sort(nums);
            int[] num=new int[2] ;
            num[0] = nums[0];
            num[1] = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if(nums[i]-nums[i-1]==0)
                {
                    num[0] = nums[i];
                }
                else if (nums[i] > nums[i - 1] + 1)
                {
                    num[1] = nums[i - 1] + 1;
                }
            }
            num[1] = nums[nums.Length - 1] != nums.Length ? nums.Length : num[1];
            return num;
        }

        //521. 最长特殊序列 Ⅰ
        public int FindLUSlength(string a, string b)
        {
            int num = 0;
            if (a.Length != b.Length)
                num = Math.Max(a.Length, b.Length);
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        num = a.Length;
                        break;
                    }
                    else
                        num = -1;
                }
            }
            return num;
        }

        //292. Nim 游戏
        public bool CanWinNim(int n)
        {
            return n % 4 != 0;
        }

        //709. 转换成小写字母
        public string ToLowerCase(string s)
        {
            return s.ToLower();
        }

        //1323. 6 和 9 组成的最大数字
        public int Maximum69Number(int num)
        {
            char[] ch = num.ToString().ToCharArray();
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<ch.Length;i++)
            {
                if(ch[i]=='6')
                {
                    ch[i] = '9';
                    break;
                }
            }
            for(int i=0;i<ch.Length;i++)
            {
                sb.Append(ch[i]);
            }
            return int.Parse(sb.ToString());
        }

        //344. 反转字符串
        public void ReverseString(char[] s)
        {
            Array.Reverse(s);
        }

        //1252. 奇数值单元格的数目(未完成)
        public int OddCells(int m, int n, int[][] indices)
        {

            return 0;
        }

        //1232. 缀点成线
        public bool CheckStraightLine(int[][] coordinates)
        {
            int x0 = coordinates[0][0];
            int y0 = coordinates[0][1];
            int x = coordinates[1][0] - x0;
            int y = coordinates[1][1] - y0;
            for(int i=2;i<coordinates.Length;i++)
            {
                int x1 = coordinates[i][0] - x0;
                int y1 = coordinates[i][1] - y0;
                if(x1*y!=x*y1)
                {
                    return false;
                }
            }
            return true;
        }

        //1047. 删除字符串中的所有相邻重复项
        public string RemoveDuplicates(string s)
        {
            int flag = 0;
            List<char> w = new List<char>();
            StringBuilder sb = new StringBuilder();
            w = s.ToList();
            for (int i = 0; i < w.Count - 1; i++)
            {
                if (w[i + 1] - w[i] == 0)
                {
                    w.RemoveAt(i);
                    w.RemoveAt(i);
                    flag = 1;
                }
            }
            for (int i = 0; i < w.Count; i++)
            {
                sb.Append(w[i]);
            }
            if (sb.ToString() == "" || sb.Length == 1)
            {
                return sb.ToString();
            }
            if (flag == 1)
            {
                return RemoveDuplicates(sb.ToString());
            }
            return sb.ToString();
        }

        //1374. 生成每种字符都是奇数个的字符串
        public string GenerateTheString(int n)
        {
            StringBuilder sb = new StringBuilder();
            if (n % 2 == 0)
            {
                for (int i = 0; i < n - 1; i++)
                {
                    sb.Append('a');
                }
                sb.Append('b');
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    sb.Append('a');
                }
            }
            return sb.ToString();
        }

        //1859. 将句子排序
        public string SortSentence(string s)
        {
            string val = "";
            string[] m = s.Split(' ');
            StringBuilder sb = new StringBuilder();
            Dictionary<int, string> dc = new Dictionary<int, string>();
            for(int i=0;i<m.Length;i++)
            {
                dc.Add(int.Parse(m[i][m[i].Length - 1].ToString()), m[i].Substring(0, m[i].Length - 1));
            }
            for (int i = 1; i <= m.Length; i++)
            {
                dc.TryGetValue(i,out val);
                sb.Append(val + " ");
            }
            return sb.ToString().Substring(0,sb.Length-1);
        }

        //面试题 08.06. 汉诺塔问题(未完成)
        public void Hanota(IList<int> A, IList<int> B, IList<int> C)
        {
            
        }

        //771. 宝石与石头
        public int NumJewelsInStones(string jewels, string stones)
        {
            int sum = 0;
            Hashtable hs = new Hashtable();
            for (int i = 0; i < jewels.Length; i++)
            {
                hs.Add(jewels[i], i);
            }
            for(int i = 0; i < stones.Length; i++)
            {
                if(hs.Contains(stones[i]))
                {
                    sum += 1;
                }
            }
            return sum;
        }

        //1431. 拥有最多糖果的孩子
        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            int temp = 0;
            IList<bool> il = new List<bool>();
            List<int> cand = new List<int>();
            for(int i=0;i<candies.Length;i++)
            {
                cand.Add(candies[i]);
            }
            for(int i=0;i<candies.Length;i++)
            {
                if (candies[i] > temp)
                    temp = candies[i];
            }
            for (int i = 0; i < candies.Length; i++)
            {
                if (candies[i] + extraCandies >= temp)
                    il.Add(true);
                else
                    il.Add(false);
            }
            return il;
        }

        //LCP 06. 拿硬币
        public int MinCount(int[] coins)
        {
            int sum = 0;
            for(int i=0;i<coins.Length;i++)
            {
                sum += (coins[i] + 1) / 2;
            }
            return sum;
        }

        //LCP 29. 乐团站位
        public int OrchestraLayout(int num, int xPos, int yPos)
        {

            return 0;
        }

        //665. 非递减数列
        public bool CheckPossibility(int[] nums)
        {

            return true;
        }

        //414. 第三大的数
        public int ThirdMax(int[] nums)
        {
            int op = 0;
            nums = nums.GroupBy(p => p).Select(p => p.Key).ToArray();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = 0; j < nums.Length - i - 1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        op = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = op;
                    }
                }
            }
            if (nums.Length < 3)
                return nums[nums.Length - 1];
            else
                return nums[nums.Length - 3];
        }

        //747. 至少是其他数字两倍的最大数
        public int DominantIndex(int[] nums)
        {
            int one=0, two = 0;
            int maxindex = 0;
            for(int i = 0; i < nums.Length ; i++)
            {
                if (one < nums[i])
                {
                    one = nums[i];
                    maxindex = i;
                }
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (i != maxindex)
                {
                    if (two < nums[i])
                        two = nums[i];
                }
            }
            if (nums.Length < 2) return 0;
            if (one >= two * 2) return maxindex;
            else return -1;
        }

        //507. 完美数
        public bool CheckPerfectNumber(int num)
        {
            int sum = 0;
            if (num == 1) return false;
            for(int i=1;i<Math.Sqrt(num);i++)
            {
                if (i == 1||i * i == num)
                {
                    sum += i;
                    continue;
                }
                if (num % i == 0)
                    sum += (i + (num / i));
            }
            if (sum == num) return true;
            else return false;
        }

        //1700. 无法吃午餐的学生数量
        public int CountStudents(int[] students, int[] sandwiches)
        {
            Stack st = new Stack();
            Queue qu = new Queue();
            for (int i = 0; i < students.Length; i++)
            {
                qu.Enqueue(students[i]);
            }
            for(int i=sandwiches.Length-1;i>-1;i--)
            {
                st.Push(sandwiches[i]);
            }
            while (qu.Contains(st.Peek()))
            {
                int m = int.Parse(qu.Dequeue().ToString());
                if (m == int.Parse(st.Peek().ToString()))
                {
                    st.Pop();
                }
                else
                {
                    qu.Enqueue(m);
                }
                if (st.Count == 0)
                    break;
            }
            return qu.Count;
        }

        //605. 种花问题
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            int flag = 0;
            List<int> ls = new List<int>();
            ls.Add(0);
            for (int i = 0; i < flowerbed.Length; i++)
            {
                ls.Add(flowerbed[i]);
            }
            ls.Add(0);
            for (int i = 1; i < ls.Count - 1; i++)
            {
                if (ls[i - 1] == 0 && ls[i + 1] == 0 && ls[i] != 1)
                {
                    ls[i] = 1;
                    flag += 1;
                }
            }
            if (n <= flag) return true;
            else return false;
        }

        //1013. 将数组分成和相等的三个部分
        public bool CanThreePartsEqualSum(int[] arr)
        {
            int sum1 = 0, sum2 = 0, sum3 = 0;
            int n = arr.Sum();
            if (n % 3 != 0) return false;
            int m = n / 3;
            int i = 0, j = 0, h = 0;
            for(int k=0; ;k++)
            {
                if(k==arr.Length)
                {
                    return false;
                }
                sum1 += arr[k];
                if (sum1 == m)
                {
                    i = k;
                    break;
                }
            }
            for (int k = i + 1; ; k++)
            {
                if (k == arr.Length)
                {
                    return false;
                }
                sum2 += arr[k];
                if (sum2 == m)
                {
                    j = k;
                    break;
                }
            }
            for (int k = j + 1; ; k++)
            {
                if (k == arr.Length)
                {
                    return false;
                }
                sum3 += arr[k];
                if (sum3 == m)
                {
                    h = k;
                    break;
                }
            }
            return true;
        }

        //1365. 有多少小于当前数字的数字
        public int[] SmallerNumbersThanCurrent(int[] nums)
        {
            int[] num = new int[nums.Length];
            for(int i=0;i<nums.Length;i++)
            {
                int no = 0;
                for (int j = 0; j < nums.Length; j++)
                {
                    if (nums[i] > nums[j])
                        no += 1;
                }
                num[i] = no;
            }
            return num;
        }

        //1512. 好数对的数目
        public int NumIdenticalPairs(int[] nums)
        {
            int no = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < nums.Length; j++)
                {
                    if (nums[i] == nums[j] && i < j)
                        no += 1;
                }
            }
            return no;
        }

        //1672. 最富有客户的资产总量
        public int MaximumWealth(int[][] accounts)
        {
            int[] num = new int[accounts.Length];
            for(int i=0;i<accounts.Length;i++)
            {
                int sum = 0;
                for(int j=0;j<accounts[i].Length;j++)
                {
                    sum += accounts[i][j];
                }
                num[i] = sum;
            }
            return num.Max();
        }

        //739. 每日温度 中等
        public int[] DailyTemperatures(int[] temperatures)
        {
            int[] num = new int[temperatures.Length];
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < temperatures.Length; i++)
            {
                if (st.Count == 0)
                    st.Push(i);
                else
                {
                    while (st.Count != 0 && temperatures[i] > temperatures[st.Peek()])
                    {
                        int m = st.Peek();
                        st.Pop();
                        num[m] = i - m;
                    }
                    st.Push(i);
                }
            }
            return num;
        }

        //735. 行星碰撞 中等(未提交)
        public int[] AsteroidCollision(int[] asteroids)
        {
            Stack<int> st = new Stack<int>();
            for(int i=0;i<asteroids.Length;i++)
            {
                if (st.Count == 0)
                    st.Push(asteroids[i]);
                else
                {
                    if(asteroids[i]<0)
                    {
                        
                    }
                }
            }
            return asteroids;
        }

    }
}

