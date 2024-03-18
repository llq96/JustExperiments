namespace ExamplesForInterview;

public class YieldILCodeTests
{
    public IEnumerable<int> Get()
    {
        yield return 0;
        yield return 1;
        yield return 2;
    }
}