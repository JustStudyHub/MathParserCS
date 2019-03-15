using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserCS
{
    internal class OperNode
    {
        public string OperType { get; private set; }
        public int OperVeight { get; private set; }
        public ValueNode LeftVal { get; set; }
        public ValueNode RightVal { get; set; }
        public OperNode NextOper { get; set; }
        public OperNode PrevOper { get; set; }
        public OperNode(string operType, int operWeight, ValueNode leftVal, OperNode prevOper)
        {
            OperType = operType;
            OperVeight = operWeight;
            LeftVal = leftVal;
            RightVal = null;
            PrevOper = prevOper;
            NextOper = null;
        }
    }
}
