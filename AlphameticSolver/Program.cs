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
            Console.WriteLine("Please wait...\n");
            for (int i = 0; i < 10; i++)
            {
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                AlphameticEquationSolver alphameticEquationSolver = new AlphameticEquationSolver();
                alphameticEquationSolver.SolveEquation(new AlphameticEquation(equations[4]), true, false, PrintSolution);
                watch.Stop();
                Console.WriteLine("Computation time: " + watch.Elapsed);
                Console.WriteLine();
            }
            Console.ReadLine();
        }


        /*public static void PrintDictionary(Dictionary<char, byte> letterNumberPairs)
        {
            foreach (KeyValuePair<char, byte> letterNumberPair in letterNumberPairs)
                Console.WriteLine($"{letterNumberPair.Key} = {letterNumberPair.Value}");
        }*/
        static object printLock = new object();
        public static void PrintSolution(AlphameticEquation equation, System.Collections.Generic.Dictionary<char, byte> letterNumberPairs)
        {
            lock (printLock)
            {
                Console.WriteLine(equation.ToString());
                Console.WriteLine(AlphameticEquationSolverHelper.TranslateAlphametic(equation.ToString(), letterNumberPairs));
                Console.WriteLine();
            }
        }
    }
}