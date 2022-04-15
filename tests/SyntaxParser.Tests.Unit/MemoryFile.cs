using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser.Tests.Unit
{
    public class MemoryFile: IDisposable
    {
        private string _path;
        private FileStream? _fileStream;

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
                _fileStream = File.OpenWrite(_path);
            }
            _fileStream.Close();
            _fileStream.Dispose();
        }
    }
}


