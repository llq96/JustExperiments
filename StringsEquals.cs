namespace ExamplesForInterview;

public class StringsEquals
{
    public void Run()
    {
        Example1();
    }

    private static void Example1()
    {
        object a = "text";
        object b = "text";
        object c = new string("text");

        Console.WriteLine(a == b); //true
        Console.WriteLine(a.Equals(b)); // true

        Console.WriteLine(a == c); //false(!)
        Console.WriteLine(a.Equals(c)); // true
    }
}