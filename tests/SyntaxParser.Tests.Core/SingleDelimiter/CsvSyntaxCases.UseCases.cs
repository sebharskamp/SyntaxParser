using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using System;
using System.Collections.Generic;

namespace SyntaxParser.Tests.Core.SingleDelimiter
{
    public partial class CsvSyntaxCases : UseCaseCollectionOf<CsvSyntaxCases.CsvSyntaxCase>
    {
        protected override List<CsvSyntaxCase> UseCases => new()
        {
            new CsvSyntaxCase
            {
                Input = new SingleDelimiterSyntaxCaseInput
                {
                    Content = "John Doe;39" + Environment.NewLine + "Cloe Doe;38",
                    Delimiter = ";"
                },
                Expected = new CsvSyntax[]
                {
                    new CsvSyntax
                    {
                        Name = "John Doe",
                        Age = 39,
                    },
                    new CsvSyntax
                    {
                        Name = "Cloe Doe",
                        Age = 38
                    }
                }
            }
        };
    }
}
