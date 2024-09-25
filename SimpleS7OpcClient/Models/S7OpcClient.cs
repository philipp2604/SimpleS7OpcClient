using SimpleS7OpcClient.Interfaces.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Models;

/// <summary>
/// The S7 OPC Client, implementing <see cref="IS7OpcClient"/> and representing the connection to the server.
/// </summary>
/// <param name="appDesc">The <see cref="ApplicationDescription"/>.</param>
/// <param name="connSettings">The <see cref="ConnectionSettings"/>.</param>
/// <param name="securitySettings">The <see cref="SecuritySettings"/>.</param>
/// <param name="sessionSettings">The <see cref="SessionSettings"/>.</param>
public class S7OpcClient(ApplicationDescription appDesc, ConnectionSettings connSettings, SecuritySettings securitySettings, SessionSettings sessionSettings) : LibUA.Client(connSettings.Host, connSettings.Port, connSettings.TimeoutMs, connSettings.MaxMessageSize), IS7OpcClient
{
    /// <inheritdoc/>
    public ApplicationDescription ApplicationDescription { get; } = appDesc;

    /// <inheritdoc/>
    public ConnectionSettings ConnectionSettings { get; } = connSettings;

    /// <inheritdoc/>
    public SecuritySettings SecuritySettings { get; } = securitySettings;

    /// <inheritdoc/>
    public SessionSettings SessionSettings { get; } = sessionSettings;

    /// <inheritdoc/>
    public override X509Certificate2? ApplicationCertificate
    {
        get => SecuritySettings.ApplicationCertificate;
    }

    /// <inheritdoc/>
    public override RSA? ApplicationPrivateKey
    {
        get => SecuritySettings.ApplicationPrivateKey;
    }
}