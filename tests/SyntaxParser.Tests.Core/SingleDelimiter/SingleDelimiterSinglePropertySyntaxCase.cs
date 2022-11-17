using SyntaxParser.Tests.Shared;
using SyntaxParser.Tests.Shared.UseCaseFramework;

namespace SyntaxParser.Tests.Core.SingleDelimiter
{
    public class SinglePropertySyntaxCase : UseCase<SingleDelimiterSyntaxCaseInput, SingleDelimiterSinglePropertySyntax[]>
    {
        public override SingleDelimiterSyntaxCaseInput Input { get; set; }
        public override SingleDelimiterSinglePropertySyntax[] Expected { get; set; }
    }
}
