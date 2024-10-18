using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Diagnostics.dotMemory;
using System.Diagnostics;

namespace Task2_LinkedList
{
    // https://benchmarkdotnet.org/articles/samples/IntroDotMemoryDiagnoser.html
    [DotMemoryDiagnoser]
    public class LinkedListBenchmark
    {
        
        private GenericLinkedList<int> _list;

        [Params(50000)] // Different sizes for benchmarking
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _list = new GenericLinkedList<int>();
        }

        [Benchmark]
        public void AddNodes()
        {
            for (int i = 0; i < N; i++)
            {
                _list.AddIntNode(i);
            }
        }
    }
}