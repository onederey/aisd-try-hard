namespace Structures;

public class MyQueue<T>
{
    class Node
    {
        public T Value { get; set; }

        public Node Next { get; set; }
    }

    private Node _head;

    private Node _tail;

    private int _length;

    public int Length => _length;

    public static MyQueue<T> Init(T[] values)
    {
        var queue = new MyQueue<T>();

        foreach (var val in values)
        {
            queue.Enqueue(val);
        }

        return queue;
    }

    public void Enqueue(T value)
    {
        _length++;

        if (_head is null)
        {
            _head = new Node
            {
                Value = value,
            };

            _tail = _head;
        }
        else
        {
            _tail.Next = new Node
            {
                Value = value,
            };

            _tail = _tail.Next;
        }
    }

    public T Dequeue()
    {
        if (_length <= 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        _length--;

        var value = _head.Value;
        _head = _head.Next;

        return value;
    }

    public void Clear()
    {
        _head = null;
        _tail = null;
        _length = 0;
    }

    public bool TryPeek(out T peek)
    {
        peek = default;

        if (_length <= 0)
        {
            return false;
        }

        peek = _head.Value;

        return true;
    }

    public bool TryDequeue(out T pop)
    {
        pop = default;

        if (_length <= 0)
        {
            return false;
        }

        pop = Dequeue();

        return true;
    }
}