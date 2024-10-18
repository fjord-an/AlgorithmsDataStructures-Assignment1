using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.dotMemory;
using Task2_LinkedList.LinkedLists;

namespace Task2_LinkedList.Benchmarkers
{
    // https://benchmarkdotnet.org/articles/samples/IntroDotMemoryDiagnoser.html
    [DotMemoryDiagnoser]
    public class LinkedListBenchmark
    {
        
        private GenericDoublyLinkedList<int> _list;

        [Params(500)] // Different sizes for benchmarking
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _list = new GenericDoublyLinkedList<int>();
        }

        [Benchmark]
        public void AddNodes()
        {
            for (int i = 0; i < N; i++)
            {
                _list.AddNodeSentinel(i);
            }
        }
    }
}