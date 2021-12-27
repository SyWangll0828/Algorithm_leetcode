using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Problems problems = new Problems();
            //实例化可以访问类成员;直接访问需要添加static
            Common.Case testCase = new Common.Case();

            //int[,] f = new int[3, 7];
            //List<string> wordList = new List<string> { "hot", "dot", "dog", "lot", "log", "cog" };
            //problems.LadderLength("hit", "cog", wordList);
            problems.SnakesAndLadders(testCase.twoArrayOne);
            Console.ReadKey();
        }
    }

    class Problems
    {
        // 处理边权为 1 的最短路问题
        #region 广度优先搜索 BFS
        // 无向图中两个顶点之间的最短路径的长度，可以通过广度优先遍历得到；
        // 为什么 BFS 得到的路径最短？可以把起点和终点所在的路径拉直来看，两点之间线段最短；
        // 已知目标顶点的情况下，可以分别从起点和目标顶点（终点）执行广度优先遍历，直到遍历的部分有交集，这是双向广度优先遍历的思想。
        // 127. 单词接龙
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            # region 双向BFS
            HashSet<string> wordsSet = new HashSet<string>(wordList);
            if (!wordsSet.Contains(endWord)) return 0;
            wordsSet.Remove(beginWord);
            //定义双向队列和哈希表
            Queue<string> beginQueue = new Queue<string>();
            Queue<string> endQueue = new Queue<string>();
            beginQueue.Enqueue(beginWord);
            endQueue.Enqueue(endWord);
            Dictionary<string, int> beginVisitedSet = new Dictionary<string, int>();
            Dictionary<string, int> endVisitedSet = new Dictionary<string, int>();
            beginVisitedSet.Add(beginWord, 1);
            endVisitedSet.Add(endWord, 1);

            //如果其中一个队列空了，搜索结束
            while (beginQueue.Any() && endQueue.Any())
            {
                int res = -1;
                if (beginQueue.Count <= endQueue.Count)
                    res = bfs(beginQueue, beginVisitedSet, endVisitedSet);
                else
                    res = bfs(endQueue, endVisitedSet, beginVisitedSet);
                if (res != -1) return res;
            }
            return 0;

            int bfs(Queue<string> queue, Dictionary<string, int> begin, Dictionary<string, int> end)
            {
                //获取要替换的源字符串
                string currStr = queue.Dequeue();
                int len = currStr.Length;
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        string nextWord = currStr.Substring(0, i) + (char)('a' + j) + currStr.Substring(i + 1);
                        if (wordsSet.Contains(nextWord))
                        {
                            //如果该字符串在「当前方向」被记录过（拓展过），跳过即可
                            if (begin.ContainsKey(nextWord)) continue;
                            // 如果该字符串在「另一方向」出现过，说明找到了联通两个方向的最短路
                            if (end.ContainsKey(nextWord))
                                return begin[currStr] + 1 + end[nextWord];
                            //否则加入队列
                            else
                            {
                                queue.Enqueue(nextWord);
                                begin.Add(nextWord, begin[currStr] + 1);
                            }
                        }
                    }
                }
                return -1;
            }
            #endregion

            //# region 单向BFS
            ////先将words放到HashSet中
            //HashSet<string> wordsSet = new HashSet<string>(wordList);
            //if (!wordList.Contains(endWord)) return 0;
            //wordList.Remove(beginWord);
            ////图的广度优先遍历必须使用的队列和表示是否访问过的visited(数组，哈希表)
            //Queue<string> queue = new Queue<string>();
            //queue.Enqueue(beginWord);
            //HashSet<string> visitedSet = new HashSet<string>();
            //visitedSet.Add(beginWord);

            //int wordlen = beginWord.Length;
            //int step = 1;
            //while (queue.Any())
            //{
            //    int currentSize = queue.Count;
            //    //尝试对 currentWord 修改每一个字符，看看是不是能与 endWord 匹配
            //    for (int i = 0; i < currentSize; i++)
            //    {   
            //        string currentWord = queue.Dequeue();
            //        char[] charArray = currentWord.ToCharArray();
            //        for (int j = 0; j < wordlen; j++)
            //        {
            //            // 先保存，然后恢复
            //            char originChar = charArray[j];
            //            for (char k = 'a'; k <= 'z'; k++)
            //            {
            //                if (k == originChar) continue;
            //                charArray[j] = k;
            //                string nextWord = new string(charArray);
            //                if (wordsSet.Contains(nextWord))
            //                {
            //                    if (nextWord == endWord) return step + 1 ;
            //                    if (!visitedSet.Contains(nextWord))
            //                    {
            //                        queue.Enqueue(nextWord);
            //                        //添加到队列之后必须标记为已访问
            //                        visitedSet.Add(nextWord);
            //                    }
            //                }
            //            }
            //            //恢复
            //            charArray[j] = originChar;
            //        }
            //    }
            //    step++;
            //}
            //return 0;
            //#endregion
        }
        // 752. 打开转盘锁
        public int OpenLock(string[] deadends, string target)
        {
            # region 双向BFS  模板
            if (target == "0000") return 0;
            HashSet<string> deadendsSet = new HashSet<string>(deadends);
            if (deadendsSet.Contains("0000")) return -1;

            Queue<string> beginQueue = new Queue<string>();
            Queue<string> endQueue = new Queue<string>();
            beginQueue.Enqueue("0000");
            endQueue.Enqueue(target);
            Dictionary<string, int> beginVisitedSet = new Dictionary<string, int>();
            Dictionary<string, int> endVisitedSet = new Dictionary<string, int>();
            beginVisitedSet.Add("0000", 0);
            endVisitedSet.Add(target, 0);

            //如果其中一个队列空了，搜索结束
            while (beginQueue.Any() && endQueue.Any())
            {
                int res = -1;
                if (beginQueue.Count <= endQueue.Count)
                    res = bfs(beginQueue, beginVisitedSet, endVisitedSet);
                else
                    res = bfs(endQueue, endVisitedSet, beginVisitedSet);
                if (res != -1) return res;
            }
            return -1;

            int bfs(Queue<string> queue, Dictionary<string, int> begin, Dictionary<string, int> end)
            {
                string currStr = queue.Dequeue();
                char[] charArray = currStr.ToCharArray();
                int step = begin[currStr];
                // 枚举替换哪个字符
                for (int i = 0; i < 4; i++)
                {
                    // 能「正向转」也能「反向转」，这里直接枚举偏移量 [-1,1] 然后跳过 0
                    for (int j = -1; j <= 1; j++)
                    {
                        if (j == 0) continue;
                        // 求得替换字符串 str
                        int origin = charArray[i] - '0';
                        int next = (origin + j) % 10;
                        if (next == -1) next = 9;

                        char[] clone = (char[])charArray.Clone();
                        clone[i] = (char)(next + '0');
                        string str = new string(clone);

                        if (deadendsSet.Contains(str)) continue;
                        if (begin.ContainsKey(str)) continue;

                        // 如果在「另一方向」找到过，说明找到了最短路，否则加入队列
                        if (end.ContainsKey(str))
                            return step + 1 + end[str];
                        else
                        {
                            queue.Enqueue(str);
                            begin.Add(str, step + 1);
                        }
                    }
                }
                return -1;
            }
            #endregion
        }

        // 909. 蛇梯棋
        public int SnakesAndLadders(int[][] board)
        {
            //朴素BFS
            int len = board[0].Length;
            //if (board[0][0] != -1) return -1;
            int[] nums = twoForOne(board, len);
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            Dictionary<int, int> visitedSet = new Dictionary<int, int>();
            visitedSet.Add(1, 0);

            while (queue.Any())
            {
                int position = queue.Dequeue();
                int step = visitedSet[position];
                if (position == len * len) return step;
                for (int i = 1; i <= 6; i++)
                {
                    //对下一步可以走的空格进行判断
                    int nextP = position + i;
                    if (nextP <= 0 || nextP > len * len) continue;
                    //梯子移位
                    if (nums[nextP] != -1) nextP = nums[nextP];
                    //被记录过,跳过
                    if (visitedSet.ContainsKey(nextP)) continue;
                    //将移动路径记录
                    visitedSet.Add(nextP, step + 1);
                    queue.Enqueue(nextP);
                }
            }
            return -1;
            //二维数组转换成一维数组
            int[] twoForOne(int[][] twoArr, int n)
            {
                int[] newNums = new int[n * n + 1];
                int index = 0;
                for (int i = n - 1; i >= 0; i--)
                {
                    //取最下方为第一行,奇数行正向,偶数行反向
                    int row = n - i;
                    if (row % 2 == 1)
                    {
                        for (int j = 0; j < n; j++)
                            newNums[++index] = board[i][j];
                    }
                    else
                    {
                        for (int j = n - 1; j >= 0; j--)
                            newNums[++index] = board[i][j];
                    }
                }
                //foreach (var num in newNums) Console.WriteLine(num + " ");
                return newNums;
            }
        }

        // 815. 公交路线
        public int NumBusesToDestination(int[][] routes, int source, int target)
        {
            //起始时将「起点车站」所能进入的「路线」进行入队，每次从队列中取出「路线」时，查看该路线是否包含「终点车站」：
            //包含「终点车站」：返回进入该线路所花费的距离
            //不包含「终点车站」：遍历该路线所包含的车站，将由这些车站所能进入的路线，进行入队
            //一些细节：由于是求最短路，同一路线重复入队是没有意义的，因此将新路线入队前需要先判断是否曾经入队。
            //朴素BFS
            if (source == target) return 0;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            Dictionary<int, int> visitedSet = new Dictionary<int, int>();
            visitedSet.Add(source, 0);

            while (queue.Any())
            {
                int station = queue.Dequeue();
                int step = visitedSet[station];
                if (target == station) return step;
                for (int i = 1; i < 100; i++)
                {
                    //被记录过,跳过
                    if (visitedSet.ContainsKey(station)) continue;
                    //将移动路径记录
                    visitedSet.Add(station, step + 1);
                    queue.Enqueue(station);
                }
            }
            return -1;
        }

        

        #endregion

        #region 深度优先搜索 DFS
        //设计好递归函数的「入参」和「出参」
        //设置好递归函数的出口（Base Case）
        //编写「最小单元」处理逻辑

        #endregion
    }
}
