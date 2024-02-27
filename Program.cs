using BenchmarkDotNet.Running;

namespace ExamplesForInterview;

internal class Program
{
    private static void Main()
    {
        // Console.WriteLine("Hello World!");

        // new BoxingAndCasting().Run();
        // new ExceptionTests().Run();
        // new FinallyReturnTests().Run();
        // new EnumFlagTests().Run();
        // new InterfaceEnumerableTests().Run();
        // new AsyncTests().Run();
        // new GoodNumberSwitchTests().Run();
        // new MyBenchmarks().Run();
        // new ClosureILCodeTests().Run();

        // BenchmarkRunner.Run<MyBenchmarks>();
        BenchmarkRunner.Run<ForeachBenchmarks>();

        // new ForeachBenchmarks().ArrayUnsafeFor();
    }
}