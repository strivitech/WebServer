using System.Net;

namespace WebServer.Core;

public record Response(HttpStatusCode StatusCode, object? Content);