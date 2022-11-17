using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using System;
using System.Collections.Generic;

namespace SyntaxParser.Tests.Core.SingleDelimiter
{
    public partial class CsvSyntaxCases : UseCaseCollectionOf<CsvSyntaxCase>
    {
        protected override List<CsvSyntaxCase> UseCases => new()
        {
            new CsvSyntaxCase
            {
                Input = new SingleDelimiterSyntaxCaseInput
                {
                    Content = "John Doe;39;24-3-2022 00:00:00" + Environment.NewLine + "Cloe Doe;38;24-3-2022 00:00:00",
                    Delimiter = ";"
                },
                Expected = new CsvSyntax[]
                {
                    new CsvSyntax
                    {
                        Name = "John Doe",
                        Age = 39,
                        SubscriptionDate = DateTime.Parse("24-3-2022 00:00:00")
                    },
                    new CsvSyntax
                    {
                        Name = "Cloe Doe",
                        Age = 38,
                        SubscriptionDate = DateTime.Parse("24-3-2022 00:00:00")
                    }
                }
            }
        };
    }
}
