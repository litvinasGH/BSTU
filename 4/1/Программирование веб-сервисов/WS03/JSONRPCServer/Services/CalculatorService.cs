
namespace JSONRPCServer.Services;

public class CalculatorService
{
    public double SUM(double x, double y)
        => x + y;

    public double SUB(double x, double y)
        => x - y;

    public double MUL(double x, double y)
        => x * y;

    public double DIV(double x, double y)
    {
        if (y == 0)
            throw new DivideByZeroException("Division by zero");

        return x / y;
    }

    public int FACT(int x)
    {
        if (x < 0)
            throw new ArgumentException("Negative factorial");

        int result = 1;

        for (int i = 2; i <= x; i++)
        {
            checked
            {
                result *= i;
            }
        }

        return result;
    }
}
