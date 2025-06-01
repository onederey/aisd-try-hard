using System.Text;

namespace Structures;

public class DoublyLinkedList<T>
{
    public class Node
    {
        public T Value { get; set; }

        public Node Next { get; set; }

        public Node Prev { get; set; }
    }

    private Dictionary<T, Node> _nodes = new();

    private Node _head;

    private Node _tail;

    private int _length;

    private bool _looped;

    public int Length => _length;

    public DoublyLinkedList(T[] values, bool looped = false)
    {
        _looped = looped;
        Init(values);
    }

    public Node Find(Func<T, bool> predicate)
    {
        if (_length <= 0)
        {
            return null;
        }

        var node = _head;

        var counter = 0;
        while (counter != _length)
        {
            counter++;

            if (predicate(node.Value))
            {
                return node;
            }

            node = node.Next;
        }

        return null;
    }

    public Node Get(T value)
    {
        return _nodes[value];
    }

    public Node Find(T value)
    {
        if (_length <= 0)
        {
            return null;
        }

        var node = _head;

        var counter = 0;
        while (counter != _length)
        {
            counter++;

            if (value.Equals(node.Value))
            {
                return node;
            }

            node = node.Next;
        }

        return null;
    }

    public void Remove(Node node)
    {
        _nodes.Remove(node.Value);
        node.Prev.Next = node.Next;
        node.Next.Prev = node.Prev;
        _length--;
    }

    public void RemoveAll(T val)
    {
        if (_length <= 0)
        {
            return;
        }

        var node = _head;

        var counter = 0;
        var length = _length;

        while (counter < length)
        {
            counter++;

            if (!node.Value.Equals(val))
            {
                node = node.Next;
                continue;
            }

            _nodes.Remove(node.Value);
            _length--;

            if (node.Prev is null || node == _head)
            {
                _head = _head.Next;
                node = _head;

                if (_head is not null) _head.Prev = null;

                continue;
            }

            node.Prev.Next = node.Next;

            if (node.Next is not null)
            {
                node.Next.Prev = node.Prev;
            }
            else
            {
                _tail = node;
            }

            node = node.Next;
        }

        LoopListIfNeeded();
    }

    public void PrintInLine(char separator = ' ')
    {
        if (_length <= 0)
        {
            Console.WriteLine("None");
        }

        var builder = new StringBuilder();
        var node = _head;

        var counter = 0;
        while (counter < _length)
        {
            counter++;

            builder.Append(node.Value);
            builder.Append(separator);
            node = node.Next;
        }

        Console.WriteLine(builder.ToString());
    }

    public void Init(T[] values)
    {
        _length = values.Length;
        _head = new Node();
        var node = _head;

        foreach (var value in values)
        {
            node.Next = new Node();
            node.Next.Prev = node;

            node = node.Next;
            node.Value = value;
            _nodes[value] = node;
        }

        _head = _head.Next;
        _head.Prev = null;

        _tail = node;

        LoopListIfNeeded();
    }

    private void LoopListIfNeeded()
    {
        if (!_looped)
        {
            return;
        }

        _head.Prev = _tail;
        _tail.Next = _head;
    }
}