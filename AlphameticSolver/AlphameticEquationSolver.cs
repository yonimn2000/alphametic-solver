using System;
using System.Collections.Generic;
using System.Linq;

namespace YonatanMankovich.AlphametricSolver
{
    public class AlphameticEquationSolver
    {
        private AlphameticEquation Equation { get; set; }
        private Dictionary<char, byte> LetterNumberPairs { get; set; } = new Dictionary<char, byte>();

        public bool IsUnique { get; set; }
        public bool AllowLeadingZeros { get; set; }

        public AlphameticEquationSolver(AlphameticEquation equation, bool isSolutionUniqe, bool allowLeadingZeros)
        {
            Equation = equation;
            IsUnique = isSolutionUniqe;
            AllowLeadingZeros = allowLeadingZeros;
        }

        public void Solve()
        {
            CreateLetterNumberPairs();
            int solutionCounter = 0;
            for (long currentNumber = 0; currentNumber < Math.Pow(10, LetterNumberPairs.Count); currentNumber++)
            {
                SetSolution(currentNumber);
                if ((!IsUnique || (IsUnique && IsSolutionUnique()))
                    && (AllowLeadingZeros || (!AllowLeadingZeros && IsNoLeadingZeroInTerms()))
                    && IsSolutionCorrect())
                {
                    PrintSolution();
                    solutionCounter++;
                }
            }
            if (solutionCounter == 0)
                Console.WriteLine("Cannot solve...");
            else
                Console.WriteLine($"Done ({solutionCounter}{(IsUnique ? " unique" : "")} solution{(solutionCounter > 1 ? "s" : "")})");
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

        private void CreateLetterNumberPairs()
        {
            foreach (char letter in Equation.ToString().Where(char.IsLetter))
                if (!LetterNumberPairs.ContainsKey(letter)) // Add only unique letters.
                    LetterNumberPairs.Add(letter, 0);
        }

        private void SetSolution(long number)
        {
            string numberString = number.ToString("D" + LetterNumberPairs.Count);
            for (int i = 0; i < LetterNumberPairs.Count; i++)
                LetterNumberPairs[LetterNumberPairs.Keys.ElementAt(i)] = (byte)(numberString[i] - 48);
        }

        private bool IsSolutionCorrect()
        {
            // TODO: Add other math operators (-/*).
            int sumOfTerms = 0;
            foreach (string term in Equation.Terms)
                sumOfTerms += int.Parse(TranslateAlphametic(term));
            return sumOfTerms == int.Parse(TranslateAlphametic(Equation.EqualsPart));
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

        private string TranslateAlphametic(string inputString)
        {
            foreach (char character in inputString)
                if (LetterNumberPairs.ContainsKey(character))
                    inputString = inputString.Replace(character.ToString(), LetterNumberPairs[character].ToString());
            return inputString;
        }

        private void PrintDictionary()
        {
            foreach (KeyValuePair<char, byte> keyValuePair in LetterNumberPairs)
                Console.WriteLine($"{keyValuePair.Key} = {keyValuePair.Value}");
        }

        private void PrintSolution()
        {
            Console.WriteLine(Equation.ToString());
            Console.WriteLine(TranslateAlphametic(Equation.ToString()));
            Console.WriteLine();
        }
    }
}