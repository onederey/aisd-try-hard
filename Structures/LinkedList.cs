using System.Text;

namespace Structures;

public class MyLinkedList
{
    private Node _head;

    private Node _tail;

    private int _length;

    class Node
    {
        public int Value { get; set; }

        public Node Next { get; set; }
    }

    public MyLinkedList(int[] values)
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
        Node prev = null;

        do
        {
            if (node.Value != val)
            {
                prev = node;
                node = node.Next;
                continue;
            }

            _length--;

            if (prev is null)
            {
                _head = _head.Next;
                node = _head;
                continue;
            }
            
            prev.Next = node.Next;
            node = node.Next;

        } while (node is not null);
    }

    public void PrintInLine(string separator = " ")
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

    public void Init(int[] values)
    {
        _length = values.Length;
        _head = new Node();

        var node = _head;

        for (var i = 0; i < values.Length; i++)
        {
            node.Next = new Node();
            node = node.Next;
            node.Value = values[i];
        }

        _head = _head.Next;
        _tail = node;
    }
}