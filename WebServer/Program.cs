using WebServer.Core;

var controllerApiHandler = new ControllerApiHandler(new RequestReader(), new ResponseWriter(),
    new ControllerActionInvoker(new ControllerFactory()));
var server = new Server(controllerApiHandler);
await server.RunAsync();