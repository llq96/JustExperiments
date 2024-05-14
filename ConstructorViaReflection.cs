using System.Reflection;

namespace ExamplesForInterview;

public class ConstructorViaReflection
{
    public void Run()
    {
        Console.WriteLine(BestSingleton.Instance);

        var constructor = typeof(BestSingleton)
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .First();
        var obj = constructor.Invoke(Array.Empty<object>());
        Console.WriteLine(obj);
    }

    public void Run2()
    {
        // TODO
        // Activator.CreateInstance();
    }

    public class BestSingleton
    {
        private readonly int _value;

        private static BestSingleton _instance;
        public static BestSingleton Instance => _instance ??= new BestSingleton();

        private BestSingleton()
        {
            _value = new Random().Next();
            Console.WriteLine($"Created with value {_value}");
        }

        public override string ToString()
        {
            return $"Value = {_value}";
        }
    }
}