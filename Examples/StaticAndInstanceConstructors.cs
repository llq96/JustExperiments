namespace ExamplesForInterview;

// Result:
// Ctor Static C
// Ctor Static B  
// Ctor Static A  
// Ctor Instance A
// Ctor Instance B
// Ctor Instance C

/// <summary>
/// Статические конструкторы вызываются в порядке от произодного к базовому
/// Конструкторы экземпляра вызываются в порядке от базового к производному
/// </summary>
public class StaticAndInstanceConstructors
{
    public void Run()
    {
        var c = new C();
    }

    private class A
    {
        static A()
        {
            Console.WriteLine($"Ctor Static A"); // 3
        }

        public A()
        {
            Console.WriteLine("Ctor Instance A"); // 4
        }
    }

    private class B : A
    {
        static B()
        {
            Console.WriteLine($"Ctor Static B"); // 2
        }

        public B()
        {
            Console.WriteLine("Ctor Instance B"); // 5
        }
    }

    private class C : B
    {
        static C()
        {
            Console.WriteLine($"Ctor Static C"); // 1
        }

        public C()
        {
            Console.WriteLine("Ctor Instance C"); // 6
        }
    }
}