namespace WebServer.Core.Request;

internal static class HttpRequestParser
{
    public static HttpRequestMetadata ParseMetadata(string? line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            throw new IndexOutOfRangeException();
        }
        
        var parts = line.Split(' ');
        var method = parts[0];
        var path = parts[1];
        var version = parts[2];
        
        return new HttpRequestMetadata(method, path, version);
    }

    public static (string Key, string Value) ParseHeader(string line)
    {
        var headerParts = line.Split(": ", 2);
        var key = headerParts[0];
        var value = headerParts[1];

        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
        {
            throw new IndexOutOfRangeException();
        }
        
        return (key, value);    
    }
}