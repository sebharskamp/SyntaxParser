using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using SyntaxParser.Tests.Shared.Util;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SyntaxParser.Tests.Core.SingleDelimiter
{
    public partial class CsvSyntaxCases
    {
        [Theory]
        [ClassData(typeof(CsvSyntaxCases))]
        public async Task ParseText(CsvSyntaxCase @case)
        {
            var result = SyntaxParser.ParseText<CsvSyntax>(@case.Input.Content);
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(CsvSyntaxCases))]
        public async Task ParseFile(CsvSyntaxCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = SyntaxParser.ParseFile<CsvSyntax>(file.Path);
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(CsvSyntaxCases))]
        public async Task ParseFileAsync_CsvSyntax(CsvSyntaxCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = new List<CsvSyntax>();
            await foreach (var partialResult in SyntaxParser.ParseFileAsync<CsvSyntax>(file.Path))
            {
                result.Add(partialResult);
            }
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(CsvSyntaxCases))]
        public async Task ParseFileToJson(CsvSyntaxCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = SyntaxParser.ParseFileToJson<CsvSyntax>(file.Path);
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }
    }



    public class CsvSyntaxCase : UseCase<SingleDelimiterSyntaxCaseInput, CsvSyntax[]>
    {
        public override SingleDelimiterSyntaxCaseInput Input { get; set; }
        public override CsvSyntax[] Expected { get; set; }
    }
}
