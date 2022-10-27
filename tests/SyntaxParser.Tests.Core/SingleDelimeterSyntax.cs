using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using SyntaxParser.Tests.Shared.Util;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SyntaxParser.Tests.Core
{
    public class SingleDelimeterSyntax
    {
        [Theory]
        [ClassData(typeof(CsvSyntaxCases))]
        public async Task ParseText_CsvSyntax(CsvSyntaxCase @case)
        {
            var result = SyntaxParser.ParseText<CsvSyntax>(@case.Input.Content);
            @case.IsResultAsExpected(result);
        }

        [Theory]
        [ClassData(typeof(CsvSyntaxCases))]
        public async Task ParseFile_CsvSyntax(CsvSyntaxCase @case)
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

    public class CsvSyntaxCases : UseCaseCollectionOf<CsvSyntaxCase>
    {
        protected override List<CsvSyntaxCase> UseCases => new()
        {
            new CsvSyntaxCase
            {
                Input = new CsvSyntaxCaseInput
                {
                    Content = "John Doe;39;24-03-2022" + Environment.NewLine + "Cloe Doe;38;16-03-2022",
                    Delimiter = ";"
                },
                Expected = new CsvSyntax[]
                {
                    new CsvSyntax
                    {
                        Name = "John Doe",
                        Age = 39,
                        SubscriptionDate = DateTime.Parse("24-03-2022")
                    },
                    new CsvSyntax
                    {
                        Name = "Cloe Doe",
                        Age = 38,
                        SubscriptionDate = DateTime.Parse("16-03-2022")
                    }
                }
            }
        };
    }

    public class CsvSyntaxCaseInput
    {
        public string Delimiter { get; set; }
        public string Content { get; set; }
    }
        


    public class CsvSyntaxCase : UseCase<CsvSyntaxCaseInput, CsvSyntax[]>
    {
        public override CsvSyntaxCaseInput Input { get; set; }
        public override CsvSyntax[] Expected { get; set; }
    }
}
