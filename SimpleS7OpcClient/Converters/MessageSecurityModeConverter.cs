using SimpleS7OpcClient.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleS7OpcClient.Converters;
public static class MessageSecurityModeConverter
{
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
