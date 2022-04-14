using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SyntaxParser.Benchmark;
using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.Extensions;
using System.Text.Json;

namespace Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmarks
    {
        private SyntaxParser<MoveSyntax> _parser;
        private SyntaxParser.SyntaxParser<MoveSyntax> _coreParser;
        [Params(@"./instructions-small", @"./instructions-large")]
        public string FilePath { get; set; } = @"./instructions-large";

        public Benchmarks()
        {
            _parser = new SyntaxParser<MoveSyntax>();
            _coreParser = new SyntaxParser.SyntaxParser<MoveSyntax>();
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
            var result = _coreParser.ParseFile($"{FilePath}.txt");
        }

        [Benchmark]
        public async Task ParseExpressionAsync()
        {
            var result = await _coreParser.ParseFileAsync($"{FilePath}.txt").ToListAsync();
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
            var result = await _coreParser.ParseFileToJsonAsync($"{FilePath}.txt");
        }

        [Benchmark]
        public void ToJson()
        {
            var result = _coreParser.ParseFileToJson($"{FilePath}.txt");
        }
    }
}
