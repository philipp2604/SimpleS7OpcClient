namespace SimpleS7OpcClient.Interfaces.Models;

/// <summary>
/// Interface for ApplicationDescriptions to contain the necessary informations for the OPC ApplicationDescription.
/// </summary>
public interface IApplicationDescription
{
    /// <summary>
    /// Gets the OPC UA Application Id
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    /// Gets the OPC UA Product Id
    /// </summary>
    public string ProductId { get; }

    /// <summary>
    /// Gets the OPC UA Application Name
    /// </summary>
    public string ApplicationName { get; }

    /// <summary>
    /// Gets the LocaleIds
    /// </summary>
    public string[] LocaleId { get; }
}