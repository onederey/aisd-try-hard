// Шаблон для отправки задач
/*
using System;
using System.Linq;
using System.Text;

public class Program
{
    public static void Main()
    {

    }
}
*/

using System.Text;
using Structures;

/// <summary>
/// Задачи курса Алгоритмы и структуры данных. Методы (https://stepik.org/course/181477)
/// </summary>
public static class AisdStepikCourseTasks2
{
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