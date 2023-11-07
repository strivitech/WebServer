// using System.Diagnostics.CodeAnalysis;
// using System.Net;
// using System.Net.Quic;
// using System.Net.Security;
// using System.Security.Cryptography.X509Certificates;
// using System.Text;
//
// namespace WebServer.Core;
//
// [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
// public class QuicBasedServer : ITransportProtocolBasedServer
// {
//     private readonly IApiHandler _apiHandler;
//     private readonly X509Certificate2 _certificate; 
//
//     public QuicBasedServer(IApiHandler apiHandler)
//     {
//         _apiHandler = apiHandler;
//         
//         var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
//         store.Open(OpenFlags.ReadOnly);
//         _certificate = store.Certificates.Find(X509FindType.FindBySubjectName, "localhost", false)[0];
//         store.Close();
//     }
//
//     public async Task StartAsync()
//     {
//         var sslOptions = new SslServerAuthenticationOptions
//         {
//             ServerCertificate = _certificate,
//             // ClientCertificateRequired = false,
//             // EnabledSslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12,
//             ApplicationProtocols = new List<SslApplicationProtocol> { SslApplicationProtocol.Http3},
//         };
//         
//         var serverConnectionOptions = new QuicServerConnectionOptions
//         {
//             DefaultStreamErrorCode = 0x0A,
//             DefaultCloseErrorCode = 0x0B,
//             ServerAuthenticationOptions = sslOptions
//         };
//         
//         Console.WriteLine(QuicListener.IsSupported ? "QuicListener is supported" : "QuicListener is NOT supported");
//         
//         var listener = await QuicListener.ListenAsync(new QuicListenerOptions
//         {
//             ListenEndPoint = new IPEndPoint(IPAddress.Any, 443),
//             ApplicationProtocols = new List<SslApplicationProtocol> { SslApplicationProtocol.Http3 },
//             ConnectionOptionsCallback = (_, _, _) => ValueTask.FromResult(serverConnectionOptions)
//         });
//         
//         Console.WriteLine($"Listening on {listener.LocalEndPoint.Address}:{listener.LocalEndPoint.Port}");
//         
//         while (true)
//         {
//             try
//             {
//                 QuicConnection client = await listener.AcceptConnectionAsync();
//                 _ = HandleClientAsync(client);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine(ex);
//             }
//         }
//     }
//
//     private async Task HandleClientAsync(QuicConnection client)
//     {
//         try
//         {
//             await using (client)
//             {
//                 // TODO: need to implement a way to decode QPACK compressed data
//                 // Until then we can't read any data from the stream
//                 await using QuicStream stream = await client.AcceptInboundStreamAsync();
//                 byte[] buffer = new byte[1024]; 
//                 int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
//                 for (int i = 0; i < bytesRead; i++)
//                 {
//                     Console.WriteLine(buffer[i]); // prints bytes in decimal
//                     Console.WriteLine(buffer[i].ToString("X2")); // prints bytes in hexadecimal
//                 }
//                 var str = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//                 Console.WriteLine(str);
//                 
//                 // using var reader = new StreamReader(stream, leaveOpen: true);
//                 // string? line = await reader.ReadLineAsync();
//                 // while (!string.IsNullOrWhiteSpace(line))
//                 // {
//                 //     Console.WriteLine(line);
//                 //     line = await reader.ReadLineAsync();
//                 // }
//                 // await _apiHandler.HandleAsync(stream);
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex);
//         }
//     }
// }