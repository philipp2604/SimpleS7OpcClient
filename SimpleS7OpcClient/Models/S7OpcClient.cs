using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Models;

public class S7OpcClient : LibUA.Client
{
    public S7OpcClient(ApplicationDescription appDesc, ConnectionSettings connSettings, SecuritySettings securitySettings, SessionSettings sessionSettings) :
        base(connSettings.Host, connSettings.Port, connSettings.TimeoutMs, connSettings.MaxMessageSize)
    {
        ApplicationDescription = appDesc;
        ConnectionSettings = connSettings;
        SecuritySettings = securitySettings;
        SessionSettings = sessionSettings;
    }

    public ApplicationDescription ApplicationDescription { get; }
    public ConnectionSettings ConnectionSettings { get; }
    public SecuritySettings SecuritySettings { get; }
    public SessionSettings SessionSettings { get; }

    public override X509Certificate2? ApplicationCertificate
    {
        get => SecuritySettings.ApplicationCertificate;
    }

    public override RSA? ApplicationPrivateKey
    {
        get => SecuritySettings.ApplicationPrivateKey;
    }
}