using System.Reflection.Emit;

namespace ExamplesForInterview.DynamicMethodsHelpers;

public static class DynamicMethodsHelper
{
    public static ActionMethod CreateActionMethod(Action<ILGenerator> generatorInit)
    {
        return new ActionMethod(Guid.NewGuid().ToString(), generatorInit);
    }

    public static ActionMethod<T> CreateActionMethod<T>(Action<ILGenerator> generatorInit)
    {
        return new ActionMethod<T>(Guid.NewGuid().ToString(), generatorInit);
    }
}

public abstract class ActionMethodBase<TDelegate> where TDelegate : Delegate
{
    protected DynamicMethod DynamicMethod { get; private set; }
    protected ILGenerator ILGenerator { get; private set; }

    protected ActionMethodBase(string name, Type[] parameterTypes, Action<ILGenerator> generatorInit)
    {
        DynamicMethod = new DynamicMethod(name, null, parameterTypes);
        ILGenerator = DynamicMethod.GetILGenerator();
        generatorInit(ILGenerator);
        ILGenerator.Emit(OpCodes.Ret);
    }

    public abstract TDelegate GetDelegate();
}

public class ActionMethod(string name, Action<ILGenerator> generatorInit)
    : ActionMethodBase<Action>(name, Type.EmptyTypes, generatorInit)
{
    public override Action GetDelegate()
    {
        return (Action)DynamicMethod.CreateDelegate(typeof(Action));
    }
}

public class ActionMethod<T>(string name, Action<ILGenerator> generatorInit)
    : ActionMethodBase<Action<T>>(name, new[] { typeof(T) }, generatorInit)
{
    public override Action<T> GetDelegate()
    {
        return (Action<T>)DynamicMethod.CreateDelegate(typeof(Action<T>));
    }
}