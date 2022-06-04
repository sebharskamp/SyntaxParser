using System.Globalization;
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
			Type toType = typeof(T);
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
					results[rCount] = valueParser(input.Slice(x - segmentSize, segmentSize), toType);
					x++;
					d = 0;
					delim = delimterSequence[d].AsSpan();

					segmentSize = 0;
					rCount++;
				}
				else if (x < input.Length - delim.Length && input.Slice(x, delim.Length).SequenceEqual(delim))
				{
					results[rCount] = valueParser(input.Slice(x - segmentSize, segmentSize), toType);
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
			results[rCount] = valueParser(input.Slice(input.Length - segmentSize), toType);
			return results;
		}

		internal static dynamic valueParser(ReadOnlySpan<char> text, Type toType)
        {
			TypeCode typeCode = Type.GetTypeCode(toType);
			return typeCode switch
            {
                TypeCode.Boolean => bool.Parse(text),
                TypeCode.Char => text[0],
                TypeCode.SByte => sbyte.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Byte => byte.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Int16 =>Int16.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.UInt16 =>UInt16.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Int32 =>Int32.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.UInt32 =>UInt32.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Int64 =>Int64.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.UInt64 =>UInt64.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Single =>Single.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Double =>double.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Decimal =>decimal.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.DateTime =>DateTime.ParseExact(text, "yyyyMMddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                TypeCode.String =>text.ToString(),
                _ => throw new NotImplementedException()
            };
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