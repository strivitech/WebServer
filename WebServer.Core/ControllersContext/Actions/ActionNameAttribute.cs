namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class ActionNameAttribute : Attribute
{
    public ActionNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}