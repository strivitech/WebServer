using System.Reflection;
using WebServer.Core.Common;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ModelBinders;

internal class QueryParametersModelBinder : IQueryParametersModelBinder
{
    private readonly IDictionary<string, string?> _queryParameters;
    private readonly ActionInternalInfo _methodInternalInfo;
    private readonly IStringToTypeConverter _stringToTypeConverter;

    public QueryParametersModelBinder(IDictionary<string, string?> queryParameters,
        ActionInternalInfo methodInternalInfo, IStringToTypeConverter stringToTypeConverter)
    {
        _queryParameters = queryParameters;
        _methodInternalInfo = methodInternalInfo;
        _stringToTypeConverter = stringToTypeConverter;
    }

    public IList<object?>? Bind()
    {
        var queryParametersBindingModels = GetQueryParametersBindingModels();

        return queryParametersBindingModels.Count switch
        {
            0 => null,
            1 when CustomClass.IsCustomClass(queryParametersBindingModels.First().Type!) => BindCustomClassModel(
                queryParametersBindingModels),
            > 1 when queryParametersBindingModels.Any(x => CustomClass.IsCustomClass(x.Type!)) => throw new
                InvalidOperationException(
                    $"Action {_methodInternalInfo.Name} in controller has binding model, but it is not a primitive type"),
            _ => BindSimpleModelTypes(queryParametersBindingModels),
        };
    }

    private List<QueryParameterBindingModel> GetQueryParametersBindingModels()
    {
        var queryParametersBindingModels = _methodInternalInfo
            .Parameters
            .Where(pi => pi.CustomAttributes.ValidateModelBinderAttributes()
                         && pi.CustomAttributes.Any(cad => cad.AttributeType == typeof(FromQueryAttribute)))
            .Select(pi => new QueryParameterBindingModel { Name = pi.Name, Type = pi.ParameterType })
            .ToList();
        return queryParametersBindingModels;
    }

    private List<object?> BindSimpleModelTypes(List<QueryParameterBindingModel> queryParametersBindingModels)
    {
        var queryParameterArgs = new List<object?>();

        foreach (var queryParametersBindingModel in queryParametersBindingModels)
        {
            if (_queryParameters.TryGetValue(queryParametersBindingModel.Name!, out var value) && value is not null)
            {
                queryParameterArgs.Add(_stringToTypeConverter.Convert(value, queryParametersBindingModel.Type!));
            }
            else
            {
                queryParameterArgs.Add(default);
            }
        }

        return queryParameterArgs;
    }

    private List<object?> BindCustomClassModel(List<QueryParameterBindingModel> queryParametersBindingModels)
    {
        var queryParametersBindingModel = queryParametersBindingModels.First();
        var bindingModel = Activator.CreateInstance(queryParametersBindingModel.Type!);
        var properties = ExtractPropertiesFromType(queryParametersBindingModel.Type!);
        foreach (var propertyInfo in properties)
        {
            if (_queryParameters.TryGetValue(propertyInfo.Name.ToLower(), out var value) && value is not null)
            {
                propertyInfo.SetValue(bindingModel,
                    _stringToTypeConverter.Convert(value, propertyInfo.PropertyType));
            }
        }

        return new List<object?> { bindingModel };
    }

    private PropertyInfo[] ExtractPropertiesFromType(Type queryParametersBindingModelType)
    {
        ActionsContainer.FullNameToProperties.TryGetValue(queryParametersBindingModelType.FullName!,
            out var properties);

        return properties ?? throw new InvalidOperationException(
            $"Action {_methodInternalInfo.Name} in controller has binding model, but it is not a primitive type");
    }
}