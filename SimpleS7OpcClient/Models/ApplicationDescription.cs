using SimpleS7OpcClient.Interfaces.Models;

namespace SimpleS7OpcClient.Models;

public class ApplicationDescription(string applicationId, string productId, string applicationName, string[] localeId) : IApplicationDescription
{
    public string ApplicationId { get; } = applicationId;
    public string ProductId { get; } = productId;
    public string ApplicationName { get; } = applicationName;
    public string[] LocaleId { get; } = localeId;
}