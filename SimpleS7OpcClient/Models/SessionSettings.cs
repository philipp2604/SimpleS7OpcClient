using SimpleS7OpcClient.Interfaces.Models;

namespace SimpleS7OpcClient.Models;

/// <summary>
/// A class implementing <see cref="ISessionSettings"/> to contain all necessary informations for the server session.
/// </summary>
/// <param name="sessionName">The <see cref="SessionName"/>.</param>
/// <param name="sessionTimeoutMs">Specifies the session timeout in milliseconds.</param>
/// <param name="anonymously">Specifies whether anonymous mode is used.</param>
public class SessionSettings(string sessionName, int sessionTimeoutMs, bool anonymously) : ISessionSettings
{
    /// <inheritdoc/>
    public string SessionName { get; } = sessionName;

    /// <inheritdoc/>
    public int SessionTimeoutMs { get; } = sessionTimeoutMs;

    /// <inheritdoc/>
    public bool Anonymously { get; } = anonymously;

    /// <inheritdoc/>
    public string? Username { get; }

    /// <inheritdoc/>
    public string? Password { get; }
}