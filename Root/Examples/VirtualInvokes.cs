using ExamplesForInterview.Extensions;

namespace ExamplesForInterview.Examples;

public class VirtualInvokes
{
    public void Run()
    {
        var e = new E();
        A a = e;
        B b = e;
        C c = e;
        D d = e;
        a.Log(); //B
        b.Log(); //B
        c.Log(); //E
        d.Log(); //E
        e.Log(); //E

        typeof(D).GetMethod("Log").InvokeNotVirtual(d); //D
    }

    private class A
    {
        public virtual void Log()
        {
            Console.WriteLine(nameof(A));
        }
    }

    private class B : A
    {
        public override void Log()
        {
            Console.WriteLine(nameof(B));
        }
    }

    private class C : B
    {
        public new virtual void Log()
        {
            Console.WriteLine(nameof(C));
        }
    }

    private class D : C
    {
        public override void Log()
        {
            Console.WriteLine(nameof(D));
        }
    }

    private class E : D
    {
        public override void Log()
        {
            Console.WriteLine(nameof(E));
        }
    }
}