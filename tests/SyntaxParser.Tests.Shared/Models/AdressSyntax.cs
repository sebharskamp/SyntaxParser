using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser.Tests.Shared
{
    [Syntax($"{nameof(RegionCode)}@{nameof(NeighbourhoodCode)}#{nameof(Number)}")]
    public class AdressSyntax
    {
        public int RegionCode { get; set; }
        public string NeighbourhoodCode { get; set; }
        public string Number { get; set; }
    }
}
