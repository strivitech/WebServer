using System.Net;

namespace WebServer.Core.Response;

public record HttpResponse(HttpStatusCode StatusCode, object? Content);