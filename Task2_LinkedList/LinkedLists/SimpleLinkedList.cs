using Task2_LinkedList.Nodes;

namespace Task2_LinkedList.LinkedLists;

public class SimpleLinkedList
{
    // The LinkedList class is a collection of nodes that are linked together, This List
    // is a doubly linked list, which means that each node has a reference to the previous and next node.

    private SimpleNode? _head;
    private SimpleNode? _tail;

    public SimpleLinkedList()
    {
        _head = null;
        _tail = null;
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
        SimpleNode newNode = new SimpleNode(data);
        _head ??= new SimpleNode (data);
        var current = _head;

        while (current!.Next != null)
            // currently an infinite loop
            current = current.Next;

        current.Next = newNode;
    }
}