namespace SimpleS7OpcClient.Interfaces.Models;

public interface IConnectionSettings
{
    public string Host { get; }
    public int Port { get; }
    public int TimeoutMs { get; }
    public int MaxMessageSize { get; }
}