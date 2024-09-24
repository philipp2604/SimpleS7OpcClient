namespace SimpleS7OpcClient.Interfaces.Models;

public interface ISessionSettings
{
    public string SessionName { get; }
    public int SessionTimeoutMs { get; }
    public bool Anonymously { get; }
    public string? Username { get; }
    public string? Password { get; }
}