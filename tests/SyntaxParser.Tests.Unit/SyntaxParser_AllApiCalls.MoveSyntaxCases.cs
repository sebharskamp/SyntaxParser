using SyntaxParser.Tests.Shared;
using System.Collections.Generic;
using System;

namespace SyntaxParser.Tests.Unit
{
    public partial class SyntaxParser_AllApiCalls
    {
        public class MoveSyntaxCases : UseCaseCollectionOf<SyntaxParserCase>
        {
            protected override List<SyntaxParserCase> UseCases => new List<SyntaxParserCase>
            {
                new SyntaxParserCase
                {
                    Input = "A=>B",
                    Expected = new Dynamic(new MoveSyntax[]{
                        new MoveSyntax
                            {
                                From = "A",
                                To = "B"
                            }
                        })
                    },
                new SyntaxParserCase
                {
                    Input = $"A=>B{Environment.NewLine}C=>D",
                    Expected = new Dynamic(
                        new MoveSyntax[]
                        {
                            new MoveSyntax
                            {
                                From = "A",
                                To = "B"
                            },
                            new MoveSyntax
                            {
                                From = "C",
                                To = "D"
                            }
                    })
                }
            };

        }
    }

}


