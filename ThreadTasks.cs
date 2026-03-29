public static class ThreadTasks
{
    public const int Limit = 1000;

    private static int _counter = 0;

    private static volatile bool _stopWork = false;

    private static Lock _lock = new(); 

    private static object _sync = new();

    /// <summary>
    /// Если сделать readonly то сломается, потому что из-за него работа происходит с копией.
    /// А копировать его нельзя, так как это структура, будет создан новый экземпляр...
    /// </summary>
    private static SpinLock _spinLock = new();

    private static SpinWait _spinWait = new();

    private static AutoResetEvent _autoResetEvent = new(false);

    private static ManualResetEvent _manualResetEvent = new(false);

    public static void CounterWithLimiter()
    {
        var threads = new Thread[10];
        for (var i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(Increment)
            {
                Name = $"Поток {i}"
            };

            threads[i].Start();
        }

        for (var i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }


        Console.WriteLine("Результат: " + _counter);
    }

    /// <summary>
    /// Метод для проверки работы разных блокировок.
    /// </summary>
    private static void Increment()
    {
        var lockTaken = false;
        try
        {
            _spinLock.Enter(ref lockTaken);
            Console.WriteLine(Thread.CurrentThread.Name + ": " + "вошел");
            if (_counter >= Limit)
            {
                return;
            }

            _counter++;
            
            Console.WriteLine(Thread.CurrentThread.Name + ": " + _counter);
        }
        finally
        {
            if (lockTaken)
            {
                Console.WriteLine(Thread.CurrentThread.Name + ": " + "вышел");
                _spinLock.Exit();
            }
        }
    }

    public static void MonitorTest()
    {
        // В это раскладывается конструкция `lock (_sync) { ... }`
        var lockTaken = false;
        try
        {
            System.Threading.Monitor.Enter(_sync, ref lockTaken);
            Console.WriteLine("Locked!");
        }
        finally
        {
            if (lockTaken)
            {
                System.Threading.Monitor.Exit(_sync);
            }
        }
    }

    public static void ResetEventTest()
    {
        //var eventWaitHadle = _autoResetEvent;
        var eventWaitHadle = _manualResetEvent;

        var main = new Thread(() =>
        {
            Console.WriteLine("main - старт");
            Thread.Sleep(1000);
            Console.WriteLine("main - подача сигнала");
            eventWaitHadle.Set();
            Console.WriteLine("main - конец");
        });

        var worker1 = new Thread(() =>
        {
            Console.WriteLine("worker1 - старт");
            Console.WriteLine("worker1 - ожидание сигнала");
            if (!eventWaitHadle.WaitOne(2000))
            {
                Console.WriteLine("worker1 - завершение ожидания");
                return;
            }
            Console.WriteLine("worker1 - сигнал!");
            Console.WriteLine("worker1 - конец");
        });

        var worker2 = new Thread(() =>
        {
            Console.WriteLine("worker2 - старт");
            Console.WriteLine("worker2 - ожидание сигнала");
            if (!eventWaitHadle.WaitOne(2000))
            {
                Console.WriteLine("worker2 - завершение ожидания");
                return;
            }
            Console.WriteLine("worker2 - сигнал!");
            Console.WriteLine("worker2 - конец");
        });

        
        worker1.Start();
        worker2.Start();
        main.Start();

        worker1.Join();
        worker2.Join();
        main.Join();
    }

    public static void VolatileTest()
    {
        // Если убрать volatile у флага, то работать перестанет, проверял в release.
        var worker = new Thread(() =>
        {
            var counter = 0L;

            while (!_stopWork)
            {
                counter++;
            }

            Console.WriteLine($"worker остановился, counter = {counter}");
        });

        worker.Start();

        Thread.Sleep(100);
        
        Console.WriteLine("main - остановка worker");
        _stopWork = true;

        worker.Join();
    }
}