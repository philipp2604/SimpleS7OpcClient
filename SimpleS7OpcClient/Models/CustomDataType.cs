using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Interfaces.Models;

namespace SimpleS7OpcClient.Models;

public abstract class CustomDataType : ICustomDataType
{
    protected Dictionary<string, (PlcDataType dataType, object? value)> _properties;

    protected CustomDataType()
    {
        _properties = [];
    }

    public static string TypeId => "";

    public Dictionary<string, (PlcDataType dataType, object? value)> Properties => _properties;

    public abstract object? Encode();

    public abstract void Decode(object data);
}