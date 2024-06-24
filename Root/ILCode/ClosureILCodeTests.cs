namespace ExamplesForInterview;

public class ClosureILCodeTests
{
    private string _someStringField;

    public event Action asd;

    // public void Run()
    // {
    //     Action a = () => Console.WriteLine(_someStringField);
    // }

    public void Run()
    {
        int someInt = 123;
        Action a = () => Console.WriteLine(_someStringField + someInt);
        Action b = delegate { Console.WriteLine(_someStringField + someInt); };
        asd = a;
        asd = b;
        asd.Invoke();
    }

    // public void Run()
    // {
    //     List<Action> actions = new();
    //     for (int i = 0; i < 5; i++)
    //     {
    //         Action a = () =>
    //         {
    //             Console.WriteLine(i);
    //             Console.WriteLine(_someStringField);
    //         };
    //         // a -= () => { Console.WriteLine(i); };
    //         actions.Add(a);
    //     }
    //
    //     foreach (var action in actions)
    //     {
    //         action?.Invoke();
    //     }
    // }


    // public void Run()
    // {
    //     Action a = Console.WriteLine;
    // }
}