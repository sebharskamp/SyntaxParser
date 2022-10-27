using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser.Tests.Shared.Util
{

    public class TemporaryFile : IDisposable
    {
        private readonly string _path;
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


