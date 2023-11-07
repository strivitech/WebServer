namespace WebServer.Core.Response.Builder;

public interface IVersionSetter
{
    IStatusCodeSetter UseVersion(string httpVersion);
}