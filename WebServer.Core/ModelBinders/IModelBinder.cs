namespace WebServer.Core.ModelBinders;

internal interface IModelBinder<out T>
{
    T? Bind();
}