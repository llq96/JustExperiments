using System.Collections.Specialized;
using BenchmarkDotNet.Attributes;

namespace ExamplesForInterview;

public class SplitBenchmarks
{
    private static readonly string TestString = string.Join("\n", Enumerable.Repeat("Line", 100).ToArray());

    [Benchmark(Baseline = true)]
    public void Test1()
    {
        string[] lines = TestString.Split(Environment.NewLine);
    }

    [Benchmark]
    public void Test2()
    {
        // Span<Range> spanRange = new Span<Range>(new Range[100 + 1]);
        // var span = TestString.AsSpan();
        for (int i = 0; i < 100; i++)
        {
            var span = TestString.AsSpan(i * 4, 4);
        }
        // var split = span.Split(spanRange, "\n");
    }
}