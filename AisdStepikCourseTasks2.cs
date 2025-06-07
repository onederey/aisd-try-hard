// Шаблон для отправки задач
/*
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {

    }
}
*/

using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Structures;

/// <summary>
/// Задачи курса Алгоритмы и структуры данных. Методы (https://stepik.org/course/181477)
/// </summary>
public static class AisdStepikCourseTasks2
{
    public static void DeepOfBrackets()
    {
        var brackets = Console.ReadLine();
        var s = new MyStack<char>();

        var maxDeep = 0;

        foreach (var bracket in brackets)
        {
            if (bracket == '(')
            {
                s.Push(bracket);
            }
            else
            {
                maxDeep = Math.Max(maxDeep, s.Length);
                s.TryPop(out var _);
            }
        }

        Console.WriteLine(maxDeep);
    }

    public static void RunDequeCommands()
    {
        var c = int.Parse(Console.ReadLine());
        var d = new Deque<string>();
        var output = new StringBuilder();

        for (var i = 0; i < c; i++)
        {
            var commandRaw = Console.ReadLine().Split(" ");
            var command = commandRaw[0];

            switch (command)
            {
                case "1": d.PushFront(commandRaw[1]); break;
                case "2": d.PushBack(commandRaw[1]); break;
                case "3": output.AppendLine(d.PopFront()); break;
                case "4": output.AppendLine(d.PopBack()); break;
            }
        }

        Console.WriteLine(output.ToString());
    }

    public static void RunQueueCommands2()
    {
        var c = int.Parse(Console.ReadLine());
        var q = new MyQueue<string>();
        var output = new StringBuilder();

        for (var i = 0; i < c; i++)
        {
            var commandRaw = Console.ReadLine().Split(" ");
            var command = commandRaw[0];

            switch (command)
            {
                case "ADD":
                    {
                        q.Enqueue(commandRaw[1]);
                        break;
                    }
                case "NEXT":
                    {
                        if (!q.TryDequeue(out var d))
                        {
                            output.AppendLine("Queue is empty");
                        }
                        break;
                    }
                case "COUNT":
                    {
                        output.AppendLine(q.Length.ToString());
                        break;
                    }
            }
        }

        Console.WriteLine(output.ToString());
    }

    public static void ShowRequestsStartTimeWithoutQueue()
    {
        var c = int.Parse(Console.ReadLine());
        var nextJobAt = 0;

        for (var i = 0; i < c; i++)
        {
            var req = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            Console.WriteLine(Math.Max(nextJobAt, req[0]));

            if (nextJobAt == 0) nextJobAt += req[0] + req[1];
            else nextJobAt += req[1];
        }
    }

    class HistogramNode
    {
        public long Pos { get; set; }
        public long Height { get; set; }
    }

    // TODO: сделать свое решение, используя поиск наименьшего слева и поиск наименьшего справа
    public static void Histogram()
    {
        var histogram = Console.ReadLine().Split(" ").Skip(1).Select(long.Parse).ToArray();
        var stack = new MyStack<HistogramNode>();

        stack.Push(new HistogramNode
        {
            Pos = 0,
            Height = -1,
        });

        long result = 0;

        for (var i = 0; i <= histogram.Length; i++)
        {
            var h = i < histogram.Length ? histogram[i] : 0;
            var x = (long)i;

            while (h <= stack.Peek().Height)
            {
                var pop = stack.Pop();
                x = pop.Pos;
                result = Math.Max(result, pop.Height * (i - x));
            }

            stack.Push(new HistogramNode
            {
                Pos = x,
                Height = h,
            });
        }

        Console.WriteLine(result);
    }

    public static void RunQueueCommands()
    {
        var queue = new MyQueue<int>();
        var result = new StringBuilder();

        while (true)
        {
            var commandRaw = Console.ReadLine().Split(" ");
            var command = commandRaw[0];
            var arg = commandRaw.Length > 1 ? commandRaw[1] : "";

            switch (command)
            {
                case "push":
                    {
                        queue.Enqueue(int.Parse(arg));
                        result.AppendLine("ok");
                        break;
                    }
                case "pop":
                    {
                        result.AppendLine(
                            queue.TryDequeue(out var pop)
                                ? pop.ToString()
                                : "error");
                        break;
                    }
                case "front":
                    {
                        result.AppendLine(
                            queue.TryPeek(out var peek)
                                ? peek.ToString()
                                : "error");
                        break;
                    }
                case "size":
                    {
                        result.AppendLine(queue.Length.ToString());
                        break;
                    }
                case "clear":
                    {
                        queue.Clear();
                        result.AppendLine("ok");
                        break;
                    }
                case "exit":
                    {
                        result.AppendLine("bye");
                        Console.WriteLine(result.ToString());
                        return;
                    }
            }
        }
    }

