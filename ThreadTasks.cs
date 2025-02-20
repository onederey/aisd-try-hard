using System.Diagnostics.Metrics;
using System.Linq.Expressions;

public static class ThreadTasks
{
    private static int _counter = 0;

    private static object _sync = new(); 

    /*
    # 1. Счетчик с ограничением
    Создайте программу, которая использует несколько потоков для увеличения общего счетчика.
    Каждый поток должен увеличивать счетчик на 1 до тех пор, пока он не достигнет определенного
    значения (например, 1000). Убедитесь, что счетчик никогда не превышает это значение.
    */
    public static void CounterWithLimiter()
    {
        const int limit = 1000;

        Parallel.For(0, 10, (_) => Increment());

        Console.WriteLine(_counter);

        static void Increment()
        {
            while (true)
            {
                Console.WriteLine(Environment.CurrentManagedThreadId);
                lock (_sync)
                {
                    if (_counter >= limit)
                    {
                        return;
                    }

                    _counter++;
                }
            }
        }
    }
}