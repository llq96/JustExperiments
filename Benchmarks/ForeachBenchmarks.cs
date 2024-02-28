using BenchmarkDotNet.Attributes;
using ExamplesForInterview.Extensions;

namespace ExamplesForInterview;

//Выводы:
// Для статического метода генерируется отдельный статический класс с экшеном и каждый раз выполняется проверка на null
// (Видимо)Из-за этого методы становятся в 3 раза медленнее
// Кэшировать размер коллекции выгодно, но в самом базовом for с массивом - нет
// ArrayForeach чуть быстрее, видимо потому что обращение к стандартному массиву медленнее
// ListFor медленнее из-за индексаторов
// ListForEach медленнее из-за try блока и постоянных MoveNext() и Current
// Варианты с передачей экшена медленнее видимо из-за самого экшена
// Простой вариант через рефлексию в 2.5 раза медленнее, и даже с разбиением на 4 всё равно в 1.5 раза медленнее
// А вот продвинутый вариант с взятием поля подходит, без фишек всего в 1.2 раза медленнее

// Последние результаты (CountElements = 100;):
// | Method                              | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
// |------------------------------------ |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
// | ArrayFor                            | 27.00 ns | 0.523 ns | 0.489 ns |  1.00 |    0.00 |      - |         - |          NA |
// | ArrayFor_Try4_2                     | 15.37 ns | 0.319 ns | 0.327 ns |  0.57 |    0.02 |      - |         - |          NA |
// | Act                                 | 46.67 ns | 0.694 ns | 0.649 ns |  1.73 |    0.04 | 0.0076 |      64 B |          NA |
// | Act_Try8                            | 38.97 ns | 0.304 ns | 0.285 ns |  1.44 |    0.03 | 0.0076 |      64 B |          NA |
// | ActWithReflection_Try4              | 11.48 ns | 0.248 ns | 0.244 ns |  0.43 |    0.01 | 0.0076 |      64 B |          NA |
// | ActWithReflection_MyReflection      | 71.67 ns | 1.309 ns | 1.225 ns |  2.66 |    0.07 | 0.0076 |      64 B |          NA |
// | ActWithReflection_Try4_MyReflection | 41.23 ns | 0.845 ns | 1.434 ns |  1.51 |    0.04 | 0.0076 |      64 B |          NA |

[MemoryDiagnoser]
public class ForeachBenchmarks
{
    private const int CountElements = 128;
    private static readonly int[] TestArray = Enumerable.Range(0, CountElements).Select(i => i).ToArray();
    private static readonly List<int> TestList = Enumerable.Range(0, CountElements).Select(i => i).ToList();

    [Benchmark(Baseline = true)]
    public void ArrayFor()
    {
        for (var i = 0; i < TestArray.Length; i++)
        {
            SomeAction(TestArray[i]);
        }
    }

    [Benchmark]
    public void ArrayForImproved()
    {
        var array = TestArray;
        for (var i = 0; i < array.Length; i++)
        {
            SomeAction(array[i]);
        }
    }

    [Benchmark]
    public void ArrayForeach()
    {
        foreach (var number in TestArray)
        {
            SomeAction(number);
        }
    }

    // [Benchmark]
    // public void Act()
    // {
    //     TestList.Act(SomeAction);
    // }

    [Benchmark]
    public void Act_Try4()
    {
        TestList.Act_Try4(SomeAction);
    }

    [Benchmark]
    public void Act_Try8()
    {
        TestList.Act_Try8(SomeAction);
    }

    [Benchmark]
    public void Act_Try8_2()
    {
        TestList.Act_Try8_2(SomeAction);
    }

    private void SomeAction(int num)
    {
    }
}

public static class TestExtensions
{
    public static void Act<T>(this List<T> list, Action<T> action)
    {
        var array = list.GetInternalArray();
        var count = array.Length;
        for (int i = 0; i < count - 1; i++)
        {
            action.Invoke(array[i]);
        }
    }

    public static void Act_Try4<T>(this List<T> list, Action<T> action)
    {
        var array = list.GetInternalArray();
        var count = array.Length;
        for (int i = 0; i <= count - 4; i += 4)
        {
            action.Invoke(array[i]);
            action.Invoke(array[i + 1]);
            action.Invoke(array[i + 2]);
            action.Invoke(array[i + 3]);
        }
    }

    public static void Act_Try8<T>(this List<T> list, Action<T> action)
    {
        var array = list.GetInternalArray();
        var count = array.Length;
        for (int i = 0; i <= count - 8; i += 8)
        {
            action.Invoke(array[i]);
            action.Invoke(array[i + 1]);
            action.Invoke(array[i + 2]);
            action.Invoke(array[i + 3]);
            action.Invoke(array[i + 4]);
            action.Invoke(array[i + 5]);
            action.Invoke(array[i + 6]);
            action.Invoke(array[i + 7]);
        }
    }

    public static void Act_Try8_2<T>(this List<T> list, Action<T> action)
    {
        var array = list.GetInternalArray();
        var count = array.Length;
        var N = 8;
        for (int i = 0; i <= count - N; i += N)
        {
            for (int j = 0; j < N; j++)
            {
                action.Invoke(array[i + j]);
            }
        }
    }
}