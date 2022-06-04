using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SyntaxParser;
using System.Collections.Generic;
using System.Collections;
using SyntaxParser.Tests.Shared;

namespace SyntaxParser.Tests.Unit
{
    public partial class StringExtensionsTests { 

        public class ExactCase: PrimaryTestCase<string> { }
        public class ToStructuredArrayExactCases : UseCaseCollectionOf<ExactCase>
        {
            protected override List<ExactCase> UseCases => new List<ExactCase>
            {
                new ExactCase
                {
                    Input = new PrimaryTestCaseInput
                    {
                        Text = "what=>if=>else",
                        DelimeterSequence = new[] { "=>" },
                        SequenceOptions = SequenceOptions.Exact
                    ,
                    StartIndex = 0,
                    FinalIndex = 0},
                    Expected = new[] { "what", "if", "else" }
                },
                new ExactCase
                {
                    Input = new PrimaryTestCaseInput
                    {
                        Text = $"what=>if=>else{Environment.NewLine}it=>has=>newLine",
                        DelimeterSequence = new[] { "=>" },
                        SequenceOptions = SequenceOptions.Exact
                    ,
                    StartIndex = 0,
                    FinalIndex = 0},
                    Expected = new[] { "what", "if", "else", "it", "has", "newLine" }
                },
            };
        }
    }
}
