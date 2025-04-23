/// <summary>
/// Задачи курса Алгоритмы и структуры данных. Методы (https://stepik.org/course/181477)
/// </summary>
public static class AisdStepikCourseTasks2
{
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

        while(boys > 0 || girls > 0)
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