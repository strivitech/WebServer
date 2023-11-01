namespace WebServer.Core.Request;

public record HttpRequestMetadata(string Method, string Path, string Version);