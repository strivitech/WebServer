using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WebServer.Core.Response.Builder;

public partial class ResponseBuilder : IResponseBuilder
{
    private string _httpVersion = null!;
    private int _statusCode;
    private string _statusDescription = null!;
    private object? _content = new { };

    public IStatusCodeSetter UseVersion(string httpVersion)
    {
        ValidateHttpVersion(httpVersion);
        _httpVersion = httpVersion;
        return this;
    }

    public IContentSetter WithStatusCode(int statusCode, string statusDescription)
    {
        ValidateStatusCode(statusCode);
        ValidateStatusDescription(statusDescription);
        _statusCode = statusCode;
        _statusDescription = statusDescription;
        return this;
    }

    public IFinalBuilder WithContent(object? content)
    {
        _content = content;
        return this;
    }

    public string Build()
    {
        var responseBody = JsonSerializer.Serialize(_content);
        var contentLength = Encoding.UTF8.GetByteCount(responseBody);
        var responseBuilder = new StringBuilder();

        responseBuilder.AppendLine($"{_httpVersion} {_statusCode} {_statusDescription}");
        responseBuilder.AppendLine("Content-Type: application/json; charset=UTF-8");
        responseBuilder.AppendLine($"Content-Length: {contentLength}");
        responseBuilder.AppendLine();
        responseBuilder.Append(responseBody);

        return responseBuilder.ToString();
    }
    
    private static void ValidateHttpVersion(string httpVersion)
    {
        if (!HttpVersionRegex().IsMatch(httpVersion))
        {
            throw new ArgumentException("Invalid HTTP version.");
        }
    }
    
    private static void ValidateStatusDescription(string statusDescription)
    {
        if (string.IsNullOrEmpty(statusDescription))
        {
            throw new ArgumentException("Invalid HTTP status description.");
        }
    }

    private static void ValidateStatusCode(int statusCode)
    {
        if (statusCode is < 100 or > 599)
        {
            throw new ArgumentException("Invalid HTTP status code.");
        }
    }

    [GeneratedRegex(@"^HTTP\/\d\.\d$")]
    private static partial Regex HttpVersionRegex();
}