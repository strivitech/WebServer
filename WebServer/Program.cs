using System.Security.Cryptography.X509Certificates;
using WebServer;
using WebServer.Core;
using WebServer.Core.Application;
using WebServer.Core.Configuration;
using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.MinimalApiContext;
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

// var requestHandler = new HttpRequestHandler(new HttpRequestReader(new HttpRequestHeadersValidator(
//     new ContentTypeValidator(),
//     new ContentLengthValidator())),
//     new HttpResponseWriter(new ResponseBuilder()),
//     new ControllerProcessor(new ControllerFactory(), new BindersFactory(), new ActionInfoFetcherFactory()));

IAppBuilder appBuilder = new AppBuilder();
appBuilder.UseEndpoints()
    .MapGet("/api/MyController", () => Results.Ok("Hello World!"))
    .MapPost("/api/MyController", async ([FromBody] Person person,
        [FromParameters] string country,
        [FromParameters] string city,
        [FromParameters] string street,
        [FromQuery] Car car) => await Results.Ok(new
    {
        Person = person,
        Address = new
        {
            Country = country,
            City = city,
            Street = street
        },
        Car = car
    }));

var requestHandler = new HttpRequestHandler(new HttpRequestReader(new HttpRequestHeadersValidator(
        new ContentTypeValidator(),
        new ContentLengthValidator())),
    new HttpResponseWriter(new ResponseBuilder()),
    new MinimalApiProcessor(new BindersFactory()));

var server = new TcpBasedServer(requestHandler, serverConfiguration);
await server.StartAsync();