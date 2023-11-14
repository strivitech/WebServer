using System.Reflection;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ModelBinders;

internal static class ModelBinderAttributesValidatorExtensions
{   
    private static readonly List<Type> AllowedModelBinderAttributes = new()
    {
        typeof(FromParametersAttribute),
        typeof(FromBodyAttribute),
        typeof(FromQueryAttribute)
    };
    
    public static bool ValidateModelBinderAttributes(this IEnumerable<CustomAttributeData> customAttributes)
    {   
        return customAttributes
            .Select(attr => attr.AttributeType)
            .Intersect(AllowedModelBinderAttributes)
            .Count() == 1;
    }
}