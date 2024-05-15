namespace ExamplesForInterview.Examples.Serialization;

public abstract class BaseConverter<T>
{
    public abstract string Serialize(T obj);
    public abstract T Deserialize(string str);
}