using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Interfaces.Services;

/// <summary>
/// Interface for the IS7OpcClientService
/// </summary>
public interface IS7OpcClientService
{
    /// <summary>
    /// Connects to the OPC UA server.
    /// </summary>
    public void Connect();

    /// <summary>
    /// Closes the connection to the OPC UA server.
    /// </summary>
    public void Disconnect();

    /// <summary>
    /// Registers a custom data type for processing.
    /// </summary>
    /// <param name="typeId">The string identifier TypeId of the data type, as used by the server.</param>
    /// <param name="type">The type of the CustomDataType.</param>
    /// <exception cref="ArgumentException"></exception>
    public void RegisterCustomDataType(string typeId, Type type);

    /// <summary>
    /// Registers a custom data type for processing.
    /// </summary>
    /// <typeparam name="T">The type of the CustomDataType.</typeparam>
    /// <param name="typeId">The string identifier TypeId of the data type, as used by the server.</param>
    public void RegisterCustomDataType<T>(string typeId);

    /// <summary>
    /// Reads a single tag from the PLC's tag tables.
    /// </summary>
    /// <param name="tagName">Name of the tag.</param>
    /// <param name="dataType">A <see cref="PlcDataType"/> value.</param>
    /// <param name="namespaceId">Server's Namespace Id of the variable, defaults to '3' with Siemens PLCs.</param>
    /// <returns>An <see cref="object"/> representing the read value of the tag.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidDataException"></exception>
    public object? ReadSingleTableTag(string tagName, PlcDataType dataType, ushort namespaceId = 3);

    /// <summary>
    /// Reads a single variable from one of the PLC's global DataBlocks.
    /// </summary>
    /// <param name="varName">Name of the variable.</param>
    /// <param name="dbName">Name of the DataBlock.</param>
    /// <param name="dataType">A <see cref="PlcDataType"/> value.</param>
    /// <param name="isArray">Specifies whether the variable is an array.</param>
    /// <param name="namespaceId">Server's Namespace Id of the variable, defaults to '3' with Siemens PLCs.</param>
    /// <returns>An <see cref="object"/> representing the read value of the tag.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidDataException"></exception>
    public object? ReadSingleDbVar(string varName, string dbName, PlcDataType dataType, bool isArray = false, ushort namespaceId = 3);

    /// <summary>
    /// Writes a value to a single tag in the PLC's tag tables.
    /// </summary>
    /// <param name="tagName">Name of the tag.</param>
    /// <param name="dataType">A <see cref="PlcDataType"/> value.</param>
    /// <param name="value">The value to be written to the tag.</param>
    /// <param name="namespaceId">Server's Namespace Id of the variable, defaults to '3' with Siemens PLCs.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void WriteSingleTableTag(string tagName, PlcDataType dataType, object value, ushort namespaceId = 3);

    /// <summary>
    /// Writes a value to a single variable in one of the PLC's global DataBlocks.
    /// </summary>
    /// <param name="varName">Name of the variable.</param>
    /// <param name="dbName">Name of the DataBlock.</param>
    /// <param name="dataType">A <see cref="PlcDataType"/> value.</param>
    /// <param name="value">The value to be written to the variable.</param>
    /// <param name="isArray">Specifies whether the variable is an array.</param>
    /// <param name="namespaceId">Server's Namespace Id of the variable, defaults to '3' with Siemens PLCs.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void WriteSingleDbVar(string varName, string dbName, PlcDataType dataType, object value, bool isArray = false, ushort namespaceId = 3);
}