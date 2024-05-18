namespace ExamplesForInterview;

public class Ideas
{
    //-------------------------------------- Неочевидно, что такое есть
    
    // CancellationTokenSource.CreateLinkedTokenSource //

    //-------------------------------------- Поэкспериментировать с try

    // ThreadPool.QueueUserWorkItem((obj) =>
    // {
    //     Console.WriteLine(obj.GetType());
    //     throw new Exception("QWe");
    // }, new bool());
    // CancellationTokenSource source = new();
    //
    // Thread.Sleep(1000);
    // Console.WriteLine("End");
    
    //-------------------------------------- Разница в ответах при разных типах второго дженерика
    
    // private static void Main()
    // {
    //     SomeClass1.Value = 1;
    //     SomeClass2.Value = 2;
    //     Console.WriteLine(SomeClass1.Value);
    //     Console.WriteLine(SomeClass2.Value);
    // }
    //
    // //
    // public class SomeClass<T>
    // {
    //     public static int Value;
    // }
    // public class SomeClass1 : SomeClass<int>;
    // public class SomeClass2 : SomeClass<int>;
}