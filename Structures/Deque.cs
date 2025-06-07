namespace Structures;

public class Deque<T>
{
    class Node
    {
        public T Value { get; set; }

        public Node Next { get; set; }
        
        public Node Prev { get; set; }
    }

    private Node _head;

    private Node _tail;

    private int _length;

    public int Length => _length;

    public void PushBack(T value)
    {
        _length++;

        var node = new Node
        {
            Value = value,
            Prev = _tail,
        };

        if (_tail is not null)
        {
            _tail.Next = node;
        }
        
        _tail = node;

        _head ??= _tail;
    }

    public void PushFront(T value)
    {
        _length++;

        var node = new Node
        {
            Value = value,
            Next = _head,
        };

        if (_head is not null)
        {
            _head.Prev = node;
        }

        _head = node;

        _tail ??= node;
    }

    public T PopBack()
    {
        if (_length == 0)
        {
            throw new InvalidOperationException("Дек пустой");
        }

        _length--;

        var val = _tail.Value;

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _tail = _tail.Prev;
            _tail.Next = null;
        }

        return val;
    }

    public T PopFront()
    {
        if (_length == 0)
        {
            throw new InvalidOperationException("Дек пустой");
        }

        _length--;

        var val = _head.Value;

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _head = _head.Next;
            _head.Prev = null;
        }

        return val;
    }
}