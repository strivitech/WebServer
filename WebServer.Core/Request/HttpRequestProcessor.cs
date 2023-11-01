using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.ModelBinders;

namespace WebServer.Core.Request;

public class HttpRequestProcessor
{
    private readonly IControllerFactory _controllerFactory;
    private readonly IBindersFactory _bindersFactory;

    public HttpRequestProcessor(IControllerFactory controllerFactory, IBindersFactory bindersFactory)
    {
        _controllerFactory = controllerFactory;
        _bindersFactory = bindersFactory;
    }

    public async Task<IActionResult> CompleteAsync(HttpRequest httpRequest)
    {
        var webRequestRoute = new WebRequestPathParser(httpRequest.HttpRequestMetadata.Path).Parse();
        var controllerInternalInfo = ControllerInternalInfoFetcher.Get(webRequestRoute.ControllerRoute);
        var methodInternalInfo = ActionInternalInfoFetcher.Get(httpRequest.HttpRequestMetadata.Method,
            controllerInternalInfo.Type.Name,
            webRequestRoute.ActionName);

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