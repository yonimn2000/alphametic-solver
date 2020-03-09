using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YonatanMankovich.AlphametricSolver
{
    public class AlphameticEquation
    {
        public string EqualsPart { get; set; }
        public IList<string> Terms { get; set; } = new List<string>();
        public IList<MathOperators> Operators { get; set; } = new List<MathOperators>();

        public AlphameticEquation (string input)
        {
            input = input.ToUpper();

            Match matchEquation = Regex.Match(input, @"^([A-Z]+[+\-/*])*[A-Z]+=[A-Z]+$"); // Check equation format.
            if (!matchEquation.Success)
                throw new FormatException("Invalid math equation format.");

            string[] equationSides = input.Split('='); // Split into left and right parts.
            EqualsPart = equationSides[1];

            MatchCollection termMatches = Regex.Matches(equationSides[0], @"[A-Z]+"); // Get all letter terms.
            foreach (Match termMatch in termMatches)
                Terms.Add(termMatch.Value);

            MatchCollection operatorMatches = Regex.Matches(equationSides[0], @"[+\-/*]"); // Get all operators.
            foreach (Match operatorMatch in operatorMatches)
                Operators.Add(GetOperatorFromString(operatorMatch.Value));
        }

        private static MathOperators GetOperatorFromString(string input)
        {
            switch (input)
            {
                case "+": return MathOperators.Add;
                case "-": return MathOperators.Subtract;
                case "*": return MathOperators.Multiply;
                case "/": return MathOperators.Divide;
                default: throw new FormatException(input + " is an invalid math operator.");
            }
        }
    }

    public enum MathOperators { Add, Subtract, Multiply, Divide }
}