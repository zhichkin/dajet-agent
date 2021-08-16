using BenchmarkDotNet.Running;
using System;

namespace DaJet.Benchmarks
{
    public static class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<SqlStreamingBenchmark>();
            _ = Console.ReadKey(false);
        }
    }
}