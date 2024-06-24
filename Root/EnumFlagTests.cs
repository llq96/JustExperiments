namespace ExamplesForInterview;

// Log Without Flags: 5
// Log With Flags: FirstBool, ThirdBool
public class EnumFlagTests
{
    public void Run()
    {
        var valueWithoutFlags = TestEnumWithoutFlags.FirstBool | TestEnumWithoutFlags.ThirdBool;
        Console.WriteLine($"Log Without Flags: {valueWithoutFlags}");

        var valueWithFlags = TestEnumWithFlags.FirstBool | TestEnumWithFlags.ThirdBool;
        Console.WriteLine($"Log With Flags: {valueWithFlags}");
    }

    private enum TestEnumWithoutFlags
    {
        FirstBool = 1,
        SecondBool = 2,
        ThirdBool = 4
    }

    [Flags]
    private enum TestEnumWithFlags
    {
        FirstBool = 1,
        SecondBool = 2,
        ThirdBool = 4
    }
}