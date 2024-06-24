namespace ExamplesForInterview;

public class BoxingAndCasting
{
    public void Run()
    {
        var struct1 = new TestStruct(1);
        object boxedObject = struct1;
        struct1.Value = 2;

        TestStruct unboxedStruct = (TestStruct)boxedObject;
        Console.WriteLine(unboxedStruct);

        //InvalidCastException
        // int unboxedInt = (int)boxedObject;
        // Console.WriteLine(unboxedInt);

        boxedObject = new TestStruct(3);

        int unboxedInt = (int)(TestStruct)boxedObject;
        Console.WriteLine(unboxedInt);
    }
}

public struct TestStruct
{
    public int Value;

    public TestStruct(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    // public static explicit operator int(TestStruct testStruct)
    // {
    //     return testStruct.Value;
    // }

    public static implicit operator int(TestStruct testStruct)
    {
        return testStruct.Value;
    }

    //Can not do this
    // public static implicit operator object(TestStruct testStruct){
    //     return null;
    // }
}