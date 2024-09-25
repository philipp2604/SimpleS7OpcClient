using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Interfaces.Models;

public interface ICustomDataType
{
    public static abstract string TypeId { get; }
    public Dictionary<string, (PlcDataType dataType, object? value)> Properties { get; }

    public abstract object? Encode();

    public abstract void Decode(object data);
}