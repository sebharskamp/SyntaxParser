using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SyntaxParser;
using System.Collections.Generic;

namespace SyntaxParser.Tests.Unit
{
    public class StringExtensionsTests
    {
        [Theory]
        [ClassData(typeof(ToStructuredArrayNaiveData))]
        public void ToStructuredArray_WhenNaiveSequencer_Tests(string text, string[] delimeterSequence, string[] expected, int sequenceOption)
        {
            var structeredArray = text.ToStructuredArray<string>(delimeterSequence, sequenceOption);
            Assert.Equal(expected, structeredArray);
        }

        [Theory]
        [ClassData(typeof(ToStructuredArrayExactData))]
        public void ToStructuredArray_WhenExactSequencer_Tests(string text, string[] delimeterSequence, string[] expected, int sequenceOption)
        {
            var structeredArray = text.ToStructuredArray<string>(delimeterSequence, sequenceOption);
            Assert.Equal(expected, structeredArray);
        }


        public class ToStructuredArrayNaiveData : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>
                {
                    new object[] { "hello=>world", new[] { "=>" }, new[] { "hello", "world" }, SequenceOptions.Naive },
                    new object[] { $"A=>B{Environment.NewLine}C=>D", new[] { "=>" }, new[] { "A", "B", "C", "D" }, SequenceOptions.Naive },
                    new object[] { "[ping, pong]=>[pong, ping]", new[] { "=>" }, new[] { "[ping, pong]", "[pong, ping]" }, SequenceOptions.Naive },
                    new object[] { "hop+step=>jump", new[] { "+", "=>" }, new[] { "hop", "step", "jump" }, SequenceOptions.Naive },
                };


            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ToStructuredArrayExactData : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>
                {
                    new object[] { "what=>if=>else", new[] { "=>" }, new[] { "what", "if", "else" }, SequenceOptions.Exact },
                    new object[] { $"what=>if=>else{Environment.NewLine}it=>has=>newLine", new[] { "=>" }, new[] { "what", "if", "else", "it", "has", "newLine" }, SequenceOptions.Exact },
                };


            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
