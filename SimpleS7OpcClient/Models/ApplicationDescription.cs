using SimpleS7OpcClient.Interfaces.Models;

namespace SimpleS7OpcClient.Models;

/// <summary>
/// A class implementing <see cref="IApplicationDescription"/> to contain the necessary informations for the OPC ApplicationDescription.
/// </summary>
/// <param name="applicationId">The ApplicationId.</param>
/// <param name="productId">The ProductId.</param>
/// <param name="applicationName">The ApplicationId.</param>
/// <param name="localeId">The LocaleIds.</param>
public class ApplicationDescription(string applicationId, string productId, string applicationName, string[] localeId) : IApplicationDescription
{
    /// <inheritdoc/>
    public string ApplicationId { get; } = applicationId;

    /// <inheritdoc/>
    public string ProductId { get; } = productId;

    /// <inheritdoc/>
    public string ApplicationName { get; } = applicationName;

    /// <inheritdoc/>
    public string[] LocaleId { get; } = localeId;
}