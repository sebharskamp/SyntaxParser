using SyntaxParser.Tests.Unit.UseCaseFramework;

namespace SyntaxParser.Tests.CSV
{
    public class UnitTest1
    {
        [Theory]
        [ClassData(typeof(CsvSyntaxCases))]
        public void ParseText(CsvSyntaxParserCase<Csv> @case)
        {
            var result = @case.InvokeMethod(typeof(SyntaxParser), nameof(SyntaxParser.ParseText), new object[] { @case.Input });
            @case.IsResultAsExpected(result);
        }
    }

    public class CsvSyntaxCases : UseCaseCollectionOf<CsvSyntaxParserCase<Csv>>
    {
        protected override List<CsvSyntaxParserCase<Csv>> UseCases => throw new NotImplementedException();
    }

    public class Csv
    {
    }

    public class Input
    {
        public string Text { get; init; }
    }

    public class CsvSyntaxParserCase<T> : UseCase<Input, T[]>
    {
        public override T[] Expected { get; set; }
        public override Input Input { get; set; }
    }
}