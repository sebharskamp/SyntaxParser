using SyntaxParser;

namespace SyntaxParser.Benchmark
{
    [Syntax($"{nameof(From)}=>{nameof(To)}")]
	internal class MoveSyntax
	{
		public string From { get; set; }

		public string To { get; set; }
	}
}