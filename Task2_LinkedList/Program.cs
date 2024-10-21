using System.Diagnostics;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Task2_LinkedList.Benchmarkers;
using Task2_LinkedList.LinkedLists;
using Task2_LinkedList.Nodes;

namespace Task2_LinkedList;

class Program<T>
{
    static void Main()
    {
        var summary = BenchmarkRunner.Run<LinkedListBenchmark>();
    }

    static void Main2(T list)
    {
        int n = 5000;
        Stopwatch stopwatch = new();
        // !## Edge case type (true for sentinel object, false for null)
        bool sentinelObj = false;
        // each have their own advantages and disadvantages, tested in the benchmarking project. 

        // !## pick the linked list to test by uncommenting ONE of the 3 lines below
        GenericDoublyLinkedList<int> genericList = new(sentinelObj); // Generic List with null edge cases 
        IntDoublyLinkedList intList = new(sentinelObj); //Int List with sentinel
        SimpleLinkedList list = new(sentinelObj); // Simple List 

        // test append method:
        stopwatch.Start();

        // add a large number of nodes (n) to test the performance of the linked list
        for (int i = 0; i <= n; i++)
        {
            list.AppendNode(i);
        }
        stopwatch.Stop();

        // !## to print the nodes added, uncomment the line below
        // list.PrintNodes(list, timeAppend, showData:true);
        // !## to count the number of valid nodes, uncomment the line below
        list.CountElements(list, stopwatch, showData: true, mode: "append");


        // test insert method:
        // reset the list to test the performance of the insert method
        list = new(sentinelObj);
        stopwatch.Reset();

        stopwatch.Start();
        // add a large number of nodes (n) to test the performance of the linked list
        for (int i = 0; i <= n; i++)
        {
            list.InsertNode(i);
        }
        stopwatch.Stop();

        // !## to print the nodes, uncomment the line below
        // list.PrintNodes(list, timeAppend, showData:true);
        // !## to count the number of valid nodes, uncomment the line below
        list.CountElements(list, stopwatch, showData: true, mode: "insert");
    }
}
