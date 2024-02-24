namespace ExamplesForInterview;

public class ExceptionTests
{
    public void Run()
    {
        Console.WriteLine($"Test {nameof(ThrowTestType.Throw)}:");
        TestThrow(ThrowTestType.Throw);
        Console.WriteLine();
        Console.WriteLine($"Test {nameof(ThrowTestType.ThrowEx)}:");
        TestThrow(ThrowTestType.ThrowEx);
    }

    private void TestThrow(ThrowTestType throwTestType)
    {
        try
        {
            switch (throwTestType)
            {
                case ThrowTestType.Throw:
                    MethodWithThrowInCatch();
                    break;
                case ThrowTestType.ThrowEx:
                    MethodWithThrowExceptionInCatch();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(throwTestType), throwTestType, null);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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

    private static void MethodWithThrowInCatch()
    {
        try
        {
            MethodWithException();
        }
        catch (Exception e)
        {
            throw;
        }
        finally
        {
            // Console.WriteLine($"{nameof(MethodWithThrowInCatch)} Finally");
        }
    }

    private static void MethodWithThrowExceptionInCatch()
    {
        try
        {
            MethodWithException();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            // Console.WriteLine($"{nameof(MethodWithThrowExceptionInCatch)} Finally");
        }
    }

    private enum ThrowTestType
    {
        Throw,
        ThrowEx
    }
}