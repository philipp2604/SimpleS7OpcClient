using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleS7OpcClient.Models;
public abstract class CustomDataType : ICustomDataType
{
    protected Dictionary<string, (PlcDataType dataType, object? value)> _properties;

    public CustomDataType()
    {
        _properties = new Dictionary<string, (PlcDataType dataType, object? value)>();
    }

    public static string TypeId => "";

    public Dictionary<string, (PlcDataType dataType, object? value)> Properties => _properties;

    public abstract object? Encode();

    public abstract void Decode(object data);
}
