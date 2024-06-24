using System.Text.Json;

namespace ExamplesForInterview.Examples.Serialization;

public class DefaultJsonConverter<T> : BaseConverter<T>
{
    public override string Serialize(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public override T Deserialize(string str)
    {
        return JsonSerializer.Deserialize<T>(str);
    }
}