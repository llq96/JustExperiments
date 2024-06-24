using Newtonsoft.Json;

namespace ExamplesForInterview.Examples.Serialization;

public class NewtonSoftJsonConverter<T> : BaseConverter<T>
{
    public override string Serialize(T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public override T Deserialize(string str)
    {
        return JsonConvert.DeserializeObject<T>(str);
    }
}