    public static void GameOfDrunk()
    {
        var first = MyQueue<int>.Init([.. Console.ReadLine().Trim().Split(" ").Select(int.Parse)]);
        var second = MyQueue<int>.Init([.. Console.ReadLine().Trim().Split(" ").Select(int.Parse)]);

        var rounds = 10_000_000;

        while (rounds > 0 && second.Length > 0 && first.Length > 0)
        {
            rounds--;

            var f = first.Dequeue();
            var s = second.Dequeue();

            if ((s == 0 && f == 9 || s > f) && !(f == 0 && s == 9))
            {
                second.Enqueue(f);
                second.Enqueue(s);
            }
            else
            {
                first.Enqueue(f);
                first.Enqueue(s);
            }
        }

        Console.WriteLine(
            first.Length == 0
                ? $"second {10_000_000 - rounds}"
                : second.Length == 0
                    ? $"first {10_000_000 - rounds}"
                    : "botva");
    }

    public static void HowManyBracketsDelete()
    {
        var brackets = Console.ReadLine();
        var stack = new MyStack<char>();
        var result = 0;

        foreach (var bracket in brackets)
        {
            if (bracket == '(')
            {
                stack.Push('(');
            }
            else if (stack.Length == 0)
            {
                result++;
            }
            else
            {
                stack.TryPop(out var _);
            }
        }

        Console.WriteLine(result + stack.Length);
    }

    public static void GetSumWithStack()
    {
        var operations = new string[] { "+", "*", "-" };
        var postfixString = Console.ReadLine();
        var stack = new MyStack<int>();

        foreach (var el in postfixString.Split(" "))
        {
            if (string.IsNullOrWhiteSpace(el))
            {
                continue;
            }

            if (!operations.Contains(el))
            {
                stack.Push(int.Parse(el));
            }
            else
            {
                stack.TryPop(out var b);
                stack.TryPop(out var a);

                switch (el)
                {
                    case "+": stack.Push(a + b); break;
                    case "*": stack.Push(a * b); break;
                    case "-": stack.Push(a - b); break;
                }
            }
        }
        stack.TryPop(out var res);
        Console.WriteLine(res);
    }

    public static void IsBracketsCorrect()
    {
        var table = new Dictionary<char, char>()
        {
            ['('] = ')',
            ['['] = ']',
            ['{'] = '}',
        };

        var brackets = Console.ReadLine();

        var stack = new MyStack<char>();
        var isCorrect = true;

        foreach (var bracket in brackets)
        {
            if (table.ContainsKey(bracket))
            {
                stack.Push(bracket);
            }
            else if (stack.Length == 0)
            {
                isCorrect = false;
                break;
            }
            else
            {
                _ = stack.TryPop(out var pop);

                if (table[pop] != bracket)
                {
                    isCorrect = false;
                    break;
                }
            }
        }

        Console.WriteLine(stack.Length == 0 && isCorrect ? "yes" : "no");
    }

    public static void RunStackCommands()
    {
        var stack = new MyStack<int>();
        var result = new StringBuilder();

        while (true)
        {
            var commandRaw = Console.ReadLine().Split(" ");
            var command = commandRaw[0];
            var arg = commandRaw.Length > 1 ? commandRaw[1] : "";

            switch (command)
            {
                case "push":
                    {
                        stack.Push(int.Parse(arg));
                        result.AppendLine("ok");
                        break;
                    }
                case "pop":
                    {
                        result.AppendLine(
                            stack.TryPop(out var pop)
                                ? pop.ToString()
                                : "error");
                        break;
                    }
                case "back":
                    {
                        result.AppendLine(
                            stack.TryPeek(out var peek)
                                ? peek.ToString()
                                : "error");
                        break;
                    }
                case "size":
                    {
                        result.AppendLine(stack.Length.ToString());
                        break;
                    }
                case "clear":
                    {
                        stack.Clear();
                        result.AppendLine("ok");
                        break;
                    }
                case "exit":
                    {
                        result.AppendLine("bye");
                        Console.WriteLine(result.ToString());
                        return;
                    }
            }
        }
    }

    class Child
    {
        public string Name;

        public int LeaveOrder;
    }

    public static void Meeting()
    {
        _ = Console.ReadLine();
        var names = Console
            .ReadLine()
            .Split(" ")
            .Select(s => new Child
            {
                Name = s,
                LeaveOrder = -1,
            })
            .ToArray();

        var leaveOrder = Console
            .ReadLine()
            .Split(" ")
            .Select(s =>
            {
                var num = int.Parse(s);
                var name = names[num - 1];
                name.LeaveOrder = num;

                return num;
            })
            .ToArray();

        var list = new DoublyLinkedList<Child>(names, looped: true);
        var builder = new StringBuilder();

        foreach (var l in leaveOrder)
        {
            var child = list.Get(names[l - 1]);

            builder.Append(child.Prev.Value.Name);
            builder.Append(' ');
            builder.AppendLine(child.Next.Value.Name);

            list.Remove(child);
        }

        Console.WriteLine(builder.ToString());
    }

