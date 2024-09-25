namespace SimpleS7OpcClient.Interfaces.Models;

/// <summary>
/// Interface for ConnectionSettings to the server.
/// </summary>
public interface IConnectionSettings
{
    /// <summary>
    /// Gets the Host.
    /// </summary>
    public string Host { get; }

    /// <summary>
    /// Gets the Port.
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// Gets the Timeout in milliseconds.
    /// </summary>
    public int TimeoutMs { get; }

    /// <summary>
    /// Gets the maximum message size.
    /// </summary>
    public int MaxMessageSize { get; }
}