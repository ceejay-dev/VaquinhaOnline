using Microsoft.AspNetCore.Identity;
using VaquinhaOnline.Infrastructure.IdentityModels;

namespace VaquinhaOnline.Application.Features.Users;

public class UserService(UserManager<AppUser> userManager, IValidator<UserCreateDto> validator,
    SignInManager<AppUser> signInManager, IConfiguration configuration,
    IJwtService jwtService, IValidator<LoginRequestDto> validatorr) : IUserService
{
    private readonly UserManager<AppUser> userManager = userManager;
    private readonly SignInManager<AppUser> signInManager = signInManager;
    private readonly IConfiguration configuration = configuration;
    private readonly IJwtService jwtService = jwtService;
    private readonly IValidator<UserCreateDto> validator = validator;
    private readonly IValidator<LoginRequestDto> validatorr = validatorr;

    public async Task<Result<Guid>> CreateUser(UserCreateDto User, CancellationToken cancellationToken)
    {
        var validateCommand = validator.Validate(User);

        if (!validateCommand.IsValid)
        {
            return Result.Failure<Guid>(Error.Invalid("Error.Validation", validateCommand.ToString()));
        }

        var existingEmail = await userManager.FindByEmailAsync(User.Email);
        //remember to verify also the phone number 

        if (existingEmail != null)
        {
            return Result.Failure<Guid>(Error.Conflit("Error.Conflict", "The email has already been registred."));
        }

        var user = new AppUser
        {
            Name = User.Name,
            CreationDate = DateTime.UtcNow,
            Email = User.Email,
            Type = User.Type,
            NormalizedEmail = User.Email.ToUpper(),
            UserName = User.PhoneNumber,
            PhoneNumber = User.PhoneNumber,
            NormalizedUserName = User.PhoneNumber.ToUpper(),
        };

        var identityReuslt = await userManager.CreateAsync(user, User.Password);

        if (!identityReuslt.Succeeded)
        {
            return Result.Failure<Guid>(Error.Failure("Error.CreateUser", identityReuslt.ToString()));
        }

        return Result.Succeed(user.Id);
    }

    public async Task<Result> DeleteUser(Guid Id, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(Id.ToString());

        if (user == null)
        {
            return Result.Failure(Error.NotFound("NotFound", "The user was not found."));
        }

        var result = await userManager.DeleteAsync(user);
        return result.Adapt<Result>();
    }

    public async Task<ResultPaginated<List<UserGetDto>>> GetAllUsers(int PageNumber, int PageSize, CancellationToken cancellationToken)
    {
        var userQuery = userManager.Users;

        var totalCount = userQuery.Count();

        if (totalCount == 0)
        {
            return Result.Failure<UserGetDto>(
                error: Error.NotFound("NotFound", "No user found."),
                totalCount: totalCount,
                currentPage: PageNumber,
                pageSize: PageSize);
        }

        var pageNumber = PageNumber < 1 ? 1 : PageNumber;
        var pageSize = PageSize < 1 ? 10 : PageSize;

        var users = await userQuery
            .OrderBy(user => user.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var userDtos = users.Adapt<List<UserGetDto>>();

        return ResultPaginated<List<UserGetDto>>.Success(
            values: userDtos,
            totalCount: totalCount,
            currentPage: pageNumber,
            pageSize: pageSize);
    }

    public async Task<Result<UserGetDto>> GetUserById(Guid Id, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(Id.ToString());

        if (user == null)
        {
            return Result.Failure<UserGetDto>(Error.NotFound("NotFound", "User not found"));
        }

        var dto = user.Adapt<UserGetDto>();

        return Result.Succeed(dto);
    }

    public async Task<Result<string>> Login(LoginRequestDto loginRequest)
    {
        // Validar o comando
        var validateCommand = validatorr.Validate(loginRequest);

        if (!validateCommand.IsValid)
        {
            return Result.Failure<string>(Error.Invalid("Error.Validation", validateCommand.ToString()));
        }

        // Encontrar o usuário pelo email
        var user = await userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            return Result.Failure<string>(Error.Failure("Error.Unauthorized", "Invalid credentials"));
        }

        // Usar o SignInManager para verificar as credenciais
        var signInResult = await signInManager.PasswordSignInAsync(user, loginRequest.Password, isPersistent: false, lockoutOnFailure: false);

        if (!signInResult.Succeeded)
        {
            return Result.Failure<string>(Error.Failure("Error.Unauthorized", "Invalid credentials"));
        }

        // Obter os papéis do usuário
        var userRoles = await userManager.GetRolesAsync(user);

        // Criar os claims do token
        var userClaims = new UserTokenDto(
            user.Id.ToString(),
            user.UserName!,
            user.Email!,
            user.PhoneNumber!
        );

        var authClaims = jwtService.GenerateAuthClaims(userClaims, userRoles);

        // Gerar o token JWT
        var token = new JwtSecurityTokenHandler().WriteToken(jwtService.Generate(authClaims, configuration));

        return Result.Succeed(token);
    }

    public async Task<Result> UpdateUser(UserUpdateDto User, CancellationToken cancellationToken)
    {
        // Validar os dados recebidos
        var dto = User.Adapt<UserCreateDto>();

        var validationResult = validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return Result.Failure(Error.Invalid("Error.Validation", validationResult.ToString()));
        }

        // Buscar o usuário pelo ID
        var user = await userManager.FindByIdAsync(User.Id.ToString());
        if (user == null)
        {
            return Result.Failure(Error.NotFound("Error.NotFound", "User not found."));
        }

        user.Name = User.Name;
        user.Email = User.Email;
        user.PhoneNumber = User.PhoneNumber;

        // Salvar as alterações
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result.Failure(Error.Failure("Error.Update", "Failed to update user information."));
        }

        return Result.Succeed();
    }
}