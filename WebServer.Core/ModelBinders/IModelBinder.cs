namespace WebServer.Core.ModelBinders;

public interface IModelBinder<out T>
{
    T? Bind();
}