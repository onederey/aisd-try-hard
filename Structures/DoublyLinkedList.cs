using System.Text;

namespace Structures;

public class DoublyLinkedList<T>
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

    public DoublyLinkedList(T[] values)
    {
        Init(values);
    }

    public void RemoveAllOccurencies(int val)
    {
        if (_length <= 0)
        {
            return;
        }

        var node = _head;

        while (node != null)
        {
            if (!node.Value.Equals(val))
            {
                node = node.Next;
                continue;
            }

            _length--;

            if (node.Prev is null)
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
    }

    public void PrintInLine(char separator = ' ')
    {
        if (_length <= 0)
        {
            Console.WriteLine("None");
        }

        var builder = new StringBuilder();
        var node = _head;

        while (node is not null)
        {
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
        }

        _head = _head.Next;
        _head.Prev = null;

        _tail = node;
    }
}