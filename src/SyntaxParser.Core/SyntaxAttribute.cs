namespace SyntaxParser
{
	/// <summary>
	/// Define your input syntax. 
	/// <see href="https://syntaxparser.netlify.app/?id=syntaxattribute">For an example</see>
	/// </summary>
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