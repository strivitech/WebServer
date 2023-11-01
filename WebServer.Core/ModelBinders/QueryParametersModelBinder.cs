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

    public object? Bind()
    {
        var queryParametersBindingModelType = _methodInternalInfo
            .Parameters
            .Where(pi => pi.CustomAttributes.Count() == 1
                         && pi.CustomAttributes.First().AttributeType == typeof(FromQueryAttribute))
            .Select(pi => pi.ParameterType)
            .SingleOrDefault();

        if (queryParametersBindingModelType is null)
        {
            return null;
        }

        var bindingModel = Activator.CreateInstance(queryParametersBindingModelType);
        foreach (var propertyInfo in queryParametersBindingModelType.GetProperties())
        {
            if (_queryParameters.TryGetValue(propertyInfo.Name.ToLower(), out var value) && value is not null)
            {
                propertyInfo.SetValue(bindingModel, _stringToTypeConverter.Convert(value, propertyInfo.PropertyType));
            }
        }

        return bindingModel;
    }
}