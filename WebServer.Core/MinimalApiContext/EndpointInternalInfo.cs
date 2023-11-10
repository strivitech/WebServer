using WebServer.Core.Common;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.MinimalApiContext;

internal class EndpointInternalInfo
{
    public EndpointInternalInfo(string path, HttpMethodType method, Delegate handler)
    {
        Path = path;
        Method = method;
        Handler = handler;
        ActionInternalInfo = new ActionInternalInfo(handler.Method, method);
    }
    
    public string Path { get; }
    public HttpMethodType Method { get; }
    public Delegate Handler { get; }
    public ActionInternalInfo ActionInternalInfo { get; }   
}