using WebServer.Requests;
using WebServer.Responses;

namespace WebServer.Services;

public interface IPersonService
{
    Task<GetPersonResponse> GetAsync(GetPersonRequest request);
    
    Task<CreatePersonResponse> CreateAsync(CreatePersonRequest request);
    
    Task<UpdatePersonResponse> UpdateAsync(UpdatePersonRequest request);
    
    Task<DeletePersonResponse> DeleteAsync(DeletePersonRequest request);
}