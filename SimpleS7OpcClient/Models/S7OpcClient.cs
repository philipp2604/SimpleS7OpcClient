using SimpleS7OpcClient.Interfaces.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Models;

public class S7OpcClient(ApplicationDescription appDesc, ConnectionSettings connSettings, SecuritySettings securitySettings, SessionSettings sessionSettings) : LibUA.Client(connSettings.Host, connSettings.Port, connSettings.TimeoutMs, connSettings.MaxMessageSize), IS7OpcClient
{
    public ApplicationDescription ApplicationDescription { get; } = appDesc;
    public ConnectionSettings ConnectionSettings { get; } = connSettings;
    public SecuritySettings SecuritySettings { get; } = securitySettings;
    public SessionSettings SessionSettings { get; } = sessionSettings;

    public override X509Certificate2? ApplicationCertificate
    {
        get => SecuritySettings.ApplicationCertificate;
    }

    public override RSA? ApplicationPrivateKey
    {
        get => SecuritySettings.ApplicationPrivateKey;
    }
}