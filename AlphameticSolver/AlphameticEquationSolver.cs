using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YonatanMankovich.AlphametricSolver
{
    public class AlphameticEquationSolver
    {
        public AlphameticEquation Equation { get; private set; }

        public void SolveEquation(AlphameticEquation equation, bool allowRepeatingNumbers, bool allowLeadingZeros, bool findOneSolutionOnly, Action<AlphameticEquation, Dictionary<char, byte>> onSolutionFound)
        {
            Equation = equation;
            AlphameticEquationSolverHelper[] alphameticEquationSolverHelpers = new AlphameticEquationSolverHelper[Environment.ProcessorCount];
            Parallel.ForEach(alphameticEquationSolverHelpers, (alphameticEquationSolverHelper, state, index) =>
            {
                alphameticEquationSolverHelper = new AlphameticEquationSolverHelper(equation, allowRepeatingNumbers, allowLeadingZeros, findOneSolutionOnly);
                long triesPerProcessor = (long)Math.Pow(10, alphameticEquationSolverHelper.GetCountOfUniqueLetters()) / Environment.ProcessorCount;
                alphameticEquationSolverHelper.Solve(index * triesPerProcessor, (index + 1) * triesPerProcessor, onSolutionFound);
                if (findOneSolutionOnly)
                    state.Break();
            });
        }
    }
}