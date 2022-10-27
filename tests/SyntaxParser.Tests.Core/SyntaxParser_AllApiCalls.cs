using SyntaxParser.Tests.Shared;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using System.Text.Json;
using SyntaxParser.Tests.Core;
using SyntaxParser.Tests.Shared.Util;
using SyntaxParser.Tests.Shared.UseCaseFramework;

namespace SyntaxParser.Tests.Core
{
    public partial class SyntaxParser_AllApiCalls
    {

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public void ParseText(SyntaxParserCase @case)
        {
            var result = @case.InvokeMethod(typeof(SyntaxParser), nameof(SyntaxParser.ParseText), new object[] { @case.Input });
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public void ParseTextToJson(SyntaxParserCase @case)
        {
            var result = @case.InvokeMethod(typeof(SyntaxParser), nameof(SyntaxParser.ParseTextToJson), new object[] { @case.Input });
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public async Task ParseFile(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var result = @case.InvokeMethod(typeof(SyntaxParser), nameof(SyntaxParser.ParseFile), new object[] { file.Path });
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        public async Task ParseFileAsync_MoveSyntax(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var result = new List<MoveSyntax>();
            await foreach (var partialResult in SyntaxParser.ParseFileAsync<MoveSyntax>(file.Path))
            {
                result.Add(partialResult);
            }
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(VectorSyntaxCases))]
        public async Task ParseFileAsync_VectorSyntax(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var result = new List<VectorSyntax>();
            await foreach (var partialResult in SyntaxParser.ParseFileAsync<VectorSyntax>(file.Path))
            {
                result.Add(partialResult);
            }
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(AdressSyntaxCases))]
        public async Task ParseFileAsync_AdressSyntax(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var result = new List<AdressSyntax>();
            await foreach (var partialResult in SyntaxParser.ParseFileAsync<AdressSyntax>(file.Path))
            {
                result.Add(partialResult);
            }
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public async Task ParseFileToJson(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var result = @case.InvokeMethod(typeof(SyntaxParser), nameof(SyntaxParser.ParseFileToJson), new object[] { file.Path });
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public async Task ParseFileToJsonAsync(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var result = await @case.InvokeMethodAsync(typeof(SyntaxParser), nameof(SyntaxParser.ParseFileToJsonAsync), new object[] { file.Path });
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }
    }

    public class SyntaxParserCase : DynamicExpectedTypeUseCase<string>
    {
        public override Dynamic Expected { get; set; }
        public override string Input { get; set; }
    }

}


