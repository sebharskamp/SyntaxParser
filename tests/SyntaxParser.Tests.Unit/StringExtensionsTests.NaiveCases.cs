using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SyntaxParser;
using System.Collections.Generic;
using System.Collections;
using SyntaxParser.Tests.Shared;
using static SyntaxParser.Tests.Unit.StringExtensionsTests;

namespace SyntaxParser.Tests.Unit
{
    public partial class StringExtensionsTests
    {
        public class NaiveCase<T> : PrimaryTestCase<T> { }
        public class ToStructuredStringArrayNaiveCases : UseCaseCollectionOf<NaiveCase<string>>
        {
            protected override List<NaiveCase<string>> UseCases => new List<NaiveCase<string>>
            {
                new NaiveCase<string>
                {
                    Input = new PrimaryTestCaseInput
                    {
                        Text = "hello=>world",
                    DelimeterSequence = new[] { "=>" },
                    SequenceOptions = SequenceOptions.Naive,
                    StartIndex = 0,
                    FinalIndex = 0},
                    Expected = new[] { "hello", "world" }
                },
                new NaiveCase<string>
                {
                    Input = new PrimaryTestCaseInput
                    {
                        Text = $"A=>B{Environment.NewLine}C=>D",
                    DelimeterSequence = new[] { "=>" },
                    SequenceOptions = SequenceOptions.Naive,
                    StartIndex = 0,
                    FinalIndex = 0},
                    Expected = new[] { "A", "B", "C", "D" }
                },
                new NaiveCase<string>
                {
                    Input = new PrimaryTestCaseInput
                    {
                        Text = "[ping, pong]=>[pong, ping]",
                    DelimeterSequence = new[] { "=>" },
                    SequenceOptions = SequenceOptions.Naive,
                    StartIndex = 0,
                    FinalIndex = 0},
                    Expected =new[] { "[ping, pong]", "[pong, ping]" }
                },
                new NaiveCase<string>
                {
                    Input = new PrimaryTestCaseInput
                    {
                        Text = "hop+step=>jump",
                    DelimeterSequence = new[] { "+", "=>" },
                    SequenceOptions = SequenceOptions.Naive,
                    StartIndex = 0,
                    FinalIndex = 0},
                    Expected = new[] { "hop", "step", "jump" }
                },
            };
        }

        public class ToStructuredCharArrayNaiveCases : UseCaseCollectionOf<NaiveCase<char>>
        {
            protected override List<NaiveCase<char>> UseCases => new List<NaiveCase<char>>
            {
                new NaiveCase<char>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "a+b=>c",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new[] { 'a', 'b', 'c' }
                }
            };
        }

        public class ToStructuredInt16ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<short>>
        {
            protected override List<NaiveCase<short>> UseCases => new List<NaiveCase<short>>
            {
                new NaiveCase<short>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "1+0=>10",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new short[] { 1, 0, 10 }
                }
            };
        }

        public class ToStructuredUInt16ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<ushort>>
        {
            protected override List<NaiveCase<ushort>> UseCases => new List<NaiveCase<ushort>>
            {
                new NaiveCase<ushort>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "1+0=>10",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new ushort[] { 1, 0, 10 }
                }
            };
        }

        public class ToStructuredInt32ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<int>>
        {
            protected override List<NaiveCase<int>> UseCases => new List<NaiveCase<int>>
            {
                new NaiveCase<int>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "1+0=>10",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new[] { 1, 0, 10 }
                }
            };
        }

        public class ToStructuredUInt32ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<uint>>
        {
            protected override List<NaiveCase<uint>> UseCases => new List<NaiveCase<uint>>
            {
                new NaiveCase<uint>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "1+0=>10",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new uint[] { 1, 0, 10 }
                }
            };
        }

        public class ToStructuredInt64ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<long>>
        {
            protected override List<NaiveCase<long>> UseCases => new List<NaiveCase<long>>
            {
                new NaiveCase<long>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "1+0=>10",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new long[] { 1, 0, 10 }
                }
            };
        }

        public class ToStructuredUInt64ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<ulong>>
        {
            protected override List<NaiveCase<ulong>> UseCases => new List<NaiveCase<ulong>>
            {
                new NaiveCase<ulong>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "1+0=>10",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new ulong[] { 1, 0, 10 }
                }
            };
        }

        public class ToStructuredDoubleArrayNaiveCases : UseCaseCollectionOf<NaiveCase<double>>
        {
            protected override List<NaiveCase<double>> UseCases => new List<NaiveCase<double>>
            {
                new NaiveCase<double>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "1+0.2=>10.1",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new[] { 1, 0.2, 10.2 }
                }
            };
        }

        public class ToStructuredBoolArrayNaiveCases : UseCaseCollectionOf<NaiveCase<bool>>
        {
            protected override List<NaiveCase<bool>> UseCases => new List<NaiveCase<bool>>
            {
                new NaiveCase<bool>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "False+True=>True",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new[] { false, true, true }
                }
            };
        }

        public class ToStructuredByteArrayNaiveCases : UseCaseCollectionOf<NaiveCase<byte>>
        {
            protected override List<NaiveCase<byte>> UseCases => new List<NaiveCase<byte>>
            {
                new NaiveCase<byte>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "0+127=>255",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new byte[] { 0, 127, 255 }
                }
            };
        }

        public class ToStructuredSByteArrayNaiveCases : UseCaseCollectionOf<NaiveCase<sbyte>>
        {
            protected override List<NaiveCase<sbyte>> UseCases => new List<NaiveCase<sbyte>>
            {
                new NaiveCase<sbyte>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "-128+127=>-1",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new sbyte[] { -128, 127, -1 }
                }
            };
        }

        public class ToStructuredSingleArrayNaiveCases : UseCaseCollectionOf<NaiveCase<float>>
        {
            protected override List<NaiveCase<float>> UseCases => new List<NaiveCase<float>>
            {
                new NaiveCase<float>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "-128+127=>-1",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new float[] { -128, 127, -1 }
                }
            };
        }

        public class ToStructuredDecimalArrayNaiveCases : UseCaseCollectionOf<NaiveCase<decimal>>
        {
            protected override List<NaiveCase<decimal>> UseCases => new List<NaiveCase<decimal>>
            {
                new NaiveCase<decimal>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = $"{decimal.MinValue}+{decimal.MaxValue}=>{decimal.Zero}",
                        DelimeterSequence = new[] { "+", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new decimal[] {  decimal.MinValue , decimal.MaxValue, decimal.Zero }
                }
            };
        }

        public class ToStructuredDateTimeArrayNaiveCases : UseCaseCollectionOf<NaiveCase<DateTime>>
        {
            protected override List<NaiveCase<DateTime>> UseCases => new List<NaiveCase<DateTime>>
            {
                new NaiveCase<DateTime>
                {
                   Input = new PrimaryTestCaseInput
                    {
                        Text = "20080501T08:30:52Z<=19990501T08:30:52Z=>20220101T00:00:00Z",
                        DelimeterSequence = new[] { "<=", "=>" },
                        SequenceOptions = SequenceOptions.Naive,
                        StartIndex = 0,
                        FinalIndex = 0
                   },
                    Expected = new [] {  new DateTime(2008, 5, 1, 8, 30, 52, DateTimeKind.Utc), new DateTime(1999, 5, 1, 8, 30, 52, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 00, 00, DateTimeKind.Utc) }
                }
            };
        }

        /*
        TypeCode.DateTime =>DateTime.Parse(text, provider: CultureInfo.InvariantCulture),
 * */
    }
}
