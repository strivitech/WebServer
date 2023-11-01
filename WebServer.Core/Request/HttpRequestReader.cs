using System.Net.Sockets;
using WebServer.Core.Response;

namespace WebServer.Core.Request;

public class HttpRequestReader : IHttpRequestReader
{
    public async Task<HttpRequest> ReadAsync(NetworkStream stream)
    {
        using var reader = new StreamReader(stream, leaveOpen: true);
        var metadata = await ReadMetadata(reader);
        var headers = await ReadHeaders(reader);
        
        ValidateContentType(headers);
        
        var body = await ReadBody(headers, reader);

        return new HttpRequest(metadata, headers, body);
    }

    private static void ValidateContentType(Dictionary<string, string> headers)
    {
        if (!headers.TryGetValue("Content-Type", out var contentType))
        {
            throw new InvalidOperationException("Content-Type header is missing");
        }

        if (!contentType.Contains("application/json"))
        {
            throw new InvalidOperationException("Content-Type header is not application/json");
        }
    }

    private static async Task<HttpRequestMetadata> ReadMetadata(StreamReader reader)
    {
        string? line = await reader.ReadLineAsync();
        var metadata = HttpRequestParser.ParseMetadata(line);
        return metadata;
    }

    private static async Task<string?> ReadBody(Dictionary<string, string> headers, StreamReader reader)
    {
        string? body = null;
        if (headers.TryGetValue("Content-Length", out var contentLengthHeader))
        {
            int contentLength = int.Parse(contentLengthHeader);
            char[] bodyChars = new char[contentLength];
            if (contentLength > 0)
            {
                await reader.ReadAsync(bodyChars, 0, contentLength);
                body = new string(bodyChars);
            }
        }

        return body;
    }

    private static async Task<Dictionary<string, string>> ReadHeaders(StreamReader reader)
    {
        string? line;
        var headers = new Dictionary<string, string>();
        while (!string.IsNullOrWhiteSpace(line = await reader.ReadLineAsync()))
        {
            var header = HttpRequestParser.ParseHeader(line);
            headers.Add(header.Key, header.Value);
        }

        return headers;
    }
}