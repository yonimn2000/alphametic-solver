using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace YonatanMankovich.AlphameticSolver
{
    class Program
    {
        static readonly string EQUATIONS_PATH = "AlphameticEquations.txt";

        static void Main(string[] args)
        {
            foreach (string equation in File.ReadLines(EQUATIONS_PATH))
            {
                Console.WriteLine("Please wait...\nSolving " + equation + "\n");
                Stopwatch watch = Stopwatch.StartNew();
                AlphameticEquationSolver alphameticEquationSolver = new AlphameticEquationSolver();
                alphameticEquationSolver.SolveEquation(new AlphameticEquation(equation),
                    allowRepeatingNumbers: true, allowLeadingZeros: false, findOneSolutionOnly: true, onSolutionFound: PrintSolution);
                watch.Stop();
                Console.WriteLine("Computation time: " + watch.Elapsed);
                Console.WriteLine("\n*******************************************\n");
            }
            Console.ReadLine();
        }

        static readonly object printLock = new object();
        public static void PrintSolution(AlphameticEquation equation, Dictionary<char, byte> letterNumberPairs)
        {
            lock (printLock)
            {
                string solution = AlphameticEquationSolverHelper.TranslateAlphametic(equation.ToString(), letterNumberPairs);
                Console.WriteLine(equation.ToString());
                Console.WriteLine(solution);
                Console.WriteLine();
                PrintDictionary(letterNumberPairs);
                Console.WriteLine();
                for (int i = 0; i < solution.Length; i++)
                    Console.Write("-");
                Console.WriteLine("\n");
            }
        }

        public static void PrintDictionary(Dictionary<char, byte> letterNumberPairs)
        {
            foreach (KeyValuePair<char, byte> letterNumberPair in letterNumberPairs)
                Console.WriteLine($"{letterNumberPair.Key}={letterNumberPair.Value}");
        }
    }
}