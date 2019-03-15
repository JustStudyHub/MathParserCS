using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParserCS
{
    public static class MathParser
    {
        private static List<OperNode> _operList;
        public static double GetRes(string expression)
        {
            double res = 0;
            expression = ValidateInput(expression);
            Parse(expression);
            var orderList = _operList.OrderByDescending(n=>n.OperVeight);
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
            _operList = new List<OperNode>();

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
            double dVal = 0;
            if (!string.IsNullOrEmpty(value))
                dVal = GetValue(value);

            ValueNode valNode = new ValueNode(dVal);
            OperNode oper = new OperNode(operType, operWeight, valNode, lastOper);
            if (lastOper != null)
            {
                lastOper.RightVal = valNode;
                lastOper.NextOper = oper;
            }
            lastOper = oper;
            _operList.Add(oper);
        }
        private static double GetValue(string strValue)
        {
            double res = 0;
            bool parseIsSuccess = double.TryParse(strValue, out res);
            if (!parseIsSuccess)
                throw new ArgumentException($"Invalid input: {strValue}");
            return res;
        }            
        private static string ValidateInput(string expression)
        {
            if(!char.IsDigit(expression[0]) && expression[0] !='(' && expression[0] != '-')
                throw new ArgumentException($"Invalid input: expression may not start whith symbol {expression[0]}");
            char[] temp = expression.ToCharArray();
            for(int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == ',')
                    temp[i] = '.';
            }
            for (int i = 0; i < temp.Length - 1; i++)
            {
                if (char.IsDigit(temp[i]) && temp[i + 1] == '(')
                    throw new ArgumentException($"Invalid input: {temp[i]}{temp[i + 1]}");
                if (temp[i] == '=')
                    throw new ArgumentException($"Invalid input: {temp[i]}{temp[i + 1]}");
                if (!char.IsDigit(temp[i]) && !char.IsDigit(temp[i+1]))
                {
                    if (temp[i]=='(' && temp[i + 1] ==')')
                        throw new ArgumentException($"Invalid input: {temp[i]}{temp[i + 1]}");
                    if (IsBracket(temp[i]) && IsBracket(temp[i + 1]))
                        continue;
                    if (IsBracket(temp[i]))
                    {
                        if((temp[i+1] != '-') && (temp[i] == '('))
                            throw new ArgumentException($"Invalid input: {temp[i]}{temp[i + 1]}");
                    }
                    else if(IsBracket(temp[i + 1]))
                    {
                        if (temp[i+1] == ')')
                            throw new ArgumentException($"Invalid input: {temp[i]}{temp[i + 1]}");
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid input: {temp[i]}{temp[i + 1]}");
                    }
                }
            }
            
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(temp);
            if (temp[temp.Length - 1] != '=')
                stringBuilder.Append('=');
            return stringBuilder.ToString();
        }
        private static bool IsBracket(char symbol)
        {
            if (symbol == ')' || symbol == '(')
                return true;
            return false;
        }
    }
}
