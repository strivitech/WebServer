using WebServer.Core.ControllersContext.Actions;
using WebServer.Core.MinimalApiContext;
using WebServer.Requests;
using WebServer.Services;

namespace WebServer.MinimalApi;

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