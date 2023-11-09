namespace WebServer.Core.Configuration;

public class TlsSettings
{
    public bool UseTls { get; set; }
    public TlsCertificate? Certificate { get; set; }
}