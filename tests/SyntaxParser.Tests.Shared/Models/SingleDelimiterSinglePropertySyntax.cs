using SyntaxParser.Core;

namespace SyntaxParser.Tests.Shared
{
    [SingleDelimiterSyntax(";")]
    public class SingleDelimiterSinglePropertySyntax
    {
        public string Name { get; set; }
    }

    [Syntax($"{nameof(Name)}")]
    public class SinglePropertySyntax
    {
        public string Name { get; set; }
    }
}