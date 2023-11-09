namespace WebServer.Core.Configuration;

public class ServerConfiguration
{
    public TlsSettings TlsSettings { get; set; } = new();
}