using System.Diagnostics;

namespace Task2_LinkedList;

public class GenericLinkedList<T>
{
    // The LinkedList class is a collection of nodes that are linked together, This List
    // is a doubly linked list, which means that each node has a reference to the previous and next node.

    private GenericNode<T>? _genericHead;
    private GenericNode<T>? _genericTail;
    private IntNode? _intHead;
    private IntNode? _intTail;
    private IntNode _intSentinel;

    public GenericLinkedList()
    {
        _intSentinel = new IntNode(0); // Sentinel node with dummy data
        _intSentinel.Next = _intSentinel;
        _intSentinel.Previous = _intSentinel;
    }

    public void InsertNode(GenericNode<T>? node)
    {
        // if head is null, assign it to node. By using the null-coalescing operator, we can assign the value if it is null without having to use an if statement
        // it the the short-hand equivalent to:
        // if (Head == null)
        // {
        //     Head = node;
        // }

        _genericHead ??= node;
        
        GenericNode<T>? current = _genericHead;

        while (current!.Next != null)
            // currently an infinite loop
            current = current.Next;

        current.Next = node;
    }

    public void AddGenericNode(GenericNode<T>? node=null!)
    {
        if (_genericHead == null)
        {
            _genericHead = node;
        }
        else
        {
            _genericTail!.Next = node;
            node.Previous = _genericTail;
        }
        _genericTail = node;
        
        // it is possible to add a node that references itself, which will cause an infinite loop.
        // To prevent this, we can add a check to see if the node references itself.
        if (node.Next == node)
            throw new Exception("Node cannot reference itself");
    }

    public void AddGenericNode(T data)
    {
        // by adding an overload method, we can add a node with just the data, making the
        // process of adding a node even more convenient/flexible.
        GenericNode<T> genericNode = new GenericNode<T>(data);
        AddGenericNode(genericNode);
    }
    
    public void AddIntNode(int data)
    {
        // infinite loop:
        // IntNode intNode = new(data); 
        // _intHead ??= intNode;
        // IntNode current = _intHead;
        //
        // while (current!.Next != null)
        //     // currently an infinite loop
        //     current = current.Next;
        //
        // current.Next = intNode;
        
        // using a sentinel node avoids null checks and if statements, substantially
        // improving the readability of the code, making it easier to understand.
        // values can be directly assigned to the sentinel node without having to
        // check if the head is null.
        // the detriment of using a sentinel node is that it adds an additional node
        // to the list, which can be a waste of memory if the list is small. It can also
        // add the possility of bugs such as infinite looping if the sentinel node is not
        // handled correctly with exceptions.
        
        IntNode intNode = new(data);
        IntNode tail = _intSentinel.Previous!;
        
        // // Check if the sentinel node is correctly assigned
        // if (_intSentinel.Next == _intSentinel || _intSentinel.Previous == _intSentinel)
        // {
        //     throw new InvalidOperationException("Sentinel node is not correctly unassigned.");
        // }
        //

        tail.Next = intNode;
        intNode.Previous = tail;
        intNode.Next = _intSentinel;
        _intSentinel.Previous = intNode;
        if (_intHead == _intSentinel)
            _intHead = intNode;
        
        _intTail = intNode;
    }

    public void PrintIntNodes(GenericLinkedList<T> list, Stopwatch stopwatch)
    {
        IntNode? current = list._intHead;
        while (current != null)
        {
            Console.Write(" " + current.Data + " ");
            current = current.Next;
        }

        Console.WriteLine("");
        Console.WriteLine("Time Taken for List Operations:\n" + stopwatch.Elapsed);
    }
}