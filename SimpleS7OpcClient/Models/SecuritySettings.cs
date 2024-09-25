using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Interfaces.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Models;

/// <summary>
/// A class implementing <see cref="ISecuritySettings"/> to contain all necessary security settings for the server connection.
/// </summary>
/// <param name="msgSecurityMode">The <see cref="MessageSecurityMode"/>.</param>
/// <param name="securityPolicy">The <see cref="SecurityPolicy"/>.</param>
/// <param name="serverCertificate">The <see cref="ServerCertificate"/>.</param>
/// <param name="applicationCertificate">The <see cref="ApplicationCertificate"/>.</param>
/// <param name="applicationPrivateKey">The <see cref="ApplicationPrivateKey"/>.</param>
public class SecuritySettings(MessageSecurityMode msgSecurityMode, SecurityPolicy securityPolicy, byte[]? serverCertificate, X509Certificate2? applicationCertificate, RSA? applicationPrivateKey) : ISecuritySettings
{
    /// <inheritdoc/>
    public MessageSecurityMode MessageSecurityMode { get; } = msgSecurityMode;

    /// <inheritdoc/>
    public SecurityPolicy SecurityPolicy { get; } = securityPolicy;

    /// <inheritdoc/>
    public byte[]? ServerCertificate { get; } = serverCertificate;

    /// <inheritdoc/>
    public X509Certificate2? ApplicationCertificate { get; } = applicationCertificate;

    /// <inheritdoc/>
    public RSA? ApplicationPrivateKey { get; } = applicationPrivateKey;
}