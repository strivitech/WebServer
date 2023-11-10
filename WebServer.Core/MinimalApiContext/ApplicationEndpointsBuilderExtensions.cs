using WebServer.Core.Common;

namespace WebServer.Core.MinimalApiContext;

public static class ApplicationEndpointsBuilderExtensions
{
    public static IEndpointsBuilder MapGet(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Get, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapPost(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Post, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapPut(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Put, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapDelete(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Delete, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapHead(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Head, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapOptions(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Options, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapPatch(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Patch, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapTrace(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Trace, handler);
        return app;
    }
    
    public static IEndpointsBuilder MapConnect(this IEndpointsBuilder app, string path, Delegate handler)
    {
        EndpointsContainer.Source.AddEndpoint(path, HttpMethodType.Connect, handler);
        return app;
    }
}