using WebServer.Core.Common;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ModelBinders;

public class QueryParametersModelBinder : IQueryParametersModelBinder
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
        var queryParametersBindingModels = _methodInternalInfo
            .Parameters
            .Where(pi => pi.CustomAttributes.Count() == 1
                         && pi.CustomAttributes.First().AttributeType == typeof(FromQueryAttribute))
            .Select(pi => new { Name = pi.Name, Type = pi.ParameterType })
            .ToList();

        if (queryParametersBindingModels.Count == 0)
        {
            return null;
        }

        if (queryParametersBindingModels.Count == 1 &&
            CustomClass.IsCustomClass(queryParametersBindingModels.First().Type))
        {
            var queryParametersBindingModelType = queryParametersBindingModels.First();
            var bindingModel = Activator.CreateInstance(queryParametersBindingModelType.Type);
            foreach (var propertyInfo in queryParametersBindingModelType.Type.GetProperties())
            {
                if (_queryParameters.TryGetValue(propertyInfo.Name.ToLower(), out var value) && value is not null)
                {
                    propertyInfo.SetValue(bindingModel,
                        _stringToTypeConverter.Convert(value, propertyInfo.PropertyType));
                }
            }

            return new List<object?> { bindingModel };
        }

        if (queryParametersBindingModels.Count > 1 &&
            queryParametersBindingModels.Any(x => CustomClass.IsCustomClass(x.Type)))
        {
            throw new InvalidOperationException(
                $"Action {_methodInternalInfo.Name} in controller has binding model, but it is not a primitive type");
        }

        var queryParameterArgs = new List<object?>();

        foreach (var queryParametersBindingModel in queryParametersBindingModels)
        {
            if (_queryParameters.TryGetValue(queryParametersBindingModel.Name!, out var value) && value is not null)
            {
                queryParameterArgs.Add(_stringToTypeConverter.Convert(value, queryParametersBindingModel.Type));
            }
            else
            {
                queryParameterArgs.Add(default);
            }
        }

        return queryParameterArgs;
    }
}