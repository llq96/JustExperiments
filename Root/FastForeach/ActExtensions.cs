namespace ExamplesForInterview;

using System.Runtime.InteropServices;

public static partial class ActExtensions
{
    private static void List_8Test_AsSpan<T>(this List<T> list, Action<T> action)
    {
        var span = CollectionsMarshal.AsSpan(list);
        unsafe
        {
            ref T reference = ref span.GetPinnableReference();
            fixed (T* p = &reference)
            {
                action.Invoke(*p);
                action.Invoke(*(p + 1));
                action.Invoke(*(p + 2));
                action.Invoke(*(p + 3));
                action.Invoke(*(p + 4));
                action.Invoke(*(p + 5));
                action.Invoke(*(p + 6));
                action.Invoke(*(p + 7));
            }
        }
    }

    private static void List_16Test_AsSpan<T>(this List<T> list, Action<T> action)
    {
        var span = CollectionsMarshal.AsSpan(list);
        unsafe
        {
            ref T reference = ref span.GetPinnableReference();
            fixed (T* p = &reference)
            {
                action.Invoke(*p);
                action.Invoke(*(p + 1));
                action.Invoke(*(p + 2));
                action.Invoke(*(p + 3));
                action.Invoke(*(p + 4));
                action.Invoke(*(p + 5));
                action.Invoke(*(p + 6));
                action.Invoke(*(p + 7));
                action.Invoke(*(p + 8));
                action.Invoke(*(p + 9));
                action.Invoke(*(p + 10));
                action.Invoke(*(p + 11));
                action.Invoke(*(p + 12));
                action.Invoke(*(p + 13));
                action.Invoke(*(p + 14));
                action.Invoke(*(p + 15));
            }
        }
    }

    private static void List_32Test_AsSpan<T>(this List<T> list, Action<T> action)
    {
        var span = CollectionsMarshal.AsSpan(list);
        unsafe
        {
            ref T reference = ref span.GetPinnableReference();
            fixed (T* p = &reference)
            {
                action.Invoke(*p);
                action.Invoke(*(p + 1));
                action.Invoke(*(p + 2));
                action.Invoke(*(p + 3));
                action.Invoke(*(p + 4));
                action.Invoke(*(p + 5));
                action.Invoke(*(p + 6));
                action.Invoke(*(p + 7));
                action.Invoke(*(p + 8));
                action.Invoke(*(p + 9));
                action.Invoke(*(p + 10));
                action.Invoke(*(p + 11));
                action.Invoke(*(p + 12));
                action.Invoke(*(p + 13));
                action.Invoke(*(p + 14));
                action.Invoke(*(p + 15));
                action.Invoke(*(p + 16));
                action.Invoke(*(p + 17));
                action.Invoke(*(p + 18));
                action.Invoke(*(p + 19));
                action.Invoke(*(p + 20));
                action.Invoke(*(p + 21));
                action.Invoke(*(p + 22));
                action.Invoke(*(p + 23));
                action.Invoke(*(p + 24));
                action.Invoke(*(p + 25));
                action.Invoke(*(p + 26));
                action.Invoke(*(p + 27));
                action.Invoke(*(p + 28));
                action.Invoke(*(p + 29));
                action.Invoke(*(p + 30));
                action.Invoke(*(p + 31));
            }
        }
    }
}