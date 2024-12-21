using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace aisd;

/// <summary>
/// Задачи курса Алгоритмы: теория и практика. Методы (https://stepik.org/course/217)
/// </summary>
public static class Tasks
{
    /// <summary>
    /// Возвращает НОД использую метод Евклида.
    /// </summary>
    /// <returns>НОД.</returns>
    /// <remarks>
    /// По данным двум числам 1 ≤ a,b ≤ 2⋅10^9 1 ≤ a,b ≤ 2⋅10^9 найдите их наибольший общий делитель.
    /// Sample 1
    ///     Sample Input 1:
    ///     18 35
    ///     Sample Output 1:
    ///     1
    /// Sample 2
    ///     Sample Input 2:
    ///     14159572 63967072
    ///     Sample Output 2:
    ///     4
    /// </remarks>
    public static int GetGCDByEuclidMethod(int first, int second)
    {
        if (first == second)
        {
            return first;
        }
        
        if (first == 0)
        {
            return second;
        }

        if (second == 0)
        {
            return first;
        }

        var max = Math.Max(first, second);
        var min = Math.Min(first, second);
        
        return GetGCDByEuclidMethod(max % min, min);
    }

    /// <summary>
    /// Покрыть отрезки точками.
    /// </summary>
    /// <returns></returns>
    /// <remarks>Надежный шаг - найти самую правую самого левого отрезка.</remarks>
    public static int[] GetPointsForLines(Tuple<int, int>[] lines)
    {
        var sortedLines = lines.OrderBy(x => x.Item2).ToArray();
        var points = new List<int>
        {
            sortedLines[0].Item2
        };

        var currentPoint = 0;

        for (var i = 1; i < sortedLines.Length; i++)
        {
            if (points[currentPoint] >= sortedLines[i].Item2)
            {
                continue;
            }

            if (points[currentPoint] >= sortedLines[i].Item1)
            {
                continue;
            }

            points.Add(sortedLines[i].Item2);
            currentPoint++;
        }

        return points.ToArray();
    }

    /// <summary>
    /// Непрерывный рюкзак.
    /// </summary>
    /// <remarks>Надежный шаг - начать заполнять рюкзак самый дорогим.</remarks>
    public static void FillBackpack()
    {
        var backpackAndItemsInfo = Console.ReadLine().Split(' ');
        var itemsCount = Convert.ToInt32(backpackAndItemsInfo[0]);
        var backpackCapacity = Convert.ToInt32(backpackAndItemsInfo[1]);
        var backpackCost = 0.0;
        var items = new Tuple<int, int, double>[itemsCount];

        for (var i = 0; i < itemsCount; i++)
        {
            var costAndVolume = Console.ReadLine().Split(' ');
            var cost = Convert.ToInt32(costAndVolume[0]);
            var volume = Convert.ToInt32(costAndVolume[1]);
            items[i] = new Tuple<int, int, double>(
                cost,
                volume,
                (double)cost / (double)volume);
        }

        items = items.OrderByDescending(x => x.Item3).ToArray(); 

        for (var i = 0; i < items.Length; i++)
        {
            if (backpackCapacity == 0)
            {
                break;
            }

            if (items[i].Item2 <= backpackCapacity)
            {
                backpackCost += items[i].Item1;
                backpackCapacity -= items[i].Item2;
                continue;
            }

            if (items[i].Item2 > backpackCapacity)
            {
                backpackCost += backpackCapacity * items[i].Item3;
                backpackCapacity = 0;
                break;
            }
        }

        Console.WriteLine(string.Format("{0:0.###}", backpackCost));
    }

    public static void Test()
    {
        var n = Convert.ToInt32(Console.ReadLine());

        if (n < 3)
        {
            Console.WriteLine(1);
            Console.Write(n);
            return;
        }

        var nums = new List<int>();
        var i = 1;

        while (n > 0)
        {
            if (i <= n)
            {
                nums.Add(i);
                n -= i;
                i++;
                continue;
            }
            else 
            {
                nums[^1] = nums[^1] + n;
                break;
            }
        }

        Console.WriteLine(nums.Count);
        Console.WriteLine(string.Join(" ", nums));
    }

