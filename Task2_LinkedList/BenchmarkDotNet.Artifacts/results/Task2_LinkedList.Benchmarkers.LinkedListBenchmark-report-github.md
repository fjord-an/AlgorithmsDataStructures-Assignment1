```

BenchmarkDotNet v0.14.0, Arch Linux
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.108
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2


```
| Method                     | N     | Mean              | Error             | StdDev             |
|--------------------------- |------ |------------------:|------------------:|-------------------:|
| **BenchmarkSimpleListAppend**  | **500**   |   **1,954,450.58 μs** |    **189,375.521 μs** |     **558,378.038 μs** |
| BenchmarkSimpleListInsert  | 500   |          34.66 μs |          0.521 μs |           0.487 μs |
| BenchmarkIntListAppend     | 500   |          40.47 μs |          0.473 μs |           0.443 μs |
| BenchmarkIntListInsert     | 500   |          42.28 μs |          0.328 μs |           0.307 μs |
| BenchmarkGenericListAppend | 500   |          40.63 μs |          0.534 μs |           0.499 μs |
| BenchmarkGenericListInsert | 500   |          40.56 μs |          0.339 μs |           0.317 μs |
| **BenchmarkSimpleListAppend**  | **5000**  |  **18,354,080.84 μs** |  **1,773,679.502 μs** |   **5,229,734.411 μs** |
| BenchmarkSimpleListInsert  | 5000  |         356.16 μs |          4.798 μs |           4.488 μs |
| BenchmarkIntListAppend     | 5000  |         408.26 μs |          7.214 μs |           6.748 μs |
| BenchmarkIntListInsert     | 5000  |         421.51 μs |          5.459 μs |           5.106 μs |
| BenchmarkGenericListAppend | 5000  |         405.21 μs |          7.262 μs |           6.438 μs |
| BenchmarkGenericListInsert | 5000  |         406.15 μs |          6.022 μs |           5.633 μs |
| **BenchmarkSimpleListAppend**  | **50000** | **612,113,892.62 μs** | **59,175,079.165 μs** | **174,479,068.712 μs** |
| BenchmarkSimpleListInsert  | 50000 |                NA |                NA |                 NA |
| BenchmarkIntListAppend     | 50000 |       4,063.07 μs |         75.675 μs |          70.787 μs |
| BenchmarkIntListInsert     | 50000 |       4,272.61 μs |         82.241 μs |          76.928 μs |
| BenchmarkGenericListAppend | 50000 |       4,072.30 μs |         77.023 μs |          72.048 μs |
| BenchmarkGenericListInsert | 50000 |       4,048.48 μs |         51.130 μs |          47.827 μs |

Benchmarks with issues:
  LinkedListBenchmark.BenchmarkSimpleListInsert: DefaultJob [N=50000]
