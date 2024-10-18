namespace Task2_LinkedList;

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

    public GenericNode(T data)
    {
        // Initialise the node with the data and set the previous and next nodes to null
        Previous = null;
        Data = data;
        Next = null;
    }
}