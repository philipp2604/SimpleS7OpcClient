using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Interfaces.Models;
using SimpleS7OpcClient.Models;

namespace SimpleS7OpcClient.Example.Models;

/// <summary>
/// An example class, implementing <see cref="CustomDataType"/> and <see cref="ICustomDataType"/>, to demonstrate the use.
/// All of those members must be implemented in a custom data type.
/// </summary>
public class MyCustomDataType : CustomDataType, ICustomDataType
{
    /// <summary>
    /// Initializes a new instance of <see cref="MyCustomDataType"/>, important is the definition of the _properties."/>
    /// </summary>
    public MyCustomDataType()
    {
        _properties = new Dictionary<string, (PlcDataType dataType, object? value)>()
        {
            { "MyTestBool", (PlcDataType.Bool, null) },
            { "MyTestInt", (PlcDataType.Int, null) }
        };
    }

    /// <summary>
    /// The OPC UA TypeId string of the custom data type, as returned by the server.
    /// </summary>
    public new static string TypeId { get; } = "TE_\"TestDataType\"";

    /// <summary>
    /// Function to decode a server's response into instance data of this custom data type.
    /// </summary>
    /// <param name="data">The server's response as an <see cref="object"/>.</param>
    /// <exception cref="InvalidDataException"></exception>
    public override void Decode(object data)
    {
        if (data is not byte[] dataArray || dataArray.Length != 3)
            throw new InvalidDataException("Data cannot be used to initiate the data type.");

        var myTestBoolValue = BitConverter.ToBoolean(dataArray, 0);
        var myTestIntValue = BitConverter.ToInt16(dataArray, 1);

        _properties["MyTestBool"] = (PlcDataType.Bool, myTestBoolValue);
        _properties["MyTestInt"] = (PlcDataType.Int, myTestIntValue);
    }

    /// <summary>
    /// Function to encode the instance into an object, that can be sen´t to the server.
    /// </summary>
    /// <returns>An <see cref="object"/> that can be sent to the server.</returns>
    public override object? Encode()
    {
        return null;
    }
}