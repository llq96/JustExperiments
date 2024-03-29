namespace ExamplesForInterview;

public class RefOutIn_InDelegates
{
    public delegate TResult MyDelegate<TResult, T1>(T1 t);

    public MyDelegate<B, B> myDelegate;

    // public void Run()
    // {
    //     myDelegate = Method_A;
    //     myDelegate = Method_B;
    //     myDelegate = Method_C;
    // }
    //
    // private B Method_A(A a)
    // {
    // }
    //
    // private B Method_B(B b)
    // {
    // }
    //
    // private B Method_C(C c)
    // {
    // }
}

public record class A();
public record class B() : A;
public record class C() : B;