using LibUA.Core;
using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Converters;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SimpleS7OpcClient.Models;

public class S7OpcClient : LibUA.Client
{
    private ApplicationDescription _applicationDescription;
    private ConnectionSettings _connectionSettings;
    private SecuritySettings _securitySettings;
    SessionSettings _sessionSettings;

    public S7OpcClient(ApplicationDescription appDesc, ConnectionSettings connSettings, SecuritySettings securitySettings, SessionSettings sessionSettings) :
        base(connSettings.Host, connSettings.Port, connSettings.TimeoutMs, connSettings.MaxMessageSize)
    {
        _applicationDescription = appDesc;
        _connectionSettings = connSettings;
        _securitySettings = securitySettings;
        _sessionSettings = sessionSettings;
    }

    public override X509Certificate2? ApplicationCertificate
    {
        get => _securitySettings.ApplicationCertificate;
    }

    public override RSA? ApplicationPrivateKey
    {
        get => _securitySettings.ApplicationPrivateKey;
    }

    public new void Connect()
    {
        base.Connect();
        base.OpenSecureChannel(
            LibUA.Core.MessageSecurityMode.None,
            LibUA.Core.SecurityPolicy.None,
            null);
        base.GetEndpoints(out EndpointDescription[] endpointDescs, _applicationDescription.LocaleId);
        base.Disconnect();

        var endpointDesc = endpointDescs.First(
            e =>
            e.SecurityMode == MessageSecurityModeConverter.Convert(_securitySettings.MessageSecurityMode) &&
            e.SecurityPolicyUri == LibUA.Core.Types.SLSecurityPolicyUris[(int)SecurityPolicyConverter.Convert(_securitySettings.SecurityPolicy)]);

        var serverCert = endpointDesc.ServerCertificate;

        base.Connect();
        base.OpenSecureChannel(
            MessageSecurityModeConverter.Convert(_securitySettings.MessageSecurityMode),
            SecurityPolicyConverter.Convert(_securitySettings.SecurityPolicy),
            serverCert);

        base.CreateSession(
            new LibUA.Core.ApplicationDescription(
                _applicationDescription.ApplicationId,
                _applicationDescription.ProductId,
                new LocalizedText(_applicationDescription.ApplicationName),
                LibUA.Core.ApplicationType.Client, null, null, null),
            _sessionSettings.SessionName, _sessionSettings.SessionTimeoutMs
            );


        if(_sessionSettings.Anonymously)
        {
            var policyId = endpointDesc.UserIdentityTokens.First(
                e => e.TokenType == UserTokenType.Anonymous).PolicyId;
            ActivateSession(new UserIdentityAnonymousToken(policyId), _applicationDescription.LocaleId);
        }
        else
        {
            var policyId = endpointDesc.UserIdentityTokens.First(
                e => e.TokenType == UserTokenType.UserName).PolicyId;

            ArgumentNullException.ThrowIfNull(_sessionSettings.Username);
            ArgumentNullException.ThrowIfNull(_sessionSettings.Password);

            ActivateSession(
                new UserIdentityUsernameToken(
                    policyId, _sessionSettings.Username,
                    new UTF8Encoding().GetBytes(_sessionSettings.Password), LibUA.Core.Types.SignatureAlgorithmRsaOaep),
                _applicationDescription.LocaleId);
        }
    }

    public object? ReadSingleTagFromTable(string tagName, PlcDataType dataType, ushort namespaceId = 3)
    {
        string identifier = "\"" + tagName + "\"";

        base.Read(new ReadValueId[]
        {
            new ReadValueId(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new QualifiedName())
        }, out DataValue[] values);

        if (values.Length == 0)
            return null;

        if (values.Length == 1)
            return ReadSingleVar(dataType, values[0], false);

        throw new InvalidDataException();
    }

    public object? ReadSingleVarFromDb(string varName, string dbName, PlcDataType dataType, bool isArray = false, bool globalDb = true, ushort namespaceId = 3)
    {
        string identifier = $"\"{dbName}\".\"{varName}\"";

        base.Read(new ReadValueId[]
        {
        new ReadValueId(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new QualifiedName())
        }, out DataValue[] values);

        if (values.Length == 0)
            return null;

