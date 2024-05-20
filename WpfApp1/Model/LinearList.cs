using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LinearList<T> : IEnumerable<T>
{
    private Node<T> head;
    private int count;
    private Node<T> current;

    public bool IsEmpty { get { return count == 0; } }
    public int Count { get { return count; } }
    public T Current
    {
        get
        {
            if (current == null)
                throw new InvalidOperationException("Current is null");
            return current.Data;
        }
    }

    public void Add(T data)
    {
        if (IsEmpty)
        {
            head = new Node<T>(data);
            current = head;
        }
        else
        {
            Node<T> newNode = new Node<T>(data);
            current.Next = newNode;
            current = newNode;
        }
        count++;
    }

    public bool Remove()
    {
        if (IsEmpty)
            return false;

        if (count == 1)
        {
            head = null;
            current = null;
        }
        else
        {
            Node<T> previous = head;
            while (previous.Next != current)
                previous = previous.Next;
            previous.Next = null;
            current = previous;
        }
        count--;
        return true;
    }

    public bool MoveToNext()
    {
        if (current == null || current.Next == null)
            return false;
        current = current.Next;
        return true;
    }

    public void MoveToBeginning()
    {
        current = head;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        Node<T> current = head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }


    private class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
   
}
