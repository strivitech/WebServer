using System.Net.Sockets;

namespace WebServer.Core;

public interface IApiHandler
{
    Task HandleAsync(NetworkStream stream);
}