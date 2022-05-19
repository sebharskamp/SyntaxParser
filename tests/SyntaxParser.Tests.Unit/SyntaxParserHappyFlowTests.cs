using SyntaxParser.Tests.Shared;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using SyntaxParser.Tests.Shared.Extensions;
using System.Text;

namespace SyntaxParser.Tests.Unit
{
    public class SyntaxParserHappyFlowTests
    {        
        [Theory]
        [ClassData(typeof(MoveSyntaxData))]
        public void ParseTextTest(string input, IEnumerable<MoveSyntax> expected)
        {
            var result = SyntaxParser.ParseText<MoveSyntax>(input);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxData))]
        public async Task ParseFileMoveSyntaxDataTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await TemporaryFile.InitializeAsync(input);
            var result = SyntaxParser.ParseFile<MoveSyntax>(file.Path);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(AddressSyntaxData))]
        public async Task ParseFileAddressSyntaxDataTest(string input, IEnumerable<AdressSyntax> expected)
        {
            using var file = await TemporaryFile.InitializeAsync(input);
            var result = SyntaxParser.ParseFile<AdressSyntax>(file.Path);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(VectorSyntaxData))]
        public async Task ParseFileVectorSyntaxDataTest(string input, IEnumerable<VectorSyntax> expected)
        {
            using var file = await TemporaryFile.InitializeAsync(input);
            var result = SyntaxParser.ParseFile<VectorSyntax>(file.Path);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxData))]
        public async Task ParseFileAsyncTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await TemporaryFile.InitializeAsync(input);
            var result = await SyntaxParser.ParseFileAsync<MoveSyntax>(file.Path).ToListAsync();
            result.Should().BeEquivalentTo(expected);
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
                    new object[] { new StringBuilder().AppendLine($"A=>B").Append("C=>D").ToString(),
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

        public class AddressSyntaxData : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>
                {
                    new object[] { "1111@AA#23",
                        new List<
                            AdressSyntax>{ new AdressSyntax
                            {
                                RegionCode = 1111,
                                NeighbourhoodCode = "AA",
                                Number = "23"
                            }
                        }
                    }
                };


            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class VectorSyntaxData : IEnumerable<object[]>
        {
            private List<object[]> _data = new List<object[]>
                {
                    new object[] { "[0.1, 2.2]=>[0.4, 1]",
                        new List<
                            VectorSyntax>{ new VectorSyntax
                            {
                                PointA = new []{0.1, 2.2},
                                PointB = new []{0.4, 1}
                            }
                        }
                    }
                };


            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}


