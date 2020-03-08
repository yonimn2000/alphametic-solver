using System;
using System.Collections.Generic;
using System.Linq;

namespace YonatanMankovich.AlphametricSolver
{
    class Program
    {
        static readonly Dictionary<char, int> letterNumberPairs = new Dictionary<char, int>();
        static readonly string[] inputs = { "SEND", "MORE" };
        static readonly string equals = "MONEY";
        //static readonly string[] inputs = { "ABC", "ABC", "ABC" };
        //static readonly string equals = "CCC";
        //static readonly string[] inputs = { "A", "A", "A", "B", "B", "B", "B" };
        //static readonly string equals = "AB";
        //static readonly string[] inputs = { "SO", "MANY", "MORE", "MEN", "SEEM", "TO", "SAY", "THAT", "THEY", "MAY", "SOON", "TRY", "TO", "STAY", "AT", "HOME", "SO", "AS", "TO", "SEE", "OR", "HEAR", "THE", "SAME", "ONE", "MAN", "TRY", "TO", "MEET", "THE", "TEAM", "ON", "THE", "MOON", "AS", "HE", "HAS", "AT", "THE", "OTHER", "TEN" };
        //static readonly string equals = "TESTS";
        //(The answer is TRANHYSMOE=9876543210.)
        //static readonly string[] inputs = { "TAB", "TAB", "TAB", "TAB" };
        //static readonly string equals = "BET";
        //static readonly string[] inputs = { "NO", "GUN", "NO" };
        //static readonly string equals = "HUNT";

        static void Main(string[] args)
        {
            Console.WriteLine("Please wait...");
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            CreateLetterNumberPairsList();
            Solve();
            watch.Stop();
            Console.WriteLine("Computation time: " + watch.Elapsed);
            Console.ReadKey();
        }

        static void CreateLetterNumberPairsList()
        {
            foreach (string input in inputs)
                AddUniqueLettersToListFromString(input);
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
                Console.WriteLine($"Done ({solutionCounter} solution(s))");
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
            bool isNoLeadingZerosOnInputs = true;
            int sumOfInputs = 0;
            foreach (string input in inputs)
            {
                sumOfInputs += GetNumberFromString(input);
                isNoLeadingZerosOnInputs = isNoLeadingZerosOnInputs && GetNumberFromString(input.Substring(0, 1)) != 0;
            }
            return sumOfInputs == GetNumberFromString(equals) && GetNumberFromString(equals.Substring(0, 1)) != 0 && isNoLeadingZerosOnInputs;
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
            Console.WriteLine(string.Join(" + ", inputs) + " = " + equals);
            int[] solutions = new int[inputs.Length];
            for (int i = 0; i < solutions.Length; i++)
                solutions[i] = GetNumberFromString(inputs[i]);
            Console.WriteLine(string.Join(" + ", solutions) + " = " + GetNumberFromString(equals));
        }
    }
}