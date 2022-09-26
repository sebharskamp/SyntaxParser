﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SyntaxParser.Tests.Unit
{
    public class TemporaryFile_Tests
    {
        [Fact]
        public async Task CreateAndReopenFile()
        {
            var fileContent = "hello world";
            using var file = await TemporaryFile.InitializeAsync(fileContent);
            Assert.Equal(fileContent, File.ReadAllText(file.Path));
        }

        [Fact]
        public async Task ThrowErrorAfterDispose()
        {
            var fileContent = "hello world";
            using var file = await TemporaryFile.InitializeAsync(fileContent);
            file.Dispose();
            Assert.Throws<FileNotFoundException>(() => File.ReadAllText(file.Path));
        }

        [Fact]
        public async Task FileDoesNotExistsAfterDispose()
        {
            var fileContent = "hello world";
            using var file = await TemporaryFile.InitializeAsync(fileContent);
            file.Dispose();
            Assert.False(File.Exists(file.Path));
        }

        [Fact]
        public async Task MultipleFiles()
        {
            var fileContentOne = "hello world";
            var fileContentTwo = "hello universe";
            using var fileOne = await TemporaryFile.InitializeAsync(fileContentOne);
            using var fileTwo = await TemporaryFile.InitializeAsync(fileContentTwo);
            Assert.Equal(fileContentOne, File.ReadAllText(fileOne.Path));
            Assert.Equal(fileContentTwo, File.ReadAllText(fileTwo.Path));
            Assert.NotEqual(fileTwo.Path, fileOne.Path);
        }

    }

    public class TemporaryFile : IDisposable
    {
        private string _path;
        private FileStream? _fileStream;

        protected TemporaryFile()
        {
            _path = $"{Guid.NewGuid()}.txt";
            int bufferSize = 1024;
            _fileStream = File.Create(_path, bufferSize, FileOptions.Asynchronous);
            _fileStream.Position = 0;
        }

        public string Path => _path;

        public static async Task<TemporaryFile> InitializeAsync(string input)
        {
            var file = new TemporaryFile();
            await file.AppendAsync(input);
            return file;
        }

        private async Task AppendAsync(string input)
        {
            if (_fileStream is null || !_fileStream.CanWrite)
            {
                _fileStream = File.OpenWrite(_path);
            }
            await _fileStream.WriteAsync(Encoding.UTF8.GetBytes(input));
            _fileStream.Close();
        }

        public void Dispose()
        {
            if (_fileStream is null || !_fileStream.CanWrite)
            {
                try
                {
                    _fileStream = File.OpenWrite(_path);
                }
                catch
                {
                    return;
                }
            }
            _fileStream.Close();
            _fileStream.Dispose();
            File.Delete(_path);
        }
    }
}


