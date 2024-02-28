using BenchmarkDotNet.Attributes;
using ExamplesForInterview.Extensions;

namespace ExamplesForInterview;

//Выводы:
// Для статического метода генерируется отдельный статический класс с экшеном и каждый раз выполняется проверка на null
// (Видимо)Из-за этого методы становятся в 3 раза медленнее
// Кэшировать размер коллекции выгодно
// ArrayForeach чуть быстрее, видимо потому что обращение к стандартному массиву медленнее
// ListFor медленнее из-за индексаторов
// ListForEach медленнее из-за try блока и постоянных MoveNext() и Current
// Варианты с передачей экшена медленнее видимо из-за самого экшена
// Простой вариант через рефлексию в 2.5 раза медленнее, и даже с разбиением на 4 всё равно в 1.5 раза медленнее
// А вот продвинутый вариант с взятием поля подходит

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

// Последние результаты (CountElements = 100000;):
// | Method                   | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0     | Gen1     | Gen2     | Allocated | Alloc Ratio |
// |------------------------- |----------:|---------:|---------:|------:|--------:|---------:|---------:|---------:|----------:|------------:|
// | ArrayFor                 |  19.10 us | 0.018 us | 0.020 us |  1.00 |    0.00 |        - |        - |        - |         - |          NA |
// | ArrayForeach             |  19.08 us | 0.017 us | 0.014 us |  1.00 |    0.00 |        - |        - |        - |         - |          NA |
// | ListFor                  |  28.88 us | 0.163 us | 0.153 us |  1.51 |    0.01 |        - |        - |        - |         - |          NA |
// | ListForeach              |  29.24 us | 0.099 us | 0.092 us |  1.53 |    0.00 |        - |        - |        - |         - |          NA |
// | ListForeachMethod        | 175.96 us | 0.412 us | 0.385 us |  9.21 |    0.02 |        - |        - |        - |         - |          NA |
// | ArrayToListForeachMethod | 278.70 us | 1.259 us | 1.116 us | 14.59 |    0.06 | 124.5117 | 124.5117 | 124.5117 |  400098 B |          NA |
// | ListMyForeachMethod      | 175.85 us | 0.511 us | 0.427 us |  9.21 |    0.03 |        - |        - |        - |         - |          NA |
// | ListMyForeach2Method     | 176.04 us | 0.471 us | 0.441 us |  9.22 |    0.02 |        - |        - |        - |         - |          NA |

[MemoryDiagnoser]
public class ForeachBenchmarks
{
    private const int CountElements = 100;
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

    // [Benchmark]
    // public void ArrayForeach()
    // {
    //     foreach (var number in TestArray)
    //     {
    //         SomeAction(number);
    //     }
    // }

    [Benchmark]
    public void ArrayFor_Try4_2()
    {
        var count = TestArray.Length;
        for (var i = 0; i <= count - 4; i += 4)
        {
            SomeAction(TestArray[i]);
            SomeAction(TestArray[i + 1]);
            SomeAction(TestArray[i + 2]);
            SomeAction(TestArray[i + 3]);
        }
    }

    [Benchmark]
    public void Act()
    {
        TestList.Act(SomeAction);
    }

    [Benchmark]
    public void Act_Try8()
    {
        TestList.Act_Try8(SomeAction);
    }

    [Benchmark]
    public void ActWithReflection_Try4()
    {
        TestList.ActWithReflection_Try4(SomeAction);
    }

    [Benchmark]
    public void ActWithReflection_MyReflection()
    {
        TestList.ActWithReflection_MyReflection(SomeAction);
    }

    [Benchmark]
    public void ActWithReflection_Try4_MyReflection()
    {
        TestList.ActWithReflection_Try4_MyReflection(SomeAction);
    }

    private void SomeAction(int num)
    {
    }
}

public static class TestExtensions
{
    public static void Act<T>(this List<T> list, Action<T> action)
    {
        var count = list.Count;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(list[i]);
        }
    }

    public static void Act_Try8<T>(this List<T> list, Action<T> action)
    {
        var count = list.Count;
        for (int i = 0; i <= count - 8; i += 8)
        {
            action.Invoke(list[i]);
            action.Invoke(list[i + 1]);
            action.Invoke(list[i + 2]);
            action.Invoke(list[i + 3]);
            action.Invoke(list[i + 4]);
            action.Invoke(list[i + 5]);
            action.Invoke(list[i + 6]);
            action.Invoke(list[i + 7]);
        }
    }

    public static void ActWithReflection_Try4<T>(this List<T> list, Action<T> action)
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

    public static void ActWithReflection_MyReflection<T>(this List<T> list, Action<T> action)
    {
        var array = list.GetInternalArray2();
        var count = array.Length;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(array[i]);
        }
    }

    public static void ActWithReflection_Try4_MyReflection<T>(this List<T> list, Action<T> action)
    {
        var array = list.GetInternalArray2();
        var count = array.Length;
        for (int i = 0; i <= count - 4; i += 4)
        {
            action.Invoke(array[i]);
            action.Invoke(array[i + 1]);
            action.Invoke(array[i + 2]);
            action.Invoke(array[i + 3]);
        }
    }
}