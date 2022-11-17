using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser.Tests.Core.SingleDelimiter
{
    public partial class SingleDelimiterSyntaxCases 
    {
        public class SinglePropertyUseCases : UseCaseCollectionOf<SinglePropertyCase>
        {
            protected override List<SinglePropertyCase> UseCases => new()
        {
             new SinglePropertyCase
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
            }, new SinglePropertyCase
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
             new SinglePropertyCase
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
              new SinglePropertyCase
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
}
