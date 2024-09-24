using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Converters;

public static class SecurityPolicyConverter
{
    public static LibUA.Core.SecurityPolicy Convert(SecurityPolicy securityPolicy)
    {
        return securityPolicy switch
        {
            SecurityPolicy.Invalid => LibUA.Core.SecurityPolicy.Invalid,
            SecurityPolicy.None => LibUA.Core.SecurityPolicy.None,
            SecurityPolicy.Basic256 => LibUA.Core.SecurityPolicy.Basic256,
            SecurityPolicy.Basic128Rsa15 => LibUA.Core.SecurityPolicy.Basic128Rsa15,
            SecurityPolicy.Basic256Sha256 => LibUA.Core.SecurityPolicy.Basic256Sha256,
            SecurityPolicy.Aes128_Sha256_RsaOaep => LibUA.Core.SecurityPolicy.Aes128_Sha256_RsaOaep,
            SecurityPolicy.Aes256_Sha256_RsaPss => LibUA.Core.SecurityPolicy.Aes256_Sha256_RsaPss,
            _ => throw new NotImplementedException()
        };
    }

    public static SecurityPolicy Convert(LibUA.Core.SecurityPolicy securityPolicy)
    {
        return securityPolicy switch
        {
            LibUA.Core.SecurityPolicy.Invalid => SecurityPolicy.Invalid,
            LibUA.Core.SecurityPolicy.None => SecurityPolicy.None,
            LibUA.Core.SecurityPolicy.Basic256 => SecurityPolicy.Basic256,
            LibUA.Core.SecurityPolicy.Basic128Rsa15 => SecurityPolicy.Basic128Rsa15,
            LibUA.Core.SecurityPolicy.Basic256Sha256 => SecurityPolicy.Basic256Sha256,
            LibUA.Core.SecurityPolicy.Aes128_Sha256_RsaOaep => SecurityPolicy.Aes128_Sha256_RsaOaep,
            LibUA.Core.SecurityPolicy.Aes256_Sha256_RsaPss => SecurityPolicy.Aes256_Sha256_RsaPss,
            _ => throw new NotImplementedException()
        };
    }
}