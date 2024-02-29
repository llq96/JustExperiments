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

// Последние результаты (CountElements = 103;):
// | Method   | Mean     | Error    | StdDev   | Ratio | Gen0   | Allocated | Alloc Ratio |
// |--------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
// | ArrayFor | 27.15 ns | 0.340 ns | 0.266 ns |  1.00 |      - |         - |          NA |
// | Act      | 13.55 ns | 0.292 ns | 0.259 ns |  0.50 | 0.0076 |      64 B |          NA |

// Последние результаты (CountElements = 32;):
// | Method             | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
// |------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
// | ArrayFor           |  8.083 ns | 0.1817 ns | 0.4526 ns |  1.00 |    0.00 |      - |         - |          NA |
// | ListAsSpan         |  7.688 ns | 0.1717 ns | 0.2350 ns |  0.95 |    0.05 |      - |         - |          NA |
// | ListAsSpan_2       | 10.836 ns | 0.2253 ns | 0.1998 ns |  1.35 |    0.04 | 0.0076 |      64 B |          NA |
// | Act                |  8.455 ns | 0.1880 ns | 0.3577 ns |  1.05 |    0.08 | 0.0076 |      64 B |          NA |
// | SimpleAct          | 25.795 ns | 0.5319 ns | 1.3635 ns |  3.19 |    0.23 | 0.0076 |      64 B |          NA |
// | Array_32Test       |  3.325 ns | 0.0357 ns | 0.0298 ns |  0.41 |    0.02 | 0.0077 |      64 B |          NA |
// | List_32Test        | 16.923 ns | 0.3586 ns | 0.7325 ns |  2.10 |    0.18 | 0.0076 |      64 B |          NA |
// | List_32Test_AsSpan |  8.207 ns | 0.1556 ns | 0.1299 ns |  1.02 |    0.04 | 0.0076 |      64 B |          NA |

[MemoryDiagnoser]
public class ForeachBenchmarks
{
    private const int CountElements = 8;
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
    public void ArrayForWithAction()
    {
        Action<int> action = SomeAction;
        for (var i = 0; i < TestArray.Length; i++)
        {
            action.Invoke(TestArray[i]);
        }
    }
    // [Benchmark]
    // public void ListForeachMethod()
    // {
    //     TestList.ForEach(SomeAction);
    // }

    // [Benchmark]
    // public void ListAsSpan()
    // {
    //     foreach (var item in CollectionsMarshal.AsSpan(TestList))
    //     {
    //         SomeAction(item);
    //     }
    // }
    //
    // [Benchmark]
    // public void ListAsSpan_2()
    // {
    //     Action<int> action = SomeAction;
    //     foreach (var item in CollectionsMarshal.AsSpan(TestList))
    //     {
    //         action.Invoke(item);
    //     }
    // }

    // [Benchmark]
    // public void Act()
    // {
    //     TestList.Act(SomeAction);
    // }
    //
    // [Benchmark]
    // public void SimpleAct()
    // {
    //     TestList.SimpleAct(SomeAction);
    // }

    private void SomeAction(int num)
    {
    }

    // [Benchmark]
    // public void Array_32Test()
    // {
    //     Action<int> action = SomeAction;
    //     action.Invoke(TestArray[0]);
    //     action.Invoke(TestArray[1]);
    //     action.Invoke(TestArray[2]);
    //     action.Invoke(TestArray[3]);
    //     action.Invoke(TestArray[4]);
    //     action.Invoke(TestArray[5]);
    //     action.Invoke(TestArray[6]);
    //     action.Invoke(TestArray[7]);
    //     action.Invoke(TestArray[8]);
    //     action.Invoke(TestArray[9]);
    //     action.Invoke(TestArray[10]);
    //     action.Invoke(TestArray[11]);
    //     action.Invoke(TestArray[12]);
    //     action.Invoke(TestArray[13]);
    //     action.Invoke(TestArray[14]);
    //     action.Invoke(TestArray[15]);
    //     action.Invoke(TestArray[16]);
    //     action.Invoke(TestArray[17]);
    //     action.Invoke(TestArray[18]);
    //     action.Invoke(TestArray[19]);
    //     action.Invoke(TestArray[20]);
    //     action.Invoke(TestArray[21]);
    //     action.Invoke(TestArray[22]);
    //     action.Invoke(TestArray[23]);
    //     action.Invoke(TestArray[24]);
    //     action.Invoke(TestArray[25]);
    //     action.Invoke(TestArray[26]);
    //     action.Invoke(TestArray[27]);
    //     action.Invoke(TestArray[28]);
    //     action.Invoke(TestArray[29]);
    //     action.Invoke(TestArray[30]);
    //     action.Invoke(TestArray[31]);
    // }

