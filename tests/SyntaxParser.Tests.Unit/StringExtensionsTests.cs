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

namespace SyntaxParser.Tests.Unit
{
    public partial class StringExtensionsTests
    {
        [Theory]
        [ClassData(typeof(ToStructuredStringArrayNaiveCases))]
        [ClassData(typeof(ToStructuredArrayExactCases))]
        [ClassData(typeof(StringToStringArrayCases))]
        public void PrimaryTestCases_String(PrimaryTestCase<string> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<string>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredCharArrayNaiveCases))]
        public void PrimaryTestCases_Char(PrimaryTestCase<char> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<char>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredInt16ArrayNaiveCases))]
        public void PrimaryTestCases_Short(PrimaryTestCase<short> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<short>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredUInt16ArrayNaiveCases))]
        public void PrimaryTestCases_UShort(PrimaryTestCase<ushort> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<ushort>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredInt32ArrayNaiveCases))]
        [ClassData(typeof(StringToIntArrayCases))]
        public void PrimaryTestCases_Int(PrimaryTestCase<int> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<int>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredUInt32ArrayNaiveCases))]
        public void PrimaryTestCases_UInt(PrimaryTestCase<uint> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<uint>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredInt64ArrayNaiveCases))]
        public void PrimaryTestCases_Long(PrimaryTestCase<long> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<long>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredUInt64ArrayNaiveCases))]
        public void PrimaryTestCases_ULong(PrimaryTestCase<ulong> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<ulong>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(StringToDoubleArrayCases))]
        public void PrimaryTestCases_Double(PrimaryTestCase<double> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<double>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredBoolArrayNaiveCases))]
        public void PrimaryTestCases_Bool(PrimaryTestCase<bool> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<bool>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredSingleArrayNaiveCases))]
        public void PrimaryTestCases_Single(PrimaryTestCase<float> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<float>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredSByteArrayNaiveCases))]
        public void PrimaryTestCases_SByte(PrimaryTestCase<sbyte> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<sbyte>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredByteArrayNaiveCases))]
        public void PrimaryTestCases_Byte(PrimaryTestCase<byte> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<byte>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredDecimalArrayNaiveCases))]
        public void PrimaryTestCases_Decimal(PrimaryTestCase<decimal> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<decimal>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredDateTimeArrayNaiveCases))]
        public void PrimaryTestCases_DateTime(PrimaryTestCase<DateTime> @case)
        {
            PrimaryTestCaseInput input = @case.Input;
            var structeredArray = input.Text.ToStructuredArray<DateTime>(input.DelimeterSequence, input.StartIndex, input.FinalIndex, input.SequenceOptions);
            @case.IsResultAsExpected(structeredArray);
        }


        public class PrimaryTestCaseInput
        {
            public string Text { get; init; }
            public string[] DelimeterSequence { get; init; }
            public int StartIndex { get; set; }
            public int FinalIndex { get; set; }
            public int SequenceOptions { get; init; }
        }

        public class PrimaryTestCase<T> : UseCase<PrimaryTestCaseInput, T[]>
        {
            public override T[] Expected { get; set; }
            public override PrimaryTestCaseInput Input { get; set; }
        }
    }
}
