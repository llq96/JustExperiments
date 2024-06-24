using System.Collections;

namespace ExamplesForInterview;

public class ForEachILCodeTests
{
    public void Run()
    {
        var classWithGetIEnumerable = new ClassWithGetIEnumerable();
        foreach (int item in classWithGetIEnumerable)
        {
            Console.WriteLine(item);
            Console.WriteLine(item);
        }

        var classWithGetMyIEnumerator = new ClassWithGetMyIEnumerator();
        foreach (float item in classWithGetMyIEnumerator)
        {
            Console.WriteLine(item);
            Console.WriteLine(item);
        }

        int[] array = { 1, 2, 3 };
        foreach (var item in array)
        {
            Console.WriteLine(item);
            Console.WriteLine(item);
        }

        List<int> list = new[] { 1, 2, 3 }.ToList();
        foreach (int item in list)
        {
            Console.WriteLine(item);
            Console.WriteLine(item);
        }
    }

    public class ClassWithGetIEnumerable : IEnumerable<int>
    {
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class ClassWithGetMyIEnumerator
    {
        public MyIEnumerator GetEnumerator()
        {
            return new MyIEnumerator();
        }
    }

    public class MyIEnumerator
    {
        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public int Current { get; }
    }
}