using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    public const int StartCount = 50;
    public const int CountIncrement = 10000;

    [Benchmark]
    public void ListFor()
    {
        List<int> list = new int[StartCount].ToList();
        for (int k = 0; k < CountIncrement; k++)
        {
            list.Add(k);
            for (var i = 0; i < list.Count; i++) SomeAction(list[i]);
        }
    }

    [Benchmark]
    public void ListFor_WithAction()
    {
        List<int> list = new int[StartCount].ToList();
        for (int k = 0; k < CountIncrement; k++)
        {
            list.Add(k);
            Action<int> action = SomeAction;
            for (var i = 0; i < list.Count; i++) action.Invoke(list[i]);
        }
    }

    [Benchmark(Baseline = true)]
    public void ListForEach_WithAction()
    {
        List<int> list = new int[StartCount].ToList();
        for (int k = 0; k < CountIncrement; k++)
        {
            list.Add(k);
            Action<int> action = SomeAction;
            list.ForEach(action);
        }
    }

    [Benchmark]
    public void ListAct()
    {
        List<int> list = new int[StartCount].ToList();
        for (int k = 0; k < CountIncrement; k++)
        {
            list.Add(k);
            list.Act(SomeAction);
        }
    }

    [Benchmark]
    public void ListActWithSpan()
    {
        List<int> list = new int[StartCount].ToList();
        for (int k = 0; k < CountIncrement; k++)
        {
            list.Add(k);
            list.ActWithSpan(SomeAction);
        }
    }

    private void SomeAction(int num)
    {
    }
}

public static partial class ActExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ActWithSpan<T>(this List<T> list, Action<T> action)
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

        var span = CollectionsMarshal.AsSpan(list);
        var rest = list.Count % 4;
        var listMultipleOf4Length = listLength - rest;
        var spanLength = span.Length;

        unsafe
        {
            ref T reference = ref span.GetPinnableReference();
            fixed (T* p = &reference)
            {
                for (int i = 0; i <= spanLength - 4; i += 4)
                {
                    if (i + 3 >= listMultipleOf4Length) break;
                    action.Invoke(*(p + i));
                    action.Invoke(*(p + i + 1));
                    action.Invoke(*(p + i + 2));
                    action.Invoke(*(p + i + 3));
                }

                for (int i = 0; i < rest; i++)
                {
                    action.Invoke(*(p + listMultipleOf4Length + i));
                }
            }
        }
    }
}