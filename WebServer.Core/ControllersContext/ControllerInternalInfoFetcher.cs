namespace WebServer.Core.ControllersContext;

internal static class ControllerInternalInfoFetcher
{
    public static ControllerInternalInfo Get(string controllerPath)
    {
        if (!ControllersContainer.PathToControllerInfo.TryGetValue(controllerPath,
                out var controllerInternalInfo))
        {
            throw new IndexOutOfRangeException($"Controller for path '{controllerPath}' not found");
        }

        return controllerInternalInfo;
    }
}