namespace ExamplesForInterview;

/// <summary>
/// Result:
/// "Finally in Test1"
/// "Error in Run" 
/// "Finally in Run"
/// </summary>
public class AfterTryDontInvoked
{
    public void Run()
    {
        try
        {
            Test1();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in Run");
        }
        finally
        {
            Console.WriteLine($"Finally in {nameof(Run)}");
        }
    }

    private void Test1()
    {
        try
        {
            while (true)
            {
                throw new Exception();
            }
        }
        // catch (Exception e)
        // {
        //     Console.WriteLine("Error");
        // }
        finally
        {
            Console.WriteLine($"Finally in {nameof(Test1)}");
        }

        //Never Invoked
        Console.WriteLine("After");
    }
}