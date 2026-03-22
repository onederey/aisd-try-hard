using System.Drawing;
using System.Net.Mail;
using System.Text;

public static class YCoderunTasks
{
    public static void Mushrooms()
    {
        _ = Console.ReadLine();
        var weights = Console
            .ReadLine()
            .Split(" ")
            .Select(w => int.Parse(w))
            .ToArray();

        var min = int.MaxValue;
        var max = int.MinValue;

        var v = 0;
        var m = 0;

        for (var i = 0; i < weights.Length; i++)
        {
            if (i % 2 == 0)
            {
                min = Math.Min(min, weights[i]);
                v += weights[i];
            }
            else
            {
                max = Math.Max(max, weights[i]);
                m += weights[i];
            }
        }

        if (min < max)
        {
            v = v - min + max;
            m = m - max + min;
        }

        Console.WriteLine(v - m);
    }

    public static void MomTasks()
    {
        var i = Console
            .ReadLine()
            .Split(" ")
            .Select(float.Parse)
            .ToArray();
        
        var a = i[0];
        var b = i[1];
        var c = i[2];
        var u0 = i[3];
        var u1 = i[4];
        var u2 = i[5];
        var res = float.MaxValue;

        res = Math.Min(res, a/u0 + c/u1 + b/u2);
        res = Math.Min(res, b/u0 + c/u1 + a/u2);

        res = Math.Min(res, a/u0 + a/u1 + b/u0 + b/u1);

        res = Math.Min(res, a/u0 + c/u0 + c/u1 + a/u2);
        res = Math.Min(res, b/u0 + c/u0 + c/u1 + b/u2);

        res = Math.Min(res, a/u0 + c/u0 + c/u1 + a/u1 + a/u0 + a/u1);
        res = Math.Min(res, b/u0 + c/u0 + c/u1 + b/u1 + b/u0 + b/u1);

        Console.WriteLine(res.ToString("F5"));
    }

    public static void PassWeeks()
    {
        // TODO: Разобраться в комбинаторике
        throw new NotImplementedException();
    }

    public static void ScoreboardCounter()
    {
        var input = Console.ReadLine().Split(" ");

        var n = long.Parse(input[0]);
        var k = long.Parse(input[1]);
        
        var l = n % 10;

        while (k > 0 && l != 0 && l != 2)
        {
            k--;
            n += l;
            l = n % 10;
        }

        if (l == 2 && k > 0)
        {
            var m = k / 4;
            n += m * 20;
            k -= m * 4;

            while (k > 0)
            {
                k--;
                n += n % 10;
            }
        }

        Console.WriteLine(n);
    }

