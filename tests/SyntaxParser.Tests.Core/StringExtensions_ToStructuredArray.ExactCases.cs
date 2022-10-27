using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SyntaxParser;
using System.Collections.Generic;
using System.Collections;
using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;

namespace SyntaxParser.Tests.Core
{
    public partial class StringExtensions_ToStructuredArray
    {

        public class ExactCase : ToStructuredArrayTestCase<string> { }
        public class StringArrayExactCases : UseCaseCollectionOf<ExactCase>
        {
            protected override List<ExactCase> UseCases => new List<ExactCase>
            {
                new ExactCase
                {
                    Input = new ToStructuredArrayInput
                    {
                        Text = "what=>if=>else",
                        Parameters = new(){
                            DelimeterSequence = new[] { "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Exact,
                            StartIndex = 0,
                            FinalIndex = 0
                        }
                    },
                    Expected = new[] { "what", "if", "else" }
                },
                new ExactCase
                {
                    Input = new ToStructuredArrayInput
                    {
                        Text = $"what=>if=>else{Environment.NewLine}it=>has=>newLine",
                        Parameters = new(){
                            DelimeterSequence = new[] { "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Exact,
                            StartIndex = 0,
                            FinalIndex = 0
                        }
                    },
                    Expected = new[] { "what", "if", "else", "it", "has", "newLine" }
                },
            };
        }
    }
}
