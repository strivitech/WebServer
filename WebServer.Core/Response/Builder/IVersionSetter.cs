namespace WebServer.Core.Response.Builder;

internal interface IVersionSetter
{
    IStatusCodeSetter UseVersion(string httpVersion);
}