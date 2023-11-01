using HttpMethod = WebServer.Core.Common.HttpMethod;

namespace WebServer.Core.ControllersContext.Actions;

public abstract class HttpVerbAttribute : Attribute
{
    protected HttpVerbAttribute(HttpMethod method)
    {
        Method = method;
    }

    public HttpMethod Method { get; set; }
}