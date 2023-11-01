using WebServer.Core.Common;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ModelBinders;

public class BindersFactory : IBindersFactory
{
    public IBodyModelBinder CreateBodyModelBinder(string? body, ActionInternalInfo methodInternalInfo)
    {
        return new BodyModelBinder(body, methodInternalInfo);
    }

    public IParametersModelBinder CreateParametersModelBinder(List<string> urlParameters,
        ActionInternalInfo methodInternalInfo)
    {
        return new ParametersModelBinder(urlParameters, methodInternalInfo, new StringToTypeConverter());
    }

    public IQueryParametersModelBinder CreateQueryParametersModelBinder(IDictionary<string, string?> queryParameters,
        ActionInternalInfo methodInternalInfo)
    {
        return new QueryParametersModelBinder(queryParameters, methodInternalInfo, new StringToTypeConverter());
    }
}