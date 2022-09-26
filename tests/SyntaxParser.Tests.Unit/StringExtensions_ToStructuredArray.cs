using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SyntaxParser;
using System.Collections.Generic;
using System.Collections;
using SyntaxParser.Tests.Shared;
using Newtonsoft.Json;
using Xunit.Abstractions;
using FluentAssertions.Equivalency;
using SyntaxParser.Tests.Unit.UseCaseFramework;

namespace SyntaxParser.Tests.Unit
{
    public partial class StringExtensions_ToStructuredArray
    {
        [Theory]
        [ClassData(typeof(StringArrayNaiveCases))]
        [ClassData(typeof(StringArrayExactCases))]
        public void String(ToStructuredArrayTestCase<string> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<string>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(CharArrayNaiveCases))]
        public void Char(ToStructuredArrayTestCase<char> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<char>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(Int16ArrayNaiveCases))]
        public void Short(ToStructuredArrayTestCase<short> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<short>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(UInt16ArrayNaiveCases))]
        public void UShort(ToStructuredArrayTestCase<ushort> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<ushort>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(Int32ArrayNaiveCases))]
        public void Int(ToStructuredArrayTestCase<int> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<int>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(UInt32ArrayNaiveCases))]
        public void UInt(ToStructuredArrayTestCase<uint> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<uint>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(Int64ArrayNaiveCases))]
        public void Long(ToStructuredArrayTestCase<long> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<long>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(UInt64ArrayNaiveCases))]
        public void ULong(ToStructuredArrayTestCase<ulong> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<ulong>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(DoubleArrayNaiveCases))]
        public void Double(ToStructuredArrayTestCase<double> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<double>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredBoolArrayNaiveCases))]
        public void Bool(ToStructuredArrayTestCase<bool> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<bool>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(SingleArrayNaiveCases))]
        public void Single(ToStructuredArrayTestCase<float> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<float>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(SByteArrayNaiveCases))]
        public void SByte(ToStructuredArrayTestCase<sbyte> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<sbyte>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ByteArrayNaiveCases))]
        public void Byte(ToStructuredArrayTestCase<byte> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<byte>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(DecimalArrayNaiveCases))]
        public void Decimal(ToStructuredArrayTestCase<decimal> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<decimal>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(DateTimeArrayNaiveCases))]
        public void DateTime(ToStructuredArrayTestCase<DateTime> @case)
        {
            Input input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<DateTime>(input.Parameters.DelimeterSequence, input.Parameters.StartIndex, input.Parameters.FinalIndex, input.Parameters.sequenceAlgorithm);
            @case.IsResultAsExpected(structeredArray);
        }


        public class Input
        {
            public string Text { get; init; }
            public MethodParameters Parameters { get; init; }

        }

        public class MethodParameters
        {
            public string[] DelimeterSequence { get; init; }
            public int StartIndex { get; init; }
            public int FinalIndex { get; init; }
            public int sequenceAlgorithm { get; init; }
        }

        public class ToStructuredArrayTestCase<T> : UseCase<Input, T[]>
        {
            public override T[] Expected { get; set; }
            public override Input Input { get; set; }
        }
    }


}
