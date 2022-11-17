using SyntaxParser.Core;

namespace SyntaxParser.Tests.Shared
{
    [SingleDelimiterSyntax(";")]
    public class CsvSyntax
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}