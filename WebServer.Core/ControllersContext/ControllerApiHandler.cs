using WebServer.Core.Request;
using WebServer.Core.Response;

namespace WebServer.Core.ControllersContext;

public class ControllerApiHandler : IApiHandler
{
    private readonly IHttpRequestReader _httpRequestReader;
    private readonly IHttpResponseWriter _httpResponseWriter;
    private readonly IHttpRequestProcessor _httpRequestProcessor;

    public ControllerApiHandler(
        IHttpRequestReader httpRequestReader, 
        IHttpResponseWriter httpResponseWriter,
        IHttpRequestProcessor httpRequestProcessor)
    {
        _httpRequestReader = httpRequestReader;
        _httpResponseWriter = httpResponseWriter;
        _httpRequestProcessor = httpRequestProcessor;
    }
    
    public async Task HandleAsync(Stream stream)
    {
        var request = await _httpRequestReader.ReadAsync(stream);
        var result = await _httpRequestProcessor.CompleteAsync(request);
        await _httpResponseWriter.WriteAsync(stream,
            new HttpResponse(result.StatusCode, result.Content));
    }
}