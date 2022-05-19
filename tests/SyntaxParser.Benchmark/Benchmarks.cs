using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SyntaxParser.Core.Extensions;
using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.Extensions;
using System.Text.Json;

namespace Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmarks
    {
        [Params(@"./instructions-small", @"./instructions-large")]
        public string FilePath { get; set; } = @"./instructions-large";

        [Benchmark]
        public void AllText()
        {
            var text = File.ReadAllText($"{FilePath}.txt");
            var result = SyntaxParser.SyntaxParser.ParseText<MoveSyntax>(text);
        }

        [Benchmark]
        public void ParseExpression()
        {
            var result = SyntaxParser.SyntaxParser.ParseFile<MoveSyntax>($"{FilePath}.txt");
        }
  

        public async Task ParseExpressionAsync()
        {
            var result = await SyntaxParser.SyntaxParser.ParseFileAsync<MoveSyntax>($"{FilePath}.txt").ToListAsync();
        }


        public async Task JsonConverterAsync()
        {
            using var stream = new StreamReader($"{FilePath}.json");
            var result = await JsonSerializer.DeserializeAsync<MoveSyntax[]>(stream.BaseStream);
        }


        public void JsonConverter()
        {
            var text = File.ReadAllText($"{FilePath}.json");
            var result = JsonSerializer.Deserialize<MoveSyntax[]>(text);
        }


        public async Task ToJsonAync()
        {
            var result = await SyntaxParser.SyntaxParser.ParseFileToJsonAsync<MoveSyntax>($"{FilePath}.txt");
        }


        public void ToJson()
        {
            var result = SyntaxParser.SyntaxParser.ParseFileToJson<MoveSyntax>($"{FilePath}.txt");
        }
    }
}
