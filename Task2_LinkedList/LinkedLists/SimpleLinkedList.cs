using System.Diagnostics;
using Task2_LinkedList.Nodes;

namespace Task2_LinkedList.LinkedLists;

public class SimpleLinkedList
{
    // The LinkedList class is a collection of nodes that are linked together, This List
    // is a doubly linked list, which means that each node has a reference to the previous and next node.

    private SimpleNode? _head;

    public SimpleLinkedList(bool isSentinel)
    {
        _head = null;
    }
    
    public void InsertNode(int data)
    {
        SimpleNode newNode = new SimpleNode(data);
        newNode.Next = _head;
        _head = newNode;
    }

    public void AppendNode(int data)
    {
        // Inserting a node at the end of the list is an O(n) operation.
        // When the list contains a large number of nodes, this operation can be slow.
        // because we are using a while loop to traverse to the end of the list.
        
        SimpleNode newNode = new SimpleNode(data);
        _head ??= new SimpleNode (data);
        var current = _head;

        while (current!.Next != null)
            current = current.Next;

        current.Next = newNode;
    }
    
   public void DeleteTopNodes()
   {
       SimpleNode? current = _head;
       while (current != null)
       {
           SimpleNode? temp = current;
           current = current.Next;
           temp.Next = null;
       }
       _head = null;
   } 
   
    public void DeleteBottomNodes()
    {
        if (_head == null)
            return;

        SimpleNode? current = _head;
        SimpleNode? previous = null;

        while (current.Next != null)
        {
            previous = current;
            current = current.Next;
        }

        while (previous != null)
        {
            previous.Next = null;
            current = _head;
            previous = null;

            while (current.Next != null)
            {
                previous = current;
                current = current.Next;
            }
        }

        _head = null;
    }
    
    public void PrintNodes(SimpleLinkedList list, Stopwatch stopwatch, bool showData)
    {
        // IntNode? current = list._intHead;
        SimpleNode? current = _head.Next;
        if (showData)
            while (current != null)
            {
                Console.Write(" " + current.Data + " ");
                current = current.Next;
            }
        
        Console.WriteLine();
        Console.WriteLine("Time Taken for List Operations:\n" + stopwatch.Elapsed);
    }
    
    public void CountElements(SimpleLinkedList list, Stopwatch stopwatch, bool showData, string mode)
    {
        // initialise count to -1 to account for zero-based indexing
        int count = 0;
        if (_head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        SimpleNode? current = _head.Next;
        
        if (showData)
            while (current != null)
            {
                count++;
                current = current.Next;
            }
        
        Console.WriteLine($"{mode}ed {count} nodes");
        Console.WriteLine($"Time Taken for {list.GetType().Name} to {mode}:\n{stopwatch.Elapsed} ({stopwatch.ElapsedMilliseconds}) milliseconds\n");
    }
}