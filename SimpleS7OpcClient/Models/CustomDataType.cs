using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Interfaces.Models;

namespace SimpleS7OpcClient.Models;

/// <summary>
/// A base class implementing <see cref="ICustomDataType"/>.
/// </summary>
public abstract class CustomDataType : ICustomDataType
{
    protected Dictionary<string, (PlcDataType dataType, object? value)> _properties;

    protected CustomDataType()
    {
        _properties = [];
    }

    /// <inheritdoc/>
    public static string TypeId => "";

    /// <inheritdoc/>
    public Dictionary<string, (PlcDataType dataType, object? value)> Properties => _properties;

    /// <inheritdoc/>
    public abstract object? Encode();

    /// <inheritdoc/>
    public abstract void Decode(object data);
}