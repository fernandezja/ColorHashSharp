using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorHashSharp.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class ColorHashSharpBenchmarks
    {
        private const string STRING_TO_HASH = "The Force will be with you always";
        
        private ColorHash _colorHash = new();


        [Benchmark(Baseline = true)]
        public void Hsl()
        {
            _ = _colorHash.Hsl(STRING_TO_HASH);
        }

        [Benchmark]
        public void Rgb()
        {
            _ = _colorHash.Rgb(STRING_TO_HASH);
        }

        [Benchmark]
        public void Hex()
        {
            _ = _colorHash.Hex(STRING_TO_HASH);
        }
    }
}
