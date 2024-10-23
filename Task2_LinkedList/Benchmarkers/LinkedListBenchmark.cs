using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.dotMemory;
using Task2_LinkedList.LinkedLists;

namespace Task2_LinkedList.Benchmarkers
{
    
    // Benchmarking tool used: BenchmarkDotNet
    // Overview | BenchmarkDotNet. (n.d.). Retrieved October 9, 2024, from https://benchmarkdotnet.org/articles/overview.html
    // using JetBrains DotMemoryDiagnoser to track memory usage
    [DotMemoryDiagnoser]
    public class LinkedListBenchmark
    {
        private SimpleLinkedList _simpleList;
        private IntDoublyLinkedList _intList;
        private GenericDoublyLinkedList<int> _genericList;

        // Boolean variable to control the sentinel usage
        private bool useSentinel = false;

        [Params(500, 5000, 50000)] // Different sizes for benchmarking
        public int N;

        [GlobalSetup(Target = nameof(BenchmarkSimpleListAppend))]
        public void SetupSimpleListAppend()
        {
            _simpleList = new SimpleLinkedList(useSentinel);
        }

        [GlobalSetup(Target = nameof(BenchmarkSimpleListInsert))]
        public void SetupSimpleListInsert()
        {
            _simpleList = new SimpleLinkedList(useSentinel);
        }

        [GlobalSetup(Target = nameof(BenchmarkIntListAppend))]
        public void SetupIntListAppend()
        {
            _intList = new IntDoublyLinkedList(useSentinel);
        }

        [GlobalSetup(Target = nameof(BenchmarkIntListInsert))]
        public void SetupIntListInsert()
        {
            _intList = new IntDoublyLinkedList(useSentinel);
        }

        [GlobalSetup(Target = nameof(BenchmarkGenericListAppend))]
        public void SetupGenericListAppend()
        {
            _genericList = new GenericDoublyLinkedList<int>(useSentinel);
        }

        [GlobalSetup(Target = nameof(BenchmarkGenericListInsert))]
        public void SetupGenericListInsert()
        {
            _genericList = new GenericDoublyLinkedList<int>(useSentinel);
        }

        [Benchmark]
        public void BenchmarkSimpleListAppend()
        {
            for (int i = 0; i < N; i++)
            {
                _simpleList.AppendNode(i);
            }
        }

        [Benchmark]
        public void BenchmarkSimpleListInsert()
        {
            for (int i = 0; i < N; i++)
            {
                _simpleList.InsertNode(i);
            }
        }

        [Benchmark]
        public void BenchmarkIntListAppend()
        {
            for (int i = 0; i < N; i++)
            {
                _intList.AppendNode(i);
            }
        }

        [Benchmark]
        public void BenchmarkIntListInsert()
        {
            for (int i = 0; i < N; i++)
            {
                _intList.InsertNode(i);
            }
        }

        [Benchmark]
        public void BenchmarkGenericListAppend()
        {
            for (int i = 0; i < N; i++)
            {
                _genericList.AppendNode(i);
            }
        }

        [Benchmark]
        public void BenchmarkGenericListInsert()
        {
            for (int i = 0; i < N; i++)
            {
                _genericList.InsertNode(i);
            }
        }
    }
}
