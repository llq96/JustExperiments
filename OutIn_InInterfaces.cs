// #define EnableIN
// #define EnableOUT

namespace ExamplesForInterview;

public class OutIn_InInterfaces
{
    public void Run()
    {
        ISomeInterface<object> obj = new SomeClass<object>();
        ISomeInterface<string> str = new SomeClass<string>();

        ISomeInterface<object>[] listObj = { new SomeClass<object>() };
        ISomeInterface<string>[] listStr = { new SomeClass<string>() };

#if EnableIN
        str = obj; // Can only with "in" in interface, because
        //T GetSomeData(); can return object without "in"
#endif
#if EnableOUT
        obj = str; // Can only with "out" in interface, because
        // SetSomeData(T) can expect string instead object without "out"
#endif

#if EnableIN
        listStr = listObj; // Can only with "in" in interface
        // var someString = listStr[0].GetSomeData(); // error because return object, but expect string
#endif
#if EnableOUT
        listObj = listStr; // Can only with "out" in interface
        // listObj[0] = new SomeClass<object>(); // System.ArrayTypeMismatchException
#endif
    }

    public interface ISomeInterface<
#if EnableIN
        in
#endif
#if EnableOUT
    out
#endif
        TType
    >;

    public class SomeClass<T> : ISomeInterface<T>;
}