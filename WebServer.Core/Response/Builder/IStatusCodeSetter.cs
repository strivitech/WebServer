namespace WebServer.Core.Response.Builder;

internal interface IStatusCodeSetter
{
    IContentSetter WithStatusCode(int statusCode, string statusDescription);
}