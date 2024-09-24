namespace SimpleS7OpcClient.Models;

public class ConnectionSettings
{
    public ConnectionSettings(string host, int port, int timeoutMs = 1000, int maxMsgSize = 65535)
    {
        Host = host;
        Port = port;
        TimeoutMs = timeoutMs;
        MaxMessageSize = maxMsgSize;
    }

    public string Host { get; }
    public int Port { get; }
    public int TimeoutMs { get; }
    public int MaxMessageSize { get; }
}