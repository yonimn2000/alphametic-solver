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
            CreateLetterNumberPairsList();
        }

        private void CreateLetterNumberPairsList()
        {
            foreach (string term in Equation.Terms)
                AddUniqueLettersToListFromString(term);
            AddUniqueLettersToListFromString(Equation.EqualsPart);
        }

        private void AddUniqueLettersToListFromString(string str)
        {
            foreach (char letter in str)
                if (!LetterNumberPairs.ContainsKey(letter))
                    for (byte i = 0; i < 10; i++)
                        if (!LetterNumberPairs.ContainsValue(i))
                        {
                            LetterNumberPairs.Add(letter, i);
                            break;
                        }
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
            for (int i = 0; i < str.Length; i++)
                if (str.Count(letter => letter == str[i]) > 1)
                    return false;
            return true;
        }

        private bool IsSolutionCorrect()
        {
            bool isNoLeadingZerosOnInputs = true;
            int sumOfInputs = 0;
            foreach (string term in Equation.Terms)
            {
                sumOfInputs += GetNumberFromString(term);
                isNoLeadingZerosOnInputs = isNoLeadingZerosOnInputs && GetNumberFromString(term.Substring(0, 1)) != 0;
            }
            return sumOfInputs == GetNumberFromString(Equation.EqualsPart) && GetNumberFromString(Equation.EqualsPart.Substring(0, 1)) != 0 && isNoLeadingZerosOnInputs;
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