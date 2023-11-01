namespace WebServer.Core.Request;

public record HttpRequest(HttpRequestMetadata HttpRequestMetadata, Dictionary<string, string> Headers, string? Body);