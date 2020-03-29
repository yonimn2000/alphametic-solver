using System;
using System.Collections.Generic;
using System.Linq;

namespace YonatanMankovich.AlphametricSolver
{
    public class AlphameticEquationSolverHelper
    {
        private AlphameticEquation Equation { get; }
        private Dictionary<char, byte> LetterNumberPairs { get; } = new Dictionary<char, byte>();

        public bool AllowRepeatingNumbers { get; set; }
        public bool AllowLeadingZeros { get; set; }
        public bool FindOneSolutionOnly { get; set; }

        public AlphameticEquationSolverHelper(AlphameticEquation equation, bool allowRepeatingNumbers, bool allowLeadingZeros, bool findOneSolutionOnly)
        {
            Equation = equation;
            AllowRepeatingNumbers = allowRepeatingNumbers;
            AllowLeadingZeros = allowLeadingZeros;
            FindOneSolutionOnly = findOneSolutionOnly;
            CreateLetterNumberPairs();
        }

        private void CreateLetterNumberPairs()
        {
            foreach (char letter in Equation.ToString().Where(char.IsLetter))
                if (!LetterNumberPairs.ContainsKey(letter)) // Add only unique letters.
                    LetterNumberPairs.Add(letter, 0);
        }

        public void Solve(long fromInclusive, long toExclusive, Action<AlphameticEquation, Dictionary<char, byte>> onSolutionFound)
        {
            for (long currentNumber = fromInclusive; currentNumber < toExclusive; currentNumber++)
            {
                SetSolution(currentNumber);
                if ((!AllowRepeatingNumbers || (AllowRepeatingNumbers && IsSolutionUnique()))
                    && (AllowLeadingZeros || (!AllowLeadingZeros && IsNoLeadingZeroInTerms()))
                    && IsSolutionCorrect())
                {
                    onSolutionFound(Equation, LetterNumberPairs.ToDictionary(entry => entry.Key, entry => entry.Value));
                    if (FindOneSolutionOnly)
                        break;
                }
            }
        }

        private bool IsSolutionUnique()
        {
            byte[] numbers = LetterNumberPairs.Values.ToArray();
            bool[] exists = new bool[10];
            for (int i = 0; i < exists.Length; i++)
                exists[i] = false;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (exists[numbers[i]])
                    return false;
                exists[numbers[i]] = true;
            }
            return true;
        }

        private void SetSolution(long number)
        {
            string numberString = number.ToString("D" + LetterNumberPairs.Count);
            for (int i = 0; i < LetterNumberPairs.Count; i++)
                LetterNumberPairs[LetterNumberPairs.Keys.ElementAt(i)] = (byte)(numberString[i] - 48);
        }

        private bool IsSolutionCorrect()
        {
            double result = int.Parse(TranslateAlphametic(Equation.Terms[0], LetterNumberPairs));
            for (int i = 0; i < Equation.Operators.Count; i++)
                result = MathOperatorsMethods.PerformOperation(result, int.Parse(TranslateAlphametic(Equation.Terms[i + 1], LetterNumberPairs)), Equation.Operators[i]);
            return result == int.Parse(TranslateAlphametic(Equation.EqualsPart, LetterNumberPairs));
        }

        private bool IsNoLeadingZeroInTerms()
        {
            if (LetterNumberPairs[Equation.EqualsPart[0]] == 0)
                return false;
            foreach (string term in Equation.Terms)
                if (LetterNumberPairs[term[0]] == 0)
                    return false;
            return true;
        }

        public byte GetCountOfUniqueLetters()
        {
            return (byte)LetterNumberPairs.Count;
        }

        public static string TranslateAlphametic(string inputString, Dictionary<char, byte> letterNumberPairs)
        {
            foreach (char letter in inputString)
                if (letterNumberPairs.ContainsKey(letter))
                    inputString = inputString.Replace(letter.ToString(), letterNumberPairs[letter].ToString());
            return inputString;
        }
    }
}