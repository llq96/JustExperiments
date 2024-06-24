namespace ExamplesForInterview.Examples.Serialization;

public class HashSetToJsonExamples
{
    public void Run()
    {
        var defaultJsonConverter = new DefaultJsonConverter<HashSet<int>>();
        RunExperiments(defaultJsonConverter);

        var newtonSoftJsonConverter = new NewtonSoftJsonConverter<HashSet<int>>();
        RunExperiments(newtonSoftJsonConverter);
    }

    private static void RunExperiments(BaseConverter<HashSet<int>> converter)
    {
        Console.WriteLine($"Run Experiments with Converter - {converter.GetType().Name}");
        var hashSet = CreateHashSet();
        Console.WriteLine("HashSet created");
        LogHashSet(hashSet); //HashSet Values : 123 456 789

        Console.WriteLine("Serialization To JSON");
        var json = converter.Serialize(hashSet);
        Console.WriteLine($"JSON : {json}"); // JSON : [123,456,789]

        Console.WriteLine("Clear HashSet");
        ClearHashSet(hashSet);
        LogHashSet(hashSet); //HashSet Is Empty

        Console.WriteLine("Deserialize JSON To HashSet");
        hashSet = converter.Deserialize(json);
        LogHashSet(hashSet); //HashSet Values : 123 456 789
        Console.WriteLine();
    }

    private static HashSet<int> CreateHashSet() =>
        new()
        {
            123,
            456,
            789
        };

    private static void ClearHashSet<T>(HashSet<T> hashSet) => hashSet.Clear();

    private static void LogHashSet<T>(HashSet<T> hashSet)
    {
        string hashSetLog;
        if (hashSet.Count > 0)
        {
            var valuesLog = string.Join(" ", hashSet);
            hashSetLog = $"HashSet Values : {valuesLog}";
        }
        else
        {
            hashSetLog = "HashSet Is Empty";
        }

        Console.WriteLine(hashSetLog);
    }
}