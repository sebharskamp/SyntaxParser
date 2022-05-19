namespace SyntaxParser.Tests.Shared
{
    [Syntax($"{nameof(PointA)}=>{nameof(PointB)}")]
    public class VectorSyntax
    {
        public double[] PointA { get; set; }
        public double[] PointB { get; set; }
    }
}
