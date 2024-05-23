using BenchmarkDotNet.Attributes;

namespace ExamplesForInterview;

// Ключевое слово async делает метод асинхронным, это видно и по IL коду и по производительности

// | Method | Mean      | Error    | StdDev   | Ratio | RatioSD |
// |------- |----------:|---------:|---------:|------:|--------:|
// | Test1  |  24.91 ns | 0.152 ns | 0.134 ns |  1.00 |    0.00 |
// | Test2  | 438.62 ns | 1.135 ns | 1.006 ns | 17.61 |    0.11 |

[MemoryDiagnoser]
public class PseudoAsyncBenchmark
{
    [Benchmark(Baseline = true)]
    public void Test1()
    {
        for (int i = 0; i < 100; i++)
        {
            NotAsync();
        }
    }

    private void NotAsync()
    {
    }

    [Benchmark]
    public void Test2()
    {
        for (int i = 0; i < 100; i++)
        {
            PseudoAsync();
        }
    }

    private async void PseudoAsync()
    {
    }
}