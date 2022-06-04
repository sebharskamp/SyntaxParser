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
    public partial class StringExtensionsTests
    {
        public class StringToArrayCase<T>: PrimaryTestCase<T> { }

        public class StringToStringArrayCases : UseCaseCollectionOf<StringToArrayCase<string>>
        {
            protected override List<StringToArrayCase<string>> UseCases => new List<StringToArrayCase<string>>
            {
                new StringToArrayCase<string>
                {
                    Input =  new PrimaryTestCaseInput
                        { Text = "[0,1]",
                    DelimeterSequence = new[] { "," },
                    SequenceOptions = SequenceOptions.Naive,
                    StartIndex = 1,
                    FinalIndex = 1
                    },
                    Expected = new[] { "0", "1" }
                }
            };
        }

        public class StringToIntArrayCases : UseCaseCollectionOf<StringToArrayCase<int>>
        {
            protected override List<StringToArrayCase<int>> UseCases => new List<StringToArrayCase<int>>
            {
                new StringToArrayCase<int>
                {
                    Input =  new PrimaryTestCaseInput
                        { Text = "[0,1]",
                    DelimeterSequence = new[] { "," },
                    SequenceOptions = SequenceOptions.Naive,
                    StartIndex = 1,
                    FinalIndex = 1
                    },
                    Expected = new[] { 0, 1 }
                }
            };
        }

        public class StringToDoubleArrayCases : UseCaseCollectionOf<StringToArrayCase<double>>
        {
            protected override List<StringToArrayCase<double>> UseCases => new List<StringToArrayCase<double>>
            {
                new StringToArrayCase<double>
                {
                    Input =  new PrimaryTestCaseInput
                        { Text = "[0.2,1.0]",
                    DelimeterSequence = new[] { "," },
                    SequenceOptions = SequenceOptions.Naive,
                    StartIndex = 1,
                    FinalIndex = 1
                    },
                    Expected = new[] { 0.2, 1.0 }
                }
            };
        }

    }
}
