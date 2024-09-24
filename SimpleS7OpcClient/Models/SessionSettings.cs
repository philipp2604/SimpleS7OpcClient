namespace SimpleS7OpcClient.Models;

public class SessionSettings
{
    public SessionSettings(string sessionName, int sessionTimeoutMs, bool anonymously)
    {
        SessionName = sessionName;
        SessionTimeoutMs = sessionTimeoutMs;
        Anonymously = anonymously;
    }

    public string SessionName { get; }
    public int SessionTimeoutMs { get; }
    public bool Anonymously { get; }
    public string? Username { get; }
    public string? Password { get; }
}