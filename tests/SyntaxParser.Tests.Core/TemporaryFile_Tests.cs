using SyntaxParser.Tests.Shared.Util;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SyntaxParser.Tests.Core
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
}


