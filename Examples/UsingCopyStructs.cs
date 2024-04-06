namespace ExamplesForInterview.Examples;

public class UsingCopyStructs
{
    public void Run()
    {
        var testStruct = new TestStruct();
        using (testStruct)
        {
            Console.WriteLine(testStruct.GetDispose()); //false
        }
        //In finally block used copy of struct

        Console.WriteLine(testStruct.GetDispose()); // (!) false
    }

    private struct TestStruct : IDisposable
    {
        private bool _dispose;

        public void Dispose() => _dispose = true;

        public bool GetDispose() => _dispose;
    }
}