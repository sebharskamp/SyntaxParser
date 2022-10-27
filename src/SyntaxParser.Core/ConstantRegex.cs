using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly:InternalsVisibleTo("SyntaxParser.Benchmark")]
namespace SyntaxParser
{    
    internal class ConstantRegex
    {
        internal static Regex SyntaxDelimiter = new Regex(@"(?=.)[^\d\s\w\[\],{}]{1,}", RegexOptions.Compiled);
        internal static Regex PropertyNameDelimiter = new Regex(@"(?=.)[\d\s\w\[\],{}]{1,}", RegexOptions.Compiled);
        internal static Regex ArrayOrClassDelimiter = new Regex(@"\[(?<array>.*?((?=.)[\w\d\s]{1,}.*?))\]|{(?<class>.*?((?<=.)[\w\d\s]{1,}))}", RegexOptions.Compiled);
    }
}
