namespace SimpleS7OpcClient.Constants;

public enum SecurityPolicy
{
    Invalid,
    None,
    Basic256,
    Basic128Rsa15,
    Basic256Sha256,
    Aes128_Sha256_RsaOaep,
    Aes256_Sha256_RsaPss
}