    public static void GetItemAtMiddle()
    {
        var n = int.Parse(Console.ReadLine());
        var linkedList = new MyLinkedList([.. Console.ReadLine().Split(" ").Select(s => int.Parse(s))]);

        var middle = linkedList.Length / 2;
        middle -= linkedList.Length % 2 == 0 ? 0 : 1;

        Console.WriteLine(linkedList.ElementAt(middle));
    }

    public static void DeleteAllFromDoublyLinkedList()
    {
        var n = int.Parse(Console.ReadLine());
        var dLinkedList = new DoublyLinkedList<int>([.. Console.ReadLine().Split(" ").Select(s => int.Parse(s))]);
        var val = int.Parse(Console.ReadLine());

        dLinkedList.RemoveAll(val);
        dLinkedList.PrintInLine();
    }

    public static void DeleteAllFromLinkedList()
    {
        var n = int.Parse(Console.ReadLine());
        var linkedList = new MyLinkedList([.. Console.ReadLine().Split(" ").Select(s => int.Parse(s))]);
        var val = int.Parse(Console.ReadLine());

        linkedList.RemoveAllOccurencies(val);
        linkedList.PrintInLine();
    }

    public static void ChangeBaseOfNumber()
    {
        var b = int.Parse(Console.ReadLine());
        var n = int.Parse(Console.ReadLine());

        var result = new System.Text.StringBuilder($"{n}(10)=");

        ChangeBase(n);

        result.Append($"({b})");

        Console.WriteLine(result.ToString());

        void ChangeBase(int current)
        {
            if (current < b)
            {
                result.Append(current);
                return;
            }

            var print = current % b;
            ChangeBase(current / b);
            result.Append(print);
        }
    }

    public static void ShowPartitions()
    {
        /*
        1 1 1 1
        2 1 1
        2 2
        3 1
        4

        1 1 1 1 1
        2 1 1 1
        2 2 1
        3 1 1
        3 2
        4 1
        5
        */

        // TODO: я не знаю как решить 🤣
        throw new NotImplementedException();

        var n = int.Parse(Console.ReadLine());

        Show(n);
        Show(n - 1);
        Show(n - 2);

        void Show(int num)
        {
            if (num == 1)
            {
                Console.Write(1);
                Console.WriteLine();
                return;
            }

            if (num == 0)
            {
                Console.WriteLine();
                return;
            }

            var a = n - num;
            Console.Write(num);

            if (a < num)
                Show(a);
            if (a > num)
                Show(num - 1);
        }
    }

    public static void CountZeros()
    {
        var num = int.Parse(Console.ReadLine());
        Console.WriteLine(Count(num));

        static int Count(int number)
        {
            if (number < 10)
            {
                return 0;
            }

            return number % 10 == 0
                ? 1 + Count(number / 10)
                : Count(number / 10);
        }
    }

    public static void Palindrome()
    {
        var input = Console.ReadLine();
        Console.WriteLine(IsPalindrome(0, input.Length - 1, input));

        static bool IsPalindrome(int start, int end, string input)
        {
            if (start >= end)
            {
                return true;
            }

            return input[start] == input[end] && IsPalindrome(start + 1, end - 1, input);
        }
    }

    /// <summary>
    /// Ханойские башни...
    /// </summary>
    public static void Towers()
    {
        var towerSize = int.Parse(Console.ReadLine());
        Move(1, 3, towerSize);

        static void Move(int from, int to, int size)
        {
            if (size == 1)
            {
                Console.WriteLine($"Переместите диск с {from} на {to}");
            }
            else
            {
                // Все просто - 3 стержня = 1 + 2 + 3 = 6
                var buffer = 6 - from - to;
                Move(from, buffer, size - 1);
                Console.WriteLine($"Переместите диск с {from} на {to}");
                Move(buffer, to, size - 1);
            }
        }
    }

    public static void Cinema()
    {
        var input = Console.ReadLine().Split(" ");

        var boys = Convert.ToInt32(input[0]);
        var girls = Convert.ToInt32(input[1]);

        if (Math.Max(boys, girls) / Math.Min(boys, girls) > 2)
        {
            Console.WriteLine("NO SOLUTION");
            return;
        }

        var result = "";

        while (boys > 0 || girls > 0)
        {
            if (boys >= girls && boys >= 2 && girls >= 1)
            {
                result += "BGB";
                boys -= 2;
                girls -= 1;
            }
            else if (girls >= boys && girls >= 2 && boys >= 1)
            {
                result += "GBG";
                girls -= 2;
                boys -= 1;
            }
            else if (girls == boys)
            {
                result += "BG";
                boys--;
                girls--;
            }
            else if (girls > boys)
            {
                result += "G";
                girls--;
            }
            else
            {
                result += "B";
                boys--;
            }
        }

        Console.WriteLine(result);
    }

    public static void Difference()
    {
        var k = int.Parse(Console.ReadLine());
        var m = int.Parse(Console.ReadLine());
        var a = int.Parse(Console.ReadLine());
        var b = int.Parse(Console.ReadLine());

        var kate = (b / k) - ((a - 1) / k);
        var masha = (b / m) - ((a - 1) / m);

        Console.WriteLine(kate - masha);
    }
}