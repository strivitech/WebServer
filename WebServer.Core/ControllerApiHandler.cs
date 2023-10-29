using System.Net.Sockets;
using System.Text.Json;

namespace WebServer.Core;

public class ControllerApiHandler : IApiHandler
{
    private readonly IRequestReader _requestReader;
    private readonly IResponseWriter _responseWriter;
    private readonly ControllerActionInvoker _controllerActionInvoker;

    public ControllerApiHandler(
        IRequestReader requestReader, 
        IResponseWriter responseWriter,
        ControllerActionInvoker controllerActionInvoker)
    {
        _requestReader = requestReader;
        _responseWriter = responseWriter;
        _controllerActionInvoker = controllerActionInvoker;
    }
    
    public async Task HandleAsync(NetworkStream stream)
    {
        var request = await _requestReader.ReadAsync(stream);
        var result = await _controllerActionInvoker.InvokeAsync(request.RequestMetadata.Path, CreateArguments(request.Body));
        await _responseWriter.WriteAsync(stream,
            new Response(result.StatusCode, result.Content));
    }

    private object?[]? CreateArguments(string? body)
    {
        return body is null ? null : new object?[] {body};
    }
}