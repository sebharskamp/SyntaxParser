using SyntaxParser.Tests.Shared;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using SyntaxParser.Tests.Shared.Extensions;

namespace SyntaxParser.Tests.Unit
{
    public class SyntaxParserHappyFlowTests
    {
        private SyntaxParser<MoveSyntax> _sut = new SyntaxParser<MoveSyntax>();
        
        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public void ParseTextTest(string input, IEnumerable<MoveSyntax> expected)
        {
            var result = _sut.ParseText(input);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public async Task ParseFileTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await MemoryFile.InitializeAsync(input);
            var result = _sut.ParseFile(file.Path);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public async Task ParseFileAsyncTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await MemoryFile.InitializeAsync(input);
            var result = await _sut.ParseFileAsync(file.Path).ToListAsync();
            result.Should().BeEquivalentTo(expected);
        }

        public class MoveSyntaxClassData : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>
                {
                    new object[] { "A=>B",
                        new List<MoveSyntax>{ new MoveSyntax
                            {
                                From = "A",
                                To = "B"
                            }
                        }
                    },
                    new object[] { $"A=>B{Environment.NewLine}C=>D",
                        new List<MoveSyntax>
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
                        }
                    }
                };


            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}


