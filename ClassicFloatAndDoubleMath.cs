namespace ExamplesForInterview;

//                  === Float ===
// Value1 = 0,1f  Value2 = 0,2f  Expected Sum = 0,3f
// Real Sum (Formatted)     = 0,30000001192092895508
// Expected Sum (Formatted) = 0,30000001192092895508
// Is Equals = True
//
//                  === Double ===
// Value1 = 0,1d  Value2 = 0,2d  Expected Sum = 0,3d
// Real Sum (Formatted)     = 0,30000000000000004441
// Expected Sum (Formatted) = 0,29999999999999998890
// Is Equals = False

public class ClassicFloatAndDoubleMath
{
    private const string DefaultFormat = "F20";

    public void Run()
    {
        CheckFloat(0.1f, 0.2f, 0.3f);
        CheckDouble(0.1d, 0.2d, 0.3d);

        // CheckFloat(0.1f, 0.8f, 0.9f);
        // CheckDouble(0.1d, 0.8d, 0.9d);
        // CheckFloat(1, 2, 3);
        // CheckDouble(1, 2, 3);
    }

    private static void CheckFloat(float value1, float value2, float expectedSum)
    {
        var realSum = value1 + value2;
        var realSumString = realSum.ToString(DefaultFormat);
        var expectedSumString = expectedSum.ToString(DefaultFormat);
        var isEquals = realSum == expectedSum;

        Log("Float", $"{value1}f", $"{value2}f", $"{expectedSum}f", realSumString, expectedSumString, isEquals);
    }

    private static void CheckDouble(double value1, double value2, double expectedSum)
    {
        var realSum = value1 + value2;
        var realSumString = realSum.ToString(DefaultFormat);
        var expectedSumString = expectedSum.ToString(DefaultFormat);
        var isEquals = realSum == expectedSum;

        Log("Double", $"{value1}d", $"{value2}d", $"{expectedSum}d", realSumString, expectedSumString, isEquals);
    }

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