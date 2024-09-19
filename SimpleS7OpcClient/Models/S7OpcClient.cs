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

    public bool? ReadBool(string identifier, ushort namespaceId = 3, bool addQuotationMarks = true)
    {
        if(addQuotationMarks)
            identifier = "\"" + identifier + "\"";

        base.Read(new ReadValueId[]
        {
            new ReadValueId(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new QualifiedName())
        }, out DataValue[] values);

        if(values.Length == 1)
        {
            var boolValue = values[0].Value as bool?;
            if (boolValue != null)
                return (bool)boolValue;
        }

        return null;
    }

    public object? ReadTag(string identifier, PlcDataType dataType, ushort namespaceId = 3, bool addQuotationMarks = true)
    {
        if (addQuotationMarks)
            identifier = "\"" + identifier + "\"";

        base.Read(new ReadValueId[]
        {
            new ReadValueId(new NodeId(namespaceId, identifier), NodeAttribute.Value, null, new QualifiedName())
        }, out DataValue[] values);

        if (values.Length == 0)
            return null;

        switch(dataType)
        {
            case PlcDataType.Invalid:
                return null;
            case PlcDataType.Bool:
            {
                return values[0].Value as bool?;
            }
            case PlcDataType.Byte:
            {
                return values[0].Value as byte?;
            }
            case PlcDataType.Word:
            {
                return values[0].Value as UInt16?;
            }
            case PlcDataType.LWord:
            {
                return values[0].Value as UInt64?;
            }
            case PlcDataType.SInt:
            {
                return values[0].Value as sbyte?;
            }
            default:
                return null;
        }
    }
}