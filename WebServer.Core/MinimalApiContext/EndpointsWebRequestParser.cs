using System.Web;
using WebServer.Core.Common;
using WebServer.Core.Request;

namespace WebServer.Core.MinimalApiContext;

public class EndpointsWebRequestParser : IWebRequestPathParser
{
    private readonly string _fullRequestPath;
    private readonly HttpMethodType _httpMethodType;

    public EndpointsWebRequestParser(string fullRequestPath, HttpMethodType httpMethodType)
    {
        _fullRequestPath = fullRequestPath ?? throw new ArgumentNullException(nameof(fullRequestPath));
        _httpMethodType = httpMethodType;
    }

    public WebRequestRoute Parse()
    {
        var (path, query) = SplitPathAndQuery(_fullRequestPath);

        var pathSegments = GetPathSegments(path);
        var operatingPath = DetermineOperatingPath(pathSegments, out var operatingPathSegments);
        var remainingUrlSegments = GetRemainingSegments(pathSegments, operatingPathSegments);
        var queryParams = ExtractQueryParameters(query);
        
        return new WebRequestRoute(operatingPath, null, remainingUrlSegments, queryParams);
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

    private string DetermineOperatingPath(List<string> segments, out int segmentsTaken)
    {
        string potentialOperatingPath = string.Empty;
        for (int i = 0; i < segments.Count; i++)
        {
            potentialOperatingPath += "/" + segments[i];
            if (EndpointsContainer.Source.Endpoints.ContainsKey((potentialOperatingPath, _httpMethodType)))
            {
                segmentsTaken = i + 1;
                return potentialOperatingPath;
            }
        }

        throw new IndexOutOfRangeException($"Controller for path '{_fullRequestPath}' not found");
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