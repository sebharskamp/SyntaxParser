namespace SyntaxParser
{
    public enum SequenceAlgorithm
    {
        Naive,
        Exact
    }

    public static class SequenceAlgorithmExtensions
    {
        public static SequenceAlgorithm ToEnumOrDefault(this string? value) 
        { 
            if(!Enum.TryParse<SequenceAlgorithm>(value, false, out var result))
            {
                return SequenceAlgorithm.Naive;
            }
            return result;
        }
    }
}