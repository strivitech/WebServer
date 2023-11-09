using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using WebServer.Core.Configuration;

namespace WebServer.Core.Transport;

public class TcpBasedServer : ITransportProtocolBasedServer
{
    private readonly ServerConfiguration _serverConfiguration;
    private readonly IApiHandler _apiHandler;

    public TcpBasedServer(IApiHandler apiHandler, ServerConfiguration serverConfiguration)
    {
        _serverConfiguration = serverConfiguration;
        _apiHandler = apiHandler;
    }

    public async Task StartAsync()
    {
        using TcpListener listener = new TcpListener(IPAddress.Any, 443);
        listener.Start();
        while (true)
        {
            try
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            using (client)
            {
                await using NetworkStream stream = client.GetStream();
                if (_serverConfiguration.TlsSettings.UseTls)
                {
                    await using SslStream sslStream = new SslStream(stream, false);
                    await sslStream.AuthenticateAsServerAsync(_serverConfiguration.TlsSettings.Certificate!.X509Certificate2!,
                        false, SslProtocols.Tls12 | SslProtocols.Tls13, false);
                    await _apiHandler.HandleAsync(sslStream);
                }
                else
                {
                    await _apiHandler.HandleAsync(stream);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}