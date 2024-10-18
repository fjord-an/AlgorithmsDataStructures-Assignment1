namespace Task2_LinkedList;

public class HpLinkedList<T>
{
    // The LinkedList class is a collection of nodes that are linked together, This List
    // is a doubly linked list, which means that each node has a reference to the previous and next node.

    private IntNode? _head;
    private IntNode? _tail;

    public void InsertNode(int data)
    {
        // if head is null, assign it to node. By using the null-coalescing operator, we can assign the value if it is null without having to use an if statement
        // it the the short-hand equivalent to:
        // if (Head == null)
        // {
        //     Head = node;
        // }

        IntNode newNode = new IntNode(data);
        _head ??= new IntNode(data);
        IntNode current = _head;

        while (current!.Next != null)
            // currently an infinite loop
            current = current.Next;

        current.Next = newNode;
    }
}