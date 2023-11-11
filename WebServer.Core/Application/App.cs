using WebServer.Core.Configuration;
using WebServer.Core.Request;
using WebServer.Core.Request.Headers;
using WebServer.Core.Response;
using WebServer.Core.Response.Builder;
using WebServer.Core.Transport;

namespace WebServer.Core.Application;

public class App : IApp
{
    internal App()
    {
    }
    
    internal IHttpRequestProcessor? HttpRequestProcessor { get; set; }  
    
    internal ServerConfiguration? ServerConfiguration { get; set; }

    public async Task RunAsync()
    {
        ThrowIfNotValidState();
        
        var requestHandler = new HttpRequestHandler(new HttpRequestReader(new HttpRequestHeadersValidator(
                new ContentTypeValidator(),
                new ContentLengthValidator())),
            new HttpResponseWriter(new ResponseBuilder()),
            HttpRequestProcessor!);
        
        var server = new Server(new TcpBasedServer(requestHandler, ServerConfiguration!));
        await server.RunAsync();
    }

    private void ThrowIfNotValidState()
    {
        if (HttpRequestProcessor is null)
        {
            throw new InvalidOperationException("HttpRequestProcessor is not set");
        }
        
        if (ServerConfiguration is null)
        {
            throw new InvalidOperationException("ServerConfiguration is not set");
        }
    }
}