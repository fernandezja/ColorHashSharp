using BenchmarkDotNet.Running;
using ColorHashSharp.Benchmarks;

Console.WriteLine("ColorHashSharp benchmarks!");

_ = BenchmarkRunner.Run<ColorHashSharpBenchmarks>();

Console.ReadKey();