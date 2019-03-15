using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserCS
{
    class OperNode
    {
        public OperNode(string operType, int operWeight, ValueNode leftVal, OperNode prevOper)
        {
            OperType = operType;
            OperVeight = operWeight;
            LeftVal = leftVal;
            RightVal = null;
            PrevOper = prevOper;
            NextOper = null;
        }
        public string OperType { get; set; }
        public int OperVeight { get; set; }
        public ValueNode LeftVal { get; set; }
        public ValueNode RightVal { get; set; }
        public OperNode NextOper { get; set; }
        public OperNode PrevOper { get; set; }
    }
}
