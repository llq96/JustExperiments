namespace ExamplesForInterview;

public class StructParameterlessCtor
{
    private MyStruct _structField;

    public void Run()
    {
        Console.WriteLine(_structField); // 0 (!), конструктор без параметров не был вызван
        MyStruct structVariable = new(); // Явный вызов конструктора
        Console.WriteLine(structVariable); // 20
    }

    public struct MyStruct
    {
        private int _value;

        public MyStruct()
        {
            _value = 20;
        }

        public override string ToString()
        {
            return $"{_value}";
        }
    }
}