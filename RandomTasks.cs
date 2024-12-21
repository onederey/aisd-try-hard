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
}