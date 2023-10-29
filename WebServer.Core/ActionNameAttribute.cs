namespace WebServer.Core;

[AttributeUsage(AttributeTargets.Method)]
public class ActionNameAttribute : Attribute
{
    public ActionNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}