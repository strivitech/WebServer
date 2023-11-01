using System.Net.Sockets;
using WebServer.Core.Request;
using WebServer.Core.Response;

namespace WebServer.Core.ControllersContext;

public class ControllerApiHandler : IApiHandler
{
    private readonly IHttpRequestReader _httpRequestReader;
    private readonly IHttpResponseWriter _httpResponseWriter;
    private readonly HttpRequestProcessor _httpRequestProcessor;

    public ControllerApiHandler(
        IHttpRequestReader httpRequestReader, 
        IHttpResponseWriter httpResponseWriter,
        HttpRequestProcessor httpRequestProcessor)
    {
        _httpRequestReader = httpRequestReader;
        _httpResponseWriter = httpResponseWriter;
        _httpRequestProcessor = httpRequestProcessor;
    }
    
    public async Task HandleAsync(NetworkStream stream)
    {
        var request = await _httpRequestReader.ReadAsync(stream);
        var result = await _httpRequestProcessor.CompleteAsync(request);
        await _httpResponseWriter.WriteAsync(stream,
            new HttpResponse(result.StatusCode, result.Content));
    }
}