using System.Security.Cryptography.X509Certificates;

namespace WebServer.Core.Configuration;

public class TlsCertificate
{
    public X509Certificate2? X509Certificate2 { get; set; }
}