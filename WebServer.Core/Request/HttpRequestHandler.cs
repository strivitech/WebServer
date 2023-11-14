using System.Net;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.Response;

namespace WebServer.Core.Request;

internal class HttpRequestHandler : IHttpRequestHandler
{
    private readonly IHttpRequestReader _httpRequestReader;
    private readonly IHttpResponseWriter _httpResponseWriter;
    private readonly IHttpRequestProcessor _httpRequestProcessor;

    public HttpRequestHandler(
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
        var result = await CompleteRequestAsync(request);
        await _httpResponseWriter.WriteAsync(stream,
            new HttpResponse(result.StatusCode, result.Content));
    }

    private async Task<IActionResult> CompleteRequestAsync(HttpRequest request)
    {
        try
        {
            return await _httpRequestProcessor.CompleteAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new ObjectResult(null, HttpStatusCode.InternalServerError);
        }
    }
}