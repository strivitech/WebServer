namespace WebServer.Core.Response.Builder;

internal interface IContentSetter
{
    IFinalBuilder WithContent(object? content); 
}