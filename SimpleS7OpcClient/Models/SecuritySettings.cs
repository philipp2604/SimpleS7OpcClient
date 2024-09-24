using SimpleS7OpcClient.Constants;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Models;

public class SecuritySettings(MessageSecurityMode msgSecurityMode, SecurityPolicy securityPolicy, byte[]? serverCertificate, X509Certificate2? applicationCertificate, RSA? applicationPrivateKey)
{
    public MessageSecurityMode MessageSecurityMode { get; } = msgSecurityMode;
    public SecurityPolicy SecurityPolicy { get; } = securityPolicy;
    public byte[]? ServerCertificate { get; } = serverCertificate;
    public X509Certificate2? ApplicationCertificate { get; } = applicationCertificate;
    public RSA? ApplicationPrivateKey { get; } = applicationPrivateKey;
}