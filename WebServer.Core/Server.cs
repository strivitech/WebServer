using System.Net;
using System.Net.Sockets;

namespace WebServer.Core;

public class Server
{
    private readonly IApiHandler _apiHandler;

    public Server(IApiHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }
    
    public async Task RunAsync()
    {
        using TcpListener listener = new TcpListener(IPAddress.Any, 80);
        listener.Start();
        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        using (client)
        {
            await using NetworkStream stream = client.GetStream();
            await _apiHandler.HandleAsync(stream);
        }
    }
}