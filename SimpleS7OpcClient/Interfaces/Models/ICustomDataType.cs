using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Interfaces.Models;

/// <summary>
/// Interface for CustomDataType.
/// </summary>
public interface ICustomDataType
{
    /// <summary>
    /// Gets the data type's string-TypeId.
    /// </summary>
    public static abstract string TypeId { get; }

    /// <summary>
    /// Gets the Properties of the CustomDataType.
    /// </summary>
    public Dictionary<string, (PlcDataType dataType, object? value)> Properties { get; }

    /// <summary>
    /// Encodes the instance of the CustomDataType to be sent to the server.
    /// </summary>
    /// <returns>The encoded instance of the CustomDataType.</returns>
    public abstract object? Encode();

    /// <summary>
    /// Decodes server response of a CustomDataType and assigns the property values to this instance.
    /// </summary>
    /// <param name="data">The encoded properties of the CustomDataType</param>
    public abstract void Decode(object data);
}