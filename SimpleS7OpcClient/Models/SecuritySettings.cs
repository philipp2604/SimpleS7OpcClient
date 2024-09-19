using SimpleS7OpcClient.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleS7OpcClient.Models;
public class SecuritySettings
{

    public SecuritySettings(MessageSecurityMode msgSecurityMode, SecurityPolicy securityPolicy, byte[]? serverCertificate, X509Certificate2? applicationCertificate, RSA? applicationPrivateKey)
    {
        MessageSecurityMode = msgSecurityMode;
        SecurityPolicy = securityPolicy;
        ServerCertificate = serverCertificate;
        ApplicationCertificate = applicationCertificate;
        ApplicationPrivateKey = applicationPrivateKey;
    }

    public MessageSecurityMode MessageSecurityMode { get; }
    public SecurityPolicy SecurityPolicy { get; }
    public byte[]? ServerCertificate { get; }
    public X509Certificate2? ApplicationCertificate { get; }
    public RSA? ApplicationPrivateKey { get; }
}
