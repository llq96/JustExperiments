using BenchmarkDotNet.Running;

namespace ExamplesForInterview;

internal class Program
{
    private static void Main()
    {
        // new InternedStrings().Run();
        BenchmarkRunner.Run<PseudoAsyncBenchmark>();
    }
}