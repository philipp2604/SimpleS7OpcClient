using LibUA.Core;
using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Converters;
using SimpleS7OpcClient.Interfaces.Services;
using SimpleS7OpcClient.Models;
using System.Text;

namespace SimpleS7OpcClient.Services;

public class S7OpcClientService(S7OpcClient client) : IS7OpcClientService
{
    private readonly S7OpcClient _client = client;
    private readonly Dictionary<string, Type> _customDataTypes = [];

    public void Connect()
    {
        _client.Connect();
        _client.OpenSecureChannel(
            LibUA.Core.MessageSecurityMode.None,
        LibUA.Core.SecurityPolicy.None,
            null);
        _client.GetEndpoints(out EndpointDescription[] endpointDescs, _client.ApplicationDescription.LocaleId);
        _client.Disconnect();

        var endpointDesc = endpointDescs.First(
            e =>
            e.SecurityMode == MessageSecurityModeConverter.Convert(_client.SecuritySettings.MessageSecurityMode) &&
            e.SecurityPolicyUri == LibUA.Core.Types.SLSecurityPolicyUris[(int)SecurityPolicyConverter.Convert(_client.SecuritySettings.SecurityPolicy)]);

        var serverCert = endpointDesc.ServerCertificate;

        _client.Connect();
        _client.OpenSecureChannel(
            MessageSecurityModeConverter.Convert(_client.SecuritySettings.MessageSecurityMode),
            SecurityPolicyConverter.Convert(_client.SecuritySettings.SecurityPolicy),
            serverCert);

        _client.CreateSession(
        new LibUA.Core.ApplicationDescription(
        _client.ApplicationDescription.ApplicationId,
        _client.ApplicationDescription.ProductId,
                new LocalizedText(_client.ApplicationDescription.ApplicationName),
                LibUA.Core.ApplicationType.Client, null, null, null),
            _client.SessionSettings.SessionName, _client.SessionSettings.SessionTimeoutMs
            );

        if (_client.SessionSettings.Anonymously)
        {
            var policyId = endpointDesc.UserIdentityTokens.First(
                e => e.TokenType == UserTokenType.Anonymous).PolicyId;
            _client.ActivateSession(new UserIdentityAnonymousToken(policyId), _client.ApplicationDescription.LocaleId);
        }
        else
        {
            var policyId = endpointDesc.UserIdentityTokens.First(
                e => e.TokenType == UserTokenType.UserName).PolicyId;

            ArgumentNullException.ThrowIfNull(_client.SessionSettings.Username);
            ArgumentNullException.ThrowIfNull(_client.SessionSettings.Password);

            _client.ActivateSession(
                new UserIdentityUsernameToken(
                    policyId, _client.SessionSettings.Username,
                    new UTF8Encoding().GetBytes(_client.SessionSettings.Password), LibUA.Core.Types.SignatureAlgorithmRsaOaep),
                _client.ApplicationDescription.LocaleId);
        }
    }

    public void Disconnect() => _client.Disconnect();

    public object? ReadSingleTableTag(string tagName, PlcDataType dataType, ushort namespaceId = 3)
    {
        if (string.IsNullOrWhiteSpace(tagName))
            throw new ArgumentException("Tag name cannot be null or whitespace.", nameof(tagName));

        string identifier = $"\"{tagName}\"";

        var readValueIds = new[]
        {
            new ReadValueId(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new QualifiedName())
        };

        _client.Read(readValueIds, out DataValue[] values);

        return values.Length == 1
            ? TransformReadValue(dataType, values[0], false)
            : throw new InvalidDataException("Unexpected number of values returned.");
    }

    public object? ReadSingleDbVar(string varName, string dbName, PlcDataType dataType, bool isArray = false, ushort namespaceId = 3)
    {
        if (string.IsNullOrWhiteSpace(varName))
            throw new ArgumentException("Variable name cannot be null or whitespace.", nameof(varName));

        if (string.IsNullOrWhiteSpace(dbName))
            throw new ArgumentException("DataBlock name cannot be null or whitespace.", nameof(dbName));

        string identifier = $"\"{dbName}\".\"{varName}\"";

        var readValueIds = new[]
        {
            new ReadValueId(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new QualifiedName())
        };

        _client.Read(readValueIds, out DataValue[] values);

        return values.Length == 1
            ? TransformReadValue(dataType, values[0], isArray)
            : throw new InvalidDataException("Unexpected number of values returned.");
    }

