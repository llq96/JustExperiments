using ReactiveUI;

namespace ExamplesForInterview;

public class ReactivePropertyTests
{
    private ReactiveProperty<int> _reactiveInt = new();

    public void Run()
    {
        _reactiveInt.Value = 1;
        _reactiveInt.Value = 2;
        _reactiveInt.Subscribe(Console.WriteLine);
        _reactiveInt.Value = 3;
    }
}