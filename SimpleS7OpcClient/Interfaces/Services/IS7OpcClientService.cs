using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Interfaces.Services;

public interface IS7OpcClientService
{
    public void Connect();

    public void Disconnect();

    public object? ReadSingleTagFromTable(string tagName, PlcDataType dataType, ushort namespaceId = 3);

    public object? ReadSingleVarFromDb(string varName, string dbName, PlcDataType dataType, bool isArray = false, ushort namespaceId = 3);

    public void WriteSingleTagToTable(string tagName, PlcDataType dataType, object value, ushort namespaceId = 3);

    public void WriteSingleVarToDb(string varName, string dbName, PlcDataType dataType, object value, bool isArray = false, ushort namespaceId = 3);
}