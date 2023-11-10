using System.Reflection;
using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

public class ActionInternalInfo
{
    public ActionInternalInfo(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
        Name = methodInfo.Name;
        HttpMethodTypeValue = methodInfo.GetCustomAttribute<HttpVerbAttribute>()!.MethodType;
        CustomAttributeData = methodInfo.GetCustomAttributesData();
        Parameters = methodInfo.GetParameters();
    }
    
    internal ActionInternalInfo(MethodInfo methodInfo, HttpMethodType httpMethodType)
    {
        MethodInfo = methodInfo;
        Name = methodInfo.Name;
        HttpMethodTypeValue = httpMethodType;
        CustomAttributeData = methodInfo.GetCustomAttributesData();
        Parameters = methodInfo.GetParameters();
    }

    public MethodInfo MethodInfo { get; }

    public string Name { get; }
    
    public HttpMethodType HttpMethodTypeValue { get; }

    public IList<CustomAttributeData> CustomAttributeData { get; }

    public ParameterInfo[] Parameters { get; }
}