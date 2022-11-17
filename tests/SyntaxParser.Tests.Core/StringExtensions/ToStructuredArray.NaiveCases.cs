using SyntaxParser.Tests.Shared.UseCaseFramework;
using System;
using System.Collections.Generic;

namespace SyntaxParser.Tests.Core.StringExtensions
{
    public partial class ToStructuredArray
    {
        public class NaiveCase<T> : ToStructuredArrayTestCase<T> { }
        public class StringArrayNaiveCases : UseCaseCollectionOf<NaiveCase<string>>
        {
            protected override List<NaiveCase<string>> UseCases => new()
            {
                new NaiveCase<string>
                {
                    Input = new ToStructuredArrayInput
                    {
                        Text = "hello=>world",
                        Parameters = new(){
                            DelimeterSequence = new[] { "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 0,
                            FinalIndex = 0
                        }
                    },
                    Expected = new[] { "hello", "world" }
                },
                new NaiveCase<string>
                {
                    Input = new ToStructuredArrayInput
                    {
                        Text = $"A=>B{Environment.NewLine}C=>D",
                        Parameters = new(){
                            DelimeterSequence = new[] { "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 0,
                            FinalIndex = 0}
                        },
                    Expected = new[] { "A", "B", "C", "D" }
                },
                new NaiveCase<string>
                {
                    Input = new ToStructuredArrayInput
                    {
                        Text = "[ping, pong]=>[pong, ping]",
                        Parameters = new(){
                            DelimeterSequence = new[] { "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 0,
                            FinalIndex = 0
                        }
                    },
                    Expected =new[] { "[ping, pong]", "[pong, ping]" }
                },
                new NaiveCase<string>
                {
                    Input = new ToStructuredArrayInput
                    {
                        Text = "hop+step=>jump",
                        Parameters = new(){
                            DelimeterSequence = new[] { "+", "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 0,
                            FinalIndex = 0
                        }
                    },
                    Expected = new[] { "hop", "step", "jump" }
                },
                new NaiveCase<string>
                {
                    Input =  new ToStructuredArrayInput
                    {
                        Text = "[0,1]",
                        Parameters = new(){
                            DelimeterSequence = new[] { "," },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 1,
                            FinalIndex = 1
                        }
                    },
                    Expected = new[] { "0", "1" }
                }
            };
        }

        public class CharArrayNaiveCases : UseCaseCollectionOf<NaiveCase<char>>
        {
            protected override List<NaiveCase<char>> UseCases => new()
            {
                new NaiveCase<char>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "a+b=>c",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new[] { 'a', 'b', 'c' }
                }
            };
        }

        public class Int16ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<short>>
        {
            protected override List<NaiveCase<short>> UseCases => new List<NaiveCase<short>>
            {
                new NaiveCase<short>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "1+0=>10",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new short[] { 1, 0, 10 }
                }
            };
        }

        public class UInt16ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<ushort>>
        {
            protected override List<NaiveCase<ushort>> UseCases => new List<NaiveCase<ushort>>
            {
                new NaiveCase<ushort>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "1+0=>10",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new ushort[] { 1, 0, 10 }
                }
            };
        }

        public class Int32ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<int>>
        {
            protected override List<NaiveCase<int>> UseCases => new List<NaiveCase<int>>
            {
                new NaiveCase<int>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "1+0=>10",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new[] { 1, 0, 10 }
                },
                new NaiveCase<int>
                {
                    Input =  new ToStructuredArrayInput
                        { Text = "[0,1]",
                        Parameters = new(){
                    DelimeterSequence = new[] { "," },
                    sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                    StartIndex = 1,
                    FinalIndex = 1
                        }
                    },
                    Expected = new[] { 0, 1 }
                }
            };
        }

        public class UInt32ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<uint>>
        {
            protected override List<NaiveCase<uint>> UseCases => new List<NaiveCase<uint>>
            {
                new NaiveCase<uint>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "1+0=>10",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new uint[] { 1, 0, 10 }
                }
            };
        }

