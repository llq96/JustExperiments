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
        // BenchmarkRunner.Run<ForeachBenchmarks>();

        // new ForeachBenchmarks().Act_Try4();
        // new ForeachBenchmarks().List_32Test_AsSpan();

        // for (int i = 0; i < 10; i++)
        // {
        //     List<int> test = Enumerable.Range(0, i).Select(x => x).ToList();
        //     Console.WriteLine();
        //     test.Act(Console.Write);
        //     Console.WriteLine();
        //     test.SimpleAct(Console.Write);
        // }

        // Series.Run();
        // new ForEachILCodeTests().Run();

        // BenchmarkRunner.Run<SplitBenchmarks>();
        // new SplitBenchmarks().Check();

        // new InterfacesILCodeTests().Run();
        // new AnimalsExample().Example5();
        // new AsyncTests2().Run();
        new ReactivePropertyTests().Run();
    }
}