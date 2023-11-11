using WebServer.Core.Configuration;
using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.MinimalApiContext;
using WebServer.Core.ModelBinders;

namespace WebServer.Core.Application;

public class AppBuilder : IAppBuilder
{
    private readonly App _app = new();

    public IFinalAppBuilder AddRequestProcessor(RequestProcessorType requestProcessorType)
    {
        _app.HttpRequestProcessor = requestProcessorType switch
        {
            RequestProcessorType.MinimalApi => new MinimalApiProcessor(new BindersFactory()),
            RequestProcessorType.Controllers => new ControllerProcessor(new ControllerFactory(), new BindersFactory(),
                new ActionInfoFetcherFactory()),
            _ => throw new ArgumentOutOfRangeException(nameof(requestProcessorType), requestProcessorType,
                "Unknown request processor type")
        };

        return this;
    }

    public IRequestProcessorSetup Configure(Func<ServerConfiguration> configuration)
    {
        _app.ServerConfiguration = configuration();
        return this;
    }
    
    public App Build()
    {
        return _app;
    }
}