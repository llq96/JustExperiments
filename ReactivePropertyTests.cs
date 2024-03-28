using ReactiveInt = ReactiveUI.ReactiveProperty<int>;

namespace ExamplesForInterview;

public class ReactivePropertyTests
{
    private ReactiveInt _reactiveInt = new();

    public void Run()
    {
        _reactiveInt.Value = 1;
        _reactiveInt.Value = 2;
        _reactiveInt.Subscribe(Console.WriteLine);
        _reactiveInt.Value = 3;
    }
}