using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

public abstract class HttpVerbAttribute : Attribute
{
    protected HttpVerbAttribute(HttpMethodType methodType)
    {
        MethodType = methodType;
    }

    public HttpMethodType MethodType { get; set; }
}