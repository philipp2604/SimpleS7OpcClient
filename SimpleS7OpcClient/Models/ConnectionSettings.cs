using SimpleS7OpcClient.Interfaces.Models;

namespace SimpleS7OpcClient.Models;

/// <summary>
/// A class implementing <see cref="IConnectionSettings"/> to contain the necessary informations to connect to the OPC UA server.
/// </summary>
/// <param name="host">The <see cref="Host"/> to connect to.</param>
/// <param name="port">The <see cref="Port"/> to connect to on the server.</param>
/// <param name="timeoutMs">Timeout value in milliseconds.</param>
/// <param name="maxMsgSize">Maximum message size.</param>
public class ConnectionSettings(string host, int port, int timeoutMs = 1000, int maxMsgSize = 65535) : IConnectionSettings
{
    /// <inheritdoc/>
    public string Host { get; } = host;

    /// <inheritdoc/>
    public int Port { get; } = port;

    /// <inheritdoc/>
    public int TimeoutMs { get; } = timeoutMs;

    /// <inheritdoc/>
    public int MaxMessageSize { get; } = maxMsgSize;
}