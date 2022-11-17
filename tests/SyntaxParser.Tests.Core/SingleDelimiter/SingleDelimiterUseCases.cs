using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser.Tests.Core.SingleDelimiter
{
    public partial class SingleDelimiterSinglePropertySyntaxCases : UseCaseCollectionOf<SinglePropertySyntaxCase>
    {
        protected override List<SinglePropertySyntaxCase> UseCases => new()
        {
             new SinglePropertySyntaxCase
            {
                Input = new SingleDelimiterSyntaxCaseInput
                {
                    Content = "John Doe",
                    Delimiter = ";"
                },
                Expected = new SingleDelimiterSinglePropertySyntax[]
                {
                    new SingleDelimiterSinglePropertySyntax
                    {
                        Name = "John Doe"
                    }
                }
            }, new SinglePropertySyntaxCase
            {
                Input = new SingleDelimiterSyntaxCaseInput
                {
                    Content = "John Doe" + Environment.NewLine + "Cloe Doe",
                    Delimiter = ";"
                },
                Expected = new SingleDelimiterSinglePropertySyntax[]
                {
                    new SingleDelimiterSinglePropertySyntax
                    {
                        Name = "John Doe"
                    },
                    new SingleDelimiterSinglePropertySyntax
                    {
                        Name = "Cloe Doe"
                    },
                }
            },
             new SinglePropertySyntaxCase
            {
                Input = new SingleDelimiterSyntaxCaseInput
                {
                    Content = "John Doe;" + Environment.NewLine + "Cloe Doe;",
                    Delimiter = ";"
                },
                Expected = new SingleDelimiterSinglePropertySyntax[]
                {
                    new SingleDelimiterSinglePropertySyntax
                    {
                        Name = "John Doe"
                    },
                    new SingleDelimiterSinglePropertySyntax
                    {
                        Name = "Cloe Doe"
                    },
                }
             },
              new SinglePropertySyntaxCase
            {
                Input = new SingleDelimiterSyntaxCaseInput
                {
                    Content = "John Doe" + Environment.NewLine + "Cloe Doe;",
                    Delimiter = ";"
                },
                Expected = new SingleDelimiterSinglePropertySyntax[]
                {
                    new SingleDelimiterSinglePropertySyntax
                    {
                        Name = "John Doe"
                    },
                    new SingleDelimiterSinglePropertySyntax
                    {
                        Name = "Cloe Doe"
                    },
                }
             }
        };
    }
}
