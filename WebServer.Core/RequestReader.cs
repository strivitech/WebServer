using System.Net.Sockets;

namespace WebServer.Core;

public class RequestReader : IRequestReader
{
    public async Task<Request> ReadAsync(NetworkStream stream)
    {
        using var reader = new StreamReader(stream, leaveOpen: true);
        var metadata = await ReadMetadata(reader);
        var headers = await ReadHeaders(reader);
        var body = await ReadBody(headers, reader);

        return new Request(metadata, headers, body);
    }

    private static async Task<RequestMetadata> ReadMetadata(StreamReader reader)
    {
        string? line = await reader.ReadLineAsync();
        var metadata = RequestParser.ParseMetadata(line);
        return metadata;
    }

    private static async Task<string?> ReadBody(Dictionary<string, string> headers, StreamReader reader)
    {
        string? body = null;
        if (headers.TryGetValue("Content-Length", out var contentLengthHeader))
        {
            int contentLength = int.Parse(contentLengthHeader);
            char[] bodyChars = new char[contentLength];
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
            var header = RequestParser.ParseHeader(line);
            headers.Add(header.Key, header.Value);
        }

        return headers;
    }
}

public interface IRequestReader
{
    Task<Request> ReadAsync(NetworkStream stream);
}