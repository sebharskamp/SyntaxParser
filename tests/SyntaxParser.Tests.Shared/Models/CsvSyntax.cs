using SyntaxParser.Core;

namespace SyntaxParser.Tests.Shared
{
    [SingleDelimterSyntax(";")]
    public class CsvSyntax
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}