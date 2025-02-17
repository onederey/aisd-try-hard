public static class LeetCodeTasks
{
    #region 3Sum
    public static IList<IList<int>> ThreeSumWithSort(int[] nums)
    {
        IList<IList<int>> result = new List<IList<int>>();
        
        // Сортируем массив, для упрощения дальнейшей работы с 2Sum
        Array.Sort(nums);

        // -2 потому что нужно найти 3Sum, то есть к текущему элементу нужно будет найти еще два
        for (var i = 0; i < nums.Length - 2; i++)
        {
            // пропуск дублирующих триплетов - условие из задачи
            // здесь пропускаем дубли по первому числу, например
            // -1 -1 0 1
            // первый триплет -1 0 1
            // второй триплет -1 0 1 будет пропущен, благодаря этому условию
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            var target = 0 - nums[i];

            var p1 = i + 1;
            var p2 = nums.Length - 1;

            // цикл для поиска 2Sum с двумя указателями
            while (p1 < p2)
            {
                var sum = nums[p1] + nums[p2];

                if (sum == target)
                {
                    result.Add([nums[i], nums[p1], nums[p2]]);
                    p1++;

                    // здесь пропускаем дубли по второму числу, так как не выходим из цикла
                    // например в случае с -4 -1 -1 -1 0 1 2
                    // первый триплет будет -1 -1 2
                    // второй и третий триплет будут -1 -1 2 - они будут пропущены благодаря этому условию
                    //
                    // дубли по третьему числу в данном случае не интересует, потому что они и так будет уникально
                    // из-за поиска суммы и пропуска первых двух дублей
                    while (nums[p1] == nums[p1 - 1] && p1 < p2)
                    {
                        p1++;
                    }

                    continue;
                }

                // Так как у нас отсортированный массив, то выходит так, что если промежуточная сумма
                // больше целевой, значит можем уменьшить правый указатель, потому что справа числа 
                // всегда больше из-за того, что массиву отсортирован. Ну и наоборот, если меньше 
                // целевого числа, то нужно увеличить левый указатель, для увеличения суммы.
                if (sum > target)
                {
                    p2--;
                }
                else
                {
                    p1++;
                }
            }
        }

        return result;
    }

    public static IList<IList<int>> ThreeSumWithHashSet(int[] nums)
    {
        IList<IList<int>> result = new List<IList<int>>();
        var set = new HashSet<int>();

        for (var i = 0; i < nums.Length - 2; i++)
        {
            set.Clear();

            var target = 0 - nums[i];
            
            for (var j = i + 1; j < nums.Length; j++)
            {
                var newNum = target - nums[j];
                if (set.Contains(newNum))
                {
                    result.Add([nums[i], nums[j], newNum]);
                }

                set.Add(nums[j]);
            }
        }
        

        return result;
    }
    #endregion

    #region Happy Number
    public static int GetSquare(int n)
    {
        var result = 0;

        while (n > 0)
        {
            var remainder = n % 10;
            result += remainder * remainder;
            n /= 10;
        }

        return result;
    }

    public static bool HappyNumberWithSlowAndFastPointer(int n)
    {
        var slow = n;
        var fast = n;

        do
        {
            slow = GetSquare(slow);
            fast = GetSquare(GetSquare(fast));
            Console.WriteLine($"{slow} {fast}");
        } while (slow != fast);

        return slow == 1;
    }

    public static bool HappyNumberWithHashSet(int n)
    {
        var set = new HashSet<int> { n };
        
        while (true)
        {
            var currentNum = GetSquare(n);
            Console.WriteLine(currentNum);
            if (currentNum == 1)
            {
                return true;
            }

            if (set.Contains(currentNum))
            {
                return false;
            }

            set.Add(currentNum);
            n = currentNum;
        }
    }
    #endregion

    #region Max sum equals K
    public static int MaxSumEqK(int[] nums, int k)
    {
        if (k > nums.Length)
        {
            return 0;
        }

        var currentSum = 0;

        for (var i = 0; i < k; i++)
        {
            currentSum += nums[i];
        }

        var maxSum = currentSum;

        for (var i = 0; i < nums.Length - k; i++)
        {
            currentSum += nums[k + i] - nums[i];
            maxSum = Math.Max(currentSum, maxSum);
        }

        // Либо, альтернативный вариант цикла - разница только в работе с индексами
        /*
        for (var i = k; i < nums.Length; i++)
        {
            currentSum += nums[i] - nums[i - k];
            maxSum = Math.Max(currentSum, maxSum);
        }
        */

        return maxSum;
    }
    #endregion

    #region 643. Maximum Average Subarray I
    public static double FindMaxAverage(int[] nums, int k)
    {
        var n = nums.Length;

        var current = 0;

        for (var i = 0; i < k; i++)
        {
            // Каждый раз делить не надо! Можно сначала найти наибольшую сумму, а потом разделить.
            // current += (double)nums[i] / k;
            current += nums[i];
        }

        var max = current;

        for (var i = k; i < n; i++)
        {
            // Каждый раз делить не надо! Можно сначала найти наибольшую сумму, а потом разделить.
            // current += (double)(nums[i] - nums[i - k]) / k;
            current += nums[i] - nums[i - k];
            max = Math.Max(current, max);
        }

        return max / (double)k;
    }
    #endregion

    #region 443. String Compression
    public static int Compress(char[] chars)
    {
        var l = 0;
        var r = 0;

        // Указатель для заполнения новой строки внутри chars.
        var p = 0;

        while (l < chars.Length)
        {
            while (r + 1 < chars.Length && chars[r] == chars[r + 1])
            {
                r++;
            }
            
            chars[p] = chars[l];
            p++;

            var diff = r - l + 1;

            if (diff > 1)
            {
                var diffString = diff.ToString();
                for (var i = 0; i < diffString.Length; i++)
                {
                    chars[p] = diffString[i];
                    p++;
                }
            }

            r++;
            l = r;
        }

        return p;
    }
    #endregion

    #region 228. Summary Ranges
    public static IList<string> SummaryRanges(int[] nums)
    {
        var result = new List<string>();

        var l = 0;
        var r = 0;

        while (l < nums.Length)
        {
            while (r + 1 < nums.Length && nums[r] + 1 == nums[r + 1])
            {
                r++;
            }

            var windowSize = r - l + 1;
            if (windowSize > 1)
            {
                result.Add(string.Concat(nums[l], "->", nums[r]));
            }
            else 
            {
                result.Add(nums[l].ToString());
            }

            r++;
            l = r;
        }

        return result;
    }
    #endregion

    #region 121. Best Time to Buy and Sell Stock
    public static int MaxProfit(int[] prices)
    {
        var price = prices[0];
        var profit = 0;

        for (var i = 1; i < prices.Length; i++)
        {
            if (prices[i] < price)
            {
                price = prices[i];
            }
            else
            {
                profit = Math.Max(profit, prices[i] - price);
            }
        }

        return profit;
    }
    #endregion

    #region 2529. Maximum Count of Positive Integer and Negative Integer
    public static int MaximumCount(int[] nums)
    {
        // Идея решения:
        // - бинарным поиском находим первое вхождение < 0
        // - бинарным поиском находим первое вхождение > 0

        var firstMinusIndex = -1;
        var firstPlusIndex = -1;

        var l = 0;
        var r = nums.Length - 1;

        // Ищем индекс первого элемента < 0
        while (l <= r)
        {
            var m = (l + r) / 2;
            var guess = nums[m];

            if (guess < 0)
            {
                firstMinusIndex = m;
                l = m + 1;
            }
            else 
            {
                r = m - 1;
            }
        }

        l = 0;
        r = nums.Length - 1;

        // Ищем индекс первого элемента > 0
        while (l <= r)
        {
            var m = (l + r) / 2;
            var guess = nums[m];

            if (guess > 0)
            {
                firstPlusIndex = m;
                r = m - 1;
            }
            else
            {
                l = m + 1;
            }
        }

        return Math.Max(
            firstMinusIndex == -1 ? 0 : firstMinusIndex + 1,
            firstPlusIndex == -1 ? 0 : nums.Length - firstPlusIndex);
    }
    #endregion

    #region 217. Contains Duplicate
    public static bool ContainsDuplicate(int[] nums) {
        var set = new HashSet<int>();

        foreach (var num in nums)
        {
            if (set.Contains(num))
            {
                return true;
            }

            set.Add(num);
        }

        return false;
    }
    #endregion

    #region (ОТЛОЖИЛ) 371. Sum of Two Integers
    public static int GetSum(int a, int b) {
        throw new NotImplementedException("Отложил, операции с битами сейчас очень не хочется изучать...");
    }
    #endregion

    #region 100. Same Tree
    public class TreeNode
    {
        public int val;
        
        public TreeNode left;
        
        public TreeNode right;
        
        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    
    public static bool IsSameTree(TreeNode p, TreeNode q)
    {
        var queue = new Queue<TreeNode>();
        queue.Enqueue(p);
        queue.Enqueue(q);

        while (queue.Count > 0)
        {
            var currP = queue.Dequeue();
            var currL = queue.Dequeue();

            if (currP == currL)
            {
                continue;
            }

            if (currP?.val != currL?.val)
            {
                return false;
            }

            queue.Enqueue(currP.left);
            queue.Enqueue(currL.left);
            queue.Enqueue(currP.right);
            queue.Enqueue(currL.right);
        }

        return true;
    }

    public static bool IsSameTreeRecursive(TreeNode p, TreeNode q)
    {
        if (p == q)
            return true;

        if (p != null && q != null && p.val == q.val) 
            return IsSameTreeRecursive(p.left, q.left) && IsSameTreeRecursive(p.right, q.right);

        return false;
    }
    #endregion

    #region 101. Symmetric Tree
    public static bool IsSymmetric(TreeNode root)
    {
        return Check(root.left, root.right);

        static bool Check(TreeNode l, TreeNode r)
        {
            if (l == r)
            {
                return true;
            }

            if (l != null && r != null && l.val == r.val)
            {
                return Check(l.left, r.right) && Check(l.right, r.left);
            }

            return false;
        }
    }
    #endregion

    #region 102. Binary Tree Level Order Traversal
    public static IList<IList<int>> LevelOrder(TreeNode root) {
        IList<IList<int>> result = [];

        if (root?.val is not null)
        {
            result.Add([root.val]);
        }

        Traverse(root.left, root.right, result);

        return result;

        static void Traverse(TreeNode l, TreeNode r, IList<IList<int>> result)
        {
            IList<int> temp = [];
            if (l?.val is not null)
            {
                temp.Add(l.val);
            }

            if (r?.val is not null)
            {
                temp.Add(r.val);
            }

            if (temp.Count > 0)
            {
                result.Add(temp);
            }

            Traverse(l.left, r.left, result);
            Traverse(l.right, r.right, result);
        }
    }
    #endregion

    #region 200. Number of Islands
    public static int NumIslandsDFS(char[][] grid) {
        var counter = 0;

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '1')
                {
                    counter++;
                    Dfs(i, j);
                }
            }
        }

        return counter;

        void Dfs(int i, int j)
        {
            if (i < 0 || j < 0 || i > grid.Length - 1 || j > grid[i].Length - 1 || grid[i][j] != '1')
            {
                return;
            }

            grid[i][j] = '0';

            Dfs(i+1, j);
            Dfs(i, j+1);
            Dfs(i-1, j);
            Dfs(i, j-1);
        }
    }

    public static int NumIslandsBFS(char[][] grid) {
        var islands = 0;

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '1')
                {
                    islands++;
                    Bfs(i, j);
                }
            }
        }

        return islands;

        void Bfs(int i, int j)
        {
            var q = new Queue<(int i, int j)>();
            q.Enqueue((i, j));

            while (q.TryDequeue(out var el))
            {
                if (el.i < 0 || el.i >= grid.Length || el.j < 0 || el.j >= grid[el.i].Length || grid[el.i][el.j] == '0')
                {
                    continue;
                }

                 grid[el.i][el.j] = '0';

                q.Enqueue((el.i + 1, el.j));
                q.Enqueue((el.i - 1, el.j));
                
                q.Enqueue((el.i, el.j + 1));
                q.Enqueue((el.i, el.j - 1));
            }
        }
    }
    #endregion

    #region 695. Max Area of Island
    public static int MaxAreaOfIslandDFS(int[][] grid) {
        var maxArea = 0;
        var currentArea = 0;

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    DFS(i, j);
                    maxArea = Math.Max(currentArea, maxArea);
                    currentArea = 0;
                }
            }
        }

        return maxArea;

        void DFS(int i, int j)
        {
            if (i > grid.Length - 1 || i < 0 || j < 0 || j > grid[i].Length - 1 || grid[i][j] == 0)
            {
                return;
            }

            currentArea++;
            grid[i][j] = 0;

            DFS(i + 1, j);
            DFS(i - 1, j);

            DFS(i, j + 1);
            DFS(i, j - 1);
        }
    }
    #endregion

    #region 733. Flood Fill
    public static int[][] FloodFillDFS(int[][] image, int sr, int sc, int color) {
        var initial = image[sr][sc];

        if (initial != color)
            DFS(sr, sc);

        return image;

        void DFS(int i, int j)
        {
            if (i < 0 || i > image.Length - 1 || j < 0 || j > image[i].Length - 1 || image[i][j] != initial)
            {
                return;
            }

            image[i][j] = color;

            DFS(i + 1, j);
            DFS(i - 1, j);

            DFS(i, j + 1);
            DFS(i, j - 1);
        }
    }
    #endregion

    #region 133. Clone Graph
    public class Node {
        public int val;
        public IList<Node> neighbors;

        public Node() {
            val = 0;
            neighbors = new List<Node>();
        }

        public Node(int _val) {
            val = _val;
            neighbors = new List<Node>();
        }

        public Node(int _val, List<Node> _neighbors) {
            val = _val;
            neighbors = _neighbors;
        }
    }

    public static Node CloneGraphBFS(Node node) {
        if (node is null) return null;
        if (node.neighbors.Count == 0) return new Node(node.val);

        var s = new Dictionary<Node, Node>();
        var q = new Queue<Node>();

        s[node] = new Node(node.val);
        q.Enqueue(node);

        while (q.TryDequeue(out var curr))
        {
            foreach (var nei in curr.neighbors)
            {
                if (!s.TryGetValue(nei, out _))
                {
                    s[nei] = new Node(nei.val);
                    q.Enqueue(nei);
                }

                // Соседей добавить нужно всех, поэтому безусловно!
                s[curr].neighbors.Add(s[nei]);
            }
        }

        return s[node];
    }

    private static Dictionary<int, Node> CloneGraphDFSDict = [];

    public static Node CloneGraphDFS(Node node) {
        if (node is null) return null;
        if (node.neighbors.Count == 0) return new Node(node.val);
        if (CloneGraphDFSDict.TryGetValue(node.val, out var ex)) return ex;

        var newNode = new Node(node.val);
        CloneGraphDFSDict[newNode.val] = newNode;

        foreach(var nei in node.neighbors)
        {
            newNode.neighbors.Add(CloneGraphDFS(nei));
        }

        return newNode;
    }
    #endregion

    #region 205. Isomorphic Strings
    public static bool IsIsomorphic(string s, string t) {
        if (s.Length != t.Length) return false;
        
        var mapS = new Dictionary<int, int>();
        var mapT = new Dictionary<int, int>();

        for (var i = 0; i < s.Length; i++)
        {
            if (!mapS.TryGetValue(s[i], out var _)) mapS[s[i]] = i;
            if (!mapT.TryGetValue(t[i], out var _)) mapT[t[i]] = i;

            if (mapS[s[i]] != mapT[t[i]]) return false;
        }

        return true;
    }
    #endregion

    #region 21. Merge Two Sorted Lists
    public class ListNode(int val = 0, LeetCodeTasks.ListNode next = null)
    {
        public int val = val;
        public ListNode next = next;
    }

    public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        var result = new ListNode();

        MergeInternal(list1, list2, result);

        return result.next;

        void MergeInternal(ListNode l1, ListNode l2, ListNode result)
        {
            if (l1 is null && l2 is null)
            {
                return;
            }

            result.next = new ListNode();
            result = result.next;

            if (l1 is null) 
            {
                result.val = l2.val;
                l2 = l2.next;
            }
            else if (l2 is null)
            {
                result.val = l1.val;
                l1 = l1.next;
            }
            else if (l1.val < l2.val) 
            {
                result.val = l1.val;
                l1 = l1.next;
            }
            else 
            {
                result.val = l2.val;
                l2 = l2.next;
            }

            MergeInternal(l1, l2, result);
        }
    }
    #endregion
}