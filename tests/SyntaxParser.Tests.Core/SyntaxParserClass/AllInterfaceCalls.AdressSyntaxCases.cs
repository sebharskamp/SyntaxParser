using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;
using System.Collections.Generic;

namespace SyntaxParser.Tests.Core.SyntaxParserClass
{
    public partial class AllInterfaceCalls
    {
        public class AdressSyntaxCases : UseCaseCollectionOf<SyntaxParserCase>
        {
            protected override List<SyntaxParserCase> UseCases => new()
        {
                new SyntaxParserCase
                {
                    Input = "1111@AA#23",
                    Expected = new Dynamic(new AdressSyntax[]
                        { new AdressSyntax
                            {
                                RegionCode = 1111,
                                NeighbourhoodCode = "AA",
                                Number = "23"
                            }
                        })
                }
            };
        }
    }

}


