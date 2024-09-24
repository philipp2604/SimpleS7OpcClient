namespace SimpleS7OpcClient.Models;

public class ApplicationDescription
{
    public ApplicationDescription(string applicationId, string productId, string applicationName, string[] localeId)
    {
        ApplicationId = applicationId;
        ProductId = productId;
        ApplicationName = applicationName;
        LocaleId = localeId;
    }

    public string ApplicationId { get; }
    public string ProductId { get; }
    public string ApplicationName { get; }
    public string[] LocaleId { get; }
}