    public static void PlusMinusQuestion()
    {
        var s = Console
            .ReadLine()
            .Split(" ")
            .Select(int.Parse)
            .ToArray();
        
        var rowSum = new int[s[0]];
        var colSum = new int[s[1]];

        var a = new int[s[0], s[1]];

        for (var i = 0; i < s[0]; i++)
        {
            var line = Console.ReadLine();

            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == '-') 
                {
                    a[i, j] = -1;
                }
                else if (line[j] == '+') 
                {
                    a[i, j] = 1;
                }
                else 
                {
                    a[i, j] = 2;
                }

                if (a[i, j] == 2)
                {
                    rowSum[i] += 1;
                    colSum[j] -= 1;
                }
                else
                {
                    rowSum[i] += a[i, j];
                    colSum[j] += a[i, j];
                }
            }
        }

        var diff = int.MinValue;

        for (var i = 0; i < s[0]; i++)
        {
            for (var j = 0; j < s[1]; j++)
            {
                diff = Math.Max(
                    diff, 
                    rowSum[i] - colSum[j] - (a[i, j] == 2 ? 2 : 0));
            }
        }

        Console.WriteLine(diff);


        /*
4 3
+-+ +-+ +-+
??- ++- ---
?-? +-+ ---
++? +++ ++-

Пересечение = 3 2

        */
    }

    #region FiveInARow
    public static void FiveInARow()
    {
        var input = Console
            .ReadLine()
            .Split(" ")
            .Select(int.Parse)
            .ToArray();
        
        var a = new string[input[0]];

        for (var i = 0; i < input[0]; i++)
        {
            a[i] = Console.ReadLine();
        }
        
        var maxInRow = SearchMaxHorizontally(a, int.MinValue);
        maxInRow = SearchMaxVertically(a, maxInRow);
        maxInRow = SearchMaxDiagLeftRight(a, maxInRow);
        maxInRow = SearchMaxDiagRightLeft(a, maxInRow);
        
        if (maxInRow >= 5)
        {
            Console.WriteLine("Yes");
            return;
        }
        Console.WriteLine("No");
    }

    private static int SearchMaxDiagRightLeft(string[] a, int maxInRow)
    {
        if (a.Length < 5 || a[0].Length < 5 || maxInRow >= 5)
        {
            return maxInRow;
        }
        
        var seenJ = new HashSet<int>();
        for (var i = 0; i < a.Length; i++)
        {
            for (var j = 0; j < a[i].Length; j++)
            {
                if (j != a[i].Length - 1 && seenJ.Contains(j))
                {
                    continue;
                }

                var tempD = 0;
                var prevD = '.';

                for (var k = 0; i + k < a.Length && j - k >= 0; k++)
                {
                    if (a[i + k][j - k] == '.' || (prevD != '.' && prevD != a[i + k][j - k]))
                    {
                        maxInRow = Math.Max(maxInRow, tempD);
                        tempD = 0;
                    }

                    prevD = a[i + k][j - k];
                    if (prevD != '.') tempD++;
                }

                maxInRow = Math.Max(maxInRow, tempD);
                if (maxInRow >= 5) break;

                seenJ.Add(j);
            }
        }

        return maxInRow;
    }

    private static int SearchMaxDiagLeftRight(string[] a, int maxInRow)
    {
        if (a.Length < 5 || a[0].Length < 5 || maxInRow >= 5)
        {
            return maxInRow;
        }

        var seenJ = new HashSet<int>();
        for (var i = 0; i < a.Length; i++)
        {
            for (var j = 0; j < a[i].Length; j++)
            {
                if (j != 0 && seenJ.Contains(j))
                {
                    break;
                }

                var tempD = 0;
                var prevD = '.';

                for (var k = 0; k + i < a.Length && k + j < a[i].Length; k++)
                {
                    if (a[i + k][j + k] == '.' || (prevD != '.' && prevD != a[i + k][j + k]))
                    {
                        maxInRow = Math.Max(maxInRow, tempD);
                        tempD = 0;
                    }

                    prevD = a[i + k][j + k];
                    if (prevD != '.') tempD++;
                }

                maxInRow = Math.Max(maxInRow, tempD);

                if (maxInRow >= 5) break;

                seenJ.Add(j);
            }
        }

        return maxInRow;
    }

    private static int SearchMaxVertically(string[] a, int maxInRow)
    {
        if (a.Length < 5 || maxInRow >= 5)
        {
            return maxInRow;
        }

        for (var i = 0; i < a[0].Length; i++)
        {
            var tempH = 0;
            var prevH = '.';

            for (var j = 0; j < a.Length; j++)
            {
                if (a[j][i] == '.' || (prevH != '.' && prevH != a[j][i]))
                {
                    maxInRow = Math.Max(maxInRow, tempH);
                    tempH = 0;
                }

                prevH = a[j][i];
                if (prevH != '.') tempH++;
            }
            
            maxInRow = Math.Max(maxInRow, tempH);
            
            if (maxInRow >= 5) break;
        }

        return maxInRow;
    }

    private static int SearchMaxHorizontally(string[] a, int maxInRow)
    {
        if (a[0].Length < 5 || maxInRow >= 5)
        {
            return maxInRow;
        }

        for (var i = 0; i < a.Length; i++)
        {
            var tempV = 0;
            var prevV = '.';

            for (var j = 0; j < a[i].Length; j++)
            {
                if (a[i][j] == '.' || (prevV != '.' && prevV != a[i][j]))
                {
                    maxInRow = Math.Max(maxInRow, tempV);
                    tempV = 0;
                }

                prevV = a[i][j];
                if (prevV != '.') tempV++;
            }

            maxInRow = Math.Max(maxInRow, tempV);

            if (maxInRow >= 5) break;
        }

        return maxInRow;
    }
    #endregion

    public static void UncutString()
    {
        var s = Console
            .ReadLine()
            .Split(" ")
            .Select(int.Parse)
            .ToArray();
        
        var partLen = s[0] / s[1];
        var original = Console.ReadLine().AsSpan();
        var parts = new Dictionary<string, Queue<int>>();

        for (var i = 0; i < s[1]; i++)
        {
            var part = Console.ReadLine();
            if (parts.TryGetValue(part, out var ind))
            {
                ind.Enqueue(i + 1);
            }
            else
            {
                parts[part] = new Queue<int>();
                parts[part].Enqueue(i + 1);
            }
        }

        var result = new StringBuilder();

        for (var i = 0; i < original.Length; i += partLen)
        {
            result
                .Append(parts[original.Slice(i, partLen).ToString()].Dequeue())
                .Append(" ");
        }

        Console.WriteLine(result.ToString());
    }

    public static void NewRules()
    {
        var from = Console
            .ReadLine()
            .Split(" ")
            .Select(int.Parse)
            .ToArray();

        var to = Console
            .ReadLine()
            .Split(" ")
            .Select(int.Parse)
            .ToArray();

        var free = true;
        var steps = 0L;

        // Сначала двигаемся прямо, не забывая, что первый шаг "бесплатный"
        var dist = GetDist(from[0], to[0]);
        if (dist > 0)
        {
            steps += (dist - 1) * 3L;
            free = false;
        }
        
        // Двигаемся налево
        dist = GetDist(from[1], to[1]);
        if (dist > 0)
        {
            dist--;
            steps = free ? steps : steps + 1;
            free = false;
        }

        // Затем снова прямо
        steps += dist * 3L;

        Console.WriteLine(steps);

        static int GetDist(int a, int b) => a >= b ? a - b : b - a;
    }

    public static void Figon()
    {
        var n = int.Parse(Console.ReadLine());
        var result = new System.Text.StringBuilder();
        
        var listBuf = new Dictionary<string, List<string>>();
        var subListBuf = new Dictionary<string, (string List, int Start)>();

        while (n > 0)
        {
            var line = Console.ReadLine();

            var parts = line.Split("=");

            if (parts.Length > 1)
            {
                var name = parts[0].Split(' ')[1];
                var subParts = parts[1].Split('.');
                
                var nums = parts[1]
                    .Split('(')[1][..^1]
                    .Split(',');

                if (subParts.Length > 1)
                {
                    var list = subParts[0].TrimStart();
                    var shift = int.Parse(nums[0]) - 1;

                    while (!listBuf.ContainsKey(list))
                    {
                        var temp = subListBuf[list];
                        shift += temp.Start;
                        list = temp.List;
                    }

                    subListBuf[name] = (list, shift);
                }
                else
                {
                    listBuf[name] = [.. nums];
                }
            }
            else
            {
                parts = line.Split('.');
                var name = parts[0];

                var command = parts[1].Split('(');

                switch (command[0])
                {
                    case "get":
                        {
                            var arg = int.Parse(command[1][..^1]) - 1;
                            var value = "";

                            if (listBuf.TryGetValue(name, out var list))
                            {
                                value = list[arg];
                            }
                            else
                            {
                                var sub = subListBuf[name];
                                value = listBuf[sub.List][sub.Start + arg];
                            }

                            result.AppendLine(value);
                            break;
                        }
                    case "set":
                        {
                            var args = command[1][..^1].Split(',');

                            if (listBuf.TryGetValue(name, out var list))
                            {
                                list[int.Parse(args[0]) - 1] = args[1];
                            }
                            else
                            {
                                var sub = subListBuf[name];
                                
                                listBuf[sub.List][sub.Start + int.Parse(args[0]) - 1] = args[1];
                            }

                            break;
                        }
                    case "add":
                        {
                            var arg = command[1][..^1];

                            if (listBuf.TryGetValue(name, out var list))
                            {
                                list.Add(arg);
                            }
                            
                            break;
                        }
                }
            }

            n--;
        }

        Console.WriteLine(result.ToString());
    }

    public static void Ball()
    {
        var n = int.Parse(Console.ReadLine());
        var a = new int[n + 1];

        a[n] = 1;
        n--;

        while (n > -1)
        {
            for (var k = n + 1; k < n + 4 && k < a.Length; k++)
            {
                a[n] += a[k];
            }

            n--;
        }

        Console.WriteLine(a[0]);
    }

    public static void River()
    {
        var l = 0;
        var r = 1;

        foreach (var c in Console.ReadLine())
        {
            var tl = l;
            var tr = r;

            if (c == 'L')
            {
                l = Math.Min(tl + 1, tr + 1);
                r = Math.Min(tl + 2, tr);
            }
            else if (c == 'R')
            {
                l = Math.Min(tl, tr + 2);
                r = Math.Min(tl + 1, tr + 1);
            }
            else
            {
                l = Math.Min(tl + 1, tr + 2);
                r = Math.Min(tl + 2, tr + 1);
            }
        }

        Console.WriteLine(r);
    }

    public static void Dictionary_14()
    {
        var input = Console.ReadLine();
        var n = int.Parse(Console.ReadLine());
        var h = new HashSet<string>(n);
        var max = -1;

        while (n > 0)
        {
            var word = Console.ReadLine();
            max = Math.Max(max, word.Length);

            h.Add(word);
            n--;
        }

        var l = 0;
        var r = 1;

        var result = new System.Text.StringBuilder();

        while (r <= input.Length)
        {
            var dr = Math.Min(input.Length, r + max - 1);

            while (dr > l)
            {
                var guess = input[l..dr];
            
                if (h.Contains(guess))
                {
                    result.Append(guess).Append(' ');
                    l = dr;
                    r = dr;
                    break;
                }

                dr--;
            }

            r++;
        }

        Console.WriteLine(result.ToString());
        // angoan
        // a, an, go
    }
}