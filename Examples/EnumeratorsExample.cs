namespace ExamplesForInterview;

public class EnumeratorsExample
{
    public void Run()
    {
        var w = new Wrap();
        var wraps = new Wrap[3];
        for (int i = 0; i < wraps.Length; i++)
        {
            wraps[i] = w;
        }

        IEnumerable<int> values = wraps.Select(x => x.Value);
        IEnumerable<int> results = Square(values);
        int sum = 0;
        int count = 0;
        foreach (var r in results)
        {
            count++;
            sum += r;
        }

        Console.WriteLine("Count {0}", count);
        Console.WriteLine("Sum {0}", sum);

        Console.WriteLine("Count {0}", results.Count());
        Console.WriteLine("Sum {0}", results.Sum());

        // Logs:
        // 1
        // 4
        // 9
        // Count 3
        // Sum 14
        // 16
        // 25
        // 36
        // Count 3
        // 49
        // 64
        // 81
        // Sum 194
    }

    private static IEnumerable<int> Square(IEnumerable<int> a)
    {
        foreach (var r in a)
        {
            Console.WriteLine(r * r);
            yield return r * r;
        }
    }

    private class Wrap
    {
        private static int _init;

        public int Value => ++_init;
    }
}