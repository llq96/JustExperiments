using System.Numerics;

namespace ExamplesForInterview.Examples;

public class SmallExamples
{
    public void CastObjectToShort()
    {
        int i = 1;
        object obj = i;
        ++i;
        Console.WriteLine(i);
        Console.WriteLine(obj);
        Console.WriteLine((short)i);
        // Console.WriteLine((short)obj); //System.InvalidCastException
    }

    public unsafe void UnsafeLog()
    {
        int* p;
        int a = 123;
        p = (int*)(a);
        // Console.WriteLine(*p); //System.NullReferenceException: ?
        Console.WriteLine((int)p); //123
        Console.WriteLine((int)&p); //Address
    }

    public void StaticGenericFields()
    {
        ClassWithStaticInt<float>.Bar = 1;
        Console.WriteLine(ClassWithStaticInt<int>.Bar); //0 (!)
    }

    private class ClassWithStaticInt<T>
    {
        public static int Bar;
    }

    public void Unboxing()
    {
        Vector3 a = new Vector3(1, 1, 1);
        object o = a;
        Vector3 b = (Vector3)o; //Распаковка с копированием
        Console.WriteLine(b.ToString());
        Console.WriteLine(((Vector3)o).X + 123); //Распаковка без копирования всего объекта, копирует только поле
        // ((Vector3)o).X = 1; // (Maybe) allow in C++/CLI (Richter)
    }
}