using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace VaquinhaOnline.Application.Features.Users;

public interface IUserService
{   
    Task<Result<Guid>> CreateUser(UserCreateDto User, IFormFile ProfilePhoto,CancellationToken cancellationToken);
    Task<Result<string>> Login (LoginRequestDto loginRequest);
    Task<IdentityResult> DeleteUser(Guid Id, CancellationToken cancellationToken);  
    Task<Result> UpdateUser(UserUpdateDto User, IFormFile profilePhoto,CancellationToken cancellationToken);
    Task<Result<UserGetDto>> GetUserById(Guid Id, CancellationToken cancellationToken);
    Task<ResultPaginated<List<UserGetDto>>> GetAllUsers(int PageNumber, int PageSize, CancellationToken cancellationToken);
    Task<Result<Guid>> GetCurrentUser(CancellationToken cancellationToken);
}