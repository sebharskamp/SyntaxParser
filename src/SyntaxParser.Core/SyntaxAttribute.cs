namespace SyntaxParser
{
    public class SyntaxAttribute : Attribute
	{
		private readonly string _syntax;

		public SyntaxAttribute(string syntax)
		{
			_syntax = syntax;
		}

		public string Value => _syntax;
	}
}