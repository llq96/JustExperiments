namespace ExamplesForInterview;

public class RecordILCodeTests
{
    public void Run()
    {
        TestRecord testRecord = new TestRecord(1, "qwe");
    }
}

public record TestRecord(int Value1, string Value2);