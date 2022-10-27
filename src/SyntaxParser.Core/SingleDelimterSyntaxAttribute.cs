using System;

namespace SyntaxParser.Core
{
    /// <summary>
    /// Provide the delimiter separating the input values. 
    /// <see href="https://syntaxparser.netlify.app/?id=singledelimiterattribute">For an example</see>
    /// </summary>
    public class SingleDelimterSyntaxAttribute : Attribute
    {
        private readonly string _delimiter;
        public SingleDelimterSyntaxAttribute(string delimiter)
        {
            _delimiter = delimiter;
        }

        public string Value => _delimiter;
    }
}
