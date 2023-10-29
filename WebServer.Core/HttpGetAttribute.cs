namespace WebServer.Core;

[AttributeUsage(AttributeTargets.Method)]
public class HttpGetAttribute : HttpVerbAttribute
{
    public HttpGetAttribute() : base(HttpMethods.Get)
    {
    }
}