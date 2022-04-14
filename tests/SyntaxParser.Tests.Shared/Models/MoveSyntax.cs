using SyntaxParser;

namespace SyntaxParser.Tests.Shared
{
    [Syntax($"{nameof(From)}=>{nameof(To)}")]
	public class MoveSyntax
	{
		public string From { get; set; }

		public string To { get; set; }
	}
}