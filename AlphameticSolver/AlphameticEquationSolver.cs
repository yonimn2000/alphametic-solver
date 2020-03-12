using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YonatanMankovich.AlphametricSolver
{
    public class AlphameticEquationSolver
    {
        private AlphameticEquation Equation { get; set; }
        private Dictionary<char, byte> LetterNumberPairs { get; set; } = new Dictionary<char, byte>();

        public AlphameticEquationSolver(AlphameticEquation equation)
        {
            Equation = equation;
            CreateLetterNumberPairs();
        }

        private void CreateLetterNumberPairs()
        {
            byte c = 0; // This is used to create the smallest unique number: 0123...
            foreach (char letter in Equation.ToString().Where(char.IsLetter))
                if (!LetterNumberPairs.ContainsKey(letter)) // Add only unique letters.
                    LetterNumberPairs.Add(letter, c++);
        }

        string numberString = "";
        public void Solve()
        {
            int solutionCounter = 0;
            for (long i = 0; i < Math.Pow(9, LetterNumberPairs.Count); i++)
            {
                if (IsSolutionCorrect())
                {
                    PrintSolution();
                    solutionCounter++;
                }
                if (numberString.Length > LetterNumberPairs.Count)
                    break;
                SetNextPossibility();
            }
            if (solutionCounter == 0)
                Console.WriteLine("Cannot solve...");
            else
                Console.WriteLine($"Done ({solutionCounter} solution(s))");
        }

        private void SetNextPossibility()
        {
            List<byte> numbers = LetterNumberPairs.Values.ToList();
            long number = long.Parse(string.Join("", numbers.ToArray()));
            numberString = "";
            do
            {
                number++;
                if (number.ToString().Length < LetterNumberPairs.Count)
                    numberString = "0" + number.ToString();
                else
                    numberString = number.ToString();
            } while (!IsStringUnique(numberString));
            for (int i = 0; i < LetterNumberPairs.Count; i++)
                LetterNumberPairs[LetterNumberPairs.Keys.ElementAt(i)] = (byte)(numberString[i] - 48);
        }

        private bool IsStringUnique(string str)
        {
            for (int firstCharIndex = 0; firstCharIndex < str.Length; firstCharIndex++)
                for (int secondCharIndex = firstCharIndex + 1; secondCharIndex < str.Length; secondCharIndex++)
                    if (str[firstCharIndex] == str[secondCharIndex])
                        return false;
            return true;
        }

        private bool IsSolutionCorrect()
        {
            // TODO: Add other math operators (-*/).
            bool isNoLeadingZerosOnInputs = true;
            int sumOfTerms = 0;
            foreach (string term in Equation.Terms)
            {
                sumOfTerms += GetNumberFromString(term);
                isNoLeadingZerosOnInputs = isNoLeadingZerosOnInputs && GetNumberFromString(term.Substring(0, 1)) != 0;
            }
            return sumOfTerms == GetNumberFromString(Equation.EqualsPart)
                && GetNumberFromString(Equation.EqualsPart.Substring(0, 1)) != 0
                && isNoLeadingZerosOnInputs;
        }

        private int GetNumberFromString(string str)
        {
            int output = 0;
            for (int i = 0; i < str.Length; i++)
                output += LetterNumberPairs[str[i]] * (int)Math.Pow(10, str.Length - 1 - i);
            return output;
        }

        private void PrintDictionary()
        {
            foreach (KeyValuePair<char, byte> keyValuePair in LetterNumberPairs)
                Console.WriteLine($"{keyValuePair.Key} = {keyValuePair.Value}");
        }

        private void PrintSolution()
        {
            Console.WriteLine(string.Join(" + ", Equation.Terms) + " = " + Equation.EqualsPart);
            int[] solutions = new int[Equation.Terms.Count];
            for (int i = 0; i < solutions.Length; i++)
                solutions[i] = GetNumberFromString(Equation.Terms[i]);
            Console.WriteLine(string.Join(" + ", solutions) + " = " + GetNumberFromString(Equation.EqualsPart));
        }
    }
}