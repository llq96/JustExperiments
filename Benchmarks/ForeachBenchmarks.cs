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

// Последние результаты (CountElements = 103;):
// | Method   | Mean     | Error    | StdDev   | Ratio | Gen0   | Allocated | Alloc Ratio |
// |--------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
// | ArrayFor | 27.15 ns | 0.340 ns | 0.266 ns |  1.00 |      - |         - |          NA |
// | Act      | 13.55 ns | 0.292 ns | 0.259 ns |  0.50 | 0.0076 |      64 B |          NA |

[MemoryDiagnoser]
public class ForeachBenchmarks
{
    private const int CountElements = 103;
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
    public void Act()
    {
        TestList.Act(SomeAction);
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
        var rest = list.Count % 4;
        var countList = list.Count - rest;
        var countArray = array.Length;
        for (int i = 0; i <= countArray - 4; i += 4)
        {
            if (i + 3 >= countList) break;
            action.Invoke(array[i]);
            action.Invoke(array[i + 1]);
            action.Invoke(array[i + 2]);
            action.Invoke(array[i + 3]);
        }

        for (int i = 0; i < rest; i++)
        {
            action.Invoke(array[countList + i]);
        }
    }
}