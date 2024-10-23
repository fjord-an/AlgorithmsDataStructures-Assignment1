using System.Diagnostics;
using Task2_LinkedList.Nodes;

namespace Task2_LinkedList;

public class IntDoublyLinkedList
{
    // The LinkedList class is a collection of nodes that are linked together, This List
    // is a doubly linked list, which means that each node has a reference to the previous and next node.

    private IntNode? _head;
    private IntNode? _tail;
    private readonly IntNode _sentinel;

    public IntDoublyLinkedList(bool isSentinel = false)
    {
        // here i give the option to create a linked list with an object as its edge case (sentinel)
        // or to create a linked list with null as its edge case like the classic linked list.
        // each has its own advantages and disadvantages, tested in the benchmarking project.
        if (isSentinel)
        {
            // Sentinel node with value (-1) and flag set to
            // true to indicate that it is a sentinel node for debugger clarity.
            _sentinel = new IntNode(-1, isSentinel: true);
            _sentinel!.Next = _sentinel;
            _sentinel.Previous = _sentinel;
        }
        else
        {
            _sentinel = new IntNode(-1, isSentinel: false);
            _sentinel!.Next = null;
            _sentinel.Previous = null;
        }
        _head = _sentinel;
        _tail = _sentinel;
    }
    
    
    public IntDoublyLinkedList(IntNode node)
    {
        _head = node;
    }

    public void InsertNode(IntNode node)
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
    
    public void InsertNode(int data)
    {
        // by adding an overload method, we can add a node with just the data, making the
        // process of adding a node even more convenient/flexible.
        
        IntNode newNode = new (data, _sentinel);
        InsertNode(newNode);
    }
    

    public void AppendNode(IntNode node)
    // TODO 18/10 the sentinel node is not being assigned correctly (is being changed to null)
    {
        if (_head == _sentinel)
        {
            _head.Previous = _sentinel;
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

    public void AppendNode(int data)
    {
        // by adding an overload method, we can add a node with just the data, making the
        // process of adding a node even more convenient/flexible.
        IntNode node = new(data, _sentinel);
        AppendNode(node);
    }
    
    public void AddNodeSentinal(IntNode node = null)
    {
        // using a sentinel node avoids null checks and if statements, substantially
        // improving the readability of the code, making it easier to understand.
        // values can be directly assigned to the sentinel node without having to
        // check if the head is null.
        // the detriment of using a sentinel node is that it adds another node
        // to the list, which can be a waste of memory if the list is small. It can also
        // add the possibility of bugs such as infinite looping if the sentinel node is not
        // handled correctly with exceptions.
        
        // if there is no node provided, use the sentinel node as the node
        node ??= _sentinel;
        IntNode tail = _sentinel.Previous!;
        
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
    
    public void AddNodeSentinal(int data)
    {
        // by adding an overload method, we can add a node with just the data, making the
        // process of adding a node even more convenient/flexible.
        IntNode node = new (data);
        AddNodeSentinal(node);
    }

    public void PrintNodes(IntDoublyLinkedList list, Stopwatch stopwatch, bool showData)
    {
        // IntNode? current = list._intHead;
        IntNode? current = _head.Next;
        if (showData)
            while (current != _sentinel)
            {
                Console.Write(" " + current.Data + " ");
                current = current.Next;
            }
        
        Console.WriteLine();
        Console.WriteLine("Time Taken for List Operations:\n" + stopwatch.Elapsed);
    }
    
    public void CountElements(IntDoublyLinkedList list, Stopwatch stopwatch, bool showData, string mode)
    {
        // initialise count to -1 to account for zero-based indexing
        int count = -1;
        IntNode? current = _head.Next;
        if (showData)
            while (current != _sentinel)
            {
                count++;
                current = current.Next;
            }
        
        Console.WriteLine(count);
        Console.WriteLine($"Time Taken for {list.GetType().Name} to {mode}:\n{stopwatch.Elapsed}");
    }
}
