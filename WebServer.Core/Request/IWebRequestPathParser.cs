namespace WebServer.Core.Request;

public interface IWebRequestPathParser
{
    WebRequestRoute Parse();
}