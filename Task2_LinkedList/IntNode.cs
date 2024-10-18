namespace Task2_LinkedList;

public class IntNode
{
    // Generic type T allows us to use any type of data in the node
    // This is a doubly linked list, so we have a previous and next node
    // to keep track of the nodes before and after the current node
    // This is useful for traversing the list in both directions and for removing nodes.
    // This Generic Node class is built for flexibility and reusability rather than performance.
    // If performance is a concern, the nodes should be optimised for specific use cases and data types.
    public IntNode? Previous {get; set; }
    public int Data { get; set; }
    public IntNode? Next { get; set; }

    public IntNode(int data)
    {
        // Initialise the node with the data and set the previous and next nodes to null
        Previous = null;
        Data = data;
        Next = null;
    }
}