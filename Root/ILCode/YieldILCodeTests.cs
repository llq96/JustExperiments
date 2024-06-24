namespace ExamplesForInterview;

/// <summary>
/// Finaly вызывается в последнем стейте и в Dispose
/// </summary>
public class YieldILCodeTests
{
    public IEnumerable<int> Get()
    {
        try
        {
            yield return 0;
            yield return 1;
            yield return 2;
        }
        finally
        {
            Console.WriteLine("Log");
        }
    }
}