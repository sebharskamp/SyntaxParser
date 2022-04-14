using SyntaxParser.Core;

namespace SyntaxParser.Benchmark
{
    [Syntax($"{nameof(From)}=>{nameof(To)}")]
	internal class MoveSyntax
	{
		public string From { get; private set; }

		public string To { get; private set; }
	}
}