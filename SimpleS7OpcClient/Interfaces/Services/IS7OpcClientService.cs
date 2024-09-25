using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Interfaces.Services;

public interface IS7OpcClientService
{
    public void Connect();

    public void Disconnect();

    public void RegisterCustomDataType(string typeId, Type type);

    public void RegisterCustomDataType<T>(string typeId);

    public object? ReadSingleTableTag(string tagName, PlcDataType dataType, ushort namespaceId = 3);

    public object? ReadSingleDbVar(string varName, string dbName, PlcDataType dataType, bool isArray = false, ushort namespaceId = 3);

    public void WriteSingleTableTag(string tagName, PlcDataType dataType, object value, ushort namespaceId = 3);

    public void WriteSingleDbVar(string varName, string dbName, PlcDataType dataType, object value, bool isArray = false, ushort namespaceId = 3);
}