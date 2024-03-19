using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ExamplesForInterview;

public class Mutation
{
    private const string constStr = "000";

    public void Run()
    {
        Mutate(constStr);

        var nonConst = "000";

        Console.WriteLine(nonConst);
    }

    private void Mutate(string str)
    {
        var span = str.AsSpan();
        ref char spanReference = ref MemoryMarshal.GetReference(span);
        ref char char0 = ref Unsafe.Add(ref spanReference, 0);
        ref char char2 = ref Unsafe.Add(ref spanReference, 2);
        char0 = '4';
        char2 = '4';
    }

    private void UnsafeMutate(string str)
    {
        unsafe
        {
            ref readonly char reference = ref str.AsSpan().GetPinnableReference();
            fixed (char* p = &reference)
            {
                *p = '4';
                *(p + 2) = '4';
            }
        }
    }
}