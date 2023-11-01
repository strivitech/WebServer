using HttpMethod = WebServer.Core.Common.HttpMethod;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPostAttribute : HttpVerbAttribute
{
    public HttpPostAttribute() : base(HttpMethod.Post)
    {
    }
}