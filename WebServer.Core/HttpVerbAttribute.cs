namespace WebServer.Core;

public abstract class HttpVerbAttribute : Attribute
{
    protected HttpVerbAttribute(HttpMethods method)
    {
        Method = method;
    }

    public HttpMethods Method { get; set; }
}