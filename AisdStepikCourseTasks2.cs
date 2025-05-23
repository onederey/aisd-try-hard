// Шаблон для отправки задач
/*
using System;

public static class Program
{
    public static void Main()
    {

    }
}
*/

/// <summary>
/// Задачи курса Алгоритмы и структуры данных. Методы (https://stepik.org/course/181477)
/// </summary>
public static class AisdStepikCourseTasks2
{
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