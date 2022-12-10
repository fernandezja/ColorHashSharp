# ColorHashSharp Benchmark

### Benchmark NET6 & NET7

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1281/21H2)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]   : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2  [AttachedDebugger]
  .NET 6.0 : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
  .NET 7.0 : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


| Method |      Job |  Runtime |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------- |--------- |--------- |---------:|----------:|----------:|---------:|------:|--------:|-------:|----------:|------------:|
|    Hsl | .NET 6.0 | .NET 6.0 | 1.289 us | 0.0292 us | 0.0789 us | 1.263 us |  1.00 |    0.00 | 0.5360 |    2.2 KB |        1.00 |
|    Rgb | .NET 6.0 | .NET 6.0 | 1.296 us | 0.0256 us | 0.0639 us | 1.280 us |  1.00 |    0.08 | 0.5360 |    2.2 KB |        1.00 |
|    Hex | .NET 6.0 | .NET 6.0 | 1.435 us | 0.0284 us | 0.0433 us | 1.432 us |  1.09 |    0.07 | 0.5741 |   2.35 KB |        1.07 |
|    Hsl | .NET 7.0 | .NET 7.0 | 1.432 us | 0.0674 us | 0.1911 us | 1.372 us |  1.12 |    0.17 | 0.5360 |    2.2 KB |        1.00 |
|    Rgb | .NET 7.0 | .NET 7.0 | 1.497 us | 0.0678 us | 0.1890 us | 1.448 us |  1.15 |    0.15 | 0.5360 |    2.2 KB |        1.00 |
|    Hex | .NET 7.0 | .NET 7.0 | 1.358 us | 0.0272 us | 0.0381 us | 1.350 us |  1.03 |    0.07 | 0.5741 |   2.35 KB |        1.07 |