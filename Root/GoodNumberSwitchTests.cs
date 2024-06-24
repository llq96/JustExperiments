namespace ExamplesForInterview;

public class GoodNumberSwitchTests
{
    public void Run()
    {
        // var pair = new Pair(5, 7);
        var pair = new Pair(2000000000, 1000000000);
        // var pair = new Pair(2123456789, 2067375643);
        Console.WriteLine(pair);
        pair.Switch1();
        Console.WriteLine(pair);
        pair.Switch2();
        Console.WriteLine(pair);
        pair.Switch3();
        Console.WriteLine(pair);
    }
}

public class Pair(int a, int b)
{
    public int A = a;
    public int B = b;

    public override string ToString()
    {
        return $"{A} , {B}";
    }

    public void Switch1()
    {
        (A, B) = (B, A);
    }

    public void Switch2()
    {
        var temp = A;
        A = B;
        B = temp;
    }

    public void Switch3()
    {
        // checked
        {
            A = A + B;
            Console.WriteLine(A);
            B = A - B;
            Console.WriteLine(B);
            A = A - B;
            Console.WriteLine(A);
        }
    }
}