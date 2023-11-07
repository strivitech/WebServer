namespace WebServer.Core.Response.Builder;

public interface IResponseBuilder : IVersionSetter, IStatusCodeSetter, IContentSetter, IFinalBuilder
{
}