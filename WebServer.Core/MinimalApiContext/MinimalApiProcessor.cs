using WebServer.Core.Common;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.ModelBinders;
using WebServer.Core.Request;

namespace WebServer.Core.MinimalApiContext;

public class MinimalApiProcessor : IHttpRequestProcessor
{
    private readonly IBindersFactory _bindersFactory;

    public MinimalApiProcessor(IBindersFactory bindersFactory)
    {
        _bindersFactory = bindersFactory;
    }

    public async Task<IActionResult> CompleteAsync(HttpRequest httpRequest)
    {
        var httpMethodType = Enum.Parse<HttpMethodType>(httpRequest.HttpRequestMetadata.Method, true);
        var webRequestRoute =
            new EndpointsWebRequestParser(httpRequest.HttpRequestMetadata.Path, httpMethodType).Parse();
        var endpointInfo = EndpointInternalInfoFetcher.Get(webRequestRoute.OperatingPath, httpMethodType);

        List<object?> args = new();
        BindBody(args, httpRequest, endpointInfo.ActionInternalInfo);
        BindParameters(args, webRequestRoute, endpointInfo.ActionInternalInfo);
        BindQueryParameters(args, webRequestRoute, endpointInfo.ActionInternalInfo);
        return await (Task<IActionResult>)endpointInfo.Handler.DynamicInvoke(args.ToArray())!;
    }

    private void BindQueryParameters(List<object?> args, WebRequestRoute webRequestRoute,
        ActionInternalInfo methodInternalInfo)
    {
        var queryParametersModelBinder =
            _bindersFactory.CreateQueryParametersModelBinder(webRequestRoute.QueryParameters, methodInternalInfo);
        var queryParametersModel = queryParametersModelBinder.Bind();
        if (queryParametersModel is not null)
        {
            args.AddRange(queryParametersModel);
        }
    }

    private void BindParameters(List<object?> args, WebRequestRoute webRequestRoute,
        ActionInternalInfo methodInternalInfo)
    {
        var parametersModelBinder =
            _bindersFactory.CreateParametersModelBinder(webRequestRoute.UrlParameters, methodInternalInfo);
        var parametersModel = parametersModelBinder.Bind();
        if (parametersModel is not null)
        {
            args.AddRange(parametersModel);
        }
    }

    private void BindBody(List<object?> args, HttpRequest httpRequest, ActionInternalInfo methodInternalInfo)
    {
        var binder = _bindersFactory.CreateBodyModelBinder(httpRequest.Body, methodInternalInfo);
        var bodyModel = binder.Bind();
        if (bodyModel is not null)
        {
            args.Add(bodyModel);
        }
    }
}