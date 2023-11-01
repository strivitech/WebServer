using System.Text.Json;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ModelBinders;

public class BodyModelBinder : IBodyModelBinder
{
    private readonly string? _body;
    private readonly ActionInternalInfo _methodInternalInfo;

    public BodyModelBinder(string? body, ActionInternalInfo methodInternalInfo)
    {
        _body = body;
        _methodInternalInfo = methodInternalInfo;
    }

    public object? Bind()
    {
        var bodyBindingModelType = _methodInternalInfo
            .Parameters
            .Where(pi => pi.CustomAttributes.Count() == 1
                         && pi.CustomAttributes.First().AttributeType == typeof(FromBodyAttribute))
            .Select(pi => pi.ParameterType)
            .SingleOrDefault();

        if (string.IsNullOrEmpty(_body) && bodyBindingModelType is not null)
        {
            throw new InvalidOperationException(
                $"Action {_methodInternalInfo.Name} in controller has binding model, but request body is empty");
        }

        if (!string.IsNullOrEmpty(_body))
        {
            if (bodyBindingModelType is null)
            {
                throw new InvalidOperationException(
                    $"Action {_methodInternalInfo.Name} in controller has no binding model, but request body is not empty");
            }

            var bindingModel = JsonSerializer.Deserialize(_body, bodyBindingModelType,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return bindingModel;
        }

        return null;
    }
}