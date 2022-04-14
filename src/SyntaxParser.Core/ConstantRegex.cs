using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly:InternalsVisibleTo("SyntaxParser.Benchmark")]
namespace SyntaxParser.Core
{    
    internal class ConstantRegex
    {
        internal static Regex Symbols = new Regex(@"[^a-zA-Z0-9 ]*", RegexOptions.Compiled);
    }
}
