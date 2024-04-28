using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ExamplesForInterview;

public class Mutation
{
    private const string constStr = "000";
    // static ref string GetX() => ref constStr; //Can not get ref Constant value

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
        ref readonly char reference = ref str.AsSpan().GetPinnableReference();
        unsafe
        {
            fixed (char* p = &reference)
            {
                *p = '4';
                *(p + 2) = '4';
            }
        }
    }

    private void MutateByGCHandle(string str)
    {
        GCHandle handle = GCHandle.Alloc(str, GCHandleType.Pinned);

        IntPtr pointer = handle.AddrOfPinnedObject();

        Marshal.WriteByte(pointer, 0, (byte)'4');
        Marshal.WriteByte(pointer, 4, (byte)'4');

        handle.Free();
    }
}