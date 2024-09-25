namespace SimpleS7OpcClient.Interfaces.Models;

/// <summary>
/// Interface for SessionSettings.
/// </summary>
public interface ISessionSettings
{
    /// <summary>
    /// Gets the <see cref="SessionName"/>.
    /// </summary>
    public string SessionName { get; }

    /// <summary>
    /// Gets the Session Timeout in milliseconds.
    /// </summary>
    public int SessionTimeoutMs { get; }

    /// <summary>
    /// Gets whether Anonymous mode is used.
    /// </summary>
    public bool Anonymously { get; }

    /// <summary>
    /// Gets the username for the session.
    /// </summary>
    public string? Username { get; }

    /// <summary>
    /// Gets the password for the session.
    /// </summary>
    public string? Password { get; }
}