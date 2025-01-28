/// <summary>
/// Разные задачки и решения.
/// </summary>
public static class RandomTasks
{
    #region Test Yandex contest
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

    #region Prefix function - алгоритмом Кнута-Морриса-Пратта
    /*
    https://ru.algorithmica.org/cs/string-searching/prefix-function/

    Еще один вариант с комментариями:

    ```cpp
    vector<int> prefix_function(string s) {
        int n = (int) s.size();
        vector<int> p(n, 0);
        for (int i = 1; i < n; i++) {
            // префикс функция точно не больше этого значения + 1
            int cur = p[i - 1];
            // уменьшаем cur значение, пока новый символ не сматчится
            while (s[i] != s[cur] && cur > 0)
                cur = p[cur - 1];
            // здесь либо s[i] == s[cur], либо cur == 0
            if (s[i] == s[cur])
                p[i] = cur + 1;
        }
        return p;
    }
    ```
    */
    public static void PrefixFunctionPlayground()
    {
        Console.WriteLine(string.Concat(SlowPrefixFunction("abacababa")));
        Console.WriteLine(string.Concat(PrefixFunction("abacababa")));

        string s = "choose";
        string t = "choose life. choose a job. choose a career. choose a family. choose a fu...";

        Console.WriteLine(s + "#" + t);
        Console.WriteLine(string.Concat(SlowPrefixFunction(s + "#" + t)));
    }

    public static int[] SlowPrefixFunction(string s)
    {
        var pi = new int[s.Length];

        for (var i = 0; i < s.Length; i++)
        {
            var k = s.Substring(0, i + 1);

            for (var j = k.Length - 1; j >= 0; j--)
            {
                if (k.Substring(0, j) == k.Substring(k.Length - j, j))
                {
                    pi[i] = j;
                    break;
                }
            }
        }

        return pi;
    }

    public static int[] PrefixFunction(string s)
    {
        var n = s.Length;
        var pi = new int[n];
        pi[0] = 0;

        for (var i = 1; i < n; i++)
        {
            var k = pi[i - 1];

            while (k > 0 && s[i] != s[k])
            {
                k = pi[k - 1];
            }

            if (s[i] == s[k])
            {
                k++;
            }

            pi[i] = k;
        }

        return pi;
    }
    #endregion
}