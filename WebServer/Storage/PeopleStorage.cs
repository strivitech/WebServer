using WebServer.Models;

namespace WebServer.Storage;

internal static class PeopleStorage
{
    public static Dictionary<string, Person> People { get; } = new();
}