using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SyntaxParser.Core.Extensions
{
    public static class SyntaxParserExtensions
    {
		public static string ParseFileToJson<T>(this SyntaxParser<T> syntaxParser, string path)
		{
			return JsonSerializer.Serialize(syntaxParser.ParseFile(path));
		}

		public async static Task<string> ParseFileToJsonAsync<T>(this SyntaxParser<T> syntaxParser, string path)
		{
			using var stream = new MemoryStream();
			await JsonSerializer.SerializeAsync(stream, syntaxParser.ParseFileAsync(path));
			using var reader = new StreamReader(stream);
			stream.Position = 0;
			return reader.ReadToEnd();
		}
	}
}
