using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YonatanMankovich.AlphametricSolver
{
    public class AlphameticEquationSolver
    {
        public List<Dictionary<char, byte>> Solutions { get; } = new List<Dictionary<char, byte>>();
        public AlphameticEquation Equation { get; private set; }

        public void SolveEquation(AlphameticEquation equation, bool isSolutionUnique, bool allowLeadingZeros)
        {
            Equation = equation;
            AlphameticEquationSolverHelper[] alphameticEquationSolverHelpers = new AlphameticEquationSolverHelper[Environment.ProcessorCount];
            Parallel.ForEach(alphameticEquationSolverHelpers, (alphameticEquationSolverHelper, pls, index) =>
            {
                alphameticEquationSolverHelper = new AlphameticEquationSolverHelper(equation, isSolutionUnique, allowLeadingZeros);
                long triesPerProcessor = (long)Math.Pow(10, alphameticEquationSolverHelper.GetCountOfUniqueLetters()) / Environment.ProcessorCount;
                alphameticEquationSolverHelper.Solve(index * triesPerProcessor, (index + 1) * triesPerProcessor);
                Solutions.AddRange(alphameticEquationSolverHelper.Solutions);
            });
            foreach (Dictionary<char, byte> solution in Solutions)
            {
                PrintSolution(solution);
                Console.WriteLine();
            }
            Solutions.Clear();
        }

        public static void PrintDictionary(Dictionary<char, byte> letterNumberPairs)
        {
            foreach (KeyValuePair<char, byte> letterNumberPair in letterNumberPairs)
                Console.WriteLine($"{letterNumberPair.Key} = {letterNumberPair.Value}");
        }

        public void PrintSolution(Dictionary<char, byte> letterNumberPairs)
        {
            Console.WriteLine(Equation.ToString());
            Console.WriteLine(AlphameticEquationSolverHelper.TranslateAlphametic(Equation.ToString(), letterNumberPairs));
            Console.WriteLine();
        }
    }
}