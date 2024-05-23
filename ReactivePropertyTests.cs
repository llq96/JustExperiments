using ReactiveInt = ReactiveUI.ReactiveProperty<int>;

namespace ExamplesForInterview;

/// <summary>
/// Выведет 2 3
/// Это отличается от ожидаемого поведения паттерна Observer
/// </summary>
public class ReactivePropertyTests
{
    private readonly ReactiveInt _reactiveInt = new();

    public void Run()
    {
        _reactiveInt.Value = 1;
        _reactiveInt.Value = 2;
        _reactiveInt.Subscribe(Console.WriteLine);
        _reactiveInt.Value = 3;
    }
}