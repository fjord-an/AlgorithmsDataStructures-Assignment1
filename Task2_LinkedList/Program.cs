using System.Diagnostics;
using BenchmarkDotNet.Running;
using Task2_LinkedList.Benchmarkers;
using Task2_LinkedList.LinkedLists;
using Task2_LinkedList.Nodes;

namespace Task2_LinkedList;

class Program
{
    static void Main3()
    {
        var summary = BenchmarkRunner.Run<LinkedListBenchmark>();
    }

    static void Main()
    {
        // the number of nodes to add to the list for testing
        // 10,000,000;
        int n = 500;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        
        // pick the linked list to test by uncommenting ONE of the lines below
        // GenericDoublyLinkedList<int> list = new (true); // Generic List with sentinel nodes edge cases
        // GenericLinkedList<int> list = new (); // Generic List with null edge cases 
        // IntDoublyLinkedList list = new(); // Int List with null
        IntDoublyLinkedList list = new(true); //Int List with sentinel
        // SimpleLinkedList list = new(); // Simple List 
        
        // add a large number of nodes (n) to test the performance of the linked list
        for(int i = 0; i <= n; i++)
        {
            // list.AddNodeSentinal(i);
            // list.AddGenericNode(i);
            list.AddGenericNode(i);
        }
        
        stopwatch.Stop();
        // to print the nodes, uncomment the line below
        list.PrintNodes(list, stopwatch, true);
    }
}