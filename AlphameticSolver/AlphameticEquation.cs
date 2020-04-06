using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YonatanMankovich.AlphameticSolver
{
    public class AlphameticEquation
    {
        public string EqualsPart { get; set; }
        public IList<string> Terms { get; } = new List<string>();
        public IList<MathOperators> Operators { get; } = new List<MathOperators>();

        public AlphameticEquation(string input)
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
                Operators.Add(MathOperatorsMethods.GetOperatorFromString(operatorMatch.Value));
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Terms.Count - 1; i++)
                output += Terms[i] + MathOperatorsMethods.OperatorToString(Operators[i]);
            return output + Terms[Terms.Count - 1] + "=" + EqualsPart;
        }
    }
}