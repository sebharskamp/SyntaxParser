using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SyntaxParser.Benchmark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmarks
    {
        private SyntaxParser<MoveSyntax> _parser;
        private SyntaxParser.Core.SyntaxParser<MoveSyntax> _coreParser;
        [Params(@"./instructions-small.txt", @"./instructions-large.txt")]
        public string FilePath { get; set; }

        public Benchmarks()
        {
            _parser = new SyntaxParser<MoveSyntax>();
            _coreParser = new SyntaxParser.Core.SyntaxParser<MoveSyntax>();
        }

        [Benchmark]
        public void AllText()
        {
            var text = File.ReadAllText(FilePath);
            var result = _parser.Parse(text);
        }

        [Benchmark]
        public void ParseLines()
        {
            var result = _parser.ParseLines(FilePath);
        }

        [Benchmark]
        public void ParseFile()
        {
            var result = _parser.ParseFile(FilePath);
        }

        [Benchmark]
        public void ParseExpression()
        {
            var result = _coreParser.Parse(FilePath);
        }

        [Benchmark]
        public async Task ParseExpressionAsync()
        {
            var result = await _coreParser.ParseAsync(FilePath).ToListAsync();
        }
    }

    public static class AsyncEnumerableExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items,
            CancellationToken cancellationToken = default)
        {
            var results = new List<T>();
            await foreach (var item in items.WithCancellation(cancellationToken)
                                            .ConfigureAwait(false))
                results.Add(item);
            return results;
        }
    }
}
