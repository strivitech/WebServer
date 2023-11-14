using WebServer.Core.Common;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ModelBinders;

internal class ParametersModelBinder : IParametersModelBinder
{
    private readonly IList<string> _urlParameters;
    private readonly ActionInternalInfo _methodInternalInfo;
    private readonly IStringToTypeConverter _stringToTypeConverter;

    public ParametersModelBinder(IList<string> urlParameters, ActionInternalInfo methodInternalInfo,
        IStringToTypeConverter stringToTypeConverter)
    {
        _urlParameters = urlParameters;
        _methodInternalInfo = methodInternalInfo;
        _stringToTypeConverter = stringToTypeConverter;
    }

    public IList<object?>? Bind()
    {
        var parametersBindingModelTypes = GetParametersBindingModelTypes();

        if (parametersBindingModelTypes.Count == 0)
        {
            return null;
        }

        if (parametersBindingModelTypes.Any(CustomClass.IsCustomClass))
        {
            throw new InvalidOperationException(
                $"Action {_methodInternalInfo.Name} in controller has binding model, but it is not a primitive type");
        }

        if (parametersBindingModelTypes.Count != _urlParameters.Count)
        {
            throw new InvalidOperationException(
                $"Action {_methodInternalInfo.Name} in controller has {_urlParameters.Count} url parameters, but binding model has {parametersBindingModelTypes.Count}");
        }

        return parametersBindingModelTypes
            .Select((t, i) => (object?)_stringToTypeConverter.Convert(_urlParameters[i], t))
            .ToList();
    }

    private List<Type> GetParametersBindingModelTypes()
    {
        var parametersBindingModelTypes = _methodInternalInfo
            .Parameters
            .Where(pi => pi.CustomAttributes.ValidateModelBinderAttributes()
                         && pi.CustomAttributes.Any(cad => cad.AttributeType == typeof(FromParametersAttribute)))
            .Select(pi => pi.ParameterType)
            .ToList();
        return parametersBindingModelTypes;
    }
}