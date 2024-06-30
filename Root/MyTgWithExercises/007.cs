public class Rounding_007
{
    public static void Main()
    {
        Console.Write($"{(int)1.5} ");
        Console.Write($"{(int)2.5} ");

        Console.Write($"{Math.Round(1.5)} ");
        Console.Write($"{Math.Round(2.5)} ");

        Console.Write($"{Convert.ToInt32(1.5)} ");
        Console.Write($"{Convert.ToInt32(2.5)} ");
    }
}