namespace Task2_LinkedList.Nodes;

public class GenericNode<T>
{
    // Generic type T allows us to use any type of data in the node
    // This is a doubly linked list, so we have a previous and next node
    // to keep track of the nodes before and after the current node
    // This is useful for traversing the list in both directions and for removing nodes.
    // This Generic Node class is built for flexibility and reusability rather than performance.
    // If performance is a concern, the nodes should be optimised for specific use cases and data types.
    public GenericNode <T>? Previous {get; set; }
    public T Data { get; set; }
    public GenericNode <T>? Next { get; set; }
    public bool IsSentinel { get; set; }

    public GenericNode(T data, GenericNode<T>? sentinel=null, bool isSentinel=false)
    {
        // Initialise the node with the data and set the previous and next nodes to the sentinel node
        // or null if no sentinel node is provided. This will allow us to avoid null checks when traversing the list
        // and use the sentinel node as the edge case for the start and end of the list instead of null.
        Previous = sentinel;
        Data = data;
        Next = sentinel;
        IsSentinel = isSentinel;
    }
}