using System.Reflection.Emit;
using ILDelegateHelpers;

namespace ExamplesForInterview;

public class DynamicMethodsHelperExperiments
{
    public void Run()
    {
        IntActionExperiment();
        IntFuncExperiment();
    }

    private static void IntActionExperiment()
    {
        var action = ILDelegate.CreateAction<int>(gen =>
        {
            gen.Emit(OpCodes.Ldarg_0);
            gen.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }), null);
        });
        action(456);
    }

    private static void IntFuncExperiment()
    {
        var func = ILDelegate.CreateFunc<int>(gen => { gen.Emit(OpCodes.Ldc_I4, 777); });
        var result = func();
        Console.WriteLine(result);
    }
}