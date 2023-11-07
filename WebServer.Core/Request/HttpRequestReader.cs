using WebServer.Core.Request.Headers;
using WebServer.Core.Response;

namespace WebServer.Core.Request;

public class HttpRequestReader : IHttpRequestReader
{
    private readonly IHttpRequestHeadersValidator _headersValidator;

    public HttpRequestReader(IHttpRequestHeadersValidator headersValidator)
    {
        _headersValidator = headersValidator;
    }

    public async Task<HttpRequest> ReadAsync(Stream stream)
    {
        using var reader = new StreamReader(stream, leaveOpen: true);
        var metadata = await ReadMetadata(reader);
        var headers = await ReadHeaders(reader);

        _headersValidator.ValidateHeaders(headers);

        var body = await TryReadBody(headers, reader);

        return new HttpRequest(metadata, headers, body);
    }

    private static async Task<string?> TryReadBody(Dictionary<string, string> headers, StreamReader reader)
    {
        string? body = null;
        if (headers.TryGetValue("Content-Length", out var contentLengthHeader))
        {
            body = await ReadBody(contentLengthHeader, reader);
        }

        return body;
    }

    private static async Task<HttpRequestMetadata> ReadMetadata(StreamReader reader)
    {
        string? line = await reader.ReadLineAsync();
        var metadata = HttpRequestParser.ParseMetadata(line);
        return metadata;
    }

    private static async Task<string?> ReadBody(string contentLengthHeader, StreamReader reader)
    {
        string? body = null;
        int contentLength = int.Parse(contentLengthHeader);
        char[] bodyChars = new char[contentLength];
        if (contentLength > 0)
        {
            await reader.ReadAsync(bodyChars, 0, contentLength);
            body = new string(bodyChars);
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