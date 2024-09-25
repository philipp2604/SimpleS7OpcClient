using SimpleS7OpcClient.Constants;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Interfaces.Models;

/// <summary>
/// Interface for SecuritySettings
/// </summary>
public interface ISecuritySettings
{
    /// <summary>
    /// Gets the <see cref="MessageSecurityMode"/>.
    /// </summary>
    public MessageSecurityMode MessageSecurityMode { get; }

    /// <summary>
    /// Gets the <see cref="SecurityPolicy"/>.
    /// </summary>
    public SecurityPolicy SecurityPolicy { get; }

    /// <summary>
    /// Gets the <see cref="ServerCertificate"/>.
    /// </summary>
    public byte[]? ServerCertificate { get; }

    /// <summary>
    /// Gets the <see cref="X509Certificate2"/>.
    /// </summary>
    public X509Certificate2? ApplicationCertificate { get; }

    /// <summary>
    /// Gets the <see cref="RSA" /> private key.
    /// </summary>
    public RSA? ApplicationPrivateKey { get; }
}