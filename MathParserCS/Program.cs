using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserCS
{
    class Program
    {
        static void Main(string[] args)
        {
            string expr1 = "1+2/2-(11*8)/((6+2)*11)=";
            string expr2 = "11*8/((6-6.9,6)/11)=";
            try
            {
                Console.WriteLine(MathParser.GetRes(expr1));
                Console.WriteLine();
                Console.WriteLine(MathParser.GetRes(expr2));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