    public void WriteSingleTableTag(string tagName, PlcDataType dataType, object value, ushort namespaceId = 3)
    {
        if (string.IsNullOrWhiteSpace(tagName))
            throw new ArgumentException("Tag name cannot be null or whitespace.", nameof(tagName));

        if (value == null)
            throw new ArgumentNullException(nameof(value), "Value cannot be null.");

        string identifier = $"\"{tagName}\"";

        object sendVal = TransformWriteValue(value, dataType) ?? throw new InvalidDataException("Transformation of the write value failed.");

        var writeValues = new[]
        {
            new WriteValue(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new DataValue(sendVal, StatusCode.Good))
        };

        _client.Write(writeValues, out uint[] results);

        if (results == null || results.Length == 0 || results[0] != (uint)StatusCode.Good)
            throw new InvalidOperationException("Failed to write the value to the PLC.");
    }

    public void WriteSingleDbVar(string varName, string dbName, PlcDataType dataType, object value, bool isArray = false, ushort namespaceId = 3)
    {
        if (string.IsNullOrWhiteSpace(varName))
            throw new ArgumentException("Variable name cannot be null or whitespace.", nameof(varName));

        if (string.IsNullOrWhiteSpace(dbName))
            throw new ArgumentException("DataBlock name cannot be null or whitespace.", nameof(dbName));

        if (value == null)
            throw new ArgumentNullException(nameof(value), "Value cannot be null.");

        string identifier = $"\"{dbName}\".\"{varName}\"";

        object sendVal = TransformWriteValue(value, dataType, isArray) ?? throw new InvalidDataException("Transformation of the write value failed.");

        var writeValues = new[]
        {
            new WriteValue(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new DataValue(sendVal, StatusCode.Good))
        };

        _client.Write(writeValues, out uint[] results);

        if (results == null || results.Length == 0 || results[0] != (uint)StatusCode.Good)
            throw new InvalidOperationException("Failed to write the value to the PLC.");
    }

    private static object? TransformReadDateAndTime(DataValue value, bool isArray)
    {
        var result = value.Value as byte[];
        if (result == null || !isArray)
            return result;

