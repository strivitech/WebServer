using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.ModelBinders;
using WebServer.Core.Request;

namespace WebServer.Core.ControllersContext;

public class ControllerProcessor : IHttpRequestProcessor
{
    private readonly IControllerFactory _controllerFactory;
    private readonly IBindersFactory _bindersFactory;
    private readonly IActionInfoFetcherFactory _actionInfoFetcherFactory;

    public ControllerProcessor(IControllerFactory controllerFactory, IBindersFactory bindersFactory,
        IActionInfoFetcherFactory actionInfoFetcherFactory)
    {
        _controllerFactory = controllerFactory;
        _bindersFactory = bindersFactory;
        _actionInfoFetcherFactory = actionInfoFetcherFactory;
    }

    public async Task<IActionResult> CompleteAsync(HttpRequest httpRequest)
    {
        var webRequestRoute = new ControllerWebRequestPathParser(httpRequest.HttpRequestMetadata.Path).Parse();
        var controllerInternalInfo = ControllerInternalInfoFetcher.Get(webRequestRoute.OperatingPath);
        var actionInfoFetcher = _actionInfoFetcherFactory.Create(controllerInternalInfo,
            httpRequest.HttpRequestMetadata.Method, webRequestRoute.ActionName);
        var methodInternalInfo = actionInfoFetcher.Get();

        List<object?> args = new();
        BindBody(args, httpRequest, methodInternalInfo);
        BindParameters(args, webRequestRoute, methodInternalInfo);
        BindQueryParameters(args, webRequestRoute, methodInternalInfo);

        var instance = _controllerFactory.Create(controllerInternalInfo.Type);
        return await (Task<IActionResult>)methodInternalInfo.MethodInfo.Invoke(instance, args.ToArray())!;
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