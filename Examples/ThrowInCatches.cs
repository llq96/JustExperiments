namespace ExamplesForInterview.Examples;

/// <summary>
/// throw пробрасывает исключение дальше по стеку, не пытаясь вызывать другой блок catch
/// </summary>
public class ThrowInCatches
{
    public void Run()
    {
        try
        {
            Catches();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // 3 System.Exception: Exception in finally
        }
    }

    public void Catches()
    {
        try
        {
            MethodWithException();
        }
        catch (MyException e)
        {
            Console.WriteLine(e); // 1
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e); //Do not invoked
        }
        finally
        {
            Console.WriteLine("Finally"); // 2
            throw new Exception("Exception in finally");
        }
    }

    public void MethodWithException()
    {
        throw new MyException();
    }

    public class MyException : Exception
    {
    }
}