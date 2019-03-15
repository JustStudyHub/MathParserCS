using System;
using MathParserCS;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MathParserCSTests
{
    [TestClass]
    public class MathParserTests
    {
        [TestMethod]
        public void CorrectExprWithoutBrackets()
        {
            string expr1 = "1+2/2-11*8/6+2*11=";
            string expr2 = "11*8/6=";
            string expr3 = "11-7*8/6=";
            string expr4 = "11.8*8/6=";
            string expr5 = "11,8*8,4/6=";
            string expr6 = "6+2/11";

            double res1 = 1 + 2.0 / 2 - 11.0 * 8.0 / 6 + 2 * 11;
            double res2 = 11 * 8.0 / 6;
            double res3 = 11 - 7 * 8.0 / 6;
            double res4 = 11.8 * 8.0 / 6;
            double res5 = 11.8 * 8.4 / 6;
            double res6 = 6 + 2.0 / 11;

            Assert.IsTrue(MathParser.GetRes(expr1) == res1);
            Assert.IsTrue(MathParser.GetRes(expr2) == res2);
            Assert.IsTrue(MathParser.GetRes(expr3) == res3);
            Assert.IsTrue(MathParser.GetRes(expr4) == res4);
            Assert.IsTrue(MathParser.GetRes(expr5) == res5);
            Assert.IsTrue(MathParser.GetRes(expr6) == res6);
        }
        [TestMethod]
        public void CorrectExprWithBrackets()
        {
            string expr1 = "1+((2/2)-11*8/6+2)*11=";
            string expr2 = "11*(8/6)=";
            string expr3 = "(11-7)*8/6=";
            string expr4 = "(11.8*8)/6=";
            string expr5 = "11,8*(8,4/6)=";
            string expr6 = "11+(-(-7))=";

            double res1 = 1 + ((2.0 / 2) - 11.0 * 8.0 / 6 + 2) * 11;
            double res2 = 11 * (8.0 / 6);
            double res3 = (11 - 7) * 8.0 / 6;
            double res4 = (11.8 * 8.0) / 6;
            double res5 = 11.8 * (8.4 / 6);
            double res6 = 11 +(- (-7));

            Assert.IsTrue(MathParser.GetRes(expr1) == res1);
            Assert.IsTrue(MathParser.GetRes(expr2) == res2);
            Assert.IsTrue(MathParser.GetRes(expr3) == res3);
            Assert.IsTrue(MathParser.GetRes(expr4) == res4);
            Assert.IsTrue(MathParser.GetRes(expr5) == res5);
            Assert.IsTrue(MathParser.GetRes(expr6) == res6);
        }
        [TestMethod]
        public void BracketsAreIncorrect()
        {
            string expr1 = "1+2)=";
            string expr2 = "(1+2=";
            string expr3 = "(11(-7))=";
            string expr4 = "(11'(8*8)/6)=";
            string expr5 = "11,8(*(8,4/6))=";

            Action action;
            action = (() => MathParser.GetRes(expr1));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr2));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr3));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr4));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr5));
            Assert.ThrowsException<ArgumentException>(action);
        }
        [TestMethod]
        public void InputIsInvalid()
        {
            string expr1 = "6+2*/11=";
            string expr2 = "*8/6="; 
            string expr3 = "11(7)*8=";
            string expr4 = "11.8*8=/6=";
            string expr5 = "11,8(*8,4/6)=";
            string expr6 = "11.8*8=6=";
            string expr7 = "d*8=6=";

            Action action;
            action = (() => MathParser.GetRes(expr1));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr2));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr3));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr4));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr5));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr6));
            Assert.ThrowsException<ArgumentException>(action);
            action = (() => MathParser.GetRes(expr7));
            Assert.ThrowsException<ArgumentException>(action);
        }
    }
}
