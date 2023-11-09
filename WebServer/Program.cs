using System.Security.Cryptography.X509Certificates;
using WebServer.Core;
using WebServer.Core.Configuration;
using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.ModelBinders;
using WebServer.Core.Request;
using WebServer.Core.Request.Headers;
using WebServer.Core.Response;
using WebServer.Core.Response.Builder;
using WebServer.Core.Transport;

var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
store.Open(OpenFlags.ReadOnly);
var certificate = store.Certificates.Find(X509FindType.FindBySubjectName, "localhost", false)[0];
store.Close();

var serverConfiguration = new ServerConfiguration
{
    TlsSettings = new TlsSettings
    {
        UseTls = true,
        Certificate = new TlsCertificate
        {
            X509Certificate2 = certificate
        }
    }
};

var controllerApiHandler = new HttpRequestHandler(new HttpRequestReader(new HttpRequestHeadersValidator(
    new ContentTypeValidator(),
    new ContentLengthValidator())),
    new HttpResponseWriter(new ResponseBuilder()),
    new ControllerProcessor(new ControllerFactory(), new BindersFactory(), new ActionInfoFetcherFactory()));

var server = new TcpBasedServer(controllerApiHandler, serverConfiguration);
await server.StartAsync();