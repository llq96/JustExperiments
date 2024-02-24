namespace ExamplesForInterview;

public class InterfaceEnumerableTests
{
    public void Run()
    {
        var test = new TestClassWithoutInterfaces();
        foreach (int value in test)
        {
            Console.WriteLine(value);
        }
    }

    public class TestClassWithoutInterfaces /*: IEnumerable<int>*/
    {
        public IEnumerator<int> GetEnumerator()
        {
            yield return 0;
            yield return 1;
            yield return 2;
        }

        // IEnumerator IEnumerable.GetEnumerator()
        // {
        //     return GetEnumerator();
        // }
    }
}