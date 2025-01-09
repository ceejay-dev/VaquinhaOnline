namespace VaquinhaOnline.Application.Features.Users;

public interface IUserService
{   
    Task<Result<Guid>> CreateUser(UserCreateDto User, CancellationToken cancellationToken);
    Task<Result<string>> Login (LoginRequestDto loginRequest);
    Task<Result> DeleteUser(Guid Id, CancellationToken cancellationToken);  
    Task<Result> UpdateUser(UserUpdateDto User, CancellationToken cancellationToken);
    Task<Result<UserGetDto>> GetUserById(Guid Id, CancellationToken cancellationToken);
    Task<ResultPaginated<List<UserGetDto>>> GetAllUsers(int PageNumber, int PageSize, CancellationToken cancellationToken);
}