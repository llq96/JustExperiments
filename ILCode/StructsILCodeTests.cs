using CommandLine;

namespace ExamplesForInterview;

public class StructsILCodeTests
{
    public struct Struct1
    {
        private Struct2 s;
        private Struct2? qwe;
        private int a;

        public void Test()
        {
            s = new Struct2();
            s = default;
            a = new int();
            qwe = null;
            qwe = null;
            qwe = null;
        }
    }

    private struct Struct2
    {
        public Struct2()
        {
        }

        public Struct2(int value)
        {
        }
        //Can not
        // private Struct1 qwe; 
    }
}