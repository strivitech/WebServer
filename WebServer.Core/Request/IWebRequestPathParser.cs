namespace WebServer.Core.Request;

internal interface IWebRequestPathParser
{
    WebRequestRoute Parse();
}