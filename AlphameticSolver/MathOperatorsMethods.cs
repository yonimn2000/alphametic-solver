using System;

namespace YonatanMankovich.AlphametricSolver
{
    public enum MathOperators { Add, Subtract, Multiply, Divide }

    public static class MathOperatorsMethods
    {
        public static MathOperators GetOperatorFromString(string input)
        {
            switch (input)
            {
                case "+": return MathOperators.Add;
                case "-": return MathOperators.Subtract;
                case "*": return MathOperators.Multiply;
                case "/": return MathOperators.Divide;
                default: throw new FormatException(input + " is an invalid math operator.");
            }
        }

        public static string OperatorToString(MathOperators mathOperator)
        {
            switch (mathOperator)
            {
                case MathOperators.Add: return "+";
                case MathOperators.Subtract: return "-";
                case MathOperators.Multiply: return "*";
                case MathOperators.Divide: return "/";
                default: throw new NotImplementedException();
            }
        }

        public static double PerformOperation(double left, double right, MathOperators mathOperator)
        {
            switch (mathOperator)
            {
                case MathOperators.Add: return left + right;
                case MathOperators.Subtract: return left - right;
                case MathOperators.Multiply: return left * right;
                case MathOperators.Divide: return left / right;
                default: throw new NotImplementedException("How did you even get here???");
            }
        }
    }
}