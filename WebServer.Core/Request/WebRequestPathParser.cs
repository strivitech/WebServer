using System.Web;
using WebServer.Core.ControllersContext;

namespace WebServer.Core.Request;

public class WebRequestPathParser
{
    private readonly string _fullRequestPath;

    public WebRequestPathParser(string fullRequestPath)
    {
        _fullRequestPath = fullRequestPath ?? throw new ArgumentNullException(nameof(fullRequestPath));
    }

    public WebRequestRoute Parse()
    {
        var (path, query) = SplitPathAndQuery(_fullRequestPath);

        var pathSegments = GetPathSegments(path);
        var controllerPath = DetermineControllerPath(pathSegments, out var controllersSegments);
        var action = DetermineAction(pathSegments, controllersSegments);

        var remainingUrlSegments = GetRemainingSegments(pathSegments, controllersSegments + 1);
        var queryParams = ExtractQueryParameters(query);

        return new WebRequestRoute(controllerPath, action, remainingUrlSegments, queryParams);
    }

    private static (string path, string query) SplitPathAndQuery(string requestPath)
    {
        string[] parts = requestPath.Split('?');
        return (parts[0], parts.Length > 1 ? parts[1] : string.Empty);
    }

    private static List<string> GetPathSegments(string path)
    {
        return path.Split('/').Skip(1).ToList();
    }

    private string DetermineControllerPath(List<string> segments, out int segmentsTaken)
    {
        string potentialController = string.Empty;
        for (int i = 0; i < segments.Count; i++)
        {
            potentialController += "/" + segments[i];
            if (ControllersContainer.PathToControllerInfo.ContainsKey(potentialController))
            {
                segmentsTaken = i + 1;
                return potentialController;
            }
        }

        throw new IndexOutOfRangeException($"Controller for path '{_fullRequestPath}' not found");
    }


    private string DetermineAction(List<string> segments, int controllerSegmentsTaken)
    {
        if (segments.Count > controllerSegmentsTaken)
        {
            return segments[controllerSegmentsTaken];
        }

        throw new IndexOutOfRangeException($"Action for path '{_fullRequestPath}' not found");
    }

    private static List<string> GetRemainingSegments(List<string> segments, int segmentsTaken)
    {
        return segments.Skip(segmentsTaken).Select(x => x.ToLower()).ToList();
    }

    private static Dictionary<string, string?> ExtractQueryParameters(string queryString)
    {
        var nameValueCollection = HttpUtility.ParseQueryString(queryString);

        return nameValueCollection.AllKeys
            .Where(key => key is not null)
            .ToDictionary(key => key!.ToLower(), key => nameValueCollection[key]);
    }
}