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
            Console.Write("Print x : ");
            numberString = Console.ReadLine();
        } while (double.TryParse(numberString, out x) == false);

        var result = Calculate(x);
        Console.WriteLine($"Result = {result}");
    }

    private static double Calculate(double x, double e = DefaultEpsilon) =>
        Enumerable.Range(0, int.MaxValue)
            .Select(n => Math.Pow(x - 1, 2 * n + 1) / ((2 * n + 1) * Math.Pow(x + 1, 2 * n + 1)))
            .TakeWhile(stepResult => stepResult >= e)
            .Sum();
}