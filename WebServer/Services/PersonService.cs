using WebServer.Models;
using WebServer.Requests;
using WebServer.Responses;
using WebServer.Storage;

namespace WebServer.Services;

internal class PersonService : IPersonService
{
    public Task<GetPersonResponse> GetAsync(GetPersonRequest request)
    {
        var person = PeopleStorage.People[request.Id];
        
        return Task.FromResult(new GetPersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age,
            Hobbies = person.Hobbies,
            CurrentLocation = person.CurrentLocation,
            Locations = person.Locations
        });
    }

    public Task<CreatePersonResponse> CreateAsync(CreatePersonRequest request)
    {   
        var personId = Guid.NewGuid().ToString();
        PeopleStorage.People.Add(personId, new Person
        {
            Id = personId,
            Age = request.Age,
            Name = request.Name,
            Hobbies = request.Hobbies,
            CurrentLocation = request.CurrentLocation,
            Locations = request.Locations
        });
        
        return Task.FromResult(new CreatePersonResponse
        {
            Id = personId
        });
    }

    public Task<UpdatePersonResponse> UpdateAsync(UpdatePersonRequest request)
    {
        var person = PeopleStorage.People[request.Id];
        person.Age = request.Age;
        person.Name = request.Name;
        person.Hobbies = request.Hobbies;
        person.CurrentLocation = request.CurrentLocation;
        person.Locations = request.Locations;

        return Task.FromResult(new UpdatePersonResponse()
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age,
            Hobbies = person.Hobbies,
            CurrentLocation = person.CurrentLocation,
            Locations = person.Locations
        });
    }

    public Task<DeletePersonResponse> DeleteAsync(DeletePersonRequest request)
    {
        PeopleStorage.People.Remove(request.Id);
        
        return Task.FromResult(new DeletePersonResponse()
        {
            Id = request.Id,
            IsDeleted = true
        });
    }
}