using System.Reflection;
using HttpMethod = WebServer.Core.Common.HttpMethod;

namespace WebServer.Core.ControllersContext.Actions;

public class ActionInternalInfo
{
    public ActionInternalInfo(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
        Name = methodInfo.GetCustomAttribute<ActionNameAttribute>()!.Name;
        HttpMethodValue = methodInfo.GetCustomAttribute<HttpVerbAttribute>()!.Method;
        CustomAttributeData = methodInfo.GetCustomAttributesData();
        Parameters = methodInfo.GetParameters();
    }

    public MethodInfo MethodInfo { get; }

    public string Name { get; }
    
    public HttpMethod HttpMethodValue { get; }

    public IList<CustomAttributeData> CustomAttributeData { get; }

    public ParameterInfo[] Parameters { get; }
}