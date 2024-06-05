using System.Reflection.Emit;
using DynamicMethodsHelpers;

namespace ExamplesForInterview;

public class DynamicMethodsHelperExperiments
{
    public void Run()
    {
        var action = DynamicMethodsHelper.CreateActionMethod<int>((gen) =>
        {
            gen.Emit(OpCodes.Ldarg_0);
            gen.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }), null);
        });
        action(456);
    }
}