using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("SyntaxParser.Tests.Unit")]
namespace SyntaxParser
{
    internal static class StringExtensions
	{
		internal static string[] ToStructuredArray(this string text, string[] delimterSequence, int sequenceOptions = SequenceOptions.Naive)
		{
			int d = 0;
			ReadOnlySpan<char> input = text.AsSpan();
			ReadOnlySpan<char> delim = delimterSequence[d].AsSpan();
			ReadOnlySpan<char> newLine = Environment.NewLine.AsSpan();

			int amountOfNewLines = 0;
			input.AmountOfOccurences(newLine, ref amountOfNewLines);
			string[] results;

            if(sequenceOptions == SequenceOptions.Exact)
            {
				var delimGroup = delimterSequence.GroupBy(v => v).ToDictionary(v => v.Key, v => v.Count());
				int delimCount = 0;
				for (int i = 0; i < delimGroup.Keys.Count; i++)
                {
					input.AmountOfOccurences(delim, ref delimCount);
				}

				results = new string[delimCount + 1 + amountOfNewLines];
			}
            else
            {
				results = new string[(delimterSequence.Length + 1) * (amountOfNewLines + 1)];
			}

			var rCount = 0;
			var builder = new StringBuilder();
			int inputMinusNewline = input.Length - newLine.Length;

			for (int x = 0; x < input.Length; x++)
			{
				if (x < inputMinusNewline && input.Slice(x, newLine.Length).SequenceEqual(newLine))
				{
					x++;
					d = 0;
					delim = delimterSequence[d].AsSpan();

					results[rCount] = builder.ToString();
					rCount++;
					builder.Clear();
				}
				else if (x < input.Length - delim.Length && input.Slice(x, delim.Length).SequenceEqual(delim))
				{
					x += delim.Length - 1;
					d++;
					if (d < delimterSequence.Length)
					{
						delim = delimterSequence[d].AsSpan();
					}

					results[rCount] = builder.ToString();
					rCount++;
					builder.Clear();
				}
				else if (!(x == inputMinusNewline && input.Slice(x, newLine.Length).SequenceEqual(newLine)))
				{
					builder.Append(input[x]);
				}
			}
			results[rCount] = builder.ToString();
			return results;
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