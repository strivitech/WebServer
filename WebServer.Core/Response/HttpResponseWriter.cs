using System.Text;
using System.Text.Json;

namespace WebServer.Core.Response;

public class HttpResponseWriter : IHttpResponseWriter
{
    public async Task WriteAsync(Stream stream, HttpResponse httpResponse)
    {
        await using var writer = new StreamWriter(stream, leaveOpen: true);
        string responseBody = JsonSerializer.Serialize(httpResponse.Content);
        int contentLength = Encoding.UTF8.GetByteCount(responseBody);
        
        var responseBuilder = new StringBuilder();
        responseBuilder.AppendLine($"HTTP/1.1 {(int)httpResponse.StatusCode} {httpResponse.StatusCode}");
        responseBuilder.AppendLine("Content-Type: application/json; charset=UTF-8");
        responseBuilder.AppendLine($"Content-Length: {contentLength}");
        responseBuilder.AppendLine();
        responseBuilder.Append(responseBody);
        
        try
        {
            await writer.WriteAsync(responseBuilder);
            await writer.FlushAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}

public interface IHttpResponseWriter
{
    Task WriteAsync(Stream stream, HttpResponse httpResponse);
}