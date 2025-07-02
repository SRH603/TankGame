/// <summary>
/// Interface that defines how to serialize, deserialize data
/// </summary>
public interface ISerializer
{
    string SerializeData<T>(T data);
    T DeserializeData<T>(string data);
}
