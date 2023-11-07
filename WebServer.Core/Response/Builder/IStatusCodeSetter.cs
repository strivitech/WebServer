namespace WebServer.Core.Response.Builder;

public interface IStatusCodeSetter
{
    IContentSetter WithStatusCode(int statusCode, string statusDescription);
}