using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("SyntaxParser.Tests.Shared")]
[assembly: InternalsVisibleTo("SyntaxParser.Tests.Core")]
namespace SyntaxParser
{
    internal static class StringExtensions
    {
        internal static T[] ToStructuredArray<T>(this string text, string[] delimterSequence, SequenceOptions sequenceOptions, int start = 0, int end = 0)
        {
            Type toType = typeof(T);
            int d = 0;
            ReadOnlySpan<char> input = text.AsSpan().Slice(start, text.Length - start - end);
            ReadOnlySpan<char> delim = delimterSequence[d].AsSpan();
            ReadOnlySpan<char> newLine = Environment.NewLine.AsSpan();
            int delimitersPerLine = delimterSequence.Length;
            int amountOfNewLines = 0;
            input.AmountOfOccurences(newLine, ref amountOfNewLines);
            T[] results;

            if (sequenceOptions.PropertyCount.HasValue)
            {
                results = new T[sequenceOptions.PropertyCount.Value * (amountOfNewLines + 1)];
                if(delimterSequence.Length >= sequenceOptions.PropertyCount.Value) 
                {
                    delimitersPerLine = sequenceOptions.PropertyCount.Value - 1;
                }
            } 
            else if (sequenceOptions.SequenceAlgorithm == SequenceAlgorithm.Exact && !sequenceOptions.PropertyCount.HasValue)
            {
                var delimGroup = delimterSequence.GroupBy(v => v).ToDictionary(v => v.Key, v => v.Count());
                foreach (var line in input.EnumerateLines())
                {
                    int delimLineCount = 0;
                    line.AmountOfOccurences(delim, ref delimLineCount);
                    if (delimLineCount > delimitersPerLine)
                    {
                        delimitersPerLine = delimLineCount;
                    }
                }
                results = new T[(delimitersPerLine + 1) * (amountOfNewLines + 1)];

            }
            else
            {
                results = new T[(delimterSequence.Length + 1) * (amountOfNewLines + 1)];
            }

            int rCount = 0;
            int segmentSize = 0;
            int inputMinusNewline = input.Length - newLine.Length;
            int delimEncounter = 0;
            for (int x = 0; x < input.Length; x++)
            {
                if (x < inputMinusNewline && input.Slice(x, newLine.Length).SequenceEqual(newLine))
                {
                    if (delimEncounter <= delimitersPerLine)
                    {
                        results[rCount++] = ValueParser(input.Slice(x - segmentSize, segmentSize), toType);
                        for (var i = delimEncounter; i < delimitersPerLine; i++)
                        {
                            rCount++;
                        }
                    }

                    x++;
                    d = 0;
                    delim = delimterSequence[d].AsSpan();

                    delimEncounter = 0;
                    segmentSize = 0;
                }
                else if (x < input.Length - delim.Length && input.Slice(x, delim.Length).SequenceEqual(delim))
                {
                    if (delimEncounter <= delimitersPerLine)
                    {
                        results[rCount++] = ValueParser(input.Slice(x - segmentSize, segmentSize), toType);
                    }

                    x += delim.Length - 1;
                    d++;
                    if (d < delimterSequence.Length)
                    {
                        delim = delimterSequence[d].AsSpan();
                    }

                    delimEncounter++;
                    segmentSize = 0;
                }
                else if (!(x == inputMinusNewline && input.Slice(x, newLine.Length).SequenceEqual(newLine)))
                {
                    segmentSize++;
                }
            }

            ReadOnlySpan<char> lastSegment = input.Slice(input.Length - segmentSize);
            var indexOfDelim = lastSegment.IndexOf(delim);
            if(indexOfDelim >= 0)
            {
                lastSegment = lastSegment.Slice(0, indexOfDelim);
                results[rCount++] = StringExtensions.ValueParser(lastSegment, toType);
                var x = input.Length - segmentSize + lastSegment.Length;
                for(var i = delimEncounter; i < delimitersPerLine; i++)
                {
               
                    if (input.Slice(x).IndexOf(delim) >= 0)
                    {
                        results[rCount++] = StringExtensions.ValueParser(string.Empty, toType);
                    }
                    x += delim.Length;
                }
            }
            else
            {
                results[rCount] = StringExtensions.ValueParser(lastSegment, toType);
            }
            
            return results;
        }

        internal static dynamic ValueParser(ReadOnlySpan<char> text, Type toType)
        {
            TypeCode typeCode = Type.GetTypeCode(toType);
            return typeCode switch
            {
                TypeCode.Boolean => bool.Parse(text),
                TypeCode.Char => text[0],
                TypeCode.SByte => sbyte.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Byte => byte.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Int16 => Int16.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.UInt16 => UInt16.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Int32 => Int32.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.UInt32 => UInt32.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Int64 => Int64.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.UInt64 => UInt64.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Single => Single.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Double => double.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.Decimal => decimal.Parse(text, provider: CultureInfo.InvariantCulture),
                TypeCode.DateTime => DateTime.ParseExact(text, "yyyyMMddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                TypeCode.String => text.ToString(),
                _ => throw new NotImplementedException()
            };
        }


        internal static void AmountOfOccurences(this ReadOnlySpan<char> text, ReadOnlySpan<char> charSet, ref int count)
        {
            int length = text.Length - charSet.Length;
            for (var i = 0; i <= length; i++)
            {
                if (text.Slice(i, charSet.Length).SequenceEqual(charSet))
                {
                    count++;
                }
            }
        }
    }
}