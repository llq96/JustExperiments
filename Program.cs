using BenchmarkDotNet.Running;

namespace ExamplesForInterview;

internal class Program
{
    private static void Main()
    {
        // BenchmarkRunner.Run<InParameterBenchmark>();
        // new StringsEquals().Run();
        new OutIn_InInterfaces().Run();
    }
}