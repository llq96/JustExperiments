using System.Reflection;
using System.Reflection.Emit;

namespace ExamplesForInterview.Extensions;

public static class ExtensionsIL
{
    public static void InvokeNotVirtual<T>(this MethodInfo methodInfo, T obj)
    {
        var type = typeof(T);
        DynamicMethod dynamicMethod = new DynamicMethod(methodInfo.Name, null, new[] { type });
        ILGenerator gen = dynamicMethod.GetILGenerator();
        gen.Emit(OpCodes.Ldarg_0);
        gen.Emit(OpCodes.Call, methodInfo);
        gen.Emit(OpCodes.Ret);

        var action = (Action<T>)dynamicMethod.CreateDelegate(typeof(Action<T>));
        action(obj);
    }
}