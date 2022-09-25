using SyntaxParser.Tests.Shared;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Text.Json;
using SyntaxParser.Core.Extensions;
using System.IO;
using SyntaxParser.Tests.Unit.UseCaseFramework;

namespace SyntaxParser.Tests.Unit
{
    public partial class SyntaxParser_AllApiCalls
    {

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public void ParseText(SyntaxParserCase @case)
        {
            var methodInfo = typeof(SyntaxParser).GetMethod(nameof(SyntaxParser.ParseText), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var genericArguments = new[] { @case.Expected.Type.GetElementType() };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            var result = genericMethodInfo?.Invoke(null, new object[] { @case.Input });
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public async Task ParseFile(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var methodInfo = typeof(SyntaxParser).GetMethod(nameof(SyntaxParser.ParseFile), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var genericArguments = new[] { @case.Expected.Type.GetElementType() };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            var result = genericMethodInfo?.Invoke(null, new object[] { file.Path });
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
            var methodInfo = typeof(SyntaxParser).GetMethod(nameof(SyntaxParser.ParseFileToJson), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var genericArguments = new[] { @case.Expected.Type.GetElementType() };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            var result = genericMethodInfo?.Invoke(null, new object[] { file.Path });
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxCases))]
        [ClassData(typeof(VectorSyntaxCases))]
        [ClassData(typeof(AdressSyntaxCases))]
        public async Task ParseFileToJsonAsync(SyntaxParserCase @case)
        {
            using var file = await TemporaryFile.InitializeAsync(@case.Input);
            var methodInfo = typeof(SyntaxParser).GetMethod(nameof(SyntaxParser.ParseFileToJsonAsync), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var genericArguments = new[] { @case.Expected.Type.GetElementType() };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            var result = await genericMethodInfo?.InvokeAsync(null, new object[] { file.Path });
            @case.IsResultAsExpected(result, parse: expected => JsonSerializer.Serialize(expected));
        }

        public class Input
        {
            public string Text { get; init; }
        }

        public class SyntaxParserCase : DynamicExpectedTypeUseCase<string>
        {
            public override Dynamic Expected { get; set; }
            public override string Input { get; set; }
        }
    }

}


