using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YonatanMankovich.AlphametricSolver
{
    public class AlphameticEquationSolver
    {
        public AlphameticEquation Equation { get; private set; }

        public void SolveEquation(AlphameticEquation equation, bool allowRepeatingNumbers, bool allowLeadingZeros, Action<AlphameticEquation, Dictionary<char, byte>> onSolutionFound)
        {
            Equation = equation;
            AlphameticEquationSolverHelper[] alphameticEquationSolverHelpers = new AlphameticEquationSolverHelper[Environment.ProcessorCount];
            Parallel.ForEach(alphameticEquationSolverHelpers, (alphameticEquationSolverHelper, pls, index) =>
            {
                alphameticEquationSolverHelper = new AlphameticEquationSolverHelper(equation, allowRepeatingNumbers, allowLeadingZeros);
                long triesPerProcessor = (long)Math.Pow(10, alphameticEquationSolverHelper.GetCountOfUniqueLetters()) / Environment.ProcessorCount;
                alphameticEquationSolverHelper.Solve(index * triesPerProcessor, (index + 1) * triesPerProcessor, onSolutionFound);
            });
        }
    }
}