using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace ExamplesForInterview;

// [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
// [SimpleJob(RuntimeMoniker.Net80)]
// [SimpleJob(RuntimeMoniker.NativeAot70)]
// [SimpleJob(RuntimeMoniker.Mono)]
// [RPlotExporter]
[MemoryDiagnoser]
public class MyBenchmarks
{
    [Benchmark]
    public void Test()
    {
        StringBuilder sb = new StringBuilder("qwe");
        sb.Append("asd");
        // Console.WriteLine(sb.ToString());
        // Console.WriteLine(sb.ToString());
        // Console.WriteLine(sb.ToString());
    }

    [Benchmark]
    public void Test2()
    {
        StringBuilder sb = new StringBuilder("qwe");
        sb.Append("asd");
        string a = sb.ToString();
        a = sb.ToString();
        a = sb.ToString();
        a = sb.ToString();
        a = sb.ToString();
    }
}