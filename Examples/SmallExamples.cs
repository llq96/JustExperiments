namespace ExamplesForInterview.Examples;

public class SmallExamples
{
    public void CastObjectToShort()
    {
        int i = 1;
        object obj = i;
        ++i;
        Console.WriteLine(i);
        Console.WriteLine(obj);
        Console.WriteLine((short)i);
        Console.WriteLine((short)obj); //System.InvalidCastException
    }
}