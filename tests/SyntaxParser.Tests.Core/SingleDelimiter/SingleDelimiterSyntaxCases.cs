using SyntaxParser.Core;
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
    public partial class SingleDelimiterSyntaxCases
    {
        [Theory]
        [ClassData(typeof(SinglePropertyUseCases))]
        public async Task ParseText(SinglePropertyCase @case)
        {
            var result = SyntaxParser.ParseText<SingleDelimiterSinglePropertySyntax>(@case.Input.Content);
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(SinglePropertyUseCases))]
        public async Task ParseFile(SinglePropertyCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = SyntaxParser.ParseFile<SingleDelimiterSinglePropertySyntax>(file.Path);
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(SinglePropertyUseCases))]
        public async Task ParseFileAsync_SingleDelimiterSinglePropertySyntax(SinglePropertyCase @case)
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
        [ClassData(typeof(SinglePropertyUseCases))]
        public async Task ParseFileToJson(SinglePropertyCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input.Content);
            var result = SyntaxParser.ParseFileToJson<SingleDelimiterSinglePropertySyntax>(file.Path);
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }

        public class SingleDelimiterSyntaxCaseInput
        {
            public string Delimiter { get; set; }
            public string Content { get; set; }
        }

        public class SinglePropertyCase : UseCase<SingleDelimiterSyntaxCaseInput, SingleDelimiterSinglePropertySyntax[]>
        {
            public override SingleDelimiterSyntaxCaseInput Input { get; set; }
            public override SingleDelimiterSinglePropertySyntax[] Expected { get; set; }

            public override Type Contract => typeof(SyntaxParser);
        }
    }
}
