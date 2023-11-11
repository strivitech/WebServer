namespace WebServer.Core.Request;

internal record HttpRequestMetadata(string Method, string Path, string Version);