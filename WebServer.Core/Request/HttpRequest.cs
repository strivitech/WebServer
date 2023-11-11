namespace WebServer.Core.Request;

internal record HttpRequest(HttpRequestMetadata HttpRequestMetadata, Dictionary<string, string> Headers, string? Body);