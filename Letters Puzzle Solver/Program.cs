using System;
using System.Collections.Generic;
using System.Linq;

namespace Letters_Puzzle_Solver
{
    class Program
    {
        static Dictionary<char, int> letterNumberPairs = new Dictionary<char, int>();
        static readonly string input1 = "SEND";
        static readonly string input2 = "MORE";
        static readonly string equals = "MONEY";
  
        static void Main(string[] args)
        {
            CreateLetterNumberPairsList();
            Solve();
            Console.ReadKey();
        }

        static void CreateLetterNumberPairsList()
        {
            AddUniqueLettersToListFromString(input1);
            AddUniqueLettersToListFromString(input2);
            AddUniqueLettersToListFromString(equals);
        }
        static void AddUniqueLettersToListFromString(string str)
        {
            foreach (char letter in str)
                if (!letterNumberPairs.ContainsKey(letter))
                    for (int i = 0; i < 10; i++)
                        if (!letterNumberPairs.ContainsValue(i))
                        {
                            letterNumberPairs.Add(letter, i);
                            break;
                        }
        }
        int Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }
        static string numberString = "";
        static void Solve()
        {
            int solutionCounter = 0;
            for (long i = 0; i < Math.Pow(9, letterNumberPairs.Count); i++)
            {
                if (IsSolutionCorrect(true))
                {
                    PrintSolution();
                    solutionCounter++;
                }
                if (numberString.Length > letterNumberPairs.Count)
                    break;
                SetNextPossibility();
            }
            if (solutionCounter == 0)
                Console.WriteLine("Cannot solve...");
            else
                Console.WriteLine($"Done ({solutionCounter} solutions)");
        }
        static void SetNextPossibility()
        {
            List<int> numbers = letterNumberPairs.Values.ToList();
            long number = long.Parse(String.Join("", numbers.ToArray()));
            numberString = "";
            do
            {
                number++;
                if (number.ToString().Length < letterNumberPairs.Count)
                    numberString = "0" + number.ToString();
                else
                    numberString = number.ToString();
            } while (!IsStringUnique(numberString));
            for (int i = 0; i < letterNumberPairs.Count; i++)
                letterNumberPairs[letterNumberPairs.Keys.ElementAt(i)] = (int)numberString[i] - 48;
        }
        static bool IsStringUnique(string str)
        {
            for (int i = 0; i < str.Length; i++)
                if (str.Count(letter => letter == str[i]) > 1)
                    return false;
            return true;
        }
        static bool IsSolutionCorrect(bool noZeros = false)
        {
            return (GetNumberFromString(input1) + GetNumberFromString(input2)) == GetNumberFromString(equals)
                && GetNumberFromString(input1.Substring(0, 1)) != 0
                && GetNumberFromString(input2.Substring(0, 1)) != 0
                && GetNumberFromString(equals.Substring(0, 1)) != 0;
        }
        static int GetNumberFromString(string str)
        {
            int output = 0;
            for (int i = 0; i < str.Length; i++)
                output += letterNumberPairs[str[i]] * (int)Math.Pow(10, str.Length - 1 - i);
            return output;
        }

        static void PrintDictionary()
        {
            foreach (KeyValuePair<char, int> keyValuePair in letterNumberPairs)
                Console.WriteLine($"{keyValuePair.Key} = {keyValuePair.Value}");
        }

        static void PrintSolution()
        {
            Console.WriteLine(GetNumberFromString(input1) + " + " + GetNumberFromString(input2) + " = " + GetNumberFromString(equals));
        }
    }
}