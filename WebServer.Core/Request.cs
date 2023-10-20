namespace WebServer.Core;

public record Request(RequestMetadata RequestMetadata, Dictionary<string, string> Headers, string? Body);