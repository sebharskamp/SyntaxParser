// See https://aka.ms/new-console-template for more information
using Benchmark;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>();


/*var benchy = new Benchmarks();
for (int i = 0; i < 10000; i++)
{
    benchy.ParseFileStringBuilder();
}*/