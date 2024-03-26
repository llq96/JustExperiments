namespace ExamplesForInterview;

public class ThreadILCodeTests
{
    public void Run()
    {
        lock (this)
        {
            Console.WriteLine("In Lock");
        }
    }
}