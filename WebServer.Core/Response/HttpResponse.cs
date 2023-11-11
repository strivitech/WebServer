using System.Net;

namespace WebServer.Core.Response;

internal record HttpResponse(HttpStatusCode StatusCode, object? Content);