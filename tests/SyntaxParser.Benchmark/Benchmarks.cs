using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.Extensions;
using System.Text.Json;

namespace Benchmark
{
    [MemoryDiagnoser]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.FullCompressed]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmarks
    {
        private string _syntaxText;
        private string _jsonText;

        [Params(@"./instructions-small", @"./instructions-large")]
        public string FilePath { get; set; } = @"./instructions-large";

        [GlobalSetup]
        public void Setup()
        {
            _syntaxText = File.ReadAllText($"{FilePath}.txt");
            _jsonText = File.ReadAllText($"{FilePath}.json");
        }



        [Benchmark]
        public void ParseText()
        {
            var result = SyntaxParser.SyntaxParser.ParseText<MoveSyntax>(_syntaxText);
        }

        public void JsonConverterText()
        {
            var result = JsonSerializer.Deserialize<MoveSyntax[]>(_jsonText);
        }




        public void ParseFile()
        {
            var result = SyntaxParser.SyntaxParser.ParseFile<MoveSyntax>($"{FilePath}.txt");
        }

        public void JsonConverterFile()
        {
            using var stream = new StreamReader($"{FilePath}.json");
            var result = JsonSerializer.Deserialize<MoveSyntax[]>(stream.BaseStream);
        }



        public async Task ParseFileAsync()
        {
            var result = await SyntaxParser.SyntaxParser.ParseFileAsync<MoveSyntax>($"{FilePath}.txt").ToListAsync();
        }


        public async Task JsonConverterFileAsync()
        {
            using var stream = new StreamReader($"{FilePath}.json");
            var result = await JsonSerializer.DeserializeAsync<MoveSyntax[]>(stream.BaseStream);
        }




        public async Task ParseFileToJsonAsync()
        {
            var result = await SyntaxParser.SyntaxParser.ParseFileToJsonAsync<MoveSyntax>($"{FilePath}.txt");
        }


        public void ParseFileToJson()
        {
            var result = SyntaxParser.SyntaxParser.ParseFileToJson<MoveSyntax>($"{FilePath}.txt");
        }
    }
}
