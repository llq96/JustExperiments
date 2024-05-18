using System.Globalization;
using System.Numerics;

namespace ExamplesForInterview;

//              === System.Single ===
// Value1 = 0,1f  Value2 = 0,2f  Expected Sum = 0,3f
// Real Sum (Formatted)     = 0.30000001192092895508
// Expected Sum (Formatted) = 0.30000001192092895508
// Is Equals = True
//
//              === System.Double ===
// Value1 = 0,1d  Value2 = 0,2d  Expected Sum = 0,3d
// Real Sum (Formatted)     = 0.30000000000000004441
// Expected Sum (Formatted) = 0.29999999999999998890
// Is Equals = False

public static class ClassicFloatAndDoubleMath
{
    private const string DefaultFormat = "F20";

    public static void Run()
    {
        Check(0.1f, 0.2f, 0.3f);
        Check(0.1d, 0.2d, 0.3d);

        // Check(0.1f, 0.8f, 0.9f);
        // Check(0.1d, 0.8d, 0.9d);
        // Check(1, 2, 3);
        // Check(1, 2, 3);
    }

    private static void Check<T>(T value1, T value2, T expectedSum) where T : INumber<T>, IFormattable
    {
        var prefix = value1 is float ? "f" : value1 is double ? "d" : "";
        var realSum = value1 + value2;
        var realSumString = realSum.ToFormattedString();
        var expectedSumString = expectedSum.ToFormattedString();
        var isEquals = realSum == expectedSum;

        Log($"{typeof(T)}", $"{value1}{prefix}", $"{value2}{prefix}", $"{expectedSum}{prefix}", realSumString,
            expectedSumString, isEquals);
    }

    private static string ToFormattedString<T>(this T value) where T : IFormattable =>
        value.ToString(DefaultFormat, CultureInfo.InvariantCulture);

    private static void Log(
        string experimentName,
        string value1,
        string value2,
        string expectedSum,
        string realSumFormattedString,
        string expectedSumFormattedString,
        bool isEquals)
    {
        Console.WriteLine($"=== {experimentName} ===");
        Console.WriteLine($"Value1 = {value1}  Value2 = {value2}  Expected Sum = {expectedSum}");
        Console.WriteLine($"Real Sum (Formatted)     = {realSumFormattedString}");
        Console.WriteLine($"Expected Sum (Formatted) = {expectedSumFormattedString}");
        Console.WriteLine($"Is Equals = {isEquals}");
        Console.WriteLine();
    }
}