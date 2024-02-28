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

        // new ForeachBenchmarks().Act_Try4();
        // new ForeachBenchmarks().Act_Try4_Correct();

        // List<int> test = Enumerable.Range(0, 5).Select(i => i).ToList();
        //
        //
        // Console.WriteLine();
        // test.Act_Try4_Correct(Console.Write);
        // Console.WriteLine();
        // test.Act_Try4_Safe(Console.Write);
    }
}