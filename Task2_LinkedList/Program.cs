using System.Diagnostics;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Task2_LinkedList.Benchmarkers;
using Task2_LinkedList.LinkedLists;
using Task2_LinkedList.Nodes;

namespace Task2_LinkedList;

class Program
{
    int n = 5000;
    // !## Linked List Edge case type (true for sentinel object, false for null)
    bool sentinelObj = false;
    static void Main()
    {
        
        // !### To use the dotnet benchmarking tool, uncomment the line below:
        // var summary = BenchmarkRunner.Run<LinkedListBenchmark>();
        
        // I have run the benchmarking tool over several days and the results are in the directory:
        // Task2_LinkedList/BenchmarkDotNet/results
        // The results are in the form of a .html file, which can be opened in a browser.
        // The benchmarking tool shows the average time taken to perform each operation on the linked list.
        // from performing all these list operations countless times and averaging the results.
        // The results show that there are some glaring differences between each linked list implementation.
        // Some are faster than others in certain operations, while some are slower. The biggest difference
        // is the memory usage, which is significantly higher in the Generic Lists implementations as well as some of the 
        // SimpleList nsert method. When benchmarking SimpleListInsert@50,000, i ran out of memory and system terminated the process.
        // even though i have 16GB of RAM and added 10GB of swap space for the test. These did not use the sentinel object as the edge case.
        // When there is enough memory available however, it performs faster than the other linked lists.
        // I used dotnet memory profiler to track the memory usage for all the tests. I have not included the results in the repository
        // as the files are too large and cumbersome to include in the submission on MyLearn (~4GB).
        
        
        // The linked list comparison test is done in the ProcessList method below:
        ProcessList();
    }

    static void ProcessList()
    {
        // number of nodes to add to the linked list:
        int n = 5000;
        Stopwatch stopwatchAppend = new();
        Stopwatch stopwatchInsert = new();
        // !## Linked List Edge case type (true for sentinel object, false for null)
        bool sentinelObj = false;

        // !## pick the linked list to test by uncommenting ONE of the 3 lines below
        // GenericDoublyLinkedList<int> list2 = new(sentinelObj); // Generic List with null edge cases 
        // GenericDoublyLinkedList<int> list = new(sentinelObj); // Generic List with null edge cases 
        // IntDoublyLinkedList list2 = new(sentinelObj); //Int List with sentinel
        // IntDoublyLinkedList list = new(sentinelObj); //Int List with sentinel
        SimpleLinkedList list2 = new(sentinelObj); // Simple List 
        SimpleLinkedList list = new(sentinelObj); // Simple List 

        // test append method:
        stopwatchAppend.Start();

        // add a large number of nodes (n) to test the performance of the linked list
        for (int i = 0; i <= n; i++)
        {
            list.AppendNode(i);
        }
        stopwatchAppend.Stop();

        // !## to count the number of valid nodes, uncomment the line below
        list.CountElements(list, stopwatchAppend, showData: true, mode: "append");


        // test insert method:
        
        stopwatchInsert.Start();
        // add a large number of nodes (n) to test the performance of the linked list
        for (int i = 0; i <= n; i++)
        {
            list2.InsertNode(i);
        }
        stopwatchInsert.Stop();

        // !## to print the nodes, uncomment the line below
        // list.PrintNodes(list, timeAppend, showData:true);
        // !## to count the number of valid nodes, uncomment the line below
        list2.CountElements(list, stopwatchInsert, showData: true, mode: "insert");
        
        if (stopwatchAppend.Elapsed > stopwatchInsert.Elapsed)
        {
            Console.WriteLine($"Insert method is faster than Append method by " +
                              $"{(stopwatchAppend.Elapsed - stopwatchInsert.Elapsed)} " +
                              $"({stopwatchAppend.ElapsedMilliseconds - stopwatchInsert.ElapsedMilliseconds} milliseconds)");
        }
        else
        {
            Console.WriteLine($"Insert method is faster than Append method by " +
                              $"{(stopwatchInsert.Elapsed - stopwatchAppend.Elapsed)} " +
                              $"({stopwatchInsert.ElapsedMilliseconds - stopwatchAppend.ElapsedMilliseconds} milliseconds)\n");
        }
        
        Stopwatch stopwatchDeleteTop = new();
        stopwatchDeleteTop.Start();
        
        list.DeleteTopNodes();
        
        stopwatchDeleteTop.Stop();
        Console.WriteLine($"\nTime Taken to Delete All nodes from the Head: {stopwatchDeleteTop.Elapsed} ({stopwatchDeleteTop.ElapsedMilliseconds}) milliseconds");
        
        list.CountElements(list, stopwatchAppend, showData: true, mode: "delete top");
        
        Stopwatch stopwatchDeleteBottom = new();
        stopwatchDeleteBottom.Start();
        
        list2.DeleteBottomNodes();
        
        stopwatchDeleteBottom.Stop();
        Console.WriteLine($"\nTime Taken to Delete All nodes from the Tail: {stopwatchDeleteBottom.Elapsed} ({stopwatchDeleteTop.ElapsedMilliseconds}) milliseconds");
        
        list2.CountElements(list, stopwatchInsert, showData: true, mode: "delete bottom");
        
        if(stopwatchDeleteBottom.Elapsed > stopwatchDeleteTop.Elapsed) 
        {
            Console.WriteLine($"Deleting from head is faster than Deleting from tail by " +
                              $"{(stopwatchDeleteBottom.Elapsed - stopwatchDeleteTop.Elapsed)} " +
                              $"({stopwatchDeleteBottom.ElapsedMilliseconds - stopwatchDeleteTop.ElapsedMilliseconds} milliseconds)");
        }
        else
        {
            Console.WriteLine($"Deleting from tail is faster than Deleting from head by " +
                              $"{(stopwatchDeleteTop.Elapsed - stopwatchDeleteBottom.Elapsed)} " +
                              $"({stopwatchDeleteTop.ElapsedMilliseconds - stopwatchDeleteBottom.ElapsedMilliseconds} milliseconds)");
        }
    }
}
