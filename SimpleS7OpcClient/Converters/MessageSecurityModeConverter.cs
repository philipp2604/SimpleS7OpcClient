using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Converters;

/// <summary>
/// Converters for <see cref="LibUA.Core.MessageSecurityMode"/> values and <see cref="MessageSecurityMode"/> values.
/// </summary>
public static class MessageSecurityModeConverter
{
    /// <summary>
    /// Converts <see cref="MessageSecurityMode"/> values to <see cref="LibUA.Core.MessageSecurityMode"/> values.
    /// </summary>
    public static LibUA.Core.MessageSecurityMode Convert(MessageSecurityMode messageSecurityMode)
    {
        return messageSecurityMode switch
        {
            MessageSecurityMode.Invalid => LibUA.Core.MessageSecurityMode.Invalid,
            MessageSecurityMode.None => LibUA.Core.MessageSecurityMode.None,
            MessageSecurityMode.Sign => LibUA.Core.MessageSecurityMode.Sign,
            MessageSecurityMode.SignAndEncrypt => LibUA.Core.MessageSecurityMode.SignAndEncrypt,
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Converts <see cref="LibUA.Core.MessageSecurityMode"/> values to <see cref="MessageSecurityMode"/> values.
    /// </summary>
    public static MessageSecurityMode Convert(LibUA.Core.MessageSecurityMode messageSecurityMode)
    {
        return messageSecurityMode switch
        {
            LibUA.Core.MessageSecurityMode.Invalid => MessageSecurityMode.Invalid,
            LibUA.Core.MessageSecurityMode.None => MessageSecurityMode.None,
            LibUA.Core.MessageSecurityMode.Sign => MessageSecurityMode.Sign,
            LibUA.Core.MessageSecurityMode.SignAndEncrypt => MessageSecurityMode.SignAndEncrypt,
            _ => throw new NotImplementedException()
        };
    }
}