using SyntaxParser.Tests.Shared;
using System.Collections.Generic;
using static SyntaxParser.Tests.Unit.SyntaxParser_AllApiCalls;

namespace SyntaxParser.Tests.Unit
{
    public class AdressSyntaxCases : UseCaseCollectionOf<SyntaxParserCase>
    {
        protected override List<SyntaxParserCase> UseCases => new List<SyntaxParserCase>
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


