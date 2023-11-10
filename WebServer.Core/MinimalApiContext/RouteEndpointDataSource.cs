using System.Collections.ObjectModel;
using WebServer.Core.Common;

namespace WebServer.Core.MinimalApiContext;

internal class RouteEndpointDataSource
{
    private readonly Dictionary<(string Path, HttpMethodType Method), EndpointInternalInfo> _endpoints = new();
    
    public ReadOnlyDictionary<(string Path, HttpMethodType Method), EndpointInternalInfo> Endpoints => _endpoints.AsReadOnly();
    
    public void AddEndpoint(string path, HttpMethodType method, Delegate handler)
    {
        _endpoints.Add((path, method), new EndpointInternalInfo(path, method, handler));
    }
}