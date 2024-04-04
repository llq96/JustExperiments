namespace ExamplesForInterview;

public class OutIn_InInterfaces
{
    public void Run()
    {
        Interface1<object>[] list1 = { new Class1<object>() };
        Interface1<string>[] list2 = { new Class1<string>() };

        list2 = list1; //Can with in in interface
    }
}

public interface Interface1<in T>
{
    void Log();
}

public class Class1<T> : Interface1<T>
{
    public void Log()
    {
        Console.WriteLine(GetType());
    }
}