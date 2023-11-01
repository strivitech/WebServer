using WebServer.Core;
using WebServer.Core.ControllersContext;
using WebServer.Core.ModelBinders;
using WebServer.Core.Request;
using WebServer.Core.Response;

var controllerApiHandler = new ControllerApiHandler(new HttpRequestReader(), new HttpResponseWriter(),
    new HttpRequestProcessor(new ControllerFactory(), new BindersFactory()));
var server = new Server(controllerApiHandler);
await server.RunAsync();