    [Benchmark]
    public void List_8Test_AsSpan()
    {
        TestList.List_8Test_AsSpan(SomeAction);
    }

    // [Benchmark]
    // public void List_16Test_AsSpan()
    // {
    //     TestList.List_16Test_AsSpan(SomeAction);
    // }

    // [Benchmark]
    // public void List_32Test_AsSpan()
    // {
    //     TestList.List_32Test_AsSpan(SomeAction);
    // }
}

public static class TestExtensions
{
    public static void Act<T>(this List<T> list, Action<T> action)
    {
        var listLength = list.Count;
        // if (listLength == 32)
        // {
        //     Act_Length_32(list, action);
        //     return;
        // }
        // if (listLength < 132)
        // {
        //     SimpleAct(list, action);
        //     return;
        // }

        // var a = action.Method;
        // CollectionsMarshal.AsSpan(list);


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

    public static void List_8Test_AsSpan<T>(this List<T> list, Action<T> action)
    {
        var span = CollectionsMarshal.AsSpan(list);
        unsafe
        {
            ref T reference = ref span.GetPinnableReference();
            fixed (T* p = &reference)
            {
                action.Invoke(*p);
                action.Invoke(*(p + 1));
                action.Invoke(*(p + 2));
                action.Invoke(*(p + 3));
                action.Invoke(*(p + 4));
                action.Invoke(*(p + 5));
                action.Invoke(*(p + 6));
                action.Invoke(*(p + 7));
            }
        }
    }

    public static void List_16Test_AsSpan<T>(this List<T> list, Action<T> action) where T : unmanaged
    {
        var span = CollectionsMarshal.AsSpan(list);
        unsafe
        {
            ref T reference = ref span.GetPinnableReference();
            fixed (T* p = &reference)
            {
                action.Invoke(*p);
                action.Invoke(*(p + 1));
                action.Invoke(*(p + 2));
                action.Invoke(*(p + 3));
                action.Invoke(*(p + 4));
                action.Invoke(*(p + 5));
                action.Invoke(*(p + 6));
                action.Invoke(*(p + 7));
                action.Invoke(*(p + 8));
                action.Invoke(*(p + 9));
                action.Invoke(*(p + 10));
                action.Invoke(*(p + 11));
                action.Invoke(*(p + 12));
                action.Invoke(*(p + 13));
                action.Invoke(*(p + 14));
                action.Invoke(*(p + 15));
            }
        }
    }

    public static void List_32Test_AsSpan<T>(this List<T> list, Action<T> action) where T : unmanaged
    {
        var span = CollectionsMarshal.AsSpan(list);
        unsafe
        {
            ref T reference = ref span.GetPinnableReference();
            fixed (T* p = &reference)
            {
                action.Invoke(*p);
                action.Invoke(*(p + 1));
                action.Invoke(*(p + 2));
                action.Invoke(*(p + 3));
                action.Invoke(*(p + 4));
                action.Invoke(*(p + 5));
                action.Invoke(*(p + 6));
                action.Invoke(*(p + 7));
                action.Invoke(*(p + 8));
                action.Invoke(*(p + 9));
                action.Invoke(*(p + 10));
                action.Invoke(*(p + 11));
                action.Invoke(*(p + 12));
                action.Invoke(*(p + 13));
                action.Invoke(*(p + 14));
                action.Invoke(*(p + 15));
                action.Invoke(*(p + 16));
                action.Invoke(*(p + 17));
                action.Invoke(*(p + 18));
                action.Invoke(*(p + 19));
                action.Invoke(*(p + 20));
                action.Invoke(*(p + 21));
                action.Invoke(*(p + 22));
                action.Invoke(*(p + 23));
                action.Invoke(*(p + 24));
                action.Invoke(*(p + 25));
                action.Invoke(*(p + 26));
                action.Invoke(*(p + 27));
                action.Invoke(*(p + 28));
                action.Invoke(*(p + 29));
                action.Invoke(*(p + 30));
                action.Invoke(*(p + 31));
            }
        }
    }
}