using WebServer.Core.Transport;

namespace WebServer.Core;

public class Server
{
    private readonly ITransportProtocolBasedServer _transportProtocolBasedServer;

    public Server(ITransportProtocolBasedServer transportProtocolBasedServer)
    {
        _transportProtocolBasedServer = transportProtocolBasedServer;
    }

    public async Task RunAsync()
    {
        await _transportProtocolBasedServer.StartAsync();
    }
}