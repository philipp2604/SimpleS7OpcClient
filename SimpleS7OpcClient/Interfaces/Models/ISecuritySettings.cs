using SimpleS7OpcClient.Constants;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Interfaces.Models;

public interface ISecuritySettings
{
    public MessageSecurityMode MessageSecurityMode { get; }
    public SecurityPolicy SecurityPolicy { get; }
    public byte[]? ServerCertificate { get; }
    public X509Certificate2? ApplicationCertificate { get; }
    public RSA? ApplicationPrivateKey { get; }
}