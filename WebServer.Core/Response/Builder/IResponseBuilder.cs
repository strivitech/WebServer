namespace WebServer.Core.Response.Builder;

internal interface IResponseBuilder : IVersionSetter, IStatusCodeSetter, IContentSetter, IFinalBuilder;