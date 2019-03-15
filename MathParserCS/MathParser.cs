using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserCS
{
    public static class MathParser
    {
        static List<OperNode> OperList;
        public static double GetRes(string expression)
        {
            double res = 0;
            expression = ClearExpr(expression);
            Parse(expression);
            var orderList = OperList.OrderByDescending(n=>n.OperVeight);
            foreach(var oper in orderList)
            {
                double operRes = 0;

                switch (oper.OperType)
                {
                    case "-":
                        operRes = oper.LeftVal.Value - oper.RightVal.Value;
                        break;
                    case "+":
                        operRes = oper.LeftVal.Value + oper.RightVal.Value;
                        break;
                    case "/":
                        if (oper.RightVal.Value == 0)
                            throw new DivideByZeroException($"Divide by zero: {oper.LeftVal.Value}/{oper.RightVal.Value}");
                        operRes = oper.LeftVal.Value / oper.RightVal.Value;
                        break;
                    case "*":
                        operRes = oper.LeftVal.Value * oper.RightVal.Value;
                        break;
                }
                RewriteNodes(oper, operRes);
                res = operRes;
            }
            return res;
        }        
        private static void Parse(string expression)
        {
            OperList = new List<OperNode>();
            int operWeight = 1;
            int factor = 1;
            string value = "";
            OperNode lastOper = null;
            for(int i = 0; i < expression.Length; ++i)
            {
                double dVal = 0;
                string operType = expression[i].ToString();
                switch (operType)
                {
                    case "-":
                    case "+":
                        operWeight = 1 * factor;
                        CreateNodes(value, operType, operWeight, ref lastOper);
                        value = "";
                        break;
                    case "/":
                    case "*":
                        operWeight = 2 * factor;
                        CreateNodes(value, operType, operWeight, ref lastOper);
                        value = "";
                        break;
                    case "=":
                        dVal = GetValue(value);
                        ValueNode valNode = new ValueNode(dVal);
                        if (lastOper != null)
                        {
                            lastOper.RightVal = valNode;
                            lastOper.NextOper = null;
                        }
                        break;
                    case "(":
                        factor *= 4;
                        break;
                    case ")":
                        factor /= 4;
                        if(operWeight < 1)
                        {
                            throw new ArgumentException("Invalid brackets");
                        }
                        break;
                    default:
                        value += operType;
                        break;
                }
            }
            if (factor != 1)
            {
                throw new ArgumentException("Invalid brackets");
            }
        }
        private static void RewriteNodes(OperNode oper, double operRes)
        {
            ValueNode resNode = new ValueNode(operRes);
            if (oper.NextOper != null)
            {
                oper.NextOper.LeftVal = resNode;
                oper.NextOper.PrevOper = oper.PrevOper;
            }
            if (oper.PrevOper != null)
            {
                oper.PrevOper.RightVal = resNode;
                oper.PrevOper.NextOper = oper.NextOper;
            }
        }
        private static void CreateNodes(string value, string operType, int operWeight, ref OperNode lastOper)
        {
            double dVal = GetValue(value);
            ValueNode valNode = new ValueNode(dVal);
            OperNode oper = new OperNode(operType, operWeight, valNode, lastOper);
            if (lastOper != null)
            {
                lastOper.RightVal = valNode;
                lastOper.NextOper = oper;
            }
            lastOper = oper;
            OperList.Add(oper);
        }
        private static double GetValue(string strValue)
        {
            double res = 0;
            bool parseIsSuccess = double.TryParse(strValue, out res);
            if (!parseIsSuccess)
                throw new ArgumentException($"Invalid input: {strValue}");
            return res;
        }    
        
        private static string ClearExpr(string expression)
        {
            char[] temp = expression.ToCharArray();
            for(int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == ',')
                    temp[i] = '.';
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(temp);
            return stringBuilder.ToString();
        }
    }
}
