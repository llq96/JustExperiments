using BenchmarkDotNet.Attributes;

namespace ExamplesForInterview;

// | Method                            | Mean     | Error   | StdDev  | Ratio |
// |---------------------------------- |---------:|--------:|--------:|------:|
// | SomethingWithChars                | 840.9 ns | 9.61 ns | 8.99 ns |  1.00 |
// | SomethingWithCharsBySpan          | 441.0 ns | 2.95 ns | 2.76 ns |  0.52 |
// | SomethingWithCharsBySplitedString | 488.2 ns | 2.63 ns | 2.46 ns |  0.58 |
// Выводы: Без обёрток не очень удобно
// По производительности хорошие результаты
// Сам AsSpan дорогой, больше 1-го раза по идее не нужно вызывать

public class SplitBenchmarks
{
    private static readonly string TestString = string.Join("\n", Enumerable.Repeat("Line", 100).ToArray());

    public void Check()
    {
        Console.WriteLine(string.Join(' ', SomethingWithChars()));
        Console.WriteLine(string.Join(' ', SomethingWithCharsBySpan()));
        Console.WriteLine(string.Join(' ', SomethingWithCharsBySplitedString()));
    }

    [Benchmark(Baseline = true)]
    public char[] SomethingWithChars()
    {
        string[] lines = TestString.Split('\n');

        char[] chars = new char[100];
        for (int i = 0; i < lines.Length; i++)
        {
            chars[i] = lines[i][0];
        }

        return chars;
    }

    [Benchmark]
    public char[] SomethingWithCharsBySpan()
    {
        Span<Range> segments = stackalloc Range[100];
        ReadOnlySpan<char> source = TestString.AsSpan();
        int segmentsCount = source.Split(segments, '\n');

        char[] chars = new char[100];
        for (int i = 0; i < segments.Length; i++)
        {
            chars[i] = source[segments[i]][0];
        }

        return chars;
    }

    [Benchmark]
    public char[] SomethingWithCharsBySplitedString()
    {
        var splitedString = new SplitedString(TestString);
        char[] chars = new char[100];
        for (int i = 0; i < 100; i++)
        {
            chars[i] = splitedString.GetSegment(i)[0];
        }

        return chars;
    }


    private readonly ref struct SplitedString
    {
        private readonly ReadOnlySpan<char> Source;
        private readonly Span<Range> Segments;

        public SplitedString(string str)
        {
            Segments = new Span<Range>(new Range[100]);
            Source = str.AsSpan();
            int segmentsCount = Source.Split(Segments, '\n');
        }


        public ReadOnlySpan<char> GetSegment(int index)
        {
            return Source[Segments[index]];
        }
    }
}