        public class Int64ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<long>>
        {
            protected override List<NaiveCase<long>> UseCases => new()
            {
                new NaiveCase<long>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "1+0=>10",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new long[] { 1, 0, 10 }
                }
            };
        }

        public class UInt64ArrayNaiveCases : UseCaseCollectionOf<NaiveCase<ulong>>
        {
            protected override List<NaiveCase<ulong>> UseCases => new List<NaiveCase<ulong>>
            {
                new NaiveCase<ulong>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "1+0=>10",
                        Parameters = new() {
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new ulong[] { 1, 0, 10 }
                }
            };
        }

        public class DoubleArrayNaiveCases : UseCaseCollectionOf<NaiveCase<double>>
        {
            protected override List<NaiveCase<double>> UseCases => new List<NaiveCase<double>>
            {
                new NaiveCase<double>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "1+0.2=>10.1",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new[] { 1, 0.2, 10.1 }
                },
                new NaiveCase<double>
                {
                    Input =  new ToStructuredArrayInput
                    {
                        Text = "[0.2,1.0]",
                        Parameters = new()
                        {
                            DelimeterSequence = new[] { "," },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 1,
                            FinalIndex = 1
                        }
                    },
                    Expected = new[] { 0.2, 1.0 }
                }
            };
        }

        public class ToStructuredBoolArrayNaiveCases : UseCaseCollectionOf<NaiveCase<bool>>
        {
            protected override List<NaiveCase<bool>> UseCases => new List<NaiveCase<bool>>
            {
                new NaiveCase<bool>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "False+True=>True",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new[] { false, true, true }
                }
            };
        }

        public class ByteArrayNaiveCases : UseCaseCollectionOf<NaiveCase<byte>>
        {
            protected override List<NaiveCase<byte>> UseCases => new List<NaiveCase<byte>>
            {
                new NaiveCase<byte>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "0+127=>255",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new byte[] { 0, 127, 255 }
                }
            };
        }

        public class SByteArrayNaiveCases : UseCaseCollectionOf<NaiveCase<sbyte>>
        {
            protected override List<NaiveCase<sbyte>> UseCases => new List<NaiveCase<sbyte>>
            {
                new NaiveCase<sbyte>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "-128+127=>-1",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0 }
                   },
                    Expected = new sbyte[] { -128, 127, -1 }
                }
            };
        }

        public class SingleArrayNaiveCases : UseCaseCollectionOf<NaiveCase<float>>
        {
            protected override List<NaiveCase<float>> UseCases => new List<NaiveCase<float>>
            {
                new NaiveCase<float>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = "-128+127=>-1",
                        Parameters = new(){
                        DelimeterSequence = new[] { "+", "=>" },
                        sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                        StartIndex = 0,
                        FinalIndex = 0
                       }
                   },
                    Expected = new float[] { -128, 127, -1 }
                }
            };
        }

        public class DecimalArrayNaiveCases : UseCaseCollectionOf<NaiveCase<decimal>>
        {
            protected override List<NaiveCase<decimal>> UseCases => new List<NaiveCase<decimal>>
            {
                new NaiveCase<decimal>
                {
                   Input = new ToStructuredArrayInput
                    {
                        Text = $"{decimal.MinValue}+{decimal.MaxValue}=>{decimal.Zero}",
                        Parameters = new(){
                            DelimeterSequence = new[] { "+", "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 0,
                            FinalIndex = 0
                        }
                   },
                    Expected = new decimal[] {  decimal.MinValue , decimal.MaxValue, decimal.Zero }
                }
            };
        }

        public class DateTimeArrayNaiveCases : UseCaseCollectionOf<NaiveCase<DateTime>>
        {
            protected override List<NaiveCase<DateTime>> UseCases => new List<NaiveCase<DateTime>>
            {
                new NaiveCase<DateTime>
                {
                   Input = new ToStructuredArrayInput
                   {
                        Text = "20080501T08:30:52Z<=19990501T08:30:52Z=>20220101T00:00:00Z",
                        Parameters = new(){
                            DelimeterSequence = new[] { "<=", "=>" },
                            sequenceAlgorithm = SequenceAlgorithm.Naive.ToString(),
                            StartIndex = 0,
                            FinalIndex = 0
                           }
                   },
                   Expected = new [] {  new DateTime(2008, 5, 1, 8, 30, 52, DateTimeKind.Utc), new DateTime(1999, 5, 1, 8, 30, 52, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 00, 00, DateTimeKind.Utc) }
                }
            };
        }
    }
}
