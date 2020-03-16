using System;

namespace YonatanMankovich.AlphametricSolver
{
    class Program
    {
        static readonly string[] equations = { "SEND+MORE=MONEY",
                                               "ABC+ABC+ABC=CCC",
                                               "A+A+A+B+B+B+B=AB",
                                               "TAB+TAB+TAB+TAB=BET",
                                               "NO+GUN+NO=HUNT",
                                               "SO+MANY+MORE+MEN+SEEM+TO+SAY+THAT+THEY+MAY+SOON+TRY+TO+STAY+AT+HOME+SO+AS+TO+SEE+OR+HEAR+THE+SAME+ONE+MAN+TRY+TO+MEET+THE+TEAM+ON+THE+MOON+AS+HE+HAS+AT+THE+OTHER+TEN=TESTS" //(The answer is TRANHYSMOE=9876543210.)
        };

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Please wait...");
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                AlphameticEquationSolver alphameticEquationSolver = new AlphameticEquationSolver();
                alphameticEquationSolver.SolveEquation(new AlphameticEquation(equations[0]), true, false);
                watch.Stop();
                Console.WriteLine("Computation time: " + watch.Elapsed);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}