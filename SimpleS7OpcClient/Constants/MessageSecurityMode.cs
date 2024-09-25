namespace SimpleS7OpcClient.Constants;

/// <summary>
/// Represent the types of OPC UA Security Modes
/// </summary>
public enum MessageSecurityMode
{
    Invalid,
    None,
    Sign,
    SignAndEncrypt
}