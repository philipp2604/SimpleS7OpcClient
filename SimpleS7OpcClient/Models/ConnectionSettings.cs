namespace SimpleS7OpcClient.Models;

public class ConnectionSettings(string host, int port, int timeoutMs = 1000, int maxMsgSize = 65535)
{
    public string Host { get; } = host;
    public int Port { get; } = port;
    public int TimeoutMs { get; } = timeoutMs;
    public int MaxMessageSize { get; } = maxMsgSize;
}