    #region Коды Хаффмана
    /*
    # Кодирование Хаффмана

    По данной непустой строке `s` длины не более `10^4`, состоящей из строчных букв латинского алфавита,
    постройте оптимальный беспрефиксный код. В первой строке выведите количество различных букв `k`, 
    встречающихся в строке, и размер получившейся закодированной строки. В следующих `k` строках запишите 
    коды букв в формате `letter: code`. В последней строке выведите закодированную строку.
    
    ## Input
    > abacabad

    ## Output
    > 4 14
    > a: 0
    > b: 10
    > c: 110
    > d: 111
    > 01001100100111

    # Заметки

    Код Хаффмана неоднородный, могут быть разные варианты, все зависит от того, как строится дерево и 
    как происходит сортировка в очереди.

    Очередь с приоритетом не подразумевает какой-либо сортировки по значениям, исключительно по приоритету.
    */
    public class Tree
    {
        public Tree Left { get; set; }

        public Tree Right { get; set; }

        public char Value { get; set; }

        public string Code { get; set; }
    }

    public static void TraverseTreeInOrder(Tree tree, string path, Dictionary<char, string> map)
    {
        if (tree == null)
        {
            return;
        }

        TraverseTreeInOrder(tree.Left, path + '0', map);

        if (tree.Value != char.MinValue)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "0";
            }
            map.Add(tree.Value, path);
        }

        TraverseTreeInOrder(tree.Right, path + '1', map);
    }

    public static void HuffmanCoding()
    {
        var input = Console.ReadLine();

        var map = new Dictionary<char, int>();
        foreach (var ch in input)
        {
            if (map.TryGetValue(ch, out var _))
            {
                map[ch]++;
            }
            else 
            {
                map.Add(ch, 1);
            }
        }

        var queue = new PriorityQueue<Tree, int>();
        foreach(var m in map)
        {
            var tree = new Tree
            {
                Value = m.Key
            };

            queue.Enqueue(tree, m.Value);
        }

        while(queue.Count > 1)
        {
            queue.TryDequeue(out var rEl, out var rPr);
            queue.TryDequeue(out var lEl, out var lPr);

            queue.Enqueue(new Tree
            {
                Left = lEl,
                Right = rEl,
            },
            rPr + lPr);
        }

        var cur = queue.Peek();
        var newMap = new Dictionary<char, string>();

        TraverseTreeInOrder(cur, string.Empty, newMap);

        var output = "";

        foreach(var ch in input)
        {
            output += newMap[ch];
        }

        Console.WriteLine($"{newMap.Count} {output.Length}");
        foreach(var m in newMap)
        {
            Console.WriteLine($"{m.Key}: {m.Value}");
        }
        Console.WriteLine(output);
    }

    public static int GetPriority(string code, int count)
    {
        if (string.IsNullOrEmpty(code))
            return int.MaxValue;

        var countMinusLen = count - code.Length;

        var sum = code
            .Reverse()
            .Select((x, i) => (int)(System.Math.Pow(2, i) * char.GetNumericValue(x)))
            .Sum();
        return (int)System.Math.Pow(countMinusLen, 3) + sum;
    }

    public static void HuffmanDecodingWithTree()
    {
        var input = Console.ReadLine().Split(' ');
        var chCount = Convert.ToInt32(input[0]);
        
        var q = new PriorityQueue<Tree, int>();

        for(var i = 0; i < chCount; i++)
        {
            var tmp = Console.ReadLine().Split(' ');

            q.Enqueue(
                new Tree
                {
                    Code = tmp[1],
                    Value = Convert.ToChar(tmp[0].TrimEnd(':'))
                },
                GetPriority(tmp[1], chCount));
        }

        while(q.Count > 1)
        {
            q.TryDequeue(out var l, out var lPr);
            q.TryDequeue(out var r, out var rPr);

            var t = new Tree
            {
                Right = r,
                Left = l,
                Code = l.Code.Remove(l.Code.Length - 1),
            };

            q.Enqueue(t, GetPriority(t.Code, chCount));
        }

        var head = q.Dequeue();
        var detachedHead = head;

        foreach(var el in Console.ReadLine())
        {
            if (el == '0')
            {
                detachedHead = detachedHead.Left;
            }
            else
            {
                detachedHead = detachedHead.Right;
            }

            if (detachedHead == null)
            {
                detachedHead = head;
            }
            
            if (detachedHead.Value != char.MinValue)
            {
                Console.Write(detachedHead.Value);
                detachedHead = head;
            }
        }
    }

    public static void HuffmanDecodingNaive()
    {
        var input = Console.ReadLine().Split(' ');

        var chCount = Convert.ToInt32(input[0]);
        var map = new Dictionary<string, char>();
        for(var i = 0; i < chCount; i++)
        {
            var tmp = Console.ReadLine().Split(' ');
            map[tmp[1]] = char.Parse(tmp[0].TrimEnd(':'));
        }

        var buf = string.Empty;
        var output = new StringBuilder();
        foreach(var ch in Console.ReadLine())
        {
            buf += ch;

            if (map.TryGetValue(buf, out var value))
            {
                output.Append(value);
                buf = string.Empty;
            }
        }

        Console.WriteLine(output);
    }
    #endregion

    #region Очередь с приоритетами
    class PriorityQueueOnArray
    {
        private readonly int[] _storage;

        private readonly bool _isMax;

        private int _lastLeaf = -1;

        public PriorityQueueOnArray(int capacity, bool isMax)
        {
            _storage = new int[capacity];
            _isMax = isMax;
        }

        public int Extract()
        {
            var extracted = _storage[0];

            _storage[0] = _storage[_lastLeaf];
            _storage[_lastLeaf] = 0;

            _lastLeaf--;
            
            var curr = 0;
            var child1 = (curr * 2) + 1;
            var child2 = (curr * 2) + 2;
            while (_storage[curr] < _storage[child1] || _storage[curr] < _storage[child2])
            {
                if (_storage[child1] >= _storage[child2])
                {
                    var t = _storage[curr];
                    _storage[curr] = _storage[child1];
                    _storage[child1] = t;

                    curr = child1;
                    child1 = (curr * 2) + 1;
                    child2 = (curr * 2) + 2;
                }
                else if (_storage[child1] < _storage[child2])
                {
                    var t = _storage[curr];
                    _storage[curr] = _storage[child2];
                    _storage[child2] = t;

                    curr = child2;
                    child1 = (curr * 2) + 1;
                    child2 = (curr * 2) + 2;
                }
                else
                {
                    break;
                }
            }

            return extracted;
        }

        public void Insert(int value)
        {
            // SiftUp
            _lastLeaf++;
            _storage[_lastLeaf] = value;

            if (_lastLeaf == 0)
            {
                return;
            }

            var curr = _lastLeaf;
            var parent = (int)Math.Floor((curr - 1) / 2f);
            while (_storage[parent] < _storage[curr] && curr > 0)
            {
                var t = _storage[parent];
                _storage[parent] = _storage[curr];
                _storage[curr] = t;

                curr = parent;
                parent = (curr - 1) / 2;
            }
        }
    }

    public static void TestPriorityQueue()
    {
        var operations = Convert.ToInt32(Console.ReadLine());
        var myQueue = new PriorityQueueOnArray(operations, true);

        for (var i = 0; i < operations; i++)
        {
            var command = Console.ReadLine().Split(' ');

            if (command[0] == "ExtractMax" || command[0] == "e")
            {
                Console.WriteLine(myQueue.Extract());
                continue;
            }
            else
            {
                myQueue.Insert(Convert.ToInt32(command[1]));
                continue;
            }
        }
    }
    #endregion

    #region Двоичный поиск
    public static int BinaryFind(int [] nums, int s)
    {
        var l = 0;
        var r = nums.Length - 1;

        // TODO: Важно!!!!!!!!!!!
        // Потому что искомое значение может быть в самом конце, когда l = r.
        while (l <= r)
        {
            var m = (l + r) / 2;
            var guess = nums[m];

            if (guess == s)
            {
                return m + 1;
            }
            else if (guess > s)
            {
                r = m - 1;
            }
            else
            {
                l = m + 1;
            }
        }

        return -1;
    }

    public static void BinSearchTest()
    {
        var nums = Console.ReadLine().Split(" ").Skip(1).Select(s => Convert.ToInt32(s)).ToArray();
        var search = Console.ReadLine().Split(" ").Skip(1).Select(s => Convert.ToInt32(s));

        foreach(var s in search)
        {
            Console.Write(BinaryFind(nums, s));
            Console.Write(" ");
        }
    }
    #endregion

    #region MergeSort
    public static void MergeSortIterative()
    {
        var n = Convert.ToInt32(Console.ReadLine());
        var nums = Console.ReadLine()
            .Split(" ")
            .Select(s => new [] { Convert.ToInt32(s) });

        if (n <= 1)
        {
            Console.WriteLine(0);
            return;
        }

        var q = new Queue<int[]>(nums);
        var p = Math.Ceiling(Math.Log2(q.Count));
        var t = (int)Math.Pow(2, p);

        for (var i = q.Count; i < t; i++)
        {
            q.Enqueue(new int[] { int.MaxValue });
        }

        while (q.Count > 1)
        {
            var nums1 = q.Dequeue();
            var nums2 = q.Dequeue();

            var result = Merge(nums1, nums2);
            q.Enqueue(result);
        }

        Console.WriteLine(_counter);
    }

    public static void MergeSortRecursive()
    {
        var n = Convert.ToInt32(Console.ReadLine());
        var nums = Console.ReadLine()
            .Split(" ")
            .Select(s => Convert.ToInt32(s));

        if (n == 1)
        {
            Console.WriteLine(0);
            return;
        }

        var result = MergeSort(nums.ToArray());
        Console.WriteLine(_counter);
    }

    private static int[] MergeSort(int[] nums)
    {
        if (nums.Length == 1)
        {
            return nums;
        }

        var m = (nums.Length + 1) / 2;
        var numsLeft = nums.Take(m).ToArray();
        var numsRight = nums.Skip(m).Take(nums.Length - m).ToArray();

        return Merge(MergeSort(numsLeft), MergeSort(numsRight));
    }

    private static int _counter = 0;

    private static int[] Merge(int[] nums1, int[] nums2)
    {
        var p1 = 0;
        var p2 = 0;
        var curr = 0;

        var result = new int[nums1.Length + nums2.Length];

        while (curr < result.Length)
        {
            if (p1 >= nums1.Length)
            {
                result[curr] = nums2[p2];
                p2++;
            }
            else if (p2 >= nums2.Length)
            {
                result[curr] = nums1[p1];
                p1++;
            }
            else if (nums1[p1] > nums2[p2])
            {
                result[curr] = nums2[p2];
                _counter += nums1.Length - p1;
                p2++;
            }
            else
            {
                result[curr] = nums1[p1];
                p1++;
            }
            
            curr++;
        }

        return result;
    }
    #endregion

    #region quicksort
    public static void DotsAndSegments()
    {
        var builder = new StringBuilder();
        var n = int.Parse(Console.ReadLine().Split(" ")[0]);

        var start = new int[n];
        var end = new int[n];

        for (var i = 0; i < n; i++)
        {
            var @in = Console.ReadLine().Split(" ");
            start[i] = int.Parse(@in[0]);
            end[i] = int.Parse(@in[1]);
        }

        var dots = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        QuickSort(start, 0, start.Length - 1);
        QuickSort(end, 0, end.Length - 1);
        
        for (var i = 0; i < dots.Length; i++)
        {
            var startOccurs = BinaryFindOccurence(start, dots[i], true);
            var endOccurs = BinaryFindOccurence(end, dots[i], false);

            builder
                .Append(startOccurs - endOccurs)
                .Append(' ');
        }
        
        Console.Write(builder.ToString());
    }

    private static int BinaryFindOccurence(
        int[] nums, 
        int numToFind, 
        bool needFindLastElseFirst)
    {
        var result = 0;
        
        var l = 0;
        var r = nums.Length - 1;

        while (l <= r)
        {
            var m = (l + r) / 2;
            var guess = nums[m];

            if (guess < numToFind)
            {
                l = m + 1;
                result = l;
            }
            else if (guess > numToFind)
            {
                r = m - 1;
            }
            else
            {
                if (needFindLastElseFirst)
                {
                    l = m + 1;
                    result = l;
                }
                else
                {
                    r = m - 1;
                }
            }
        }

        return result;
    }

    private static void QuickSort(int[] nums, int l, int r)
    {
        if (l >= r)
        {
            return;
        }

        // Это важно! Так как метод рекурсивный, то элемент должен браться из отрезка,
        // с которым сейчас работаем, а не из всего массива.
        var mValue = nums[Random.Shared.Next(l, r)];

        var left = l;
        var right = r;

        while (l <= r)
        {
            while (nums[l] < mValue) l++;
            while (nums[r] > mValue) r--;

            if (l <= r)
            {
                (nums[r], nums[l]) = (nums[l++], nums[r--]);
            }
        }

        QuickSort(nums, left, r);
        QuickSort(nums, l, right);
    }
    #endregion

    #region counting sort...
    public static void CountingSort()
    {
        Console.ReadLine();
        var nums = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        var sums = new int[11];

        for (var i = 0; i < nums.Length; i++)
        {
            sums[nums[i]]++;
        }

        for (var i = 1; i < sums.Length; i++)
        {
            sums[i] += sums[i - 1];
        }

        var result = new int[nums.Length];

        for (var i = nums.Length - 1; i >= 0; i--)
        {
            result[sums[nums[i]] - 1] = nums[i];
            sums[nums[i]]--;
        }

        Console.WriteLine(string.Join(" ", result));
    }
    #endregion

    #region наибольшая последовательность
    public static void НаибольшаяПоследовательнократнаяПодпоследовательность()
    {
        Console.ReadLine();
        var nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        var result = new int[nums.Length];
        var ans = 1;
        for (var i = 0; i < nums.Length; i++)
        {
            result[i] = 1;
            for (var j = 0; j <= i - 1; j++)
            {
                if (nums[i] % nums[j] == 0 && result[j] + 1 > result[i])
                {
                    result[i]++;
                    ans = Math.Max(ans, result[i]);
                }
            }
        }

        Console.WriteLine(ans);
    }

    // Очень сложно!!!
    public static void НаибольшаяНевозрастающаяПодпоследовательностьСВосстановлениемРезультата()
    {
        Console.ReadLine();
        var nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        if (nums.Length == 1)
        {
            Console.WriteLine(1);
            Console.WriteLine(nums[0]);
            return;
        }

        var subResult = new int[nums.Length + 1];
        var prev = new (int Index, int Length)[nums.Length + 1];

        subResult[0] = int.MaxValue;

        for (var i = 1; i < subResult.Length; i++)
        {
            subResult[i] = int.MinValue;
        }
        var ans = int.MinValue;
        for (var i = 0; i < nums.Length; i++)
        {
            var j = FindLowerBound(subResult, nums[i]);
            
            if (subResult[j - 1] >= nums[i] && nums[i] >= subResult[j])
            {
                subResult[j] = nums[i];
                ans = Math.Max(j, ans);
                prev[i] = (i, j);
            }
        }

        Console.WriteLine(ans);

        var result = new int[ans];

        for (var i = prev.Length - 1; i >= 0; i--)
        {
            if (prev[i].Length == ans)
            {
                result[--ans] = ++prev[i].Index;
            }
        }

        Console.WriteLine(string.Join(" ", result));

        return;

        static int FindLowerBound(int[] nums,  int num)
        {
            var l = 0;
            var r = nums.Length - 1;
            var res = r;

            while (l <= r)
            {
                var m = (l + r) / 2;
                var guess = nums[m];

                if (guess >= num)
                {
                    l = m + 1;
                    res = l;
                }
                else
                {
                    r = m - 1;
                }
            }

            return res;
        }
    }
    #endregion

    #region test contest
    public static void GoldAndStones()
    {
        var gold = Console.ReadLine();
        var stones = Console.ReadLine();
        
        var set = new HashSet<char>();

        for (var i = 0; i < gold.Length; i++)
        {
            if (!set.Contains(gold[i]))
            {
                set.Add(gold[i]);
            }
        }
        var counter = 0;
        for (var i = 0; i < stones.Length; i++)
        {
            if (set.Contains(stones[i]))
            {
                counter++;
            }
        }

        Console.WriteLine(counter);
    }

    public static void AlarmClocks()
    {
        var info = Console.ReadLine().Split(" ").Select(s => int.Parse(s)).ToArray();
        
        var count = info[0];
        var period = info[1];
        var countToWake = info[2];

        var alarmClocks = Console.ReadLine().Split(" ").Select(s => int.Parse(s)).ToArray();
        
        var queue = new PriorityQueue<int, int>();
        var set = new HashSet<int>();

        for (var i = 0; i < alarmClocks.Length; i++)
        {
            if (!set.Contains(alarmClocks[i]))
            {
                queue.Enqueue(alarmClocks[i], alarmClocks[i]);
                set.Add(alarmClocks[i]);
            }
        }

        while (countToWake > 1)
        {
            var next = queue.Dequeue() + period;

            if (!set.Contains(next))
            {
                queue.Enqueue(next, next);
                set.Add(next);
            }

            countToWake--;
        }

        Console.WriteLine(queue.Dequeue());
    }
    #endregion
}
