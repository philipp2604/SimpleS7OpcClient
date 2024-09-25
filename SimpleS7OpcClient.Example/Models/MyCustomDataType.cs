using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Interfaces.Models;
using SimpleS7OpcClient.Models;

namespace SimpleS7OpcClient.Example.Models;

public class MyCustomDataType : CustomDataType, ICustomDataType
{
    public MyCustomDataType()
    {
        _properties = new Dictionary<string, (PlcDataType dataType, object? value)>()
        {
            { "MyTestBool", (PlcDataType.Bool, null) },
            { "MyTestInt", (PlcDataType.Int, null) }
        };
    }

    public new static string TypeId { get; } = "TE_\"TestDataType\"";

    public override void Decode(object data)
    {
        if (data is not byte[] dataArray || dataArray.Length != 3)
            throw new InvalidDataException("Data cannot be used to initiate the data type.");

        var myTestBoolValue = BitConverter.ToBoolean(dataArray, 0);
        var myTestIntValue = BitConverter.ToInt16(dataArray, 1);

        _properties["MyTestBool"] = (PlcDataType.Bool, myTestBoolValue);
        _properties["MyTestInt"] = (PlcDataType.Int, myTestIntValue);
    }

    public override object? Encode()
    {
        return null;
    }
}