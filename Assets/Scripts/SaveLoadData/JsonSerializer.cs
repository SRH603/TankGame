using Newtonsoft.Json;

/// <summary>
/// Serialize data using json format. 
/// </summary>
public class JsonSerializer : ISerializer
{
    public T DeserializeData<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    public string SerializeData<T>(T data)
    {
        return JsonConvert.SerializeObject(data);
    }
}
