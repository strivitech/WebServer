using WebServer.Core.Response.Builder;

namespace WebServer.Core.Response;

public class HttpResponseWriter : IHttpResponseWriter
{
    private readonly IResponseBuilder _responseBuilder;

    public HttpResponseWriter(IResponseBuilder responseBuilder)
    {
        _responseBuilder = responseBuilder;
    }
    
    public async Task WriteAsync(Stream stream, HttpResponse httpResponse)
    {
        await using var writer = new StreamWriter(stream, leaveOpen: true);

        string response = _responseBuilder
            .UseVersion("HTTP/1.1")
            .WithStatusCode((int)httpResponse.StatusCode, httpResponse.StatusCode.ToString())
            .WithContent(httpResponse.Content)
            .Build();

        try
        {
            await writer.WriteAsync(response);
            await writer.FlushAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}