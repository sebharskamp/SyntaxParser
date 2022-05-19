using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("SyntaxParser.Tests.Unit")]
namespace SyntaxParser
{
    internal static class StringExtensions
	{
		internal static T[] ToStructuredArray<T>(this string text, string[] delimterSequence, int start = 0, int end = 0, int sequenceOptions = SequenceOptions.Naive)
		{
			int d = 0;
			ReadOnlySpan<char> input = text.AsSpan().Slice(start, text.Length - start - end);
			ReadOnlySpan<char> delim = delimterSequence[d].AsSpan();
			ReadOnlySpan<char> newLine = Environment.NewLine.AsSpan();

			int amountOfNewLines = 0;
			input.AmountOfOccurences(newLine, ref amountOfNewLines);
			T[] results;

            if(sequenceOptions == SequenceOptions.Exact)
            {
				var delimGroup = delimterSequence.GroupBy(v => v).ToDictionary(v => v.Key, v => v.Count());
				int delimCount = 0;
				for (int i = 0; i < delimGroup.Keys.Count; i++)
                {
					input.AmountOfOccurences(delim, ref delimCount);
				}

				results = new T[delimCount + 1 + amountOfNewLines];
			}
            else
            {
				results = new T[(delimterSequence.Length + 1) * (amountOfNewLines + 1)];
			}

			int rCount = 0;
			int segmentSize = 0;
			int inputMinusNewline = input.Length - newLine.Length;
			for (int x = 0; x < input.Length; x++)
			{
				if (x < inputMinusNewline && input.Slice(x, newLine.Length).SequenceEqual(newLine))
				{
					results[rCount] = valueParser<T>(input.Slice(x - segmentSize, segmentSize));
					x++;
					d = 0;
					delim = delimterSequence[d].AsSpan();

					segmentSize = 0;
					rCount++;
				}
				else if (x < input.Length - delim.Length && input.Slice(x, delim.Length).SequenceEqual(delim))
				{
					results[rCount] = valueParser<T>(input.Slice(x - segmentSize, segmentSize));
					x += delim.Length - 1;
					d++;
					if (d < delimterSequence.Length)
					{
						delim = delimterSequence[d].AsSpan();
					}

					segmentSize = 0;
					rCount++;
				}
				else if (!(x == inputMinusNewline && input.Slice(x, newLine.Length).SequenceEqual(newLine)))
				{
					segmentSize++;
				}
			}
			results[rCount] = valueParser<T>(input.Slice(input.Length - segmentSize));
			return results;
		}

		internal static T valueParser<T>(ReadOnlySpan<char> text)
        {
			return (T)Convert.ChangeType(text.ToString(), typeof(T));
        }


		internal static void AmountOfOccurences(this ReadOnlySpan<char> text, ReadOnlySpan<char> charSet, ref int count)
		{
			int length = text.Length - charSet.Length;
			for (var i = 0; i < length; i++)
			{
				if (text.Slice(i, charSet.Length).SequenceEqual(charSet))
				{
					count++;
				}
			}
		}
	}
}