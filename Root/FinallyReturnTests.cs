namespace ExamplesForInterview;

/// <summary>
/// Return можно использовать в блоках try и catch, но нельзя в блоке finally
/// </summary>
public class FinallyReturnTests
{
    public void Run()
    {
        var number = GetNumber();
        Console.WriteLine($"Number = {number}");
    }

    private int GetNumber()
    {
        try
        {
            // throw new Exception();
            return ReturnWithLog(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ReturnWithLog(2);
        }
        finally
        {
            Console.WriteLine("Finally");
            // return ReturnWithLog(3); //Can Not
        }

        return ReturnWithLog(0);
    }

    public int ReturnWithLog(int value)
    {
        Console.WriteLine($"{nameof(ReturnWithLog)}, {nameof(value)} = {value}");
        return value;
    }
}