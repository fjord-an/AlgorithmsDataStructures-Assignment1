using System.Diagnostics;
using BenchmarkDotNet.Running;
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
        int n = 50000;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        GenericLinkedList<int> list = new ();
        GenericNode<int> genericNode = new (1);
        
        // list.InsertNode(new Node(1));
        // list.InsertNode(new Node(2));
        // list.InsertNode(new Node(3));

        // adding two nodes that reference each other will cause an infinite loop, catched by the exception
        // list.AddGenericNode(node);
        // list.AddGenericNode(node);
        
        // iterate through a large number of nodes to test the performance of the linked list
        for(int i = 0; i <= n; i++)
        {
            // 13.498s@100,000,000:3.82gb&16.46%cpu
            list.AddIntNode(i);
            // 13.51s@100,000,000:3.8gb:16.82%cpu
            // list.AddIntNode(i);
            
            // Force garbage collection every 100,000 nodes
            // if (i % 100000 == 0)
            // {
            //     GC.Collect();
            //     GC.WaitForPendingFinalizers();
            // }
            
        }
        
        stopwatch.Stop();
        list.PrintIntNodes(list, stopwatch);
    }
}