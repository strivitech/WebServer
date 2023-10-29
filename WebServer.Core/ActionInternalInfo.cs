using System.Reflection;

namespace WebServer.Core;

public class ActionInternalInfo
{
    public ActionInternalInfo(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
        Name = methodInfo.GetCustomAttribute<ActionNameAttribute>()!.Name;
        CustomAttributeData = methodInfo.GetCustomAttributesData();
        Parameters = methodInfo.GetParameters();
    }

    public MethodInfo MethodInfo { get; }

    public string Name { get; }

    public IList<CustomAttributeData> CustomAttributeData { get; }

    public ParameterInfo[] Parameters { get; }
}