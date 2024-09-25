using SimpleS7OpcClient.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SimpleS7OpcClient.Interfaces.Models;

/// <summary>
/// Interface of the IS7OpcClient.
/// </summary>
public interface IS7OpcClient
{
    /// <summary>
    /// Gets the <see cref="ApplicationDescription"/>.
    /// </summary>
    public ApplicationDescription ApplicationDescription { get; }

    /// <summary>
    /// Gets the <see cref="ConnectionSettings"/>.
    /// </summary>
    public ConnectionSettings ConnectionSettings { get; }

    /// <summary>
    /// Gets the <see cref="SecuritySettings"/>.
    /// </summary>
    public SecuritySettings SecuritySettings { get; }

    /// <summary>
    /// Gets the <see cref="SessionSettings"/>.
    /// </summary>
    public SessionSettings SessionSettings { get; }

    /// <summary>
    /// Gets the <see cref="X509Certificate2"/>.
    /// </summary>
    public X509Certificate2? ApplicationCertificate { get; }

    /// <summary>
    /// Gets the <see cref="RSA" /> private key.
    /// </summary>
    public RSA? ApplicationPrivateKey { get; }
}