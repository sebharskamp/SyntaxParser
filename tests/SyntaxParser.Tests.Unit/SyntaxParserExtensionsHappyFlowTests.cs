using SyntaxParser.Tests.Shared;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using SyntaxParser.Core.Extensions;
using System.IO;

namespace SyntaxParser.Tests.Unit
{
    public class SyntaxParserExtensionsHappyFlowTests
    {
        [Theory]
        [ClassData(typeof(MoveSyntaxData))]
        public async Task ParseFileToJsonTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await TemporaryFile.InitializeAsync(input);
            var result = SyntaxParser.ParseFileToJson<MoveSyntax>(file.Path);
            result.Should().BeEquivalentTo(JsonSerializer.Serialize(expected));
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxData))]
        public async Task FileToJsonAsyncTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await TemporaryFile.InitializeAsync(input);
            var path = file.Path;
            var result = await SyntaxParser.ParseFileToJsonAsync<MoveSyntax>(file.Path);
            result.Should().BeEquivalentTo(JsonSerializer.Serialize(expected));
        }

        public class MoveSyntaxData : IEnumerable<object[]>
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


