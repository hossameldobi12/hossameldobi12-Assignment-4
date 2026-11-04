# Benchmark Analysis

## Benchmark Results

These results were produced on my own machine using BenchmarkDotNet.

**Benchmark configuration**
- Benchmarking library: BenchmarkDotNet
- Memory diagnostics: `MemoryDiagnoser`
- Iteration counts: 100, 1,000, 10,000, and 100,000
- Test: Append the same text repeatedly using string concatenation and `StringBuilder`

| Method | Iterations | Mean | Error | StdDev | Median | Gen0 | Gen1 | Gen2 | Allocated |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| StringConcatenation | 100 | 3,432.4 ns | 254.59 ns | 750.68 ns | 3,726.2 ns | 6.3324 | 0.0153 | - | 51.73 KB |
| StringBuilderConcatenation | 100 | 202.4 ns | 3.93 ns | 4.21 ns | 202.0 ns | 0.2964 | 0.0007 | - | 2.42 KB |
| StringConcatenation | 1,000 | 134,663.0 ns | 2,657.33 ns | 4,723.40 ns | 133,915.1 ns | 601.0742 | 14.6484 | - | 4,912.08 KB |
| StringBuilderConcatenation | 1,000 | 1,522.6 ns | 21.77 ns | 20.36 ns | 1,522.7 ns | 3.2463 | 0.2022 | - | 26.49 KB |
| StringConcatenation | 10,000 | 17,872,187.9 ns | 178,600.87 ns | 158,325.02 ns | 17,829,209.4 ns | 85,406.2500 | 50,593.7500 | 43,593.7500 | 488,588.48 KB |
| StringBuilderConcatenation | 10,000 | 48,217.1 ns | 894.22 ns | 836.45 ns | 47,887.6 ns | 31.1890 | 31.1890 | 31.1890 | 208.56 KB |
| StringConcatenation | 100,000 | 3,941,239,100.0 ns | 76,113,224.66 ns | 67,472,391.17 ns | 3,939,347,000.0 ns | 11,315,000.0000 | 11,275,000.0000 | 11,268,000.0000 | 48,834,514.64 KB |
| StringBuilderConcatenation | 100,000 | 487,157.4 ns | 4,286.78 ns | 3,800.11 ns | 487,357.3 ns | 249.0234 | 249.0234 | 249.0234 | 1,966.48 KB |

## Analysis Questions

### 1. Which approach was faster with 100 iterations?

`StringBuilderConcatenation` was faster. Its mean was 202.4 ns, compared with 3,432.4 ns for `StringConcatenation`.

### 2. Which approach was faster with 100,000 iterations?

`StringBuilderConcatenation` was much faster. Its mean was 487,157.4 ns (about 0.487 ms), compared with 3,941,239,100.0 ns (about 3.94 seconds) for `StringConcatenation`.

### 3. Which approach allocated more memory?

`StringConcatenation` allocated more memory at every tested iteration count. At 100,000 iterations, it allocated 48,834,514.64 KB, while `StringBuilderConcatenation` allocated 1,966.48 KB.

### 4. What happened to string concatenation performance as the loop size increased?

String concatenation became dramatically slower as the iteration count increased. Its mean rose from 3,432.4 ns at 100 iterations to about 3.94 seconds at 100,000 iterations. `StringBuilder` also took longer with more iterations, but its increase was much smaller.

### 5. Why does repeated string concatenation create additional allocations?

Strings in C# are immutable, meaning their contents cannot be changed after creation. With `result += Text`, each iteration creates a new string containing the previous result and the appended text. This repeatedly allocates memory and copies the growing content.

### 6. Why does StringBuilder usually perform better when text is repeatedly appended?

`StringBuilder` builds text in a mutable buffer and can append many pieces without creating a new string for every append. This generally reduces repeated copying and memory allocations. The final string is created when `ToString()` is called.

### 7. Is StringBuilder always better than normal string operations? Explain.

No. For a few simple concatenations, normal string operations are often clearer and can be efficient enough. `StringBuilder` is usually a better choice when repeatedly appending text, especially in large loops. The appropriate choice depends on the amount of text and the use case.

## Conclusion

In this benchmark, `StringBuilderConcatenation` was faster and allocated much less memory at all four iteration counts. The performance and allocation gap became especially large as the loop size increased. These measurements reflect this machine and benchmark setup; results can differ on other systems.
