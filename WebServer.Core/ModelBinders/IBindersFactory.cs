using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ModelBinders;

public interface IBindersFactory
{
    IBodyModelBinder CreateBodyModelBinder(string? body, ActionInternalInfo methodInternalInfo);

    IParametersModelBinder CreateParametersModelBinder(List<string> urlParameters,
        ActionInternalInfo methodInternalInfo);

    IQueryParametersModelBinder CreateQueryParametersModelBinder(IDictionary<string, string?> queryParameters,
        ActionInternalInfo methodInternalInfo);
}