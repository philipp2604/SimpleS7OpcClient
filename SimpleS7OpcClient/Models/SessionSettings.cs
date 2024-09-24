namespace SimpleS7OpcClient.Models;

public class SessionSettings(string sessionName, int sessionTimeoutMs, bool anonymously)
{
    public string SessionName { get; } = sessionName;
    public int SessionTimeoutMs { get; } = sessionTimeoutMs;
    public bool Anonymously { get; } = anonymously;
    public string? Username { get; }
    public string? Password { get; }
}