        if (values.Length == 1)
            return ReadSingleVar(dataType, values[0], isArray);

        throw new InvalidDataException();
    }

    private object? ReadSingleVar(PlcDataType dataType, DataValue value, bool isArray = false)
    {
        switch (dataType)
        {
            case PlcDataType.Invalid:
                return null;
            case PlcDataType.Bool:
            {
                return isArray ? value.Value as bool[] : value.Value as bool?;
            }
            case PlcDataType.Byte:
            {
                return isArray ? value.Value as byte[] : value.Value as byte?;
            }
            case PlcDataType.Word:
            {
                return isArray ? value.Value as UInt16[] : value.Value as UInt16?;
            }
            case PlcDataType.DWord:
            {
                return isArray ? value.Value as UInt32[] : value.Value as UInt32?;
            }
            case PlcDataType.LWord:
            {
                return isArray ? value.Value as UInt64[] : value.Value as UInt64?;
            }
            case PlcDataType.SInt:
            {
                return isArray ? value.Value as sbyte[] : value.Value as sbyte?;
            }
            case PlcDataType.Int:
            {
                return isArray ? value.Value as Int16[] : value.Value as Int16?;
            }
            case PlcDataType.DInt:
            {
                return isArray ? value.Value as Int32[] : value.Value as Int32?;
            }
            case PlcDataType.USInt:
            {
                return isArray ? value.Value as byte[] : value.Value as byte?;
            }
            case PlcDataType.UInt:
            {
                return isArray ? value.Value as UInt16[] : value.Value as UInt16?;
            }
            case PlcDataType.UDInt:
            {
                return isArray ? value.Value as UInt32[] : value.Value as UInt32?;
            }
            case PlcDataType.LInt:
            {
                return isArray ? value.Value as Int64[] : value.Value as Int64?;
            }
            case PlcDataType.ULInt:
            {
                return isArray ? value.Value as UInt64[] : value.Value as UInt64?;
            }
            case PlcDataType.Real:
            {
                return isArray ? value.Value as float[] : value.Value as float?;
            }
            case PlcDataType.LReal:
            {
                return isArray ? value.Value as float[] : value.Value as float?;
            }
            case PlcDataType.S5Time:
            {
                return isArray ? value.Value as UInt16[] : value.Value as UInt16?;
            }
            case PlcDataType.Time:
            {
                return isArray ? value.Value as Int32[] : value.Value as Int32?;
            }
            case PlcDataType.LTime:
            {
                return isArray ? value.Value as Int32[] : value.Value as Int32?;
            }
            case PlcDataType.Char:
            {
                return isArray ? value.Value as byte[] : value.Value as byte?;
            }
            case PlcDataType.WChar:
            {
                return isArray ? value.Value as UInt16[] : value.Value as UInt16?;
            }
            case PlcDataType.String:
            {
                return isArray ? value.Value as string[] : value.Value as string;
            }
            case PlcDataType.WString:
            {
                return isArray ? value.Value as string[] : value.Value as string;
            }
            case PlcDataType.Date:
            {
                return isArray ? value.Value as UInt16[] : value.Value as UInt16?;
            }
            case PlcDataType.Time_Of_Day:
            {
                return isArray ? value.Value as UInt32[] : value.Value as UInt32?;
            }
            case PlcDataType.LTime_Of_Day:
            {
                return isArray ? value.Value as UInt64[] : value.Value as UInt64?;
            }
            case PlcDataType.Date_And_Time:
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
                        resultArray[i, j] = result[i * 8 + j];
                    }
                }

                return resultArray;
            }
            case PlcDataType.LDT:
            {
                return isArray ? value.Value as DateTime[] : value.Value as DateTime?;
            }
            case PlcDataType.DTL:
            {
                if (!isArray)
                {
                    if (value.Value is ExtensionObject eo)
                    {
                        return eo.Body as byte[];
                    }
                    return null;
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
            case PlcDataType.Timer:
            {
                return isArray ? value.Value as UInt16[] : value.Value as UInt16?;
            }
            case PlcDataType.Counter:
            {
                return isArray ? value.Value as UInt16[] : value.Value as UInt16?;
            }
            case PlcDataType.Array:
            {
                throw new NotImplementedException();
            }
            default:
                throw new NotImplementedException();
        }
    }
}