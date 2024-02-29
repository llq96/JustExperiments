using BenchmarkDotNet.Attributes;
using ExamplesForInterview.Extensions;
using Microsoft.Diagnostics.Tracing.Stacks;

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
// В два раза быстрее цикла for (!)
// Создание и использование экшена сравнимо с 16-18 обработанными числами, то есть при 16 числах for с отдельным экшеном
// в два раза медленнее

// Последние результаты:
// | Method                 | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
// |----------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
// | ArrayFor_8             |  1.983 ns | 0.0214 ns | 0.0167 ns |  1.00 |    0.00 |      - |         - |          NA |
// | ArrayFor_16            |  3.623 ns | 0.0568 ns | 0.0504 ns |  1.82 |    0.02 |      - |         - |          NA |
// | ArrayFor_32            |  7.462 ns | 0.1703 ns | 0.1893 ns |  3.78 |    0.10 |      - |         - |          NA |
// | ArrayFor_64            | 19.377 ns | 0.1384 ns | 0.1156 ns |  9.78 |    0.12 |      - |         - |          NA |
// | ArrayFor_WithAction_8  |  6.012 ns | 0.1373 ns | 0.1284 ns |  3.04 |    0.07 | 0.0077 |      64 B |          NA |
// | ArrayFor_WithAction_16 |  7.354 ns | 0.1558 ns | 0.1301 ns |  3.71 |    0.08 | 0.0076 |      64 B |          NA |
// | ArrayFor_WithAction_32 | 10.925 ns | 0.2042 ns | 0.1594 ns |  5.51 |    0.10 | 0.0076 |      64 B |          NA |
// | ArrayFor_WithAction_64 | 23.490 ns | 0.4737 ns | 0.4431 ns | 11.82 |    0.26 | 0.0076 |      64 B |          NA |
// | ListAct_8              |  7.174 ns | 0.1242 ns | 0.0970 ns |  3.62 |    0.06 | 0.0076 |      64 B |          NA |
// | ListAct_16             |  8.353 ns | 0.1324 ns | 0.1106 ns |  4.22 |    0.05 | 0.0076 |      64 B |          NA |
// | ListAct_32             | 10.913 ns | 0.2126 ns | 0.1885 ns |  5.51 |    0.13 | 0.0076 |      64 B |          NA |
// | ListAct_64             | 14.370 ns | 0.2740 ns | 0.2563 ns |  7.25 |    0.14 | 0.0076 |      64 B |          NA |

[MemoryDiagnoser]
public class ForeachBenchmarks
{
    // private const int CountElements = 8;
    private static readonly int[] TestArray_8 = Enumerable.Range(0, 8).Select(i => i).ToArray();
    private static readonly List<int> TestList_8 = Enumerable.Range(0, 8).Select(i => i).ToList();
    private static readonly int[] TestArray_16 = Enumerable.Range(0, 16).Select(i => i).ToArray();
    private static readonly List<int> TestList_16 = Enumerable.Range(0, 16).Select(i => i).ToList();
    private static readonly int[] TestArray_32 = Enumerable.Range(0, 32).Select(i => i).ToArray();
    private static readonly List<int> TestList_32 = Enumerable.Range(0, 32).Select(i => i).ToList();
    private static readonly int[] TestArray_64 = Enumerable.Range(0, 64).Select(i => i).ToArray();
    private static readonly List<int> TestList_64 = Enumerable.Range(0, 64).Select(i => i).ToList();

    #region ArrayFor

    [Benchmark(Baseline = true)]
    public void ArrayFor_8()
    {
        for (var i = 0; i < TestArray_8.Length; i++) SomeAction(TestArray_8[i]);
    }

    [Benchmark]
    public void ArrayFor_16()
    {
        for (var i = 0; i < TestArray_16.Length; i++) SomeAction(TestArray_16[i]);
    }

    [Benchmark]
    public void ArrayFor_32()
    {
        for (var i = 0; i < TestArray_32.Length; i++) SomeAction(TestArray_32[i]);
    }

    [Benchmark]
    public void ArrayFor_64()
    {
        for (var i = 0; i < TestArray_64.Length; i++) SomeAction(TestArray_64[i]);
    }

    #endregion

    #region ArrayFor_WithAction

    [Benchmark]
    public void ArrayFor_WithAction_8()
    {
        Action<int> action = SomeAction;
        for (var i = 0; i < TestArray_8.Length; i++) action.Invoke(TestArray_8[i]);
    }

    [Benchmark]
    public void ArrayFor_WithAction_16()
    {
        Action<int> action = SomeAction;
        for (var i = 0; i < TestArray_16.Length; i++) action.Invoke(TestArray_16[i]);
    }

    [Benchmark]
    public void ArrayFor_WithAction_32()
    {
        Action<int> action = SomeAction;
        for (var i = 0; i < TestArray_32.Length; i++) action.Invoke(TestArray_32[i]);
    }

    [Benchmark]
    public void ArrayFor_WithAction_64()
    {
        Action<int> action = SomeAction;
        for (var i = 0; i < TestArray_64.Length; i++) action.Invoke(TestArray_64[i]);
    }

    #endregion

    [Benchmark] public void ListAct_8() => TestList_8.Act(SomeAction);
    [Benchmark] public void ListAct_16() => TestList_16.Act(SomeAction);
    [Benchmark] public void ListAct_32() => TestList_32.Act(SomeAction);
    [Benchmark] public void ListAct_64() => TestList_64.Act(SomeAction);

    private void SomeAction(int num)
    {
    }
}

public static partial class ActExtensions
{
    public static void Act<T>(this List<T> list, Action<T> action)
    {
        var listLength = list.Count;
        if (listLength <= 32)
        {
            if (listLength == 32)
            {
                list.List_32Test_AsSpan(action);
                return;
            }

            if (listLength == 16)
            {
                list.List_16Test_AsSpan(action);
                return;
            }

            if (listLength == 8)
            {
                list.List_8Test_AsSpan(action);
                return;
            }
        }

        var array = list.GetInternalArray();
        var rest = list.Count % 4;
        var listMultipleOf4Length = listLength - rest;
        var arrayLength = array.Length;
        for (int i = 0; i <= arrayLength - 4; i += 4)
        {
            if (i + 3 >= listMultipleOf4Length) break;
            action.Invoke(array[i]);
            action.Invoke(array[i + 1]);
            action.Invoke(array[i + 2]);
            action.Invoke(array[i + 3]);
        }

        for (int i = 0; i < rest; i++)
        {
            action.Invoke(array[listMultipleOf4Length + i]);
        }
    }

    public static void SimpleAct<T>(this List<T> list, Action<T> action)
    {
        List<T>.Enumerator enumerator = list.GetEnumerator();
        while (enumerator.MoveNext())
        {
            action(enumerator.Current);
        }
    }
}