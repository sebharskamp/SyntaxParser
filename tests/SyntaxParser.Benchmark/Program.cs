﻿// See https://aka.ms/new-console-template for more information
using Benchmark;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

/*var ben = new Benchmarks();
ben.AllText();*/

BenchmarkRunner.Run<Benchmarks>();