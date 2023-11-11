using WebServer.Core.Configuration;

namespace WebServer.Core.Application;

internal interface IConfigurationSetup
{
    IRequestProcessorSetup Configure(Func<ServerConfiguration> configuration);
}