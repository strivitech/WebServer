namespace WebServer.Core.Application;

internal interface IAppBuilder : IFinalAppBuilder, IConfigurationSetup, IRequestProcessorSetup;