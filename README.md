# WebServer
## Description
WebServer is a web server framework, inspired by the robustness of asp.net core with Kestrel but significantly simplified for ease of use and understanding. This project, born out of a personal endeavor, is not intended for production environments but serves as an excellent resource for those interested in the inner workings of web server technologies.

## Project Status
WebServer serves as a valuable learning tool and a foundation for understanding web server mechanics.

## Key Features
- Asynchronous TCP-based Architecture: At its core, WebServer is built on a TCP framework, leveraging asynchronous operations to handle web requests efficiently. This ensures optimal performance and scalability, making it ideal for handling multiple requests simultaneously.

- HTTP/HTTPS Support: WebServer supports both HTTP and HTTPS protocols, providing versatility in handling web traffic. Users can easily configure their preferred protocol using the ApplicationBuilder, ensuring secure and reliable communication.

- RESTful and Ordinal Controllers: The server supports the implementation of both ordinal and RESTful controllers, giving users the flexibility to define their web services in a way that best suits their project's needs.

- MinimalAPI Integration: Embracing the concept of MinimalAPI, WebServer allows users to define endpoints through intuitive extensions. This feature simplifies the process of setting up a web server, making it accessible even to those with basic programming knowledge.

- Cached Reflection: Inside the framework, cached reflection is utilized to enhance performance. This method reduces the overhead associated with reflection, thereby improving the server's response time.

- Model Binding from JSON Requests: WebServer includes a feature for model binding, which automatically maps data from JSON requests to predefined models using appropriate attributes. This streamlines the process of handling and manipulating request data.

- Exclusive Use of Controllers or MinimalAPI: To maintain simplicity and focus, the server allows the use of either Controllers or MinimalAPI in a given instance but not both concurrently. This approach encourages a clear and manageable project structure.

## Project Setup
To get started with WebServer, follow these steps to set up the project:

1. Begin by creating an instance of AppBuilder. This is the starting point for configuring and building your web server application.

`var appBuilder = new AppBuilder();`

2. Configure your server by defining ServerConfiguration within the Configure method. Here you can set up TLS (Transport Layer Security) settings if you wish to use HTTPS.

```
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
```

3. Choose between using Controllers or MinimalApi for handling requests. This is done by setting RequestProcessorType in AddRequestProcessor.

`.AddRequestProcessor(RequestProcessorType.Controllers); // Change to MinimalApi if needed`

4. Finalize the configuration and build the application instance.

`var app = appBuilder.Build();`

5. If using MinimalApi, configure the endpoints as needed. Uncomment the following line and modify it according to your endpoint configuration.

`app.UseEndpoints().MapPersonEndpoints(); // Use this for MinimalApi`

6. Start the server asynchronously.

`await app.RunAsync();`

7. The GetCertificate method is used to retrieve a TLS certificate from the local machine's certificate store. This is necessary for enabling HTTPS in your server.

```
X509Certificate2 GetCertificate()
{
    var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
    store.Open(OpenFlags.ReadOnly);
    var x509Certificate2 = store.Certificates.Find(X509FindType.FindBySubjectName, "localhost", false)[0];
    store.Close();
    return x509Certificate2;
}
```

# Examples
## Source code

The project includes all necessary models, Data Transfer Objects (DTOs), request and response classes, services, and fake storage implementations.
You can explore these components in the source code for a detailed understanding of their structure and functionalities.

## REST Controller Example in WebServer

In WebServer, you can create RESTful APIs using controllers. Here's a concise explanation of how a PersonController works in the project, along with examples of how to use it with Postman:

```
[Rest]
[Route("/api/Person")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService = new PersonService();

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPersonRequest request)
    {
        try
        {
            return Ok(await _personService.GetAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
    {
        try
        {
            return Ok(await _personService.CreateAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePersonRequest request)
    {
        try
        {
            return Ok(await _personService.UpdateAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeletePersonRequest request)
    {
        try
        {
            return Ok(await _personService.DeleteAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
```

### PersonController Overview
- Controller Setup: The PersonController class, defined in the WebServer.Controllers namespace, handles various HTTP requests (GET, POST, PUT, DELETE, etc) for managing person data.
- Error Handling: Each method includes error handling, returning BadRequest in case of exceptions. Possible other variations: NotFound or manually created with `ObjectResult`.

### Using Postman for API Requests

1. Get Person:
- URL: `https://localhost/api/Person?Id=11c83ceb-3020-438b-9a39-693ef71d10d2`
- Method: GET
- Description: Fetches a person's details based on their ID.

