namespace ExamplesForInterview.TgSolution;

public static class Series
{
    private const double DefaultEpsilon = 0.00001;

    public static void Run()
    {
        string numberString;
        double x;
        do
        {
            Console.WriteLine("X number: ");
            numberString = Console.ReadLine();
        } while (double.TryParse(numberString, out x) == false);

        var result = Calculate(x);
        var result2 = Calculate2(x);
        Console.WriteLine($"Result : {result}");
        Console.WriteLine($"Result2 : {result2}");
    }

    private static double Calculate(double x, double e = DefaultEpsilon)
    {
        double result = 0;
        for (int n = 0;; n++)
        {
            var numerator = Math.Pow(x - 1, 2 * n + 1);
            var denominator = (2 * n + 1) * Math.Pow(x + 1, 2 * n + 1);
            var stepResult = numerator / denominator;
            if (stepResult < e)
            {
                break;
            }

            result += stepResult;
        }

        return result;
    }

    private static double Calculate2(double x, double e = DefaultEpsilon)
    {
        var result = Enumerable.Range(0, int.MaxValue)
            .Select(n => Math.Pow(x - 1, 2 * n + 1) / ((2 * n + 1) * Math.Pow(x + 1, 2 * n + 1)))
            .TakeWhile(stepResult => stepResult >= e)
            .Sum();
        return result;
    }
}