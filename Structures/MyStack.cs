namespace Structures;

public class MyStack<T>
{
    class Node
    {
        public T Value { get; set; }

        public Node Prev { get; set; }
    }

    private Node _head;

    private int _length;

    public int Length => _length;

    public void Push(T value)
    {
        _length++;

        if (_head is null)
        {
            _head = new Node
            {
                Value = value,
            };
        }
        else
        {
            var node = new Node
            {
                Value = value,
                Prev = _head,
            };

            _head = node;
        }
    }

    public T Pop()
    {
        _length--;

        var result = _head.Value;
        _head = _head.Prev;

        return result;
    }

    public bool TryPop(out T result)
    {
        result = default;

        if (_length <= 0)
        {
            return false;
        }

        result = Pop();

        return true;
    }

    public T Peek()
    {
        return _head.Value;
    }

    public bool TryPeek(out T result)
    {
        result = default;

        if (_length <= 0)
        {
            return false;
        }

        result = Peek();

        return true;
    }

    public void Clear()
    {
        _head = null;
        _length = 0;
    }
}