using System.Net;
using System.Text.Json;

namespace WebServer.Core;

public class ResponseWriter : IResponseWriter
{
    public async Task WriteAsync(Stream stream, Response response)
    {
        await using StreamWriter writer = new StreamWriter(stream);
        writer.AutoFlush = true;
        Console.WriteLine($"HTTP/1.1 {(int)response.StatusCode} {response.StatusCode}");
        await writer.WriteLineAsync($"HTTP/1.1 {(int)response.StatusCode} {response.StatusCode}");
        await writer.WriteLineAsync("Content-Type: application/json");
        await writer.WriteLineAsync();
        await writer.WriteLineAsync(JsonSerializer.Serialize(response.Content));
    }
}

public interface IResponseWriter
{
    Task WriteAsync(Stream stream, Response response);
}