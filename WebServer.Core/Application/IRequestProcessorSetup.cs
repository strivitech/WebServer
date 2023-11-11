namespace WebServer.Core.Application;

public interface IRequestProcessorSetup
{
    IFinalAppBuilder AddRequestProcessor(RequestProcessorType requestProcessorType);
}