using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using SyntaxParser.Tests.Shared.Util;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SyntaxParser.Tests.Core.SingleDelimiter
{
    public partial class SingleDelimiterSinglePropertySyntaxCases
    {
        [Theory]
        [ClassData(typeof(SingleDelimiterSinglePropertySyntaxCases))]
        public async Task ParseText(SinglePropertySyntaxCase @case)
        {
            var result = SyntaxParser.ParseText<SingleDelimiterSinglePropertySyntax>(@case.Input.Content);
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(SingleDelimiterSinglePropertySyntaxCases))]
        public async Task ParseFile(SinglePropertySyntaxCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = SyntaxParser.ParseFile<SingleDelimiterSinglePropertySyntax>(file.Path);
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(SingleDelimiterSinglePropertySyntaxCases))]
        public async Task ParseFileAsync_SingleDelimiterSinglePropertySyntax(SinglePropertySyntaxCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = new List<SingleDelimiterSinglePropertySyntax>();
            await foreach (var partialResult in SyntaxParser.ParseFileAsync<SingleDelimiterSinglePropertySyntax>(file.Path))
            {
                result.Add(partialResult);
            }
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(SingleDelimiterSinglePropertySyntaxCases))]
        public async Task ParseFileToJson(SinglePropertySyntaxCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = SyntaxParser.ParseFileToJson<SingleDelimiterSinglePropertySyntax>(file.Path);
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }
    }
}
