namespace Task2_LinkedList.Nodes;

public class IntNode
{
    // This is a doubly linked list, so we have a previous and next node
    // to keep track of the nodes before and after the current node
    // This is useful for traversing the list in both directions and for removing nodes.
    // This Generic Node class is built for flexibility and reusability rather than performance.
    // If performance is a concern, the nodes should be optimised for specific use cases and data types.
    
    // reference type properties use 8 bytes of memory
    public IntNode? Previous {get; set; }
    public int Data { get; set; }
    public IntNode? Next { get; set; }
    public bool IsSentinel { get; set; }

    public IntNode(int data, IntNode? sentinel = null, bool isSentinel = false)
    {
        // Initialise the node with the data and set the previous and next nodes to default values.
        // alternatively, we can set the previous and next nodes to the sentinel node
        // this will allow us to avoid null checks when traversing the list and use the sentinel
        // node as the edge case for the start and end of the list instead of null. It is useful for
        Previous = sentinel;
        Data = data;
        Next = sentinel;
        IsSentinel = isSentinel;
    }
}