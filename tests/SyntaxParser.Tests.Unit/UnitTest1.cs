using SyntaxParser.Tests.Shared;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SyntaxParser.Tests.Shared.Extensions;
using System.Text.Json;

namespace SyntaxParser.Tests.Unit
{
    public class MemoryFile: IDisposable
    {
        private string _path;
        private FileStream _fileStream;

        protected MemoryFile()
        {
            _path = $"{Guid.NewGuid()}.txt";
            int bufferSize = 1024;
            _fileStream = File.Create(_path, bufferSize, FileOptions.Asynchronous);
            _fileStream.Position = 0;
        }

        public string Path => _path;

        public static async Task<MemoryFile> InitializeAsync(string input)
        {
            var file = new MemoryFile();
            await file.AppendAsync(input);
            return file;
        }

        private async Task AppendAsync(string input)
        {
            if (!_fileStream.CanWrite)
            {
                _fileStream = File.OpenWrite(_path);
            }
            await _fileStream.WriteAsync(Encoding.UTF8.GetBytes(input));
            _fileStream.Close();
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }
    }

    public class UnitTest1
    {
        private SyntaxParser<MoveSyntax> _sut = new SyntaxParser<MoveSyntax>();
        
        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public void BasicParseTextTest(string input, IEnumerable<MoveSyntax> expected)
        {
            var result = _sut.ParseText(input);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public async Task BasicParseFileTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await MemoryFile.InitializeAsync(input);
            var result = _sut.ParseFile(file.Path);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public async Task BasicParseFileAsyncTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await MemoryFile.InitializeAsync(input);
            var result = await _sut.ParseFileAsync(file.Path).ToListAsync();
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public async Task BasicParseFileToJsonTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await MemoryFile.InitializeAsync(input);
            var result = _sut.ParseFileToJson(file.Path);
            result.Should().BeEquivalentTo(JsonSerializer.Serialize(expected));
        }

        [Theory]
        [ClassData(typeof(MoveSyntaxClassData))]
        public async Task BasicParseFileToJsonAsyncTest(string input, IEnumerable<MoveSyntax> expected)
        {
            using var file = await MemoryFile.InitializeAsync(input);
            var result = await _sut.ParseFileToJsonAsync(file.Path);
            result.Should().BeEquivalentTo(JsonSerializer.Serialize(expected));
        }

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


