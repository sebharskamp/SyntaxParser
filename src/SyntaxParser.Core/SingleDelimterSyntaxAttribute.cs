using System;

namespace SyntaxParser.Core
{
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
