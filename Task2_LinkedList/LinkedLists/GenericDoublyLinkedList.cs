using System.Diagnostics;
using Task2_LinkedList.Nodes;

namespace Task2_LinkedList.LinkedLists;

public class GenericDoublyLinkedList<T>
{
    // The LinkedList class is a collection of nodes that are linked together, This List
    // is a doubly linked list, which means that each node has a reference to the previous and next node.

    private GenericNode<T>? _head;
    private GenericNode<T>? _tail;
    private readonly GenericNode<T>? _sentinel;

    public GenericDoublyLinkedList(bool isSentinel = false)
    {
        // here i give the option to create a linked list with an object as its edge case (sentinel)
        // or to create a linked list with null as its edge case like the classic linked list.
        // each has its own advantages and disadvantages, tested in the benchmarking project.
        if (isSentinel)
        {
            // Sentinel node with default value (0) and flag set to
            // true to indicate that it is a sentinel node for debugger clarity.
            _sentinel = new GenericNode<T>(default!, isSentinel: true);
            _sentinel!.Next = _sentinel;
            _sentinel.Previous = _sentinel;
        }
        else
        {
            _sentinel = null;
        }
        _head = _sentinel;
        _tail = _sentinel;
    }
    
    public GenericDoublyLinkedList(GenericNode<T> node)
    {
        _head = node;
    }

    public void InsertNode(GenericNode<T> node)
    {
        // O(1) operation to insert a node at the beginning of the list.
        
        // if head is null, assign it to node using the null-coalescing operator
        // short-hand equivalent to:
        // if (Head == null)
        //     Head = node;
        // must be assigned to the edge case to stop infinite loops when enumerating/iterating the list
        _head ??= _sentinel;
        
        node.Next = _head;
        _head = node;
    }
    
    public void InsertNode(T data)
    {
        // by adding an overload method, we can add a node with just the data, making the
        // process of adding a node even more convenient/flexible.
        
        GenericNode<T> newNode = new GenericNode<T>(data, _sentinel);
        InsertNode(newNode);
    }

    public void AppendNode(GenericNode<T>? node=null!)
    {
        // Inserting a node at the end of the list using Tail pointer is an O(1) operation.
        if (_head == _sentinel)
        {
            _head = node;
        }
        else
        {
            _tail!.Next = node;
            node.Previous = _tail;
        }
        _tail = node;
        
        // it is possible to add a node that references itself, which will cause an infinite loop.
        // To prevent this, we can add a check to see if the node references itself.
        if (node.Next == node)
            throw new Exception("Node cannot reference itself");
    }

    public void AppendNode(T data)
    {
        // by adding an overload method, we can add a node with just the data, making the
        // process of adding a node even more convenient/flexible.
        
        // check to see what type of edge case is being used (sentinel object or null)
        
        GenericNode<T> genericNode = new GenericNode<T>(data, _sentinel);
        AppendNode(genericNode);
    }
    
    public void AddNodeSentinel(GenericNode<T>? node = null)
    {
        // using a sentinel node avoids null checks and if statements, substantially
        // improving the readability of the code, making it easier to understand.
        // values can be directly assigned to the sentinel node without having to
        // check if the head is null.
        // the detriment of using a sentinel node is that it adds an additional node
        // to the list, which can be a waste of memory if the list is small. It can also
        // add the possility of bugs such as infinite looping if the sentinel node is not
        // handled correctly with exceptions.
        
        // if there is no node provided, use the sentinel node as the node
        node ??= _sentinel;
        GenericNode<T> tail = _sentinel.Previous!;
        
        // // Check if the sentinel node is correctly assigned
        // if (_intSentinel.Next == _intSentinel || _intSentinel.Previous == _intSentinel)
        // {
        //     throw new InvalidOperationException("Sentinel node is not correctly unassigned.");
        // }
        //

        tail.Next = node;
        node.Previous = tail;
        node.Next = _sentinel;
        _sentinel.Previous = node;
        if (_head == _sentinel)
            _head = node;
        
        _tail = node;
    }
    
    public void AddNodeSentinel(T data)
    {
        // by adding an overload method, we can add a node with just the data, making the
        // process of adding a node even more convenient/flexible.
        GenericNode<T> node = new (data);
        AddNodeSentinel(node);
    }

    public void PrintNodes(GenericDoublyLinkedList<T> list, Stopwatch stopwatch, bool showData)
    {
        // IntNode? current = list._intHead;
        GenericNode<T>? current = _head!.Next;
        if (showData)
            while (current != _sentinel)
            {
                Console.Write(" " + current!.Data + " ");
                current = current.Next;
            }

        Console.WriteLine();
        Console.WriteLine("Time Taken for List Operations:\n" + stopwatch.Elapsed);
    }
    
    public void CountElements(GenericDoublyLinkedList<T> list, Stopwatch stopwatch, bool showData, string mode)
    {
        // initialise count to -1 to account for zero-based indexing
        int count = -1;
        GenericNode<T>? current = _head.Next;
        if (showData)
            while (current != null)
            {
                count++;
                current = current.Next;
            }
        
        Console.WriteLine(count);
        Console.WriteLine($"Time Taken for {list.GetType().Name} to {mode}:\n{stopwatch.Elapsed}");
    }
}