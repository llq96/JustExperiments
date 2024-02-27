using BenchmarkDotNet.Attributes;

namespace ExamplesForInterview;

//Выводы:
//Для статического метода генерируется отдельный статический класс с экшеном и каждый раз выполняется проверка на null
//(Видимо)Из-за этого методы становятся в 3 раза медленнее

// Последние результаты (CountElements = 100;):
// | Method                   | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
// |------------------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
// | ArrayFor                 |  27.02 ns | 0.340 ns | 0.318 ns |  1.00 |    0.00 |      - |         - |          NA |
// | ArrayForeach             |  25.54 ns | 0.351 ns | 0.311 ns |  0.94 |    0.02 |      - |         - |          NA |
// | ListFor                  |  37.79 ns | 0.766 ns | 1.632 ns |  1.41 |    0.04 |      - |         - |          NA |
// | ListForeach              |  36.19 ns | 0.434 ns | 0.406 ns |  1.34 |    0.02 |      - |         - |          NA |
// | ListForeachMethod        | 184.26 ns | 2.264 ns | 2.118 ns |  6.82 |    0.13 |      - |         - |          NA |
// | ArrayToListForeachMethod | 230.56 ns | 3.568 ns | 3.337 ns |  8.53 |    0.19 | 0.0544 |     456 B |          NA |
// | ListMyForeachMethod      | 165.86 ns | 1.468 ns | 1.373 ns |  6.14 |    0.09 |      - |         - |          NA |
// | ListMyForeach2Method     | 185.44 ns | 3.313 ns | 3.099 ns |  6.86 |    0.12 |      - |         - |          NA |
// ArrayForeach чуть быстрее, видимо потому что обращение к стандартному массиву медленнее
// ListFor медленнее из-за индексаторов
// ListForEach медленнее из-за try блока и постоянных MoveNext() и Current
// Варианты с передачей экшена медленнее видимо из-за самого экшена

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
    private const int CountElements = 100000;
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
    public void ArrayForeach()
    {
        foreach (var number in TestArray)
        {
            SomeAction(number);
        }
    }

    [Benchmark]
    public void ListFor()
    {
        for (var i = 0; i < TestList.Count; i++)
        {
            SomeAction(TestList[i]);
        }
    }

    [Benchmark]
    public void ListForeach()
    {
        foreach (var number in TestList)
        {
            SomeAction(number);
        }
    }

    [Benchmark]
    public void ListForeachMethod()
    {
        TestList.ForEach(SomeAction);
    }

    [Benchmark]
    public void ArrayToListForeachMethod()
    {
        TestArray.ToList().ForEach(SomeAction);
    }

    [Benchmark]
    public void ListMyForeachMethod()
    {
        TestList.MyForEach(SomeAction);
    }

    [Benchmark]
    public void ListMyForeach2Method()
    {
        TestList.MyForEach2(SomeAction);
    }

    // [Benchmark]
    // public void ArrayMyForeach3Method()
    // {
    //     TestArray.MyForEach3(SomeAction);
    // }

    // [Benchmark]
    // public unsafe void ArrayMyForeach4Method()
    // {
    //     MyForEach4(TestArray, &SomeAction);
    // }
    //
    // private static unsafe void MyForEach4(int[] array, delegate*<int, void> action)
    // {
    //     foreach (var element in array)
    //     {
    //         action(element);
    //     }
    // }

    // [Benchmark]
    // public unsafe void ArrayUnsafeFor()
    // {
    //     var count = TestArray.Length;
    //     fixed (int* ptr = TestArray)
    //         for (int i = 0; i < count; i++)
    //         {
    //             SomeAction(*(ptr + i));
    //         }
    // }

    private static void SomeAction(int num)
    {
    }
}

public static class TestExtensions
{
    public static void MyForEach<T>(this List<T> list, Action<T> action)
    {
        var count = list.Count;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(list[i]);
        }
    }

    public static void MyForEach2<T>(this List<T> list, Action<T> action)
    {
        using List<T>.Enumerator enumerator = list.GetEnumerator();

        while (enumerator.MoveNext())
        {
            action.Invoke(enumerator.Current);
        }
    }

    // public static unsafe void MyForEach3<T>(this T[] array, Action<T> action)
    // {
    //     var count = array.Length;
    //     fixed (T* ptr = array)
    //         for (int i = 0; i < count; i++)
    //         {
    //             action.Invoke(*(ptr + i));
    //         }
    // }

    // public static unsafe void MyForEach4<T>(this T[] array, delegate*<T, void> action)
    // {
    //     foreach (var element in array)
    //     {
    //         action(element);
    //     }
    //     // var count = array.Length;
    //     // fixed (T* ptr = array)
    //     //     for (int i = 0; i < count; i++)
    //     //     {
    //     //         action(default);
    //     //         // action(*(ptr + i));
    //     //         // action?.Invoke(*(ptr + i));
    //     //     }
    // }

    // public static unsafe void MyForEach4(this object[] array, Action<int>* action)
    // {
    //     var count = array.Length;
    //     fixed (object* ptr = array)
    //         for (int i = 0; i < count; i++)
    //         {
    //             (*action)?.Invoke((int)(*(ptr + i)));
    //         }
    // }
}