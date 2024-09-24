namespace SimpleS7OpcClient.Interfaces.Models;

public interface IApplicationDescription
{
    public string ApplicationId { get; }
    public string ProductId { get; }
    public string ApplicationName { get; }
    public string[] LocaleId { get; }
}