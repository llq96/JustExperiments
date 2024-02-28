namespace ExamplesForInterview;

public class CovarianceDelegates
{
    void ProcessString(String s)
    {
    }

    void ProcessAnyObject(Object o)
    {
    }

    String GetString() => null;

    Object GetAnyObject() => null;

    public void Run()
    {
        Action<String> process = ProcessAnyObject;
        process("myString"); // легальное действие

        Func<Object> getter = GetString;
        Object obj = getter(); // легальное действие

        // Action<Object> process2 = ProcessString; //Can Not
    }
}