2. Create Person:
- URL: https://localhost/api/Person
- Method: POST
- Description: Creates a new person record
- Body (application/json):

```
{
  "Name": "John Doe",
  "Age": 30,
  "Hobbies": ["Reading", "Hiking", "Photography"],
  "CurrentLocation": {
    "Country": "USA",
    "City": "New York"
  },
  "Locations": [
    {
      "Country": "USA",
      "City": "Los Angeles"
    },
    {
      "Country": "UK",
      "City": "London"
    }
  ]
}
```

3. Update Person:
- URL: https://localhost/api/Person
- Method: PUT
- Description: Updates the details of an existing person
- Body (application/json):

```
{
  "Id": "11c83ceb-3020-438b-9a39-693ef71d10d2",
  "Name": "John Doe",
  "Age": 35,
  "Hobbies": ["Reading", "Hiking", "Photography"],
  "CurrentLocation": {
    "Country": "USA",
    "City": "New York"
  },
  "Locations": [
    {
      "Country": "USA",
      "City": "Los Angeles"
    },
    {
      "Country": "UK",
      "City": "London"
    }
  ]
}
```

4. Delete Person:
- URL: https://localhost/api/Person
- Method: DELETE
- Description: Deletes a person's record based on their ID
- Body (application/json):

```
{
  "Id": "11c83ceb-3020-438b-9a39-693ef71d10d2"
}
```

## Ordinal Controller Example in WebServer

In WebServer, you can also set up ordinal controllers for handling specific types of requests. Here's a brief explanation of the InfoOrdinalController and how it functions:

```
[Route("/api/Information")]
public class InfoOrdinalController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetInfo()
    {
        return Task.FromResult(Ok(new
        {
            ServerInformation = new
            {
                ServerName = "WebServer",
                ServerVersion = "1.0.0",
                ServerPort = 443
            },
        }));
    }
}
```

### InfoOrdinalController Overview

- Controller Definition: The InfoOrdinalController is an ordinal controller, meaning it handles non-RESTful routes and actions. It's defined with the route "/api/Information".
- GET Method Implementation: This controller includes a single GET method, GetInfo, designed to provide server information.

### Using Postman to Access InfoOrdinalController

- URL: https://localhost/api/Information/GetInfo
- Method: GET
- Description: To fetch server information such as its name, version, and port.

## MinimalAPI Example in WebServer

WebServer provides the flexibility to use MinimalAPI for creating endpoint routes. Here's a description of how the Endpoints class is used to map routes for a Person entity using the MinimalAPI approach:

```
public static class Endpoints
{
    public static IEndpointsBuilder MapPersonEndpoints(this IEndpointsBuilder builder)
    {
        builder.MapGet("/api/Person", async ([FromQuery] GetPersonRequest request) =>
            {
                var personService = new PersonService();
                try
                {
                    return Results.Ok(await personService.GetAsync(request));
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            })
            .MapPost("/api/Person", async ([FromBody] CreatePersonRequest request) =>
            {
                var personService = new PersonService();
                try
                {
                    return Results.Ok(await personService.CreateAsync(request));
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            })
            .MapPut("/api/Person", async ([FromBody] UpdatePersonRequest request) =>
            {
                var personService = new PersonService();
                try
                {
                    return Results.Ok(await personService.UpdateAsync(request));
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            })
            .MapDelete("/api/Person", async ([FromBody] DeletePersonRequest request) =>
            {
                var personService = new PersonService();
                try
                {
                    return Results.Ok(await personService.DeleteAsync(request));
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            });

        return builder;
    }
}
```

Endpoints Class Overview
- Purpose: The Endpoints class serves as a centralized location for defining HTTP routes and their corresponding actions for Person related operations.
- Extension Method: MapPersonEndpoints is an extension method for IEndpointsBuilder, which is used to define routes for various HTTP methods (GET, POST, PUT, DELETE).

### Defining Routes and Actions

GET /api/Person:
- Fetches a person's details based on the provided query parameters.
- Returns Results.Ok on success, and Results.BadRequest on failure.

POST /api/Person:
- Creates a new person record.
- Returns Results.Ok for successful creation, and Results.BadRequest for errors.

PUT /api/Person:
- Updates an existing person's details.
- Responds with Results.Ok on successful update, or Results.BadRequest if an error occurs.

DELETE /api/Person:
- Deletes a person based on the provided ID.
- Returns Results.Ok for successful deletion, and Results.BadRequest in case of an error.