        int rows = result.Length / 8;
        byte[,] resultArray = new byte[rows, 8];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                resultArray[i, j] = result[(i * 8) + j];
            }
        }

        return resultArray;
    }

    private static object? TransformReadDTL(DataValue value, bool isArray)
    {
        if (!isArray)
        {
            return value.Value is ExtensionObject eo ? eo.Body as byte[] : (object?)null;
        }

        if (value.Value is ExtensionObject[] eoArray)
        {
            int rows = eoArray.Length;
            byte[,] resultArray = new byte[rows, 12];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    resultArray[i, j] = eoArray[i].Body[j];
                }
            }

            return resultArray;
        }

        return null;
    }

    private object? TransformReadValue(PlcDataType dataType, DataValue value, bool isArray = false)
    {
        return value == null
            ? throw new ArgumentNullException(nameof(value), "DataValue cannot be null.")
            : dataType switch
            {
                PlcDataType.Invalid => null,
                PlcDataType.Bool => isArray ? value.Value as bool[] : value.Value as bool?,
                PlcDataType.Byte => isArray ? value.Value as byte[] : value.Value as byte?,
                PlcDataType.Word => isArray ? value.Value as ushort[] : value.Value as ushort?,
                PlcDataType.DWord => isArray ? value.Value as uint[] : value.Value as uint?,
                PlcDataType.LWord => isArray ? value.Value as ulong[] : value.Value as ulong?,
                PlcDataType.SInt => isArray ? value.Value as sbyte[] : value.Value as sbyte?,
                PlcDataType.Int => isArray ? value.Value as short[] : value.Value as short?,
                PlcDataType.DInt => isArray ? value.Value as int[] : value.Value as int?,
                PlcDataType.USInt => isArray ? value.Value as byte[] : value.Value as byte?,
                PlcDataType.UInt => isArray ? value.Value as ushort[] : value.Value as ushort?,
                PlcDataType.UDInt => isArray ? value.Value as uint[] : value.Value as uint?,
                PlcDataType.LInt => isArray ? value.Value as long[] : value.Value as long?,
                PlcDataType.ULInt => isArray ? value.Value as ulong[] : value.Value as ulong?,
                PlcDataType.Real => isArray ? value.Value as float[] : value.Value as float?,
                PlcDataType.LReal => isArray ? value.Value as double[] : value.Value as double?,
                PlcDataType.S5Time => isArray ? value.Value as ushort[] : value.Value as ushort?,
                PlcDataType.Time => isArray ? value.Value as int[] : value.Value as int?,
                PlcDataType.LTime => isArray ? value.Value as long[] : value.Value as long?,
                PlcDataType.Char => isArray ? value.Value as byte[] : value.Value as byte?,
                PlcDataType.WChar => isArray ? value.Value as ushort[] : value.Value as ushort?,
                PlcDataType.String => isArray ? value.Value as string[] : value.Value as string,
                PlcDataType.WString => isArray ? value.Value as string[] : value.Value as string,
                PlcDataType.Date => isArray ? value.Value as ushort[] : value.Value as ushort?,
                PlcDataType.Time_Of_Day => isArray ? value.Value as uint[] : value.Value as uint?,
                PlcDataType.LTime_Of_Day => isArray ? value.Value as ulong[] : value.Value as ulong?,
                PlcDataType.Date_And_Time => TransformReadDateAndTime(value, isArray),
                PlcDataType.LDT => isArray ? value.Value as DateTime[] : value.Value as DateTime?,
                PlcDataType.DTL => TransformReadDTL(value, isArray),
                PlcDataType.Timer => isArray ? value.Value as ushort[] : value.Value as ushort?,
                PlcDataType.Counter => isArray ? value.Value as ushort[] : value.Value as ushort?,
                PlcDataType.Custom => TransformCustomDataTypeRead(value, isArray),
                _ => throw new NotImplementedException()
            };
    }

    private static object? TransformWriteValue(object value, PlcDataType dataType, bool isArray = false)
    {
        return value == null
            ? throw new ArgumentNullException(nameof(value), "Value cannot be null.")
            : dataType switch
            {
                PlcDataType.Invalid => throw new InvalidDataException(),
                PlcDataType.Bool => value as bool?,
                PlcDataType.Byte => value as byte?,
                PlcDataType.Word => isArray ? value as ushort[] : value as ushort?,
                PlcDataType.DWord => isArray ? value as uint[] : value as uint?,
                PlcDataType.LWord => isArray ? value as ulong[] : value as ulong?,
                PlcDataType.SInt => isArray ? value as sbyte[] : value as sbyte?,
                PlcDataType.Int => isArray ? value as short[] : value as short?,
                PlcDataType.DInt => isArray ? value as int[] : value as int?,
                PlcDataType.USInt => isArray ? value as byte[] : value as byte?,
                PlcDataType.UInt => isArray ? value as ushort[] : value as ushort?,
                PlcDataType.UDInt => isArray ? value as uint[] : value as uint?,
                PlcDataType.LInt => isArray ? value as long[] : value as long?,
                PlcDataType.ULInt => isArray ? value as ulong[] : value as ulong?,
                PlcDataType.Real => isArray ? value as float[] : value as float?,
                PlcDataType.LReal => isArray ? value as double[] : value as double?,
                PlcDataType.S5Time => isArray ? value as ushort[] : value as ushort?,
                PlcDataType.Time => isArray ? value as int[] : value as int?,
                PlcDataType.LTime => isArray ? value as long[] : value as long?,
                PlcDataType.Char => isArray ? value as byte[] : value as byte?,
                PlcDataType.WChar => isArray ? value as ushort[] : value as ushort?,
                PlcDataType.String => isArray ? value as string[] : value as string,
                PlcDataType.WString => isArray ? value as string[] : value as string,
                PlcDataType.Date => isArray ? value as ushort[] : value as ushort?,
                PlcDataType.Time_Of_Day => isArray ? value as uint[] : value as uint?,
                PlcDataType.LTime_Of_Day => isArray ? value as ulong[] : value as ulong?,
                PlcDataType.Date_And_Time => isArray ? value as byte[,] : value as byte[],
                PlcDataType.LDT => isArray ? value as DateTime[] : value as DateTime?,
                PlcDataType.DTL => isArray ? value as byte[,] : value as byte[],
                PlcDataType.Timer => isArray ? value as ushort[] : value as ushort?,
                PlcDataType.Counter => isArray ? value as ushort[] : value as ushort?,
                PlcDataType.Custom => throw new NotImplementedException(),
                _ => throw new NotImplementedException()
            };
    }

    private CustomDataType? TransformCustomDataTypeRead(DataValue value, bool isArray)
    {
        if (value.Value is not ExtensionObject eo)
            throw new InvalidDataException("Unexpected value. Expected an ExtensionObject.");

        if (!_customDataTypes.TryGetValue(eo.TypeId.StringIdentifier, out var type))
            throw new InvalidDataException($"Unregistered custom data type: {eo.TypeId.StringIdentifier}");

        if (Activator.CreateInstance(type) is not CustomDataType instance)
            throw new InvalidDataException($"Failed to create an instance of the custom data type: {eo.TypeId.StringIdentifier}");

        instance.Decode(eo.Body);

        return instance;
    }

    public void RegisterCustomDataType(string typeId, Type type)
    {
        if (string.IsNullOrWhiteSpace(typeId))
            throw new ArgumentException("Type id cannot be null or whitespace.");

        _customDataTypes.Add(typeId, type);
    }

    public void RegisterCustomDataType<T>(string typeId)
    {
        RegisterCustomDataType(typeId, typeof(T));
    }
}