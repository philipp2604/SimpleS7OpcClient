using SimpleS7OpcClient.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Interfaces.Models;

public interface IS7OpcClient
{
    public ApplicationDescription ApplicationDescription { get; }
    public ConnectionSettings ConnectionSettings { get; }
    public SecuritySettings SecuritySettings { get; }
    public SessionSettings SessionSettings { get; }

    public X509Certificate2? ApplicationCertificate { get; }

    public RSA? ApplicationPrivateKey { get; }
}