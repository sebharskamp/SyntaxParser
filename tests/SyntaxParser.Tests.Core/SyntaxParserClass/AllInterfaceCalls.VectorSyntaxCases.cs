using SyntaxParser.Tests.Shared;
using System.Collections.Generic;
using SyntaxParser.Tests.Shared.UseCaseFramework;

namespace SyntaxParser.Tests.Core.SyntaxParserClass
{
    public partial class AllInterfaceCalls
    {
        public class VectorSyntaxCases : UseCaseCollectionOf<SyntaxParserCase>
        {
            protected override List<SyntaxParserCase> UseCases => new()
            {
                new SyntaxParserCase
                {
                    Input = "[0.1, 2.2]=>[0.4, 1]",
                    Expected = new Dynamic(new VectorSyntax[]
                        { new VectorSyntax
                            {
                                PointA = new []{0.1, 2.2},
                                PointB = new []{0.4, 1}
                            }
                        })
                }
            };
        }
    }
}


