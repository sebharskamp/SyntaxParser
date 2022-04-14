using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SyntaxParser.Benchmark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmarks
    {
        private SyntaxParser<MoveSyntax> _parser;
        private SyntaxParser.Core.SyntaxParser<MoveSyntax> _coreParser;
        [Params(@"./instructions-small", @"./instructions-large")]
        public string FilePath { get; set; } = @"./instructions-large";

        public Benchmarks()
        {
            _parser = new SyntaxParser<MoveSyntax>();
            _coreParser = new SyntaxParser.Core.SyntaxParser<MoveSyntax>();
        }

        [Benchmark]
        public void AllText()
        {
            var text = File.ReadAllText($"{FilePath}.txt");
            var result = _parser.Parse(text);
        }

        [Benchmark]
        public void ParseLines()
        {
            var result = _parser.ParseLines($"{FilePath}.txt");
        }

        [Benchmark]
        public void ParseFile()
        {
            var result = _parser.ParseFile($"{FilePath}.txt");
        }

        [Benchmark]
        public void ParseExpression()
        {
            var result = _coreParser.Parse($"{FilePath}.txt");
        }

        [Benchmark]
        public async Task ParseExpressionAsync()
        {
            var result = await _coreParser.ParseAsync($"{FilePath}.txt").ToListAsync();
        }

        [Benchmark]
        public async Task JsonConverterAsync()
        {
            using var stream = new StreamReader($"{FilePath}.json");
            var result = await JsonSerializer.DeserializeAsync<MoveSyntax[]>(stream.BaseStream);
        }

        [Benchmark]
        public void JsonConverter()
        {
            var text = File.ReadAllText($"{FilePath}.json");
            var result = JsonSerializer.Deserialize<MoveSyntax[]>(text);
        }

        [Benchmark]
        public async Task ToJsonAync()
        {
            var result = await _coreParser.ToJsonAsync($"{FilePath}.txt");
        }

        [Benchmark]
        public void ToJson()
        {
            var result = _coreParser.ToJson($"{FilePath}.txt");
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
