using System.Security.Cryptography.X509Certificates;
using WebServer;
using WebServer.Core.Application;
using WebServer.Core.Configuration;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.MinimalApiContext;

var appBuilder = new AppBuilder()
    .Configure(() => new ServerConfiguration
    {
        TlsSettings = new TlsSettings
        {
            UseTls = true,
            Certificate = new TlsCertificate
            {
                X509Certificate2 = GetCertificate()
            }
        }
    })
    .AddRequestProcessor(RequestProcessorType.MinimalApi);

var app = appBuilder.Build();

app.UseEndpoints()
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

await app.RunAsync();

X509Certificate2 GetCertificate()
{
    var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
    store.Open(OpenFlags.ReadOnly);
    var x509Certificate2 = store.Certificates.Find(X509FindType.FindBySubjectName, "localhost", false)[0];
    store.Close();
    return x509Certificate2;
}