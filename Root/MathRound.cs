namespace ExamplesForInterview;

/// <summary>
/// Округление до ближайшего, в случае .5 - округление до чётного
/// </summary>
public class MathRound
{
    public void Run()
    {
        Console.WriteLine(MathF.Round(15.4f)); // 15
        Console.WriteLine(MathF.Round(15.6f)); // 16

        Console.WriteLine(MathF.Round(15.5f)); // 16
        Console.WriteLine(MathF.Round(16.5f)); // 16
        Console.WriteLine(MathF.Round(17.5f)); // 18
        Console.WriteLine(MathF.Round(18.5f)); // 18

        Console.WriteLine(Math.Round(15.5)); // 16
        Console.WriteLine(Math.Round(16.5)); // 16
        Console.WriteLine(Math.Round(17.5)); // 18
        Console.WriteLine(Math.Round(18.5)); // 18
    }
}