namespace ExamplesForInterview;

// Experiment Throw:
// System.Exception: Exception of type 'System.Exception' was thrown.
//    at ExamplesForInterview.ExceptionTests.<MethodWithException>g__D|3_3() in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 53     
//    at ExamplesForInterview.ExceptionTests.<MethodWithException>g__C|3_2() in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 52     
//    at ExamplesForInterview.ExceptionTests.<MethodWithException>g__B|3_1() in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 51     
//    at ExamplesForInterview.ExceptionTests.<MethodWithException>g__A|3_0() in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 50     
//    at ExamplesForInterview.ExceptionTests.MethodWithException() in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 48
//    at ExamplesForInterview.ExceptionTests.MethodWithThrow(ThrowType throwType) in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 30
//    at ExamplesForInterview.ExceptionTests.RunExperiment(ThrowType throwType) in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 16  
//
// Experiment ThrowEx:
// System.Exception: Exception of type 'System.Exception' was thrown.
//    at ExamplesForInterview.ExceptionTests.MethodWithThrow(ThrowType throwType) in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 39
//    at ExamplesForInterview.ExceptionTests.RunExperiment(ThrowType throwType) in C:\Users\Vlad\RiderProjects\ExamplesForInterview\ExceptionTests.cs:line 16

public class ExceptionTests
{
    public void Run()
    {
        RunExperiment(ThrowType.Throw);
        RunExperiment(ThrowType.ThrowEx);
    }

    private void RunExperiment(ThrowType throwType)
    {
        Console.WriteLine($"Experiment {throwType}:");
        try
        {
            MethodWithThrow(throwType);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine();
    }

    private static void MethodWithThrow(ThrowType throwType)
    {
        try
        {
            MethodWithException();
        }
        catch (Exception e)
        {
            switch (throwType)
            {
                case ThrowType.Throw:
                    throw;
                case ThrowType.ThrowEx:
                    throw e;
                default:
                    throw new ArgumentOutOfRangeException(nameof(throwType), throwType, null);
            }
        }
    }

    private static void MethodWithException()
    {
        A();
        return;
        void A() => B();
        void B() => C();
        void C() => D();
        void D() => throw new Exception();
    }

    private enum ThrowType
    {
        Throw,
        ThrowEx
    }
}