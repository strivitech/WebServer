namespace WebServer.Core.Response.Builder;

public interface IContentSetter
{
    IFinalBuilder WithContent(object? content); 
}