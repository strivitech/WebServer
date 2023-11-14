using System.Security.Cryptography.X509Certificates;
using WebServer.Core.Application;
using WebServer.Core.Configuration;
using WebServer.MinimalApi;

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
    .AddRequestProcessor(RequestProcessorType.Controllers); // set RequestProcessorType to MinimalApi to use MinimalApi

var app = appBuilder.Build();
// app.UseEndpoints().MapPersonEndpoints(); // configure endpoints if RequestProcessorType is set to MinimalApi

await app.RunAsync();

X509Certificate2 GetCertificate()
{
    var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
    store.Open(OpenFlags.ReadOnly);
    var x509Certificate2 = store.Certificates.Find(X509FindType.FindBySubjectName, "localhost", false)[0];
    store.Close();
    return x509Certificate2;
}