using WebServer.Core.Common;

namespace WebServer.Core.MinimalApiContext;

internal static class EndpointInternalInfoFetcher
{
    public static EndpointInternalInfo Get(string endpointPath, HttpMethodType httpMethodType)
    {
        if (!EndpointsContainer.Source.Endpoints.TryGetValue((endpointPath, httpMethodType),
                out var endpointInternalInfo))
        {
            throw new IndexOutOfRangeException($"Endpoint for path '{endpointPath}' not found");
        }

        return endpointInternalInfo;
    }
}