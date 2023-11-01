using System.Net.Sockets;
using WebServer.Core.Request;

namespace WebServer.Core.Response;

public interface IHttpRequestReader
{
    Task<HttpRequest> ReadAsync(NetworkStream stream);
}