using SyntaxParser.Tests.Shared;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using SyntaxParser.Tests.Shared.Extensions;
using System.Text;
using static SyntaxParser.Tests.Unit.SyntaxParser_AllApiCalls;

namespace SyntaxParser.Tests.Unit
{
    public class VectorSyntaxCases : UseCaseCollectionOf<SyntaxParserCase>
    {
        protected override List<SyntaxParserCase> UseCases => new List<SyntaxParserCase>
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


