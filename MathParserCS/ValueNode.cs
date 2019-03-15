using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserCS
{
    internal class ValueNode
    {
        public double Value { get; set; }
        public ValueNode(double value)
        {
            Value = value;
        }
    }
}
