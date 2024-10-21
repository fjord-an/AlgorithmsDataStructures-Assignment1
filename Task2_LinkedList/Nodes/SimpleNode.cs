namespace Task2_LinkedList.Nodes;

public class SimpleNode
{
    // This is a stripped down node for higher memory performance
    // this will often result in higher CPU usage as we have to check for null values before accessing the next node
    // when inserting at the end of the list. When adding at the start of the list, we can avoid null checks.
    // This is a trade-off between memory and CPU usage, and the choice depends on the specific use case.
    
    // TODO REFERENCE
    // reference type typically properties use 8 bytes of memory
    // int typically uses 4 bytes of memory
    public int Data { get; set; }
    public SimpleNode? Next { get; set; }

    public SimpleNode(int data)
    {
        // Initialise the node with the data and set the previous and next nodes to default values.
        // alternatively, we can set the previous and next nodes to the sentinel node
        // this will allow us to avoid null checks when traversing the list and use the sentinel
        // node as the edge case for the start and end of the list instead of null. It is useful for
        Data = data;
        Next